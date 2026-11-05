# Instrucciones para GitHub Copilot

Aplicación web de control de horarios laborales: solución .NET `GHL` con Blazor (interactivo de servidor), MudBlazor, EF Core y SQL Server. Estas son las reglas esenciales; el detalle completo está en `AGENTS.md` y `openspec/config.yaml` (si algo contradice a `config.yaml`, gana `config.yaml`).

## Flujo de trabajo: todo pasa por OpenSpec
- No escribas código sin un change de OpenSpec aprobado en `openspec/changes/`. No hagas desarrollo improvisado.
- `openspec/specs/` es la fuente de verdad del comportamiento. No lo edites directamente: el comportamiento cambia mediante un change.
- Implementa solo lo que indica el `tasks.md` del change activo y marca cada tarea al terminarla. Si surge trabajo nuevo, detente y actualiza el change.
- Cada requirement usa SHALL o MUST y tiene al menos un scenario con WHEN y THEN.
- Valida con `openspec validate --all`.

## Arquitectura (solución en `src/`)
- Las dependencias apuntan hacia adentro: `GHL.Domain` no referencia otros proyectos (excepción: el paquete de Identity por `ApplicationUser`), `GHL.Application` solo depende de Domain, `GHL.Infrastructure` implementa las interfaces, y `GHL.WebApi` y `GHL.BlazorApp` consumen Application.
- La lógica de negocio vive en Domain o Application, nunca en componentes Blazor ni en controllers.
- Las entidades cambian de estado mediante métodos del dominio, no con setters públicos.

## Datos y seguridad
- Cada usuario solo accede a sus propios registros: toda consulta filtra por el usuario autenticado y la identidad sale del claim de la sesión, nunca de la URL ni del formulario.
- Los cambios de esquema se hacen con migraciones de EF Core.
- Timestamps en UTC; la conversión a hora local ocurre solo en la interfaz.
- Horas trabajadas = (salida - entrada) - total de pausas.
- Nunca guardes credenciales ni cadenas de conexión en el repositorio: usa User Secrets o variables de entorno.
- No muestres `ex.Message` al usuario: registra la excepción con `ILogger` y muestra un mensaje genérico.

## Código
- Specs, proposals, design y tasks en español; el código (variables, funciones, comentarios técnicos) en inglés.
- Early return en lugar de `if` anidados. Nunca silencies un error.
- Interfaz: sin valores mágicos (usa variables CSS semánticas), componentes con estados Loading, Error, Empty y texto largo.
