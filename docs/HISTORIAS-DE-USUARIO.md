# Historias de usuario y Product Backlog — TechMundo

Unidad 4, Clase 20 (Metodologías ágiles). Formato: *Como [usuario], quiero [acción] para [beneficio]*.
Estimación en puntos de historia con Planning Poker (escala Fibonacci: 1, 2, 3, 5, 8, 13).

## Roles del proyecto

| Rol Scrum | Quién |
|---|---|
| Product Owner | Marcos (define prioridades de la tienda) |
| Scrum Master | Marcos (proyecto individual: cumple ambos roles) |
| Development Team | Marcos |

## Definition of Done (aplica a todas las historias)

- La funcionalidad está implementada y probada manualmente en escritorio y en celular (375 px).
- El código está en una rama `feature/*` y se integró con un Pull Request revisado.
- El build de GitHub Actions está en verde.
- No hay errores en la consola del navegador.
- Si cambió el modelo, hay una migración nueva (no se borran las anteriores).
- README y CHANGELOG actualizados si corresponde.

---

## Las 5 historias del Sprint

### HU-01 · Encontrar productos en el catálogo — 3 puntos

**Como** cliente de TechMundo,
**quiero** filtrar por categoría, buscar por texto y ordenar por precio,
**para** encontrar rápido el producto que necesito.

Criterios de aceptación:
- Hay una barra lateral con las 5 categorías y la opción "Todas".
- La búsqueda encuentra coincidencias en nombre, marca o descripción, sin distinguir mayúsculas ("usb" encuentra "USB").
- "Ordenar por" ofrece Destacados (orden original), Menor precio y Mayor precio.
- Los filtros se aplican sin recargar la página y la paginación vuelve a la página 1.
- Solo se muestran productos disponibles para la venta.

### HU-02 · Armar un carrito de compras — 5 puntos

**Como** cliente,
**quiero** agregar productos a un carrito y ajustar las cantidades,
**para** comprar varios productos juntos.

Criterios de aceptación:
- Desde el detalle del producto puedo "Agregar al carrito" y veo una confirmación.
- No puedo agregar más unidades que el stock disponible; el sistema me dice cuántas quedan.
- El carrito se abre en una ventana modal sin salir de la página y muestra productos, cantidades, subtotales y total.
- El ícono del header muestra la cantidad de unidades.
- Si recargo la página, el carrito se conserva.

### HU-03 · Confirmar la compra — 5 puntos

**Como** cliente,
**quiero** confirmar mi compra ingresando mis datos,
**para** recibir mi pedido.

Criterios de aceptación:
- Se piden nombre (2 a 80 caracteres) y email válido, con mensajes de error claros.
- Al confirmar, el sistema vuelve a validar el stock contra la base; si algo se agotó, me avisa y vuelvo al carrito.
- Si todo está bien, se descuenta el stock, se registra la venta y veo el número de pedido.
- Un producto que queda en stock 0 pasa a "no disponible" automáticamente.

### HU-04 · Gestionar el catálogo — 5 puntos

**Como** administrador,
**quiero** dar de alta, editar, deshabilitar y eliminar productos,
**para** mantener el catálogo actualizado.

Criterios de aceptación:
- El panel pide un PIN y bloquea 30 segundos después de 5 intentos fallidos.
- El formulario valida todos los campos y no permite "Disponible" con stock 0.
- Eliminar pide confirmación en un modal (sin JavaScript).
- Si el producto cambió mientras lo editaba (por ejemplo, por una venta), se me avisa en vez de pisar los datos.
- Las estadísticas del panel (productos, valor del inventario, stock bajo) se actualizan solas.

### HU-05 · Consultar el historial de ventas — 3 puntos

**Como** administrador,
**quiero** ver las ventas realizadas con filtros,
**para** controlar los ingresos de la tienda.

Criterios de aceptación:
- Lista paginada con número, fecha, cliente, unidades y total.
- Filtros por rango de fechas, búsqueda por cliente / email / N.º de venta y orden por fecha o total.
- Un resumen muestra cantidad de ventas, unidades e ingresos del filtro aplicado.
- Al hacer clic en una venta veo el detalle de productos en un modal.
- Una venta hecha en otra pestaña aparece sin recargar.

**Total del Sprint 1: 21 puntos.**

---

## Product Backlog completo

Estado al 20/10/2026 (versión 0.11.0). Prioridad: Alta / Media / Baja.

| ID | Historia (resumida) | Pts | Prioridad | Estado |
|---|---|---|---|---|
| HU-01 | Como cliente, quiero filtrar, buscar y ordenar el catálogo | 3 | Alta | ✅ Done |
| HU-02 | Como cliente, quiero armar un carrito con validación de stock | 5 | Alta | ✅ Done |
| HU-03 | Como cliente, quiero confirmar la compra con mis datos | 5 | Alta | ✅ Done |
| HU-04 | Como admin, quiero gestionar los productos (CRUD) | 5 | Alta | ✅ Done |
| HU-05 | Como admin, quiero ver el historial de ventas con filtros | 3 | Alta | ✅ Done |
| HU-06 | Como cliente, quiero ver el detalle y las características de un producto | 2 | Alta | ✅ Done |
| HU-07 | Como cliente, quiero ver productos destacados en un carrusel | 3 | Media | ✅ Done |
| HU-08 | Como usuario, quiero usar el sitio en modo oscuro y que se recuerde mi elección | 2 | Media | ✅ Done |
| HU-09 | Como usuario, quiero usar el sitio cómodamente desde el celular | 3 | Alta | ✅ Done |
| HU-10 | Como admin, quiero recibir una alerta de productos con stock bajo | 2 | Media | ✅ Done |
| HU-11 | Como admin, quiero copiar el nombre de un producto con un clic | 1 | Baja | ✅ Done |
| HU-12 | Como admin, quiero que el panel no se cierre al recargar la página | 2 | Media | ✅ Done |
| HU-13 | Como dueño, quiero la app publicada en una URL pública | 5 | Alta | 🔄 En progreso |
| HU-14 | Como admin, quiero crear y editar categorías desde el panel | 3 | Baja | 📋 Backlog |
| HU-15 | Como admin, quiero anular una venta y devolver el stock | 5 | Baja | 📋 Backlog |
| HU-16 | Como admin, quiero ver los productos más vendidos en el dashboard | 3 | Baja | 📋 Backlog |
| HU-17 | Como cliente, quiero que la búsqueda ignore los acentos ("camara" → "cámara") | 3 | Baja | 📋 Backlog |
| HU-18 | Como desarrollador, quiero tests automáticos del carrito y las ventas | 5 | Media | 📋 Backlog |

## Daily Standup (ejemplo)

- **¿Qué hice ayer?** Corregí los errores críticos de la revisión de las unidades 1 a 3.
- **¿Qué haré hoy?** Documentación de la Unidad 4 y preparación del despliegue en Azure.
- **¿Hay impedimentos?** Necesito una cuenta de Azure y no trabajo en mi propia PC.
