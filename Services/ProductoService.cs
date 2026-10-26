using TechMundo.Data;
using TechMundo.Models;
using Microsoft.EntityFrameworkCore;

namespace TechMundo.Services;

public class ProductoService : IProductoService
{
    private readonly IDbContextFactory<TechMundoDbContext> _factory;
    private readonly ILogger<ProductoService> _logger;
    private readonly CambiosCatalogo _cambios;

    /// <summary>
    /// El evento se "reenvía" al notificador Singleton: así un cambio hecho en una pestaña
    /// (ej. una venta) llega también a las demás pestañas abiertas.
    /// </summary>
    public event Action? OnChange
    {
        add => _cambios.OnChange += value;
        remove => _cambios.OnChange -= value;
    }

    public ProductoService(IDbContextFactory<TechMundoDbContext> factory, ILogger<ProductoService> logger, CambiosCatalogo cambios)
    {
        _factory = factory;
        _logger = logger;
        _cambios = cambios;
    }

    public async Task<PagedResult<Producto>> ObtenerPaginado(
        int pagina, int tamanioPagina, int? categoriaId = null, bool soloStockBajo = false,
        string? busqueda = null, bool soloDisponibles = false, OrdenProducto orden = OrdenProducto.Original)
    {
        await using var ctx = await _factory.CreateDbContextAsync();

        var query = ctx.Productos.Include(p => p.Categoria).AsNoTracking().AsQueryable();

        if (categoriaId is not null)
            query = query.Where(p => p.CategoriaId == categoriaId);

        if (soloStockBajo)
            query = query.Where(p => p.Stock <= p.StockMinimo);

        if (soloDisponibles)
            query = query.Where(p => p.Disponible);

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            // EF.Functions.Like se traduce a LIKE de SQL (Unidad 3). En SQLite, LIKE no distingue
            // mayúsculas en letras ASCII: "usb" encuentra "USB". Limitación conocida: con letras
            // acentuadas sí las distingue ("cámara" no encuentra "Cámara"). Ver HU-17 del backlog.
            // (string.Contains se traduce a instr(), que distingue mayúsculas siempre.)
            var patron = PatronLike(busqueda);
            query = query.Where(p => EF.Functions.Like(p.Nombre, patron, "\\")
                                  || (p.Marca != null && EF.Functions.Like(p.Marca, patron, "\\"))
                                  || (p.Descripcion != null && EF.Functions.Like(p.Descripcion, patron, "\\")));
        }

        var total = await query.CountAsync();

        // SQLite no puede ordenar por decimal directamente: se castea a double solo para el ORDER BY.
        var ordenada = orden switch
        {
            OrdenProducto.PrecioMenor => query.OrderBy(p => (double)p.Precio).ThenBy(p => p.Nombre.ToLower()),
            OrdenProducto.PrecioMayor => query.OrderByDescending(p => (double)p.Precio).ThenBy(p => p.Nombre.ToLower()),
            // ToLower(): SQLite ordena distinguiendo mayúsculas ("ASUS" quedaba antes que "Acer").
            _ => query.OrderBy(p => p.Nombre.ToLower())
        };

        var items = await ordenada
            .Skip((pagina - 1) * tamanioPagina)
            .Take(tamanioPagina)
            .ToListAsync();

        return new PagedResult<Producto>
        {
            Items = items,
            PaginaActual = pagina,
            TotalRegistros = total,
            TotalPaginas = Math.Max(1, (int)Math.Ceiling(total / (double)tamanioPagina))
        };
    }

    /// <summary>
    /// Arma el patrón "%texto%" para LIKE, escapando los comodines % y _ que pudiera
    /// escribir el usuario (así buscar "50%" no devuelve todo).
    /// </summary>
    internal static string PatronLike(string texto) =>
        "%" + texto.Trim().Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%";

    public async Task<List<Producto>> ObtenerDestacados(int cantidad = 8)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Productos.Include(p => p.Categoria).AsNoTracking()
            .Where(p => p.Destacado && p.Disponible)
            .OrderByDescending(p => p.CreadoEn)
            .Take(cantidad)
            .ToListAsync();
    }

    public async Task<Producto?> ObtenerPorId(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Productos.Include(p => p.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <summary>Evita dos productos con el mismo nombre (sin distinguir mayúsculas ni espacios de más).</summary>
    private static async Task ValidarNombreUnico(TechMundoDbContext ctx, Producto producto)
    {
        var nombre = producto.Nombre.Trim().ToLower();
        if (await ctx.Productos.AnyAsync(p => p.Id != producto.Id && p.Nombre.ToLower() == nombre))
            throw new InvalidOperationException($"Ya existe un producto llamado \"{producto.Nombre.Trim()}\".");
    }

    /// <summary>Limpia espacios sobrantes de los textos antes de guardar.</summary>
    private static void Normalizar(Producto p)
    {
        p.Nombre = p.Nombre.Trim();
        p.Marca = string.IsNullOrWhiteSpace(p.Marca) ? null : p.Marca.Trim();
        p.Descripcion = string.IsNullOrWhiteSpace(p.Descripcion) ? null : p.Descripcion.Trim();
        p.ImagenUrl = string.IsNullOrWhiteSpace(p.ImagenUrl) ? null : p.ImagenUrl.Trim();
        p.Caracteristicas = string.IsNullOrWhiteSpace(p.Caracteristicas)
            ? null
            : string.Join('\n', p.Caracteristicas.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
    }

    public async Task<Producto> Crear(Producto producto)
    {
        try
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            Normalizar(producto);
            await ValidarNombreUnico(ctx, producto);
            producto.CreadoEn = DateTime.UtcNow;
            ctx.Productos.Add(producto);
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Producto creado: {Nombre} (Id {Id})", producto.Nombre, producto.Id);
            NotificarCambio();
            return producto;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al crear el producto {Nombre}", producto.Nombre);
            throw new InvalidOperationException("No se pudo guardar el producto. Intentá nuevamente.", ex);
        }
    }

    public async Task<Producto> Actualizar(Producto producto)
    {
        const string mensajeConflicto =
            "Este producto cambió mientras lo editabas (por ejemplo, se vendieron unidades). " +
            "Recargá la página para ver los datos actuales y volvé a aplicar tus cambios.";
        try
        {
            await using var ctx = await _factory.CreateDbContextAsync();
            var actual = await ctx.Productos.FirstOrDefaultAsync(p => p.Id == producto.Id)
                         ?? throw new InvalidOperationException("El producto ya no existe en el catálogo.");
            Normalizar(producto);
            await ValidarNombreUnico(ctx, producto);

            // Concurrencia optimista: si la versión que tenía el formulario no es la actual,
            // alguien más modificó el producto y no se pisan sus cambios.
            if (actual.Version != producto.Version)
                throw new InvalidOperationException(mensajeConflicto);

            ctx.Entry(actual).CurrentValues.SetValues(producto);
            actual.Version = producto.Version + 1;
            await ctx.SaveChangesAsync();

            producto.Version = actual.Version;
            _logger.LogInformation("Producto actualizado: Id {Id}", producto.Id);
            NotificarCambio();
            return producto;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Mismo caso, pero detectado por la base (el cambio ocurrió justo entre la lectura y el guardado).
            _logger.LogWarning(ex, "Conflicto de concurrencia al actualizar producto {Id}", producto.Id);
            throw new InvalidOperationException(mensajeConflicto, ex);
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al actualizar el producto {Id}", producto.Id);
            throw new InvalidOperationException("No se pudo guardar el producto. Intentá nuevamente.", ex);
        }
    }

    public async Task Eliminar(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var p = await ctx.Productos.FindAsync(id);
        if (p is not null)
        {
            ctx.Productos.Remove(p);
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Producto eliminado: Id {Id}", id);
            NotificarCambio();
        }
    }

    public async Task ToggleDisponible(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        var p = await ctx.Productos.FindAsync(id);
        if (p is not null)
        {
            var nuevoValor = !p.Disponible;

            // Misma regla de negocio que la validación cruzada del formulario (Producto.Validate):
            // el toggle rápido de la tarjeta no pasa por EditForm/DataAnnotationsValidator,
            // así que hay que reforzarla acá para que no se pueda evadir.
            if (nuevoValor && p.Stock == 0)
                throw new InvalidOperationException(
                    $"No se puede marcar \"{p.Nombre}\" como disponible: no tiene stock cargado.");

            p.Disponible = nuevoValor;
            p.Version++;
            await ctx.SaveChangesAsync();
            _logger.LogInformation("Producto {Id} ahora está {Estado}", id, p.Disponible ? "disponible" : "no disponible");
            NotificarCambio();
        }
    }

    public async Task<EstadisticasInventario> ObtenerEstadisticas()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        // Se traen solo las columnas necesarias (no las entidades completas). La suma de
        // decimales se hace en memoria porque SQLite no soporta SUM sobre decimal.
        var datos = await ctx.Productos.AsNoTracking()
            .Select(p => new { p.Precio, p.Stock, p.StockMinimo })
            .ToListAsync();
        var valor = datos.Sum(p => p.Precio * p.Stock);
        var stockBajo = datos.Count(p => p.Stock <= p.StockMinimo);
        return new EstadisticasInventario(datos.Count, valor, stockBajo);
    }

    public async Task<List<Categoria>> ObtenerCategorias()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
    }

    public void NotificarCambio() => _cambios.Notificar();
}
