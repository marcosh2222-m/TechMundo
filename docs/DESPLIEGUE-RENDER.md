# Despliegue en Render — TechMundo

Render publica la app gratis y **sin tarjeta de crédito** usando el `Dockerfile` del proyecto. GitHub Actions
compila, prueba la imagen Docker y, si todo pasa, le pide a Render que publique la nueva versión.
(La guía de Azure App Service sigue en [`DESPLIEGUE.md`](DESPLIEGUE.md) como alternativa.)

```
push a main → GitHub Actions: build + prueba de la imagen Docker → Deploy Hook → Render publica
```

## 1. Crear la cuenta

1. Entrá a [render.com](https://render.com) → **Get Started** → **GitHub** (registrarse con tu cuenta de GitHub).
2. Cuando Render pida acceso a GitHub, elegí **Only select repositories** → `TechMundo`.

## 2. Crear el Web Service

Dashboard → **+ New** → **Web Service** → elegí el repositorio **TechMundo** → **Connect**.

| Campo | Valor |
|---|---|
| Name | `techmundo` (define la URL: `https://techmundo-xxxx.onrender.com`) |
| Language | **Docker** |
| Branch | `main` |
| Region | **Virginia (US East)** u **Ohio (US East)** (las más cercanas) |
| Root Directory | *(vacío)* |
| Instance Type | **Free** |

**Environment Variables** → **Add Environment Variable**:
- `AdminPin` = el PIN que vas a usar en la defensa (no uses `2468`, que figura en el README público).

**Advanced**:
- **Health Check Path**: `/health`
- **Auto-Deploy**: **Off** (los deploys los dispara GitHub Actions cuando el build pasa).

**Create Web Service**. El primer deploy tarda 5–10 minutos (construye la imagen Docker). En la pestaña
**Logs** tiene que aparecer `Now listening on: http://[::]:10000` y el estado pasa a **Live**.

## 3. Conectar GitHub Actions (CI/CD)

1. En Render: el servicio → **Settings** → **Deploy Hook** → copiá la URL (es secreta: quien la tenga puede
   disparar deploys).
2. En GitHub: repo **TechMundo** → **Settings → Secrets and variables → Actions → New repository secret**:
   - Name: `RENDER_DEPLOY_HOOK_URL`
   - Secret: la URL copiada.

Desde ahí, cada push o merge a `main` corre el workflow **CI/CD**: `build` → `docker` (construye la imagen y
verifica `/health`) → `deploy-render` (llama al Deploy Hook). Render publica solo si todo lo anterior pasó.

## 4. Probar

- `https://techmundo-xxxx.onrender.com` → catálogo
- `https://techmundo-xxxx.onrender.com/health` → `Healthy`
- `/admin` → ingresar con el `AdminPin` configurado
- Copiá la URL en la sección *About* del repositorio de GitHub.

## Cosas a tener en cuenta (plan gratis)

| Situación | Qué pasa | Qué hacer |
|---|---|---|
| 15 minutos sin visitas | El servicio se duerme | Abrir la página ~2 minutos antes de la defensa (la primera carga tarda ~1 min) |
| Se duerme o se vuelve a publicar | La base SQLite vuelve a los datos de ejemplo (40 productos, 15 ventas) | Nada: la demo siempre arranca limpia. Las ventas de prueba no se conservan |
| Mientras se despierta, Blazor muestra "reconectando" | Normal en el primer acceso | Esperar o recargar la página |
| El deploy falla | Ver **Logs** del evento en Render | Revisar que `AdminPin` exista y que el job `docker` haya pasado en GitHub |
| El panel dice "falta configurar el PIN" | No se cargó `AdminPin` | Paso 2 (variables de entorno) y **Manual Deploy → Deploy latest commit** |

Límites del plan gratis: 512 MB de RAM, 750 horas de servicio activo por mes (alcanza para tener la app
publicada todo el mes con el servicio durmiendo cuando no se usa).
