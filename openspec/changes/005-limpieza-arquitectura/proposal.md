# Proposal: Limpieza de arquitectura y repositorio

## Why
La auditoría encontró desviaciones respecto a las reglas del proyecto (`AGENTS.md` y `config.yaml`) que no cambian el comportamiento visible:
- `Settings.razor` borra los registros del usuario usando el `DbContext` directamente desde la página, saltándose las capas Application e Infrastructure.
- Identity está configurado dos veces: `AddIdentityCore` en `GHL.Infrastructure` y `AddIdentity` en `GHL.BlazorApp/Program.cs`, con la política de contraseña repetida.
- `GHL.Domain` depende del paquete de Identity porque `ApplicationUser` hereda de `IdentityUser`, lo que contradice la regla "Domain sin dependencias".
- `NavMenu.razor` y su CSS son restos de la plantilla de Blazor (enlaces a `counter` y `weather`) que ya no se usan.
- `Settings.razor` muestra preferencias (horas por día y por semana, notificaciones) que no se guardan en ningún sitio.
- Copilot en Visual Studio no lee `AGENTS.md`, por lo que no recibe las reglas del proyecto.
- La directiva principal de `.agent/rules/buenas-practicas-desarrollo.md` prioriza la velocidad ("Vibe") y contradice el flujo spec-first.
- `GHL.BlazorApp` usa `Server=.` (SQL Server por defecto de un equipo concreto) y `GHL.WebApi` usa LocalDB, por lo que apuntan a bases distintas; además, la regla de no guardar cadenas de conexión en el repositorio no se cumple.
- Con `Server=.` la app falla en desarrollo con un error de certificado de SQL Server ("La cadena de certificación fue emitida por una entidad en la que no se confía"), porque la conexión va cifrada por defecto y el certificado local no es de confianza.
- El repositorio es público y no tiene `README.md`.
- El SRS y `diseño_ux.png` están dentro de `openspec/specs/`, que debe contener solo las capabilities; el SRS todavía describe Supabase y React.

## What Changes
- Mover el borrado de registros a un método de `IWorkSessionService` y su repositorio.
- Consolidar el registro de Identity en un solo lugar y eliminar la política de contraseña duplicada.
- Documentar en `AGENTS.md` la excepción de `ApplicationUser` en Domain, en lugar de reestructurar.
- Eliminar `NavMenu.razor` y su CSS.
- Ocultar las preferencias de Settings que no se guardan, hasta que un change propio las especifique.
- Mover el SRS y la imagen de diseño a `docs/` y marcar el SRS como histórico.
- Unificar la cadena de conexión (genérica en `appsettings.json`, la propia de cada equipo en User Secrets) y reformular la regla de credenciales en `AGENTS.md`, `config.yaml` y `copilot-instructions.md`.
- Crear un `README.md` con los pasos para configurar, ejecutar y trabajar con OpenSpec.
- Crear `.github/copilot-instructions.md` y alinear la directiva principal de la guía de buenas prácticas de Antigravity con el flujo spec-first. Las carpetas de Antigravity se conservan.

## Impact
- **Specs**: ninguna capability cambia (`skip_specs: true`); es un change de refactor y orden.
- **Decisión por defecto**: se ocultan las preferencias no persistidas en lugar de implementarlas. Si se quieren, requieren un change nuevo con su spec (el spec `perfil-usuario` hoy no las menciona).
- **Dependencia**: conviene aplicarlo al final; la tarea 2 toca el registro de Identity que los changes `001` y `002` también modifican.
- **Riesgo**: bajo. Los cambios son de estructura y los cubre el proyecto de pruebas creado en el change `002`.
