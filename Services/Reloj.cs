namespace TechMundo.Services;

/// <summary>
/// Hora oficial de Argentina, independiente de la zona horaria del servidor.
/// Azure App Service (Linux) corre en UTC: con <c>DateTime.Now</c> una compra a las 22:00
/// quedaba registrada al día siguiente y los filtros por fecha se corrían 3 horas.
/// </summary>
public static class Reloj
{
    private static readonly TimeZoneInfo? Argentina = BuscarZona();

    /// <summary>Fecha y hora actual en Argentina.</summary>
    public static DateTime Ahora => Argentina is null
        ? DateTime.Now
        : TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, Argentina);

    /// <summary>Fecha de hoy (sin hora) en Argentina.</summary>
    public static DateTime Hoy => Ahora.Date;

    private static TimeZoneInfo? BuscarZona()
    {
        // "America/Argentina/Buenos_Aires" es el nombre IANA (Linux/macOS y Windows con .NET 6+);
        // "Argentina Standard Time" es el nombre clásico de Windows.
        foreach (var id in new[] { "America/Argentina/Buenos_Aires", "Argentina Standard Time" })
        {
            try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
            catch (TimeZoneNotFoundException) { }
            catch (InvalidTimeZoneException) { }
        }
        return null;
    }
}
