# Despliegue en Azure App Service — TechMundo

Unidad 4, Clases 22 y 23. La defensa exige la app **publicada en una URL pública** antes de empezar
(no se aceptan demos en localhost). Esta guía usa el plan gratuito **F1** de Azure App Service y el
workflow `.github/workflows/deploy.yml`, que ya está listo.

## 0. Probar la publicación en tu PC (Clase 22, laboratorio 17)

```powershell
dotnet publish -c Release -o ./publish
dir publish          # TechMundo.dll, wwwroot/, appsettings*.json, TechMundo.xml (XML docs)
```

## 1. Cuenta de Azure

- Con email de la institución podés probar **Azure for Students** (crédito gratuito, sin tarjeta).
- Si no, la cuenta gratuita común pide una tarjeta para verificar identidad; el plan F1 no tiene costo.

## 2. Crear el App Service

Portal de Azure → **App Services** → **Crear** → **Aplicación web**:

| Campo | Valor |
|---|---|
| Grupo de recursos | Nuevo: `rg-techmundo` |
| Nombre | `techmundo-tunombre` (tiene que ser único en todo Azure) |
| Publicar | Código |
| Pila del entorno de ejecución | **.NET 8 (LTS)** |
| Sistema operativo | **Linux** |
| Región | Brazil South (la más cercana) |
| Plan de precios | **Gratis F1** |

Crear → esperar → **Ir al recurso**.

## 3. Configurar la aplicación (imprescindible para Blazor Server)

En el App Service → **Configuración**:

1. **Configuración general**
   - **Sockets web: Activado** ← sin esto Blazor Server se reconecta en bucle o queda en blanco.
   - **Afinidad ARR: Activado** (viene así por defecto).
   - **Credenciales de publicación de autenticación básica de SCM: Activado** ← sin esto no se puede
     descargar el perfil de publicación y el deploy de GitHub Actions falla con 401/403.
   - Guardar.
2. **Variables de entorno** → *Configuración de la aplicación* → **Agregar**:
   - `AdminPin` = el PIN que vas a usar en la defensa (no lo escribas en el repo).
   - `ASPNETCORE_ENVIRONMENT` = `Production` (es el valor por defecto; opcional).
   - Aplicar → Confirmar (la app se reinicia).

Las fechas de las ventas se guardan en hora argentina aunque el servidor de Azure esté en UTC (ver
`Services/Reloj.cs`), así que no hace falta configurar la zona horaria.

La base SQLite se guarda en `/home/data/techmundo.db` (ver `appsettings.Production.json`). La carpeta
`/home` de App Service es persistente: los datos sobreviven a reinicios y a nuevos deploys. La app crea
la carpeta y aplica las migraciones sola al iniciar.

## 4. Conectar GitHub Actions

1. App Service → **Información general** → **Descargar perfil de publicación** (archivo `.PublishSettings`).
2. GitHub → tu repo → **Settings → Secrets and variables → Actions**:
   - **Secrets** → *New repository secret* → nombre `AZURE_WEBAPP_PUBLISH_PROFILE` → pegar **todo** el
     contenido del archivo descargado.
   - **Variables** → *New repository variable* → nombre `AZURE_WEBAPP_NAME` → el nombre que elegiste en
     el paso 2 (si no la creás, el workflow usa `techmundo-blazor`).
3. Borrá el archivo `.PublishSettings` de la PC (contiene credenciales).

## 5. Desplegar

- Mergeá `develop` → `main` (o hacé push a `main`). En la pestaña **Actions** vas a ver el workflow
  **CI/CD** con dos jobs: `build` y `deploy`. Cuando los dos estén en verde, la app está publicada.
- Probá:
  - `https://techmundo-tunombre.azurewebsites.net` → catálogo
  - `https://techmundo-tunombre.azurewebsites.net/health` → `Healthy`
  - `/admin` → ingresá con el `AdminPin` configurado
- Opcional: App Service → **Supervisión → Comprobación de estado** → ruta `/health`.
- Copiá la URL en la sección *About* del repo (el badge del README ya apunta a `marcosh2222-m/TechMundo`).

## Problemas frecuentes

| Síntoma | Causa probable | Solución |
|---|---|---|
| Página en blanco o "Intentando reconectar…" en bucle | Sockets web desactivados | Paso 3.1 |
| El deploy falla con 401 / 403 / *publish profile invalid* | Autenticación básica de SCM desactivada o perfil viejo | Activarla (paso 3.1) y volver a descargar el perfil |
| Error 500.30 al abrir la app | La app no pudo arrancar | **Supervisión → Secuencia de registro** (Log stream) y leer el error |
| "El panel está deshabilitado: falta configurar el PIN" | No se cargó `AdminPin` | Paso 3.2 |
| La app no arranca: "El proyecto no tiene migraciones" | No se subió la carpeta `Migrations/` | `dotnet ef migrations add Inicial`, commitear `Migrations/` y volver a desplegar |
| `/health` responde *Unhealthy* | La base no tiene tablas o no se puede abrir | Revisar el Log stream; verificar la cadena de conexión |
| La primera carga tarda 20–30 s | El plan F1 "duerme" la app tras ~20 min sin uso | Abrirla 5 minutos antes de la defensa |
| Se agotó la cuota del día | F1 da 60 min de CPU por día | Esperar al día siguiente; evitar dejar pestañas abiertas con autoplay |

> `/home` es almacenamiento de red: para una demo con una sola instancia (F1) SQLite funciona bien, pero
> con mucho tráfico simultáneo podría aparecer "database is locked". En producción real se usaría
> Azure SQL Database cambiando el proveedor de EF Core.

## Reiniciar los datos de la demo

Las ventas que hagas en las pruebas quedan guardadas. Para volver al seed original:
App Service → **Herramientas avanzadas (Kudu)** → *Debug console* → borrar `/home/data/techmundo.db`
→ reiniciar la app (se recrea con los 40 productos y las 15 ventas de prueba).
