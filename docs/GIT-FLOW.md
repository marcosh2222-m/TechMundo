# Git Flow, commits y GitHub — TechMundo

Unidad 4, Clase 19. Guía para subir el proyecto con un historial limpio, trabajar con ramas y
Pull Requests, y organizar el trabajo con issues y un tablero Kanban.

> **Estado:** el repositorio ya está creado y subido en
> [github.com/marcosh2222-m/TechMundo](https://github.com/marcosh2222-m/TechMundo), con las ramas `main` y
> `develop` y los 5 issues iniciales (#1 a #5). Queda pendiente el laboratorio del Pull Request (paso 5). La sección "Primer push" queda como referencia del proceso; para trabajar
> desde otra PC alcanza con `git clone https://github.com/marcosh2222-m/TechMundo.git`.

## Ramas

| Rama | Para qué |
|---|---|
| `main` | Producción. Cada push a `main` despliega en Azure (GitHub Actions). |
| `develop` | Integración. Acá se juntan las funcionalidades antes de pasar a `main`. |
| `feature/<nombre>` | Una funcionalidad o mejora. Sale de `develop` y vuelve por Pull Request. |
| `hotfix/<nombre>` | Corrección urgente de producción. Sale de `main` y vuelve a `main` y `develop`. |

Flujo: `feature/*` → PR → `develop` → PR → `main` → deploy automático.

## Conventional Commits

```
feat:     nueva funcionalidad            feat(carrito): validar stock al agregar
fix:      corrección de un bug           fix(catalogo): desborde horizontal en celulares
docs:     solo documentación             docs: historias de usuario del Sprint 1
style:    formato, sin cambio de lógica
refactor: reestructura sin fix ni feat   refactor(ui): componente Modal reutilizable
test:     tests
chore:    dependencias, build, config    chore: actualizar EF Core a 8.0.10
ci:       pipelines                      ci: separar build y deploy en el workflow
```

Commits **atómicos**: cada commit hace una sola cosa y su mensaje explica qué y por qué.

## Primer push trabajando en una PC que no es tuya

Objetivo: subir el proyecto **sin dejar el repositorio ni tus credenciales** en esa PC.

1. En GitHub creá el repositorio **público** `techmundo` vacío (sin README ni .gitignore: ya vienen en el proyecto).
2. Creá un **token de acceso personal** (GitHub → Settings → Developer settings → Fine-grained tokens) con
   permiso *Contents: Read and write* sobre ese repo y vencimiento de 7 días. Se usa como contraseña.
3. En Git Bash, desde la carpeta del proyecto (descomprimido en el Escritorio, por ejemplo):

```bash
cd "/c/Users/Usuario 14/Desktop/TechMundo"
git init -b main
git config --local user.name  "Tu Nombre"
git config --local user.email "tu-email@ejemplo.com"
git config --local credential.helper ""      # no guardar la contraseña/token en esta PC
git remote add origin https://github.com/marcosh2222-m/TechMundo.git
```

4. Antes de commitear, asegurate de haber generado la migración final (carpeta `Migrations/`, ver README).
   Después, commits por partes (historial legible, en vez de un único "subo todo"):

```bash
git add .gitignore TechMundo.csproj Program.cs App.razor Routes.razor _Imports.razor appsettings*.json Properties
git commit -m "chore: estructura inicial del proyecto Blazor Server (.NET 8)"

git add Models Data Migrations
git commit -m "feat(data): modelos, DbContext con relaciones, seed y migración inicial"

git add Services
git commit -m "feat(services): servicios de productos, ventas, carrito y acceso de admin"

git add Shared Components wwwroot
git commit -m "feat(ui): layouts, componentes reutilizables y estilos"

git add Pages
git commit -m "feat: catálogo público, panel de administración e historial de ventas"

git add README.md CHANGELOG.md ROADMAP.md
git commit -m "docs: README, CHANGELOG y roadmap del proyecto"

git add .github/workflows
git commit -m "ci: workflow de build y deploy a Azure App Service"

git push -u origin main      # usuario: tu usuario de GitHub · contraseña: el token
```

5. Rama `develop` y un Pull Request real (laboratorio de la Clase 19):

```bash
git switch -c develop
git push -u origin develop

git switch -c feature/documentacion-unidad-4
git add docs .github/ISSUE_TEMPLATE .github/PULL_REQUEST_TEMPLATE.md
git commit -m "docs: historias de usuario, Git Flow, guía de despliegue y defensa"
git push -u origin feature/documentacion-unidad-4
```

   En GitHub: **Compare & pull request** → base `develop` → completá la plantilla → comentá una línea
   en la pestaña *Files changed* → **Merge** → borrá la rama. Después abrí otro PR de `develop` a `main`
   y mergealo: eso dispara el deploy.

6. **Al terminar, limpiar la PC:**

```bash
cd ..
rm -rf "TechMundo/.git"     # o borrar la carpeta entera si ya no la necesitás
```

   Y por las dudas, en Windows: *Administrador de credenciales → Credenciales de Windows* → borrar
   cualquier entrada `git:https://github.com`. Cuando termines la materia, revocá el token en GitHub.

> Alternativa sin instalar nada: abrí el repo en GitHub y apretá la tecla `.` (punto) para usar
> **github.dev**, un editor en el navegador que permite crear ramas, editar y commitear.

## Issues y tablero Kanban

1. GitHub → *Projects* → **New project** → plantilla *Board* → columnas **Backlog / In Progress / Review / Done**.
2. Creá estos 5 issues (Clase 19) usando las plantillas de `.github/ISSUE_TEMPLATE` y asignalos al tablero:

| Issue | Etiqueta | Estado sugerido |
|---|---|---|
| Desplegar la app en Azure App Service (HU-13) | `enhancement` | In Progress |
| La búsqueda no ignora acentos: "camara" no encuentra "cámara" (HU-17) | `bug` | Backlog |
| Anular una venta y devolver el stock (HU-15) | `enhancement` | Backlog |
| Agregar capturas de pantalla al README | `documentation` | Backlog |
| Tests automáticos de CarritoState y VentaService (HU-18) | `enhancement` | Backlog |

3. Límite de trabajo en progreso (WIP): **1** tarjeta en *In Progress* (proyecto individual).

## Revisión de código (qué mirar en un PR)

- ¿El cambio hace lo que dice la historia y cumple sus criterios de aceptación?
- ¿Hay código duplicado o nombres poco claros?
- ¿Se manejan los errores (try/catch, mensajes en la UI)?
- ¿Los componentes que se suscriben a eventos se desuscriben en `Dispose`/`DisposeAsync`?
- ¿Se agregó una migración si cambió el modelo?
- Feedback constructivo: proponer, no ordenar ("¿Qué te parece extraer esto a un componente?").
