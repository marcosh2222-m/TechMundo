using System.ComponentModel.DataAnnotations;

namespace TechMundo.Models;

/// <summary>Categoría de producto (Computadoras, Celulares, Accesorios, Gaming, Redes). Relación uno a muchos con <see cref="Producto"/>.</summary>
public class Categoria
{
    /// <summary>Clave primaria.</summary>
    public int Id { get; set; }

    /// <summary>Nombre visible, usado también en la URL (/categoria/{nombre}).</summary>
    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [MaxLength(50)]
    public string Nombre { get; set; } = string.Empty;

    /// <summary>Productos de la categoría (propiedad de navegación).</summary>
    public List<Producto> Productos { get; set; } = new();
}
