# Changelog

Formato basado en [Keep a Changelog](https://keepachangelog.com/es-ES/1.0.0/).

## [1.2.2] - 2026-10-26

### Added
- App publicada en Render: https://techmundo.onrender.com, con deploy automático desde GitHub Actions (Deploy Hook).

### Fixed
- Orden alfabético del catálogo: SQLite ordenaba distinguiendo mayúsculas ("ASUS" aparecía antes que "Acer").

## [1.2.1] - 2026-10-26

### Fixed
- Modo oscuro: los números del dashboard quedaban oscuros sobre fondo oscuro (la clase `card` de `EstadisticaCard`
  chocaba con `.card` de Bootstrap). Se agregaron las variables de Bootstrap y `color-scheme: dark`.
- El email del checkout aceptaba cualquier texto con "@" (por ejemplo "asd@asd"): `[EmailAddress]` de .NET es muy permisivo.

### Added
- Validaciones de campos en todo el programa (`Models/Validaciones.cs`):
  - Checkout: nombre y apellido (solo letras) y email con dominio válido; se validan también en `VentaService`.
  - Productos: marca y descripción obligatorias, precio entre $1 y $100.000.000 con hasta 2 decimales, stock y stock
    mínimo con rangos, hasta 15 características de 150 caracteres, nombre único y mensajes en español.
  - Ventas: el rango de fechas no puede estar invertido ni empezar en el futuro.
  - Carrito: máximo 99 unidades por producto; límites de longitud en búsquedas y en el PIN.
- Los formularios usan `novalidate` para que siempre se vean los mensajes propios en español.

## [1.2.0] - 2026-10-26

### Added
- `Dockerfile` en dos etapas (SDK → runtime ASP.NET Core 8) y `.dockerignore`, para publicar en Render.
- Workflow CI/CD: job `docker` que construye la imagen, la levanta y verifica `/health`; job `deploy-render` que
  dispara el Deploy Hook de Render (secret `RENDER_DEPLOY_HOOK_URL`) solo si el build y la imagen pasaron.
- `docs/DESPLIEGUE-RENDER.md`: guía paso a paso (gratis, sin tarjeta). Azure queda como alternativa.

## [1.1.0] - 2026-10-26

### Added
- 37 de los 40 productos con foto, servidas desde `wwwroot/img/productos/` (WebP, máx. 800 px) en lugar de enlazar
  sitios externos. Las fotos faltantes salen de Wikimedia Commons; créditos y licencias en `docs/CREDITOS-IMAGENES.md`.
- Migración `ImagenesLocales` con los cambios del seed (la migración `Inicial` no se modifica).

### Changed
- Productos reemplazados para que coincidan con la foto disponible: Raspberry Pi 5 (antes ASUS ROG NUC),
  Samsung T7 Shield (antes T7), Logitech G PRO TKL (antes G PRO X 60) y ASUS ROG Strix RTX 4070 Ti SUPER
  (antes RTX 4070 SUPER FE). Las ventas de prueba que los incluían se recalcularon.
- Dell UltraSharp, Motorola Edge 2025 y HyperX Cloud II quedan sin imagen a propósito, para la demo del ícono por defecto.
- `ImagenUrl` acepta rutas del propio sitio (`/img/...`) además de URLs `https://` (antes `[Url]` las rechazaba).
- Capturas del README actualizadas con las fotos reales.

## [1.0.1] - 2026-10-22

### Added
- Migración `Inicial` generada con dotnet-ef 8.0.10 y verificada con SQLite real en GitHub Actions
  (40 productos, 15 ventas, 26 ítems, 5 categorías; 34 pruebas de navegador en verde).
- Repositorio público en GitHub con ramas `main` / `develop` y 5 issues iniciales (#1 a #5).

### Changed
- El job de deploy solo corre cuando existe la variable `AZURE_WEBAPP_NAME`: antes de configurar Azure el
  workflow queda en verde en lugar de fallar en el paso de deploy.
- Documentación actualizada al estado final (roadmap, matriz de requisitos, checklist de defensa, Git Flow).

## [1.0.0] - 2026-10-20

Versión de entrega del Proyecto Integrador.

### Added
- Dashboard del admin con ventas, ingresos y unidades de los últimos 30 días y las 5 últimas ventas
  (se actualiza solo ante una venta en otra pestaña).
- `Properties/launchSettings.json` con perfiles http/https en **Development**. Sin este archivo, `dotnet run`
  arrancaba en Production: el panel quedaba sin PIN y la base iba a `C:\home\data`.
- Bootstrap 5.3.3 y Bootstrap Icons 1.11.3 servidos desde `wwwroot/lib` (con sus licencias MIT), sin depender de un CDN.
- `docs/REQUISITOS.md`: matriz de requisitos y criterios de evaluación → archivos.
- Capturas de pantalla en `docs/capturas/` y en el README.

### Fixed
- Una categoría inexistente (`/categoria/xyz`) mostraba todos los productos; ahora avisa que no existe.
- El título del catálogo usa el nombre real de la categoría ("Redes") en lugar del texto de la URL.
- El ícono por defecto se desbordaba en las miniaturas de la gestión de productos.
- Hora de las ventas: se registra en hora argentina (`Reloj.Ahora`) aunque el servidor de Azure esté en UTC;
  antes una compra a las 22:00 quedaba con fecha del día siguiente.
- La página `/Error` se genera en el servidor sin interactividad (patrón de .NET 8), así se muestra aunque el
  circuito de Blazor no pueda arrancar.
- Sin migraciones, la app se detenía recién en la primera consulta ("no such table"); ahora no arranca y explica
  qué comando correr. `/health` consulta la tabla `Productos`, así una base sin tablas ya no figura como Healthy.

### Changed
- `.gitignore` excluye `publish/` y los perfiles de publicación de Azure (`*.PublishSettings`, contienen credenciales).
- Mensajes y textos menores (rango de precio en pesos, títulos de página, búsqueda del admin).

## [0.11.0] - 2026-10-20

Unidad 4: prácticas profesionales.

### Added
- XML docs completos (`<summary>`, `<param>`, `<returns>`, `<exception>`, `<example>`) en `IProductoService`,
  `IVentaService`, `CarritoState`, `PagedResult` y `Formato`; el build genera `TechMundo.xml`.
- `docs/HISTORIAS-DE-USUARIO.md`: 5 historias del Sprint 1 con criterios de aceptación y estimación,
  Definition of Done y Product Backlog de 18 historias.
- `docs/GIT-FLOW.md`: ramas, Conventional Commits, primer push sin dejar credenciales, PR, issues y Kanban.
- `docs/DESPLIEGUE.md`: guía de Azure App Service F1 (WebSockets, autenticación básica de SCM, `AdminPin`).
- `docs/DEFENSA.md`: guion de 15 minutos, checklist de entrega y preguntas probables.
- Plantillas de GitHub para Pull Requests, historias de usuario y bugs.

### Changed
- **Precios en pesos argentinos**: precio de lista en USD × dólar oficial BNA ($1.540, 20/10/2026), redondeado
  a los $1.000. Formato `es-AR` ("$ 1.463.000"). Las ventas de prueba se recalcularon. **Requiere regenerar la base.**
- Workflow de CI/CD separado en jobs `build` y `deploy`: compila también `develop` y los Pull Requests;
  despliega solo desde `main`; nombre del App Service configurable con la variable `AZURE_WEBAPP_NAME`.

## [0.10.0] - 2026-10-20

Revisión de las Unidades 1 a 3: correcciones críticas, importantes y de calidad.
Primera versión **compilada y probada en un navegador** (31 pruebas automáticas de punta a punta).

### Fixed
- Página `/Error` inexistente (en producción cualquier error terminaba en una pantalla en blanco). Se agregaron
  `Pages/Error.razor`, el aviso `#blazor-error-ui` y un `ErrorBoundary` en cada layout.
- SQLite en Azure: se crea la carpeta de la base (`/home/data`) antes de migrar.
- Fuga de memoria: `CatalogoLayout` y `AdminLayout` implementaban `IDisposable` e `IAsyncDisposable` a la vez,
  y Blazor solo llama a `DisposeAsync`, así que nunca se desuscribían de los eventos.
- "Copiar" en el admin podía cortar la sesión de Blazor si el navegador negaba el portapapeles.
- Paginación atascada en una página vacía al borrar/vender el último producto de la última página.
- Errores al guardar un producto o al cargar el catálogo ya no tiran abajo la sesión: se muestran en pantalla.
- La notificación de stock bajo del dashboard nunca se disparaba (se evaluaba antes de cargar los datos).
- En celulares la página se desbordaba horizontalmente (~1100 px a 375 px) por el carrusel.
- Búsqueda sensible a mayúsculas: ahora usa `EF.Functions.Like` ("usb" encuentra "USB") y busca también en la descripción.

### Security
- El PIN de admin ya no está en `appsettings.json`: se lee de `AdminPin` (User Secrets en desarrollo, variable de
  entorno en Azure). Comparación en tiempo constante y bloqueo de 30 s tras 5 intentos fallidos.

### Changed
- Sin prerender: cada página consulta la base una sola vez y el PIN no "parpadea" al recargar.
- La sesión del admin y el carrito se conservan al recargar (F5) mediante `ProtectedSessionStorage`.
- Notificaciones entre pestañas: `CambiosCatalogo` (Singleton) avisa a todas las pestañas abiertas de una venta o cambio.
- Concurrencia optimista en `Producto` (`Version` + `[ConcurrencyCheck]`). **Requiere regenerar la migración.**
- Componentes nuevos reutilizables: `Modal` (overlay, Escape, foco) e `ImagenProducto`; `Formato` centraliza moneda y fechas.
- CSS duplicado unificado en `app.css`; grilla del catálogo mobile-first; modo oscuro sin destello al cargar.
- HTML semántico: tarjetas como `<article>`, `footer` en el admin, tarjetas y carrusel operables con teclado.
- Estadísticas del inventario consultan solo las columnas necesarias. Logs `Information` de la app visibles en producción.

## [0.9.0] - 2026-10-05

### Added
- Carrito de compras en modal (header con contador): valida stock al agregar, cambia cantidades, resumen y total.
- Confirmación de compra con datos del cliente (validados); descuenta stock en una transacción y registra la venta.
- Historial de ventas en el admin (`/admin/ventas`): lista paginada, filtros por fecha, búsqueda y orden, resumen e detalle en modal.
- 15 ventas de prueba en el seed. Navegación del admin centrada, estilizada y siempre visible (Dashboard, Productos, Ventas).
- Modelos `Venta` y `VentaItem`. **Requiere regenerar la migración**.

## [0.8.2] - 2026-10-05

### Changed
- Producto sin imagen o con imagen caída: ícono grande que sigue el modo oscuro (reemplaza `placeholder.svg`).

## [0.8.1] - 2026-10-05

### Changed
- Header: se quitó el texto de categorías y la búsqueda quedó centrada. Sidebar pegado a la izquierda y listado centrado.

## [0.8.0] - 2026-10-05

### Added
- Carrusel con autoplay (4 s), pausa al pasar el mouse y reinicio al navegar manualmente.
- "Ordenar por" (Destacados / Menor precio / Mayor precio) arriba a la derecha del listado del catálogo. `Destacados` mantiene el orden original.

### Fixed
- Imágenes rotas: fallback global en `App.razor` que reemplaza cualquier `<img>` caído por `placeholder.svg` (el `onerror` inline se desactivaba al reutilizar elementos del DOM).

## [0.7.0] - 2026-10-05

### Added
- Catálogo inicial de 40 productos reales (8 por categoría) con descripción, características e imagen.
- Redes incluye 3 adaptadores USB WiFi (TP-Link Archer T3U Plus, T2U Nano, TX20U Plus WiFi 6) y el ASUS USB-AC53 Nano.
- Campo `Caracteristicas` en `Producto` (formulario admin y panel de detalle del catálogo).
- Imagen de respaldo (`wwwroot/img/placeholder.svg`) si una URL falla.

### Changed
- Precios en USD (cultura en-US). `ImagenUrl` ampliada a 500 caracteres.
- **Requiere regenerar la migración** (borrar `Migrations/` y el `.db`).

## [0.6.1] - 2026-09-08
### Fixed
- **Crítico**: `App.razor` no tenía el `<link rel="stylesheet" href="TechMundo.styles.css" />` que Blazor requiere para aplicar la CSS Isolation. Ninguno de los 14 archivos `.razor.css` del proyecto se estaba aplicando en el navegador — la interfaz se veía sin grid, sin sidebar, sin tarjetas, como texto plano.

## [0.6.0] - 2026-09-08
### Fixed
- **Crítico**: `Producto.cs` declaraba la propiedad `Destacado` dos veces (error de compilación CS0102). Esto impedía que el proyecto compilara y generaba el error "no se puede iniciar el programa" en Visual Studio.
- Registro duplicado de `AdminAccessState` en `Program.cs` (no rompía la build, pero estaba de más).
### Added
- 2 categorías nuevas en el seed: Gaming y Redes (ahora son 5 en total, como pedía la consigna original de la Unidad 3).
- 4 productos nuevos para poblar esas categorías (silla gamer, teclado mecánico, router, switch de red).

## [0.5.0] - 2026-09-08
### Added
- Notificación del navegador en `Admin/Dashboard` cuando hay productos con stock bajo (`mostrarNotificacion`).
### Changed
- `interop.js` reescrito como módulo ES6 puro (`export function`), sin exponer nada en `window`.
- `CatalogoLayout`, `AdminLayout`, `ProductoCard` y `Admin/Dashboard` ahora usan el patrón `import()` dinámico → `IJSObjectReference` → `IAsyncDisposable`, en vez de `JS.InvokeVoidAsync` sobre un objeto global.
- `ProductoCard` usa import **perezoso** del módulo (recién al primer clic en "Copiar"), para no importarlo una vez por cada tarjeta renderizada en la lista.
### Fixed
- Se recuperó el botón de modo oscuro, perdido al dividir `MainLayout` en `CatalogoLayout`/`AdminLayout` en la versión anterior.

## [0.4.0] - 2026-09-08
### Added
- Split completo en dos secciones: catálogo público (`/`) y panel de administración (`/admin`).
- `AdminAccessState`: gate de acceso al admin con PIN simple (sin login/usuarios reales — documentado en el README).
- `CatalogoLayout.razor`: header con buscador + sidebar de categorías + footer, estructura típica de tienda online.
- `ProductoCarrusel.razor`: carrusel de productos destacados, 100% Blazor/CSS (sin JS externo).
- `ProductoVidriera.razor`: tarjeta de producto de solo lectura para el catálogo público.
- `ProductoModal.razor`: modal de detalle de producto con botón "Comprar" (decorativo, fuera de alcance por ahora).
- Campo `Destacado` en `Producto`, para elegir manualmente qué aparece en el carrusel.
- `IProductoService.ObtenerPaginado` acepta `soloDisponibles` — el catálogo público nunca muestra productos no disponibles.
- Media queries mobile-first en `CatalogoLayout` (sidebar pasa a fila de chips en pantallas angostas).
### Changed
- Las páginas de gestión de productos se movieron a `/admin/*` (antes en la raíz).
- El viejo dashboard (`Pages/Index.razor`) ahora es `Pages/Admin/Dashboard.razor`, enfocado en alertas de stock bajo.
### Removed
- `Shared/MainLayout.razor` (reemplazado por `CatalogoLayout` y `AdminLayout`).

## [0.3.0] - 2026-09-07
### Added
- `EstadisticaCard.razor`: 6to componente reutilizable, usado en `Index` (dashboard) y `MainLayout` (header), sin duplicar markup.
- Validación cruzada en `Producto` (`IValidatableObject`): no se puede marcar "Disponible" un producto con Stock = 0.
### Fixed
- `Producto` declaraba `IValidatableObject` sin implementar `Validate()` — no compilaba. Corregido.
- La regla de "Disponible requiere stock" se reforzó también en `ProductoService.ToggleDisponible`, que no pasa por el formulario y podía evadir la validación.

## [0.2.0] - 2026-09-07
### Added
- Campo `Disponible` en `Producto` con toggle inmediato (equivalente a completada/pendiente).
- `ConfirmDialog.razor`: modal de confirmación de borrado 100% Blazor/CSS, sin JavaScript.
- Filtro por categoría con `NavLink` y rutas con parámetro (`/productos/{categoria}`).
- Bootstrap 5 + Bootstrap Icons (grid responsive, iconografía).
### Changed
- `ProductoCard` ya no decide el borrado: emite `OnEliminarSolicitado` y la página controla el modal.
### Removed
- `confirmarAccion` de `interop.js` (reemplazado por el modal nativo de Blazor).

## [0.1.0] - 2026-09-07
### Added
- Modelo de dominio: Producto (con marca, descripción, imagen, precio, stock y stock mínimo) y Categoria.
- Persistencia con EF Core + SQLite: migraciones y seed data (3 categorías, 10 productos).
- Servicio `IProductoService` con CRUD, paginación, búsqueda y filtro por categoría/stock bajo.
- Interfaz minimalista: catálogo, alta y edición de productos, dashboard con estadísticas.
- Modo oscuro con persistencia en localStorage vía JS Interop.
