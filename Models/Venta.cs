using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechMundo.Models;

/// <summary>Venta confirmada desde el carrito del catálogo (cabecera).</summary>
public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = Services.Reloj.Ahora;

    [Required, MaxLength(80)]
    public string ClienteNombre { get; set; } = string.Empty;

    [Required, MaxLength(120)]
    public string ClienteEmail { get; set; } = string.Empty;

    public decimal Total { get; set; }

    public List<VentaItem> Items { get; set; } = new();

    [NotMapped] public int CantidadUnidades => Items.Sum(i => i.Cantidad);
}

/// <summary>
/// Línea de una venta. Guarda una "foto" del nombre y el precio al momento de la compra,
/// así el historial no cambia si después se edita o elimina el producto.
/// </summary>
public class VentaItem
{
    public int Id { get; set; }
    public int VentaId { get; set; }
    public Venta? Venta { get; set; }

    /// <summary>Null si el producto fue eliminado del catálogo después de la venta.</summary>
    public int? ProductoId { get; set; }

    [Required, MaxLength(100)]
    public string NombreProducto { get; set; } = string.Empty;

    public decimal PrecioUnitario { get; set; }
    public int Cantidad { get; set; }

    [NotMapped] public decimal Subtotal => PrecioUnitario * Cantidad;
}

public enum OrdenVenta { MasRecientes, MasAntiguas, MayorTotal, MenorTotal }

/// <summary>Filtros del historial de ventas (todos opcionales).</summary>
public record FiltroVentas(DateTime? Desde = null, DateTime? Hasta = null, string? Busqueda = null,
                           OrdenVenta Orden = OrdenVenta.MasRecientes);

public record ResumenVentas(int CantidadVentas, int Unidades, decimal Ingresos);
