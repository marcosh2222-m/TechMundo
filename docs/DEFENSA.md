# Guion de la defensa — TechMundo

Unidad 4, Clase 24. **15 minutos**: 10 de presentación + 5 de preguntas. La app tiene que estar
publicada en Azure (no se acepta localhost). Abrila 5 minutos antes: el plan F1 "duerme" la app.

## Checklist de entrega (antes de la clase)

- [ ] App desplegada en Azure con URL funcionando (`/health` responde `Healthy`) — **único pendiente**
- [ ] Tablero Kanban en GitHub Projects con los 5 issues
- [ ] Un Pull Request abierto, comentado y mergeado (`docs/GIT-FLOW.md`, paso 5)
- [x] Repositorio GitHub **público** con historial de commits limpio (Conventional Commits)
- [x] README completo: descripción, stack, instalación, capturas (`docs/capturas/`)
- [x] CHANGELOG con al menos 3 versiones (hay más de 10)
- [x] GitHub Actions en verde (badge del README)
- [x] Base de datos con datos de seed (40 productos, 15 ventas)
- [x] Sin credenciales en el código (el PIN está en Azure / User Secrets)

> Tené a mano [`docs/REQUISITOS.md`](REQUISITOS.md): dice qué archivo abrir para cada requisito.

## Pestañas abiertas antes de empezar

1. Catálogo en Azure (modo claro)
2. `/admin` en Azure, ya con el PIN ingresado
3. El repositorio en GitHub (pestaña *Code*)
4. Visual Studio con el proyecto abierto
5. Ventana del navegador en modo responsive (375 px) por si preguntan por mobile

## 1. Introducción — 1 min

"TechMundo es una tienda online de tecnología hecha en Blazor Server con .NET 8. Tiene dos partes: un
catálogo público donde el cliente busca productos, arma un carrito y compra, y un panel de administración
protegido por PIN para gestionar el inventario y ver las ventas. Integra las cuatro unidades de la materia."

## 2. Demo en vivo — 4 min

| Paso | Qué mostrar | Qué decir (requisito) |
|---|---|---|
| 1 | Carrusel, categorías, buscar "usb", ordenar por precio | Componentes, routing con parámetros, búsqueda con LIKE, paginación (U1, U3) |
| 2 | Abrir un producto → características → "Agregar al carrito" | Modal 100% Blazor, validación de stock |
| 3 | Carrito: subir cantidad hasta el tope, F5 y el carrito sigue | State Service + ProtectedSessionStorage |
| 4 | Confirmar con email inválido → error → corregir → N.º de pedido | EditForm + Data Annotations (U1) |
| 5 | Pestaña admin: la venta aparece sola en *Ventas*; abrir el detalle | Notificación reactiva entre pestañas |
| 6 | *Productos*: editar, intentar stock 0 con "Disponible" | Validación cruzada `IValidatableObject` |
| 7 | Eliminar con el modal de confirmación | Modal sin JavaScript |
| 8 | Modo oscuro y "Copiar" | JS Interop con módulo ES6 (U2) |
| 9 | Achicar a 375 px | Responsive mobile-first (U2) |

## 3. Recorrido por el código — 3 min

1. **Un componente:** `Components/ProductoCard.razor` — parámetros `[Parameter]`, `EventCallback`, CSS
   Isolation (`ProductoCard.razor.css`) e import perezoso del módulo JS.
2. **El servicio de datos:** `Services/VentaService.cs` → `RegistrarVenta`: transacción, revalidación de
   stock con datos frescos, concurrencia optimista (`Version`), notificación a todas las pestañas.
3. **Una migración:** `Migrations/<fecha>_Inicial.cs` y `Data/TechMundoDbContext.cs` — relaciones
   Producto↔Categoría y Venta↔VentaItem, `DeleteBehavior`, seed con `HasData`.

## 4. Repositorio y CI/CD — 2 min

- Historial con Conventional Commits, ramas `main` / `develop` / `feature/*` y un Pull Request mergeado.
- Pestaña **Actions**: job `build` en cada push/PR y `deploy` solo desde `main`.
- Issues con etiquetas y el tablero Kanban (*Projects*).
- Docs: `docs/HISTORIAS-DE-USUARIO.md`, XML docs en `IProductoService` / `IVentaService`.

## 5. Reflexión — 1 min

Desafíos reales del proyecto (buenos para mostrar aprendizaje):
- **CSS Isolation que no se aplicaba:** faltaba `<link href="TechMundo.styles.css">` en `App.razor`;
  compilaba bien pero la página se veía sin estilos.
- **Fuga de memoria:** un componente con `IDisposable` e `IAsyncDisposable` a la vez — Blazor solo llama a
  `DisposeAsync`, así que nunca se desuscribía de los eventos.
- **SQLite y decimales:** no ordena ni suma `decimal` en SQL; se ordena con un cast a `double` y se suma en memoria.
- **Mobile:** un `margin: auto` en un contenedor flex hacía que el carrusel desbordara la página a 375 px.

## Preguntas probables

**¿Por qué Blazor Server y no WebAssembly?** La lógica y la base corren en el servidor, el cliente descarga
poco y no hace falta una API aparte. La contra es que necesita conexión permanente (SignalR / WebSockets).

**¿Por qué `DbContextFactory` y no inyectar el `DbContext`?** En Blazor Server un circuito vive mucho tiempo;
un `DbContext` compartido acumula entidades y no es thread-safe. La factory crea uno corto por operación.

**¿Qué pasa si dos clientes compran el último producto a la vez?** `RegistrarVenta` revalida el stock dentro
de una transacción; el segundo recibe "solo quedan X" y vuelve al carrito.

**¿Y si el admin edita un producto mientras alguien lo compra?** Concurrencia optimista: `Version` con
`[ConcurrencyCheck]`. El guardado del admin se rechaza con un aviso en vez de pisar el stock.

**¿El PIN es seguro?** No es autenticación real (no hay usuarios ni roles). Tiene comparación en tiempo
constante, bloqueo por intentos y el PIN no está en el código. Para producción haría falta ASP.NET Core Identity.

**¿Dónde usás JavaScript?** Solo en `wwwroot/js/interop.js`, un módulo ES6 importado con `IJSObjectReference`
y liberado en `DisposeAsync`: modo oscuro, portapapeles y notificaciones. Los modales y el carrusel son Blazor puro.

**¿Cómo se actualiza el admin cuando hay una venta en otra pestaña?** `CambiosCatalogo` es un Singleton con un
evento; cada pestaña se suscribe y refresca con `InvokeAsync(StateHasChanged)`.

**¿Por qué desactivaste el prerender?** Para no consultar la base dos veces por página y para poder leer el
`sessionStorage` (carrito y sesión del admin) antes de mostrar la pantalla.

**¿Cómo se aplican las migraciones en Azure?** `Database.Migrate()` al iniciar en `Program.cs`, después de crear
la carpeta de la base si no existe.

**¿Qué pasa con la hora de las ventas si el servidor está en otro país?** Azure corre en UTC; `Reloj.Ahora`
convierte a la zona `America/Argentina/Buenos_Aires`, así las fechas y los filtros usan la hora local.

**¿Los precios son reales?** Son precios de lista de productos reales convertidos a pesos al dólar oficial
(octubre 2026) y redondeados; son aproximados.
