# Proposal: Seguridad transversal

## Why
La auditoría del código encontró riesgos que no pertenecen a una sola capability:
- `GHL.WebApi` no tiene autenticación y recibe el `userId` en la URL (`/api/sessions/clock-in/{userId}`). Quien pueda alcanzar la API puede leer o modificar las jornadas de cualquier usuario.
- Las páginas de Blazor no tienen `[Authorize]`; la protección depende de que `MainLayout` redirija al login, lo cual es frágil: una página nueva fuera de ese layout quedaría pública.
- Varios componentes muestran `ex.Message` al usuario (Register, Settings, Home), lo que filtra detalles internos; otros (History, Statistics) solo escriben en consola y ocultan el error.
- `IWorkSessionRepository.GetByIdAsync` devuelve una sesión sin comprobar a quién pertenece.

## What Changes
- Exigir autenticación en todos los endpoints de `GHL.WebApi` y obtener la identidad del token, nunca de la URL.
- Definir una política de autorización por defecto en `GHL.BlazorApp` (usuario autenticado) y marcar de forma explícita las páginas y endpoints públicos.
- Reemplazar los mensajes con detalles internos por mensajes genéricos y registrar el detalle con `ILogger`.
- Hacer que el acceso a una sesión por id verifique el propietario.
- Crear el proyecto de pruebas y cubrir con pruebas el aislamiento de datos entre usuarios.

## Impact
- **Capabilities afectadas**: `seguridad` (nueva, ADDED, ver `specs/seguridad/spec.md`).
- **Decisión tomada por defecto**: ningún cliente usa hoy `GHL.WebApi`, así que este change la deja protegida y sin forma de obtener un token (emisión de tokens fuera de alcance). Mientras no exista un cliente, la API queda efectivamente cerrada. Si en cambio se decide retirarla, se reemplazan las tareas de la sección 1 por borrar el proyecto.
- **Fuera de alcance**: emisión de tokens y registro de clientes de la API.
- **Dependencia**: se recomienda aplicarlo después del change `001`, porque comparte archivos de `Program.cs` y `AccountController`.
- **Riesgo**: bajo a medio. Una política de autorización por defecto puede bloquear por error páginas que deben ser públicas; la tarea 2.3 lo verifica.
