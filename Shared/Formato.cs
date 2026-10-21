using System.Globalization;

namespace TechMundo.Shared;

/// <summary>
/// Formatos de presentación centralizados (antes cada componente tenía su propia
/// CultureInfo). Los precios del catálogo están en pesos argentinos (ARS).
/// </summary>
public static class Formato
{
    private static readonly CultureInfo Argentina = CultureInfo.GetCultureInfo("es-AR");

    /// <summary>
    /// Precio en pesos con separador de miles, ej. "$ 1.463.000".
    /// Solo muestra centavos si el precio los tiene (ej. "$ 1.999,50").
    /// </summary>
    /// <param name="valor">Monto en pesos argentinos.</param>
    /// <returns>El monto formateado con la cultura es-AR.</returns>
    public static string Precio(decimal valor) =>
        valor.ToString(valor == decimal.Truncate(valor) ? "C0" : "C2", Argentina);

    /// <summary>Monto en pesos sin decimales, para totales grandes, ej. "$ 152.040.000".</summary>
    /// <param name="valor">Monto en pesos argentinos.</param>
    /// <returns>El monto redondeado y formateado con la cultura es-AR.</returns>
    public static string Monto(decimal valor) => valor.ToString("C0", Argentina);

    /// <summary>Fecha y hora corta, ej. "05/10/2026 14:30".</summary>
    /// <param name="fecha">Fecha a mostrar.</param>
    /// <returns>La fecha en formato día/mes/año hora:minutos.</returns>
    public static string FechaHora(DateTime fecha) => fecha.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
}
