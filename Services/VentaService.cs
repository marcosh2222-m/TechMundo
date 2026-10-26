using Microsoft.EntityFrameworkCore;
using TechMundo.Data;
using TechMundo.Models;

namespace TechMundo.Services;

public class VentaService : IVentaService
{
    private readonly IDbContextFactory<TechMundoDbContext> _factory;
    private readonly IProductoService _productos;
    private readonly ILogger<VentaService> _logger;

    public VentaService(IDbContextFactory<TechMundoDbContext> factory, IProductoService productos, ILogger<VentaService> logger)
    {
        _factory = factory;
        _productos = productos;
        _logger = logger;
    }

    public async Task<Venta> RegistrarVenta(string clienteNombre, string clienteEmail, IReadOnlyList<LineaCarrito> lineas)
    {
        if (lineas.Count == 0)
            throw new InvalidOperationException("El carrito está vacío.");

        // Validación del lado del servidor: aunque el formulario ya valida, el servicio no confía
        // en que los datos lleguen siempre desde la interfaz.
        clienteNombre = Validaciones.NormalizarNombre(clienteNombre);
        clienteEmail = Validaciones.NormalizarEmail(clienteEmail);
        if (!Validaciones.EsNombrePersonaValido(clienteNombre))
            throw new InvalidOperationException("El nombre del cliente no es válido: ingresá nombre y apellido.");
        if (!Validaciones.EsEmailValido(clienteEmail))
            throw new InvalidOperationException("El email del cliente no es válido.");
        if (lineas.Any(l => l.Cantidad < 1 || l.Cantidad > Validaciones.MaxUnidadesPorProducto))
            throw new InvalidOperationException($"Cada producto debe tener entre 1 y {Validaciones.MaxUnidadesPorProducto} unidades.");
        if (lineas.GroupBy(l => l.ProductoId).Any(g => g.Count() > 1))
            throw new InvalidOperationException("El carrito tiene productos repetidos.");

        await using var ctx = await _factory.CreateDbContextAsync();
        await using var tx = await ctx.Database.BeginTransactionAsync();

        var ids = lineas.Select(l => l.ProductoId).ToList();
        var productos = await ctx.Productos.Where(p => ids.Contains(p.Id)).ToDictionaryAsync(p => p.Id);

        var problemas = new List<string>();
        foreach (var l in lineas)
        {
            if (!productos.TryGetValue(l.ProductoId, out var p) || !p.Disponible)
                problemas.Add($"\"{l.Nombre}\" ya no está disponible.");
            else if (p.Stock < l.Cantidad)
                problemas.Add($"\"{p.Nombre}\": pediste {l.Cantidad} pero solo quedan {p.Stock}.");
        }

        if (problemas.Count > 0)
            throw new StockInsuficienteException(string.Join(" ", problemas));

        var venta = new Venta { Fecha = Reloj.Ahora, ClienteNombre = clienteNombre, ClienteEmail = clienteEmail };

        foreach (var l in lineas)
        {
            var p = productos[l.ProductoId];
            p.Stock -= l.Cantidad;
            p.Version++; // invalida formularios de edición abiertos con el stock viejo
            // Regla de negocio de Producto.Validate: sin stock no puede quedar "Disponible".
            if (p.Stock == 0) p.Disponible = false;

            venta.Items.Add(new VentaItem
            {
                ProductoId = p.Id,
                NombreProducto = p.Nombre,
                PrecioUnitario = p.Precio, // precio vigente en la base, no el del carrito
                Cantidad = l.Cantidad
            });
        }

        venta.Total = venta.Items.Sum(i => i.Subtotal);

        ctx.Ventas.Add(venta);
        await ctx.SaveChangesAsync();
        await tx.CommitAsync();

        _logger.LogInformation("Venta #{Id} registrada: {Cliente}, total {Total}", venta.Id, venta.ClienteNombre, venta.Total);
        _productos.NotificarCambio(); // refresca catálogo y estadísticas del admin
        return venta;
    }

    private static IQueryable<Venta> Filtrar(IQueryable<Venta> query, FiltroVentas f)
    {
        if (f.Desde is not null)
        {
            var desde = f.Desde.Value.Date;
            query = query.Where(v => v.Fecha >= desde);
        }

        if (f.Hasta is not null)
        {
            var hastaExcl = f.Hasta.Value.Date.AddDays(1);
            query = query.Where(v => v.Fecha < hastaExcl);
        }

        if (!string.IsNullOrWhiteSpace(f.Busqueda))
        {
            var b = f.Busqueda.Trim().TrimStart('#');
            var esId = int.TryParse(b, out var id);
            var patron = ProductoService.PatronLike(b);
            query = query.Where(v => EF.Functions.Like(v.ClienteNombre, patron, "\\")
                                  || EF.Functions.Like(v.ClienteEmail, patron, "\\")
                                  || (esId && v.Id == id));
        }

        return query;
    }

    public async Task<PagedResult<Venta>> ObtenerPaginado(int pagina, int tamanioPagina, FiltroVentas filtro)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var query = Filtrar(ctx.Ventas.AsNoTracking(), filtro);

        var total = await query.CountAsync();

        // SQLite no ordena decimal directamente: se castea a double solo para el ORDER BY.
        var ordenada = filtro.Orden switch
        {
            OrdenVenta.MasAntiguas => query.OrderBy(v => v.Fecha),
            OrdenVenta.MayorTotal => query.OrderByDescending(v => (double)v.Total).ThenByDescending(v => v.Fecha),
            OrdenVenta.MenorTotal => query.OrderBy(v => (double)v.Total).ThenByDescending(v => v.Fecha),
            _ => query.OrderByDescending(v => v.Fecha)
        };

        var items = await ordenada
            .Include(v => v.Items)
            .Skip((pagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync();

        return new PagedResult<Venta>
        {
            Items = items,
            PaginaActual = pagina,
            TotalRegistros = total,
            TotalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)tamanioPagina))
        };
    }

    public async Task<ResumenVentas> ObtenerResumen(FiltroVentas filtro)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var query = Filtrar(ctx.Ventas.AsNoTracking(), filtro);

        // Los decimales se suman en memoria (SQLite no soporta SUM sobre decimal).
        var datos = await query.Select(v => new { v.Total, Unidades = v.Items.Sum(i => i.Cantidad) }).ToListAsync();
        return new ResumenVentas(datos.Count, datos.Sum(d => d.Unidades), datos.Sum(d => d.Total));
    }

    public async Task<Venta?> ObtenerPorId(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Ventas.Include(v => v.Items).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }
}
