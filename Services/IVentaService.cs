using TechMundo.Models;

namespace TechMundo.Services;

/// <summary>Se lanza cuando, al confirmar la compra, algún producto ya no tiene stock suficiente o dejó de estar disponible.</summary>
public class StockInsuficienteException : Exception
{
    /// <summary>Crea la excepción con un mensaje apto para mostrar al cliente.</summary>
    /// <param name="mensaje">Detalle de qué productos no alcanzan.</param>
    public StockInsuficienteException(string mensaje) : base(mensaje) { }
}

/// <summary>Registro y consulta de las ventas hechas desde el carrito del catálogo.</summary>
public interface IVentaService
{
    /// <summary>
    /// Confirma una compra: valida disponibilidad y stock con datos frescos de la base,
    /// descuenta el stock y guarda la venta, todo en una única transacción.
    /// </summary>
    /// <param name="clienteNombre">Nombre y apellido del cliente.</param>
    /// <param name="clienteEmail">Email del cliente.</param>
    /// <param name="lineas">Productos y cantidades del carrito. Los precios se toman de la base, no del carrito.</param>
    /// <returns>La venta registrada, con su número (Id) y el detalle de ítems.</returns>
    /// <exception cref="StockInsuficienteException">Si algún producto no está disponible o no alcanza el stock.</exception>
    /// <exception cref="InvalidOperationException">Si el carrito está vacío.</exception>
    Task<Venta> RegistrarVenta(string clienteNombre, string clienteEmail, IReadOnlyList<LineaCarrito> lineas);

    /// <summary>Obtiene una página del historial de ventas, con sus ítems.</summary>
    /// <param name="pagina">Número de página, empezando en 1.</param>
    /// <param name="tamanioPagina">Cantidad de ventas por página.</param>
    /// <param name="filtro">Rango de fechas, búsqueda por cliente / email / N.º de venta y orden.</param>
    /// <returns>Las ventas de la página pedida y los totales para paginar.</returns>
    Task<PagedResult<Venta>> ObtenerPaginado(int pagina, int tamanioPagina, FiltroVentas filtro);

    /// <summary>Calcula el resumen del conjunto filtrado completo (sin paginar).</summary>
    /// <param name="filtro">Los mismos filtros que el listado.</param>
    /// <returns>Cantidad de ventas, unidades vendidas e ingresos.</returns>
    Task<ResumenVentas> ObtenerResumen(FiltroVentas filtro);

    /// <summary>Busca una venta por su número, con sus ítems.</summary>
    /// <param name="id">Número de venta.</param>
    /// <returns>La venta, o <c>null</c> si no existe.</returns>
    Task<Venta?> ObtenerPorId(int id);
}
