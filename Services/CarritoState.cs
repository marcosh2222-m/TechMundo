using TechMundo.Models;

namespace TechMundo.Services;

/// <summary>Línea del carrito: copia de los datos del producto al momento de agregarlo.</summary>
public class LineaCarrito
{
    public int ProductoId { get; init; }
    public string Nombre { get; init; } = string.Empty;
    public string? Marca { get; init; }
    public string? ImagenUrl { get; init; }
    public decimal PrecioUnitario { get; init; }
    public int Cantidad { get; set; }
    /// <summary>Stock que tenía el producto al agregarlo; tope para el +/- del carrito.</summary>
    public int StockDisponible { get; set; }
    public decimal Subtotal => PrecioUnitario * Cantidad;
}

/// <summary>Resultado de una operación del carrito.</summary>
/// <param name="Ok">True si la operación se realizó.</param>
/// <param name="Mensaje">Motivo del rechazo, apto para mostrar al cliente.</param>
public record ResultadoCarrito(bool Ok, string? Mensaje = null);

/// <summary>
/// Carrito de compras en memoria (scoped = uno por sesión/circuito de Blazor Server).
/// Mismo patrón de State Service que el resto del proyecto: expone OnChange para que los
/// componentes se actualicen. Valida stock al agregar; la validación definitiva ocurre
/// al confirmar la compra, dentro de una transacción (VentaService).
/// </summary>
public class CarritoState
{
    private readonly List<LineaCarrito> _lineas = new();

    /// <summary>Líneas del carrito (solo lectura; se modifican con los métodos de esta clase).</summary>
    public IReadOnlyList<LineaCarrito> Lineas => _lineas;
    /// <summary>Suma de unidades de todas las líneas (el número del ícono del header).</summary>
    public int CantidadTotal => _lineas.Sum(l => l.Cantidad);
    /// <summary>Importe total del carrito, en pesos.</summary>
    public decimal Total => _lineas.Sum(l => l.Subtotal);
    /// <summary>Indica si el modal del carrito está abierto.</summary>
    public bool Abierto { get; private set; }

    /// <summary>Se dispara ante cualquier cambio del carrito (para refrescar el contador y el modal).</summary>
    public event Action? OnChange;

    /// <summary>Abre el modal del carrito.</summary>
    public void Abrir() { Abierto = true; OnChange?.Invoke(); }
    /// <summary>Cierra el modal del carrito.</summary>
    public void Cerrar() { Abierto = false; OnChange?.Invoke(); }

    /// <summary>Unidades de un producto que ya están en el carrito.</summary>
    /// <param name="productoId">Id del producto.</param>
    /// <returns>Cantidad en el carrito, o 0 si no está.</returns>
    public int CantidadDe(int productoId) => _lineas.FirstOrDefault(l => l.ProductoId == productoId)?.Cantidad ?? 0;

    /// <summary>Agrega unidades de un producto validando disponibilidad y stock.</summary>
    /// <param name="p">Producto leído recién de la base (con stock actualizado).</param>
    /// <param name="cantidad">Unidades a sumar.</param>
    /// <returns>Ok, o un mensaje explicando por qué no se pudo agregar.</returns>
    public ResultadoCarrito Agregar(Producto p, int cantidad = 1)
    {
        if (!p.Disponible || p.Stock <= 0)
            return new(false, $"\"{p.Nombre}\" no está disponible por el momento.");

        var linea = _lineas.FirstOrDefault(l => l.ProductoId == p.Id);
        var enCarrito = linea?.Cantidad ?? 0;

        if (cantidad < 1)
            return new(false, "La cantidad debe ser al menos 1.");

        if (enCarrito + cantidad > Validaciones.MaxUnidadesPorProducto)
            return new(false, $"Podés comprar hasta {Validaciones.MaxUnidadesPorProducto} unidades de un mismo producto.");

        if (enCarrito + cantidad > p.Stock)
        {
            return new(false, enCarrito == 0
                ? $"Solo quedan {p.Stock} unidades de \"{p.Nombre}\"."
                : $"Solo quedan {p.Stock} unidades de \"{p.Nombre}\" y ya tenés {enCarrito} en el carrito.");
        }

        if (linea is null)
        {
            _lineas.Add(new LineaCarrito
            {
                ProductoId = p.Id, Nombre = p.Nombre, Marca = p.Marca, ImagenUrl = p.ImagenUrl,
                PrecioUnitario = p.Precio, Cantidad = cantidad, StockDisponible = p.Stock
            });
        }
        else
        {
            linea.Cantidad += cantidad;
            linea.StockDisponible = p.Stock;
        }

        OnChange?.Invoke();
        return new(true);
    }

    /// <summary>Cambia la cantidad de una línea sin superar el stock conocido; con 0 o menos la quita.</summary>
    /// <param name="productoId">Id del producto de la línea.</param>
    /// <param name="nuevaCantidad">Nueva cantidad deseada.</param>
    /// <returns>Ok, o un mensaje si se supera el stock.</returns>
    public ResultadoCarrito CambiarCantidad(int productoId, int nuevaCantidad)
    {
        var linea = _lineas.FirstOrDefault(l => l.ProductoId == productoId);
        if (linea is null) return new(false);

        if (nuevaCantidad <= 0) { Quitar(productoId); return new(true); }

        if (nuevaCantidad > Validaciones.MaxUnidadesPorProducto)
            return new(false, $"Podés comprar hasta {Validaciones.MaxUnidadesPorProducto} unidades de un mismo producto.");

        if (nuevaCantidad > linea.StockDisponible)
            return new(false, $"Solo hay {linea.StockDisponible} unidades de \"{linea.Nombre}\" en stock.");

        linea.Cantidad = nuevaCantidad;
        OnChange?.Invoke();
        return new(true);
    }

    /// <summary>Reemplaza el contenido del carrito (al recuperarlo del sessionStorage tras un F5).</summary>
    /// <param name="lineas">Líneas recuperadas del almacenamiento de la pestaña.</param>
    public void Restaurar(IEnumerable<LineaCarrito> lineas)
    {
        _lineas.Clear();
        _lineas.AddRange(lineas.Where(l => l.Cantidad > 0));
        OnChange?.Invoke();
    }

    /// <summary>Quita un producto del carrito.</summary>
    /// <param name="productoId">Id del producto a quitar.</param>
    public void Quitar(int productoId)
    {
        _lineas.RemoveAll(l => l.ProductoId == productoId);
        OnChange?.Invoke();
    }

    /// <summary>Vacía el carrito (por ejemplo, después de confirmar la compra).</summary>
    public void Vaciar()
    {
        _lineas.Clear();
        OnChange?.Invoke();
    }
}
