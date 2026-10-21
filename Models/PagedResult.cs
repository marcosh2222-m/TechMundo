namespace TechMundo.Models;

/// <summary>Resultado paginado genérico (Unidad 3, Clase 18).</summary>
/// <typeparam name="T">Tipo de los elementos de la página.</typeparam>
public class PagedResult<T>
{
    /// <summary>Elementos de la página actual.</summary>
    public List<T> Items { get; set; } = new();

    /// <summary>Número de la página devuelta (empieza en 1).</summary>
    public int PaginaActual { get; set; }

    /// <summary>Cantidad total de páginas (al menos 1).</summary>
    public int TotalPaginas { get; set; }

    /// <summary>Cantidad total de registros que cumplen el filtro.</summary>
    public int TotalRegistros { get; set; }
}

/// <summary>Estadísticas del inventario, mostradas en la barra del panel de admin.</summary>
/// <param name="TotalProductos">Cantidad de productos cargados.</param>
/// <param name="ValorInventario">Suma de precio × stock, en pesos.</param>
/// <param name="ProductosStockBajo">Productos con stock igual o menor a su mínimo.</param>
public record EstadisticasInventario(int TotalProductos, decimal ValorInventario, int ProductosStockBajo);
