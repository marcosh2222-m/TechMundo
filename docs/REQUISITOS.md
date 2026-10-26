# Matriz de requisitos — Proyecto Integrador

Dónde se cumple cada requisito técnico del Proyecto Integrador (Unidad 4) y cada criterio de
evaluación. Sirve como guía rápida para la defensa: cada fila dice qué archivo abrir.

## Requerimientos técnicos

### Unidad 1 — Componentes Blazor

| Requisito | Dónde | Cómo |
|---|---|---|
| Mínimo 6 componentes reutilizables y encapsulados | `Components/` | 13 componentes: `Modal`, `ImagenProducto`, `ProductoVidriera`, `ProductoCarrusel`, `ProductoModal`, `CarritoModal`, `ProductoCard`, `ProductoForm`, `ConfirmDialog`, `VentaDetalleModal`, `EstadisticaCard`, `Paginacion`, `AlertError` |
| Parámetros y eventos | `ProductoCard.razor`, `Paginacion.razor` | `[Parameter]`, `[EditorRequired]`, `EventCallback<T>` hacia la página |
| Composición con `RenderFragment` | `Modal.razor` | `ChildContent`: los 4 modales reutilizan overlay, cierre con Escape y foco |
| Formularios con EditForm y validación avanzada | `ProductoForm.razor`, `CarritoModal.razor`, `Models/Producto.cs` | `DataAnnotationsValidator`, `[Required]`, `[Range]`, `[Url]`, `[EmailAddress]` + validación cruzada `IValidatableObject` (no "Disponible" con stock 0) |
| Routing con parámetros | `Pages/Catalogo/Index.razor`, `Pages/Admin/EditarProducto.razor` | `/categoria/{CategoriaFiltro}`, `/admin/editar-producto/{Id:int}`, query `?q=` con `[SupplyParameterFromQuery]` |
| Navegación programática | `CatalogoLayout.razor`, `Productos.razor` | `NavigationManager.NavigateTo` (búsqueda, editar, volver) |
| State Service con notificación reactiva | `Services/IProductoService.cs`, `CarritoState.cs`, `CambiosCatalogo.cs` | Servicios Scoped con `event Action OnChange`; los componentes se suscriben y refrescan con `InvokeAsync(StateHasChanged)` |

### Unidad 2 — Interfaz visual

| Requisito | Dónde | Cómo |
|---|---|---|
| HTML5 semántico en todos los layouts | `Shared/CatalogoLayout.razor`, `Shared/AdminLayout.razor` | `header`, `nav`, `aside`, `main`, `footer`, `article` en las tarjetas, `role="search"` |
| CSS Isolation en al menos 3 componentes | `*.razor.css` | 17 archivos; `::deep` para estilizar componentes hijos |
| Variables CSS globales | `wwwroot/css/app.css` | Paleta, espaciados y radios en `:root`; modo oscuro redefine las variables |
| Bootstrap 5 responsive (mobile-first) | `wwwroot/lib/`, `Pages/Catalogo/Index.razor.css` | Grilla de 2 → 3 → 4 columnas con `min-width`; probado a 375 px |
| JS Interop (módulo ES6) | `wwwroot/js/interop.js` | `import()` + `IJSObjectReference` + `DisposeAsync`: modo oscuro con `localStorage`, portapapeles, notificaciones |
| Modal sin JS externo | `Components/Modal.razor`, `ConfirmDialog.razor` | 100% Blazor/CSS; foco con `ElementReference.FocusAsync()` |

### Unidad 3 — Persistencia

| Requisito | Dónde | Cómo |
|---|---|---|
| EF Core con SQLite y DbContextFactory | `Program.cs`, `Data/TechMundoDbContext.cs` | `AddDbContextFactory` + `UseSqlite`; un contexto corto por operación |
| Al menos 2 entidades relacionadas | `Data/TechMundoDbContext.cs` | Categoría 1→N Producto (`Restrict`), Venta 1→N VentaItem (`Cascade`), VentaItem N→1 Producto (`SetNull`) |
| Migraciones versionadas y seed | `Migrations/`, `HasData` | 5 categorías, 40 productos reales y 15 ventas; `Database.Migrate()` al iniciar |
| CRUD completo con paginación y búsqueda | `Services/ProductoService.cs`, `Pages/Admin/Productos.razor` | Alta, edición (con concurrencia optimista), baja, toggle; `Skip/Take`; búsqueda con `EF.Functions.Like` |
| Manejo de errores visible | `AlertError`, `ErrorBoundary` en los layouts, `Pages/Error.razor` | `try/catch` con mensajes; nada termina en "pantalla de muerte" |
| Estadísticas desde consulta real | `ProductoService.ObtenerEstadisticas`, `VentaService.ObtenerResumen` | Proyecciones con `Select`; sumas de decimales en memoria (limitación de SQLite) |

### Unidad 4 — Prácticas profesionales

| Requisito | Dónde | Estado |
|---|---|---|
| Repositorio GitHub con historial limpio (Conventional Commits) | [marcosh2222-m/TechMundo](https://github.com/marcosh2222-m/TechMundo) | Listo: commits por tema, ramas `main` / `develop`, issues (falta mergear un Pull Request) |
| CI/CD con GitHub Actions | `.github/workflows/deploy.yml` | Listo: `build` + prueba de la imagen Docker en push/PR; deploy a Render desde `main` |
| README profesional | `README.md` | Listo: descripción, stack, arquitectura, capturas, instalación, configuración |
| App desplegada en URL pública | `Dockerfile`, `docs/DESPLIEGUE-RENDER.md` | Render (gratis, sin tarjeta) con deploy desde GitHub Actions |
| Historias de usuario y DoD | `docs/HISTORIAS-DE-USUARIO.md` | Listo |
| XML docs | `Services/IProductoService.cs`, `IVentaService.cs`, `CarritoState.cs` | Listo; genera `TechMundo.xml` |
| Configuración por ambiente, User Secrets, logging, health check | `appsettings*.json`, `TechMundo.csproj`, `Program.cs` | Listo: `/health`, `ILogger<T>`, PIN fuera del código |

## Criterios de evaluación

| Criterio | Pts | Evidencia principal |
|---|---|---|
| Componentes Blazor: estructura, parámetros, eventos | 20 | `Components/` (13 componentes), `Modal` con `RenderFragment`, `EventCallback` |
| Formularios y validaciones avanzadas | 15 | `ProductoForm`, checkout del carrito, `IValidatableObject`, concurrencia optimista |
| Interfaz visual: HTML5, CSS, Bootstrap responsive | 15 | Layouts semánticos, 17 `.razor.css`, mobile-first, modo oscuro |
| JS Interop implementado correctamente | 10 | `interop.js` como módulo ES6, `IJSObjectReference`, `DisposeAsync`, manejo de `JSException` |
| Persistencia con EF Core: CRUD, paginación, búsqueda | 20 | `ProductoService`, `VentaService` (transacción), migraciones, seed |
| Git: historial limpio, ramas, CI/CD | 10 | Repositorio en GitHub, ramas, issues, workflow en verde (+ un Pull Request a mergear) |
| Aplicación desplegada y accesible | 5 | Render (`docs/DESPLIEGUE-RENDER.md`) |
| README y documentación | 5 | `README.md`, `docs/`, `CHANGELOG.md`, XML docs |
