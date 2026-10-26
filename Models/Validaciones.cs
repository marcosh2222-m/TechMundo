using System.Text.RegularExpressions;

namespace TechMundo.Models;

/// <summary>
/// Reglas de validación compartidas por los formularios (Data Annotations) y por los servicios
/// (validación del lado del servidor). Tenerlas en un solo lugar evita que la interfaz y la base
/// acepten cosas distintas.
/// </summary>
public static partial class Validaciones
{
    /// <summary>
    /// Email con usuario, @, dominio y extensión de al menos 2 letras (ej. "ana@correo.com").
    /// <c>[EmailAddress]</c> de .NET solo exige que haya una "@" y aceptaba cosas como "asd@asd".
    /// </summary>
    public const string PatronEmail = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)*\.[A-Za-z]{2,}$";

    /// <summary>Nombre y apellido: al menos dos palabras, solo letras (con acentos), apóstrofo, punto o guion.</summary>
    public const string PatronNombrePersona = @"^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ'.-]+( [A-Za-zÁÉÍÓÚÜÑáéíóúüñ'.-]+)+$";

    /// <summary>Marca: letras, números, espacios y los signos . - &amp; + (ej. "Logitech G", "TP-Link").</summary>
    public const string PatronMarca = @"^[A-Za-z0-9ÁÉÍÓÚÜÑáéíóúüñ .&+\-]+$";

    /// <summary>URL externa (https://...) o ruta del propio sitio (/img/...).</summary>
    public const string PatronImagen = @"^(https?://\S+|/\S+)$";

    /// <summary>Cantidad máxima de unidades de un mismo producto por compra.</summary>
    public const int MaxUnidadesPorProducto = 99;

    [GeneratedRegex(PatronEmail)] private static partial Regex RegexEmail();
    [GeneratedRegex(PatronNombrePersona)] private static partial Regex RegexNombrePersona();

    /// <summary>Indica si el texto (ya sin espacios sobrantes) es un email válido.</summary>
    /// <param name="email">Email a validar.</param>
    /// <returns><c>true</c> si cumple <see cref="PatronEmail"/> y no supera 120 caracteres.</returns>
    public static bool EsEmailValido(string? email) =>
        !string.IsNullOrWhiteSpace(email) && email.Length <= 120 && RegexEmail().IsMatch(email);

    /// <summary>Indica si el texto es un nombre y apellido válido.</summary>
    /// <param name="nombre">Nombre a validar (ya normalizado con <see cref="NormalizarNombre"/>).</param>
    /// <returns><c>true</c> si tiene entre 3 y 80 caracteres y cumple <see cref="PatronNombrePersona"/>.</returns>
    public static bool EsNombrePersonaValido(string? nombre) =>
        !string.IsNullOrWhiteSpace(nombre) && nombre.Length is >= 3 and <= 80 && RegexNombrePersona().IsMatch(nombre);

    /// <summary>Quita espacios al principio y al final y deja un solo espacio entre palabras.</summary>
    /// <param name="texto">Texto ingresado por el usuario.</param>
    /// <returns>El texto normalizado (vacío si era null).</returns>
    public static string NormalizarNombre(string? texto) =>
        string.Join(' ', (texto ?? "").Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));

    /// <summary>Email sin espacios y en minúsculas.</summary>
    /// <param name="email">Email ingresado por el usuario.</param>
    /// <returns>El email normalizado (vacío si era null).</returns>
    public static string NormalizarEmail(string? email) => (email ?? "").Trim().ToLowerInvariant();
}

/// <summary>
/// Campo de texto obligatorio (no vacío ni solo espacios). Se usa en lugar de <c>[Required]</c> en columnas
/// que en la base admiten NULL: <c>[Required]</c> haría que EF Core cambie la columna a NOT NULL y obligaría
/// a una migración. A diferencia de las validaciones de <c>IValidatableObject.Validate</c>, este atributo se
/// evalúa junto con los demás, así el mensaje aparece desde el primer envío del formulario.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class ObligatorioAttribute : System.ComponentModel.DataAnnotations.ValidationAttribute
{
    /// <inheritdoc />
    public override bool IsValid(object? value) => value is string s ? !string.IsNullOrWhiteSpace(s) : value is not null;
}
