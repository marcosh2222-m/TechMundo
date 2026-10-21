using System.ComponentModel.DataAnnotations;

namespace TechMundo.Models;

/// <summary>Producto del catálogo de TechMundo (computadoras, celulares, accesorios).</summary>
public class Producto : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Marca { get; set; }

    [MaxLength(500)]
    public string? Descripcion { get; set; }

    /// <summary>Características técnicas, una por línea (se muestran como lista en el detalle).</summary>
    [MaxLength(1500)]
    public string? Caracteristicas { get; set; }

    [MaxLength(500)]
    [Url(ErrorMessage = "Ingresá una URL válida")]
    public string? ImagenUrl { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(0.01, 100_000_000, ErrorMessage = "El precio debe ser mayor a $0 y menor a $100.000.000")]
    public decimal Precio { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
    public int Stock { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "El stock mínimo no puede ser negativo")]
    public int StockMinimo { get; set; } = 5;

    public DateTime CreadoEn { get; set; } = DateTime.UtcNow;

    /// <summary>Indica si el producto está activo para la venta (toggle, Unidad 1 Clase 8).</summary>
    public bool Disponible { get; set; } = true;

    /// <summary>Si está marcado, aparece en el carrusel de destacados del catálogo público.</summary>
    public bool Destacado { get; set; }

    /// <summary>
    /// Control de concurrencia optimista: se incrementa en cada cambio (edición, toggle o venta).
    /// Si el admin guarda un formulario con una versión vieja (porque mientras tanto hubo una
    /// venta), EF Core rechaza el cambio en lugar de pisar el stock con un valor desactualizado.
    /// </summary>
    [ConcurrencyCheck]
    public int Version { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná una categoría")]
    public int CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }

    /// <summary>True si el stock actual está en o por debajo del mínimo (alerta de reposición).</summary>
    public bool StockBajo => Stock <= StockMinimo;

    /// <summary>
    /// Validación cruzada entre campos (Unidad 1, Clase 7): no tiene sentido
    /// vender un producto sin stock, así que no se puede dejar "Disponible"
    /// un producto con Stock = 0.
    /// </summary>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Disponible && Stock == 0)
        {
            yield return new ValidationResult(
                "No podés marcar como disponible un producto sin stock. Cargá stock o desmarcá 'Disponible'.",
                new[] { nameof(Disponible) });
        }
    }
}
