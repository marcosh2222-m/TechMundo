using System.Security.Cryptography;
using System.Text;

namespace TechMundo.Services;

/// <summary>Resultado de un intento de ingreso al panel de administración.</summary>
public enum ResultadoIngreso { Ok, Incorrecto, Bloqueado, NoConfigurado }

/// <summary>
/// Gate de acceso al panel de administración SIN sistema de usuarios/login real
/// (no se dio en la materia). Un único PIN desbloquea el panel para la pestaña actual.
///
/// De dónde sale el PIN (nunca queda escrito en el código fuente ni en appsettings.json):
///   • Producción (Azure): variable de entorno / App Setting "AdminPin".
///   • Desarrollo: User Secrets (dotnet user-secrets set "AdminPin" "1234").
///     Si no se configuró nada, en Development se usa el PIN de demo 2468.
///   • Si en producción no hay PIN configurado, el panel queda deshabilitado.
///
/// IMPORTANTE (a explicar en la defensa): esto NO es seguridad real — no hay usuarios,
/// roles ni contraseñas hasheadas en una base. Mitigaciones básicas incluidas:
/// comparación en tiempo constante y bloqueo de 30 s tras 5 intentos fallidos (por
/// pestaña). Para producción real haría falta ASP.NET Core Identity o similar.
/// </summary>
public class AdminAccessState
{
    /// <summary>PIN de demostración, válido SOLO en el entorno Development.</summary>
    private const string PinDemoDesarrollo = "2468";
    private const int MaxIntentos = 5;
    private static readonly TimeSpan DuracionBloqueo = TimeSpan.FromSeconds(30);

    private readonly string? _pin;
    private readonly ILogger<AdminAccessState> _logger;
    private int _intentosFallidos;
    private DateTime? _bloqueadoHasta;

    public AdminAccessState(IConfiguration config, IWebHostEnvironment env, ILogger<AdminAccessState> logger)
    {
        _logger = logger;
        _pin = config["AdminPin"];
        if (string.IsNullOrWhiteSpace(_pin) && env.IsDevelopment())
            _pin = PinDemoDesarrollo;
    }

    public bool Autenticado { get; private set; }

    /// <summary>False si no hay PIN configurado (en producción el panel queda cerrado).</summary>
    public bool Configurado => !string.IsNullOrWhiteSpace(_pin);

    /// <summary>Segundos que faltan para poder volver a intentar (0 si no hay bloqueo).</summary>
    public int SegundosBloqueo =>
        _bloqueadoHasta is { } hasta && hasta > DateTime.UtcNow ? (int)Math.Ceiling((hasta - DateTime.UtcNow).TotalSeconds) : 0;

    public event Action? OnChange;

    /// <summary>Intenta desbloquear el panel con el PIN ingresado.</summary>
    public ResultadoIngreso Ingresar(string pinIngresado)
    {
        if (!Configurado) return ResultadoIngreso.NoConfigurado;
        if (SegundosBloqueo > 0) return ResultadoIngreso.Bloqueado;

        if (SonIguales(pinIngresado, _pin!))
        {
            Autenticado = true;
            _intentosFallidos = 0;
            _bloqueadoHasta = null;
            OnChange?.Invoke();
            return ResultadoIngreso.Ok;
        }

        _intentosFallidos++;
        _logger.LogWarning("Intento de PIN de admin incorrecto ({Intentos}/{Max})", _intentosFallidos, MaxIntentos);
        if (_intentosFallidos >= MaxIntentos)
        {
            _intentosFallidos = 0;
            _bloqueadoHasta = DateTime.UtcNow.Add(DuracionBloqueo);
            return ResultadoIngreso.Bloqueado;
        }
        return ResultadoIngreso.Incorrecto;
    }

    /// <summary>
    /// "Huella" del PIN vigente que se guarda (cifrada) en el sessionStorage de la pestaña
    /// para no pedir el PIN otra vez al recargar con F5. Si el PIN cambia, la huella
    /// guardada deja de coincidir y se vuelve a pedir.
    /// </summary>
    public string HuellaSesion => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("TechMundo.Admin:" + _pin)));

    /// <summary>Restaura la sesión si la huella guardada coincide con la del PIN actual.</summary>
    public bool RestaurarSesion(string? huellaGuardada)
    {
        if (!Configurado || huellaGuardada is null || !SonIguales(huellaGuardada, HuellaSesion)) return false;
        Autenticado = true;
        OnChange?.Invoke();
        return true;
    }

    public void Salir()
    {
        Autenticado = false;
        OnChange?.Invoke();
    }

    // Comparación en tiempo constante: no "filtra" cuántos caracteres coinciden.
    private static bool SonIguales(string a, string b) =>
        CryptographicOperations.FixedTimeEquals(Encoding.UTF8.GetBytes(a ?? ""), Encoding.UTF8.GetBytes(b));
}
