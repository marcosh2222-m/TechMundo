# Roadmap — TechMundo (Proyecto Integrador)

Última actualización: 2026-10-26 · versión 1.2.0 (entrega)

Estado del proyecto frente a los requerimientos oficiales de las 4 unidades de
"Desarrollo Web con Blazor". Cada ítem indica si está resuelto, pendiente, o es
una adaptación de dominio (Tareas → Productos).

Leyenda: ✅ Resuelto · ⚠️ Parcial / a revisar · ❌ Pendiente

---

## Unidad 1 — Fundamentos de Blazor y Razor Components

| Requisito | Estado | Detalle |
|---|---|---|
| Listado con filtro | ✅ | Filtro por categoría (`/productos/{categoria}`) en vez de estado |
| Formulario de alta | ✅ | `ProductoForm.razor` |
| Formulario de edición | ✅ | Mismo componente, reutilizado |
| Eliminar con confirmación modal sin JS | ✅ | `ConfirmDialog.razor` |
| Toggle de estado | ✅ | `Disponible` (switch) en vez de completada/pendiente |
| Estadísticas reactivas en header | ✅ | `MainLayout` suscripto a `OnChange` |
| Validación con Data Annotations | ✅ | Incluye validación **cruzada** (`IValidatableObject`): no se puede marcar "Disponible" un producto con Stock = 0. Se refuerza en dos lugares: el formulario (`DataAnnotationsValidator`) y el toggle rápido de la tarjeta (`ProductoService.ToggleDisponible`), que no pasa por el formulario |
| State Service (Scoped) | ✅ | `IProductoService` |
| NavLink + rutas con parámetros | ✅ | Filtro de categoría + `/editar-producto/{id}` |
| Bootstrap responsive | ✅ | Grid de Bootstrap 5 vía CDN |
| **Mínimo 6 componentes** (criterio del Integrador) | ✅ | `ProductoCard`, `ProductoForm`, `AlertError`, `Paginacion`, `ConfirmDialog`, **`EstadisticaCard`** (nuevo — reutilizado en el dashboard y en el header del layout, evita duplicar el mismo markup dos veces) |

## Unidad 2 — Interfaces Web: HTML, CSS, Bootstrap y JS Interop

| Requisito | Estado | Detalle |
|---|---|---|
| HTML5 semántico en todos los layouts | ✅ | `header`, `nav`, `aside`, `main` y `footer` en ambos layouts; tarjetas de producto como `<article>`; búsqueda con `role="search"` |
| CSS Isolation en ≥3 componentes | ✅ | 17 archivos `.razor.css` entre componentes, layouts y páginas |
| Variables CSS globales | ✅ | `app.css` |
| Bootstrap 5: grilla, cards, badges, botones con iconos | ✅ | Grid + Bootstrap Icons |
| Modo oscuro con persistencia | ✅ | `localStorage` vía JS Interop |
| **JS Interop con módulo ES6 + `IJSObjectReference` + `DisposeAsync`** | ✅ | `interop.js` ahora exporta funciones ES6 puras (sin `window.x` global). Cada componente que lo necesita (`CatalogoLayout`, `AdminLayout`, `ProductoCard`, `Admin/Dashboard`) hace `import()` dinámico, guarda el `IJSObjectReference` y lo libera con `IAsyncDisposable`. En `ProductoCard` el import es **perezoso** (recién al primer clic en "Copiar"), para no importar el módulo N veces en una lista |
| Notificaciones (equivalente a "tareas vencidas") | ✅ | `Admin/Dashboard.razor` dispara una notificación del navegador con la cantidad de productos en stock bajo al entrar |
| Modal de confirmación sin JS | ✅ | `ConfirmDialog` (borrado) y `ProductoModal` (detalle) — ambos sin una sola línea de JS |
| Mobile-first, usable en 375px | ✅ | Probado con Chromium a 375 px (catálogo, modal, carrito, admin) sin scroll horizontal. Grilla del catálogo escrita mobile-first (`min-width`) |

## Unidad 3 — Persistencia con Entity Framework Core

| Requisito | Estado | Detalle |
|---|---|---|
| SQLite + EF Core + `DbContextFactory` | ✅ | |
| Entidad principal con propiedades validadas | ✅ | `Producto` |
| Entidad relacionada (1 a muchos) | ✅ | `Categoria` (Producto) y `Venta` → `VentaItem` (nuevo) |
| Migraciones automáticas al iniciar | ✅ | `db.Database.Migrate()` en `Program.cs` |
| **Datos de seed: 5 categorías** | ✅ | Computadoras, Celulares, Accesorios, Gaming y Redes, con **40 productos reales** (8 por categoría, con descripción, características e imagen; Redes incluye 4 adaptadores USB WiFi) y **15 ventas de prueba** |
| Servicio con CRUD + paginación | ✅ | `IProductoService` |
| Manejo de errores visible en la UI | ✅ | `AlertError` + `try/catch` en todas las páginas, `ErrorBoundary` en los layouts, página `/Error` y aviso `#blazor-error-ui` |
| Búsqueda con LIKE | ✅ | `EF.Functions.Like` por nombre, marca y descripción (sin distinguir mayúsculas) |
| Estadísticas desde consulta real | ✅ | `ObtenerEstadisticas()` |

## Unidad 4 — Integración, Buenas Prácticas y Despliegue

| Requisito | Estado | Detalle |
|---|---|---|
| Mínimo 2 entidades relacionadas | ✅ | Producto ↔ Categoria y Venta ↔ VentaItem ↔ Producto |
| README profesional | ✅ | Con diagrama Mermaid |
| CHANGELOG.md | ✅ | Formato Keep a Changelog |
| .gitignore | ✅ | |
| GitHub Actions (build + deploy) | ✅ | Build en verde en cada push/PR; el deploy se activa al configurar Azure (variable `AZURE_WEBAPP_NAME` + secret) |
| **XML docs en el servicio** (`///`, `<summary>`, `<param>`) | ✅ | Completos en `IProductoService`, `IVentaService` y `CarritoState`; el build genera `TechMundo.xml` |
| Historias de usuario (Scrum/Kanban) | ✅ | `docs/HISTORIAS-DE-USUARIO.md`: 5 historias + DoD + backlog de 18; plantillas de issues y PR |
| Repositorio GitHub con historial limpio | ✅ | [marcosh2222-m/TechMundo](https://github.com/marcosh2222-m/TechMundo): Conventional Commits, ramas `main` / `develop` / `docs/*`, issues |
| Aplicación desplegada en URL pública | ⚠️ (preparado) | Render con Docker: `docs/DESPLIEGUE-RENDER.md` (Azure como alternativa) |
| Health checks | ✅ | `/health` |
| Logging con `ILogger<T>` | ✅ | Ya en `ProductoService` |
| User Secrets en desarrollo | ✅ | `UserSecretsId` en el `.csproj`; el PIN de admin se configura con `dotnet user-secrets` (dev) o variable de entorno (Azure) |
| Sin credenciales en el código fuente | ✅ | El PIN salió de `appsettings.json` (v0.10.0) |

---

## Arquitectura nueva: catálogo público + panel admin

Fuera del checklist oficial de la materia, pero mejora bastante la presentación
del proyecto para la defensa: la app ahora se divide en dos experiencias
separadas, con sus propios layouts (`CatalogoLayout` / `AdminLayout`):

- **`/`** — catálogo público tipo tienda online: sidebar de categorías, carrusel
  de destacados con autoplay, grilla con "Ordenar por", modal de detalle con
  características y botón "Agregar al carrito", y carrito en modal. Solo se ven
  los productos `Disponible = true`.
- **`/admin`** — gestión completa (CRUD) e historial de ventas, protegida por un PIN simple
  (`AdminAccessState`, ver el README para el detalle de por qué no es un login real).

Ya son 13 componentes reutilizables (`Modal`, `ImagenProducto`, `ProductoVidriera`, `ProductoCarrusel`,
`ProductoModal`, `CarritoModal` y `VentaDetalleModal` son nuevos).

## Plan de trabajo sugerido (orden de las próximas versiones)

**Fase A — Cerrar Unidad 1** ✅ Completa

**Fase B — Cerrar Unidad 2** ✅ Completa
- [x] Modal de confirmación sin JS (ahora hay dos: `ConfirmDialog` y `ProductoModal`)
- [x] Mobile-first con media queries en `CatalogoLayout`
- [x] Refactor de `interop.js` al patrón `IJSObjectReference` + `IAsyncDisposable` (import perezoso en `ProductoCard`, import en `OnAfterRenderAsync` en los layouts)
- [x] Notificación del navegador para productos con stock bajo (dashboard de admin)
- [x] De paso: se recuperó el toggle de modo oscuro, que se había perdido al dividir `MainLayout` en `CatalogoLayout`/`AdminLayout`

**Fase C — Cerrar Unidad 3** ✅ Completa
- [x] Sumar 2 categorías más al seed (llegar a 5) — ahora Computadoras, Celulares, Accesorios, Gaming, Redes
- [ ] (Opcional, sigue pendiente) `ICategoriaService` para poder crear/editar categorías desde la UI

> ⚠️ **Bugs críticos encontrados y corregidos en estas dos últimas vueltas**:
>
> 1. `Producto.cs` tenía la propiedad `Destacado` declarada **dos veces**
>    (error de compilación CS0102). Era la causa del error "no se puede
>    iniciar el programa" — el build fallaba y el `.exe` nunca se generaba.
> 2. `App.razor` nunca tuvo el `<link rel="stylesheet"
>    href="TechMundo.styles.css" />` que Blazor necesita para servir el CSS
>    Isolation. Los 14 archivos `.razor.css` del proyecto compilaban bien,
>    pero **ninguno se aplicaba en el navegador** — por eso la interfaz se
>    veía como texto plano sin grid, sin sidebar, sin tarjetas. Visual Studio
>    no avisa nada de esto: compila perfecto, el problema es puramente qué se
>    carga en el `<head>` del HTML.

**Fase E — Catálogo real y sistema de ventas** ✅ Completa (fuera de la consigna oficial)
- [x] 40 productos reales con descripción, características e imágenes (campo `Caracteristicas`, v0.7.0)
- [x] Imagen por defecto que sigue el modo oscuro cuando falta o falla la foto (v0.8.2)
- [x] Carrusel con autoplay y "Ordenar por" (Destacados / Menor precio / Mayor precio) (v0.8.0)
- [x] Header y layout del catálogo rediseñados: búsqueda centrada, categorías a la izquierda (v0.8.1)
- [x] Carrito en modal con validación de stock, resumen y total (v0.9.0)
- [x] Confirmación de compra con datos del cliente; descuenta stock y registra la venta en una transacción
- [x] Historial de ventas en admin: lista paginada, filtros (fechas, búsqueda, orden), resumen y detalle en modal
- [x] Menú del admin centrado, estilizado y siempre visible (Dashboard / Productos / Ventas)
- [x] Compilación y flujo completo del carrito verificados (v0.10.0)

**Fase F — Revisión de las Unidades 1 a 3** ✅ Completa (v0.10.0)
- [x] Página `/Error`, `ErrorBoundary` y aviso de error de Blazor (antes: pantalla en blanco en producción)
- [x] Carpeta de SQLite creada al iniciar (Azure `/home/data`)
- [x] PIN fuera del código + bloqueo por intentos fallidos
- [x] Fuga de memoria por `IDisposable` + `IAsyncDisposable` en los layouts
- [x] Paginación que se corrige sola, errores del formulario y de "Copiar" controlados
- [x] Sin prerender + sesión de admin y carrito que sobreviven a F5
- [x] Concurrencia optimista en productos, notificaciones entre pestañas
- [x] Componentes `Modal` e `ImagenProducto`, `Formato`, CSS unificado, accesibilidad por teclado
- [x] Desborde horizontal en celulares corregido
- [x] Verificado con 31 pruebas automáticas en Chromium (escritorio y 375 px)
- [x] Migración inicial generada con dotnet-ef 8.0.10 y verificada con SQLite real en GitHub Actions

**Ideas opcionales para después** (no están en la consigna)
- [x] ~~Persistir el carrito~~ (hecho en v0.10.0, con `ProtectedSessionStorage`)
- [ ] Anular una venta y devolver el stock
- [x] ~~Mostrar ventas recientes en el Dashboard~~ (hecho en v1.0.0)
- [ ] Productos más vendidos en el Dashboard
- [ ] `ICategoriaService` para gestionar categorías desde la UI
- [ ] Tests (xUnit) para `CarritoState` y `VentaService`

**Fase D — Cerrar Unidad 4**
- [x] XML docs completos en los servicios (v0.11.0)
- [x] Historias de usuario, Definition of Done y Product Backlog (`docs/HISTORIAS-DE-USUARIO.md`)
- [x] Guía de Git Flow y plantillas de issues / PR (`docs/GIT-FLOW.md`)
- [x] Workflow CI/CD separado en build y deploy
- [x] Guía de despliegue en Azure (`docs/DESPLIEGUE.md`) y guion de defensa (`docs/DEFENSA.md`)
- [x] Precios en pesos argentinos
- [x] Migración final generada y verificada con SQLite real (34 pruebas de navegador en verde)
- [x] Repositorio público, historial por partes, ramas `main` / `develop` / `docs/*`
- [ ] **Pendiente:** abrir, comentar y mergear un Pull Request (laboratorio de la Clase 19, `docs/GIT-FLOW.md`)
- [x] 5 issues creados en GitHub con etiquetas `bug` / `enhancement` / `documentation`
- [ ] **Pendiente:** tablero Kanban en GitHub Projects con los issues (`docs/GIT-FLOW.md`)
- [x] `Dockerfile` + jobs `docker` y `deploy-render` en el workflow (v1.2.0)
- [ ] **Pendiente:** crear el Web Service en Render, cargar `AdminPin` y el secret `RENDER_DEPLOY_HOOK_URL` (`docs/DESPLIEGUE-RENDER.md`)
- [x] Capturas en el README (`docs/capturas/`), actualizadas con fotos reales (v1.1.0)
- [x] 37 productos con foto local + créditos (`docs/CREDITOS-IMAGENES.md`); 3 sin imagen para la demo
- [x] Matriz de requisitos (`docs/REQUISITOS.md`)
- [x] Dashboard con ventas, `launchSettings.json`, Bootstrap local (v1.0.0)
- [x] Badge y links del README apuntan a `marcosh2222-m/TechMundo`

