using TechMundo.Data;
using TechMundo.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Unidad 1: servicios de Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Unidad 3: EF Core + SQLite con DbContextFactory
builder.Services.AddDbContextFactory<TechMundoDbContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Notificador de cambios compartido por todas las pestañas (Singleton) +
// servicio de datos por sesión (Scoped, State Service de la Unidad 1)
builder.Services.AddSingleton<CambiosCatalogo>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// Carrito (por sesión, persistido en sessionStorage) y ventas
builder.Services.AddScoped<CarritoState>();
builder.Services.AddScoped<IVentaService, VentaService>();

// PIN de acceso al admin — no es login real, ver comentario en AdminAccessState (Unidad 4)
builder.Services.AddScoped<AdminAccessState>();

// Unidad 4: Health check para despliegue
// El chequeo consulta la tabla Productos: si la base existe pero sin tablas (por ejemplo, sin
// migraciones), /health informa Unhealthy en vez de un falso Healthy.
builder.Services.AddHealthChecks()
    .AddDbContextCheck<TechMundoDbContext>(
        customTestQuery: (db, ct) => db.Productos.AnyAsync(ct));

var app = builder.Build();

// SQLite no crea carpetas: si la base vive en una carpeta que todavía no existe
// (en Azure App Service: /home/data), hay que crearla antes de migrar.
var archivoDb = new SqliteConnectionStringBuilder(builder.Configuration.GetConnectionString("Default")).DataSource;
if (!string.IsNullOrWhiteSpace(archivoDb) && archivoDb != ":memory:")
{
    var carpeta = Path.GetDirectoryName(Path.GetFullPath(archivoDb));
    if (!string.IsNullOrEmpty(carpeta)) Directory.CreateDirectory(carpeta);
}

using (var scope = app.Services.CreateScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<TechMundoDbContext>>();
    try
    {
        using var db = factory.CreateDbContext();

        // Sin migraciones, Migrate() crea una base VACÍA sin avisar y la app falla después con
        // "no such table". Mejor detenerse acá con un mensaje claro.
        if (!db.Database.GetMigrations().Any())
            throw new InvalidOperationException(
                "El proyecto no tiene migraciones. Ejecutá 'dotnet ef migrations add Inicial' y volvé a iniciar la app.");

        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(ex, "No se pudo crear/migrar la base de datos ({Archivo})", archivoDb);
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapHealthChecks("/health");

app.MapRazorComponents<TechMundo.App>()
    .AddInteractiveServerRenderMode();

app.Run();
