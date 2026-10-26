using System.ComponentModel.DataAnnotations;

namespace TechMundo.Models;

/// <summary>Producto del catálogo de TechMundo (computadoras, celulares, accesorios).</summary>
public class Producto : IValidatableObject
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Obligatorio(ErrorMessage = "La marca es obligatoria")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "La marca debe tener entre 2 y 50 caracteres")]
    [RegularExpression(Validaciones.PatronMarca, ErrorMessage = "La marca solo puede tener letras, números, espacios y . - & +")]
    public string? Marca { get; set; }

    [Obligatorio(ErrorMessage = "La descripción es obligatoria")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "La descripción debe tener entre 10 y 500 caracteres")]
    public string? Descripcion { get; set; }

    /// <summary>Características técnicas, una por línea (se muestran como lista en el detalle).</summary>
    [StringLength(1500, ErrorMessage = "Las características no pueden superar los 1500 caracteres")]
    public string? Caracteristicas { get; set; }

    // Acepta una URL externa (https://...) o una imagen del propio sitio (/img/productos/...).
    // [Url] no sirve acá porque rechaza las rutas relativas.
    [StringLength(500, ErrorMessage = "La URL no puede superar los 500 caracteres")]
    [RegularExpression(Validaciones.PatronImagen, ErrorMessage = "Ingresá una URL (https://...) o una ruta del sitio (/img/...)")]
    public string? ImagenUrl { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio")]
    [Range(1, 100_000_000, ErrorMessage = "El precio debe estar entre $1 y $100.000.000")]
    public decimal Precio { get; set; }

    [Required(ErrorMessage = "El stock es obligatorio")]
    [Range(0, 100_000, ErrorMessage = "El stock debe estar entre 0 y 100.000 unidades")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "El stock mínimo es obligatorio")]
    [Range(0, 10_000, ErrorMessage = "El stock mínimo debe estar entre 0 y 10.000 unidades")]
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

        // Los precios en pesos admiten como máximo 2 decimales (centavos).
        if (decimal.Round(Precio, 2) != Precio)
            yield return new ValidationResult("El precio puede tener como máximo 2 decimales.", new[] { nameof(Precio) });

        // Características: una por línea, como máximo 15 líneas de 150 caracteres.
        if (!string.IsNullOrWhiteSpace(Caracteristicas))
        {
            var lineas = Caracteristicas.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (lineas.Length > 15)
                yield return new ValidationResult("Cargá como máximo 15 características (una por línea).", new[] { nameof(Caracteristicas) });
            if (lineas.Any(l => l.Length > 150))
                yield return new ValidationResult("Cada característica puede tener como máximo 150 caracteres.", new[] { nameof(Caracteristicas) });
        }
    }
}
