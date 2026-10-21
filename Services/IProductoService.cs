using TechMundo.Models;

namespace TechMundo.Services;

/// <summary>
/// Servicio principal para la gestión del catálogo de productos (State Service, Unidad 1).
/// Encapsula el acceso a datos con EF Core (Unidad 3) y avisa a la interfaz cuando el
/// catálogo cambia, para que los componentes se actualicen sin recargar la página.
/// </summary>
/// <remarks>
/// Se registra como Scoped (una instancia por pestaña / circuito de Blazor Server).
/// El evento <see cref="OnChange"/> se reenvía al notificador Singleton
/// <see cref="CambiosCatalogo"/>, así los cambios llegan a todas las pestañas abiertas.
/// </remarks>
public interface IProductoService
{
    /// <summary>Obtiene una página de productos aplicando filtros, búsqueda y orden.</summary>
    /// <param name="pagina">Número de página, empezando en 1.</param>
    /// <param name="tamanioPagina">Cantidad de productos por página.</param>
    /// <param name="categoriaId">Si se indica, solo devuelve productos de esa categoría.</param>
    /// <param name="soloStockBajo">Si es <c>true</c>, solo productos con stock igual o menor al mínimo.</param>
    /// <param name="busqueda">Texto a buscar (con LIKE) en nombre, marca y descripción, sin distinguir mayúsculas.</param>
    /// <param name="soloDisponibles"><c>true</c> para el catálogo público (oculta los no disponibles); <c>false</c> para el admin.</param>
    /// <param name="orden">Orden del listado: original (por nombre), menor precio o mayor precio.</param>
    /// <returns>Los productos de la página pedida (con su categoría) y los totales para paginar.</returns>
    /// <example>
    /// <code>
    /// var pagina2 = await productoService.ObtenerPaginado(2, 8, busqueda: "usb", soloDisponibles: true);
    /// </code>
    /// </example>
    Task<PagedResult<Producto>> ObtenerPaginado(
        int pagina, int tamanioPagina, int? categoriaId = null, bool soloStockBajo = false,
        string? busqueda = null, bool soloDisponibles = false, OrdenProducto orden = OrdenProducto.Original);

    /// <summary>Obtiene los productos destacados y disponibles para el carrusel del catálogo público.</summary>
    /// <param name="cantidad">Cantidad máxima de productos a devolver.</param>
    /// <returns>Los destacados, del más nuevo al más viejo.</returns>
    Task<List<Producto>> ObtenerDestacados(int cantidad = 8);

    /// <summary>Busca un producto por su Id, incluyendo su categoría.</summary>
    /// <param name="id">Id del producto.</param>
    /// <returns>El producto, o <c>null</c> si no existe.</returns>
    Task<Producto?> ObtenerPorId(int id);

    /// <summary>Crea un nuevo producto en la base de datos.</summary>
    /// <param name="producto">Entidad con los datos del nuevo producto (ya validada por el formulario).</param>
    /// <returns>La entidad creada, con el Id asignado por la base.</returns>
    /// <exception cref="InvalidOperationException">Si falla la persistencia.</exception>
    Task<Producto> Crear(Producto producto);

    /// <summary>
    /// Guarda los cambios de un producto existente usando concurrencia optimista: si el
    /// producto cambió desde que se cargó el formulario (por ejemplo, por una venta), el
    /// cambio se rechaza en lugar de pisar los datos nuevos.
    /// </summary>
    /// <param name="producto">Producto editado, con la <see cref="Producto.Version"/> que tenía al cargarse.</param>
    /// <returns>El producto guardado, con la versión actualizada.</returns>
    /// <exception cref="InvalidOperationException">Si el producto no existe, cambió mientras se editaba o falla la persistencia.</exception>
    Task<Producto> Actualizar(Producto producto);

    /// <summary>Elimina un producto. Las ventas que lo incluían conservan el nombre y el precio.</summary>
    /// <param name="id">Id del producto a eliminar. Si no existe, no hace nada.</param>
    Task Eliminar(int id);

    /// <summary>Alterna si el producto está disponible para la venta (Unidad 1, Clase 8: toggle de estado).</summary>
    /// <param name="id">Id del producto.</param>
    /// <exception cref="InvalidOperationException">Si se intenta habilitar un producto sin stock.</exception>
    Task ToggleDisponible(int id);

    /// <summary>Calcula las estadísticas del inventario con una consulta real a la base.</summary>
    /// <returns>Total de productos, valor del inventario (precio × stock) y cantidad con stock bajo.</returns>
    Task<EstadisticasInventario> ObtenerEstadisticas();

    /// <summary>Obtiene todas las categorías ordenadas por nombre.</summary>
    /// <returns>Lista de categorías (sin sus productos).</returns>
    Task<List<Categoria>> ObtenerCategorias();

    /// <summary>Dispara <see cref="OnChange"/> manualmente (lo usa <see cref="IVentaService"/> tras descontar stock).</summary>
    void NotificarCambio();

    /// <summary>
    /// Se dispara cada vez que el catálogo cambia (alta, edición, baja, toggle o venta), en
    /// cualquier pestaña. Los suscriptores deben usar <c>InvokeAsync</c> y desuscribirse al hacer Dispose.
    /// </summary>
    event Action? OnChange;
}
