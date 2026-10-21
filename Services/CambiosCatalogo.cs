namespace TechMundo.Services;

/// <summary>
/// Notificador de cambios del catálogo compartido por TODA la aplicación (Singleton).
///
/// Antes el evento OnChange vivía en ProductoService, que es Scoped (uno por pestaña /
/// circuito de Blazor Server): una venta hecha en una pestaña no actualizaba el panel
/// de admin abierto en otra. Ahora todos los ProductoService reenvían su OnChange a este
/// único objeto, así cualquier cambio llega a todas las pestañas abiertas.
///
/// Importante: los suscriptores reciben la notificación desde OTRO circuito, por eso
/// siempre deben usar InvokeAsync(...) y desuscribirse al hacer Dispose.
/// </summary>
public class CambiosCatalogo
{
    private readonly ILogger<CambiosCatalogo> _logger;

    public CambiosCatalogo(ILogger<CambiosCatalogo> logger) => _logger = logger;

    public event Action? OnChange;

    public void Notificar()
    {
        var suscriptores = OnChange;
        if (suscriptores is null) return;

        // Se invoca a cada suscriptor por separado: si uno falla (ej. una pestaña que se
        // está cerrando), el resto igual recibe la notificación.
        foreach (Action suscriptor in suscriptores.GetInvocationList())
        {
            try { suscriptor(); }
            catch (Exception ex) { _logger.LogWarning(ex, "Un suscriptor de cambios del catálogo falló"); }
        }
    }
}
