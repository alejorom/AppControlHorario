# AGENTS.md

Aplicación web de control de horarios laborales: registro de entradas y salidas, pausas, cálculo automático de horas trabajadas y reportes. Solución .NET `GHL` con Blazor, EF Core y SQL Server.

## Flujo de trabajo (OpenSpec)

Todo el desarrollo de este proyecto es spec-driven. No se hace desarrollo improvisado ("vibe coding"): ningún código se escribe sin un change de OpenSpec aprobado.

- `openspec/specs/` es la fuente de verdad del comportamiento del sistema. No cambies el comportamiento editando un spec directamente: crea un change en `openspec/changes/`.
- Antes de escribir cualquier código (funcionalidades, correcciones y refactors), comprueba que exista un change que lo cubra. Si no existe, propón uno con `/opsx-propose` (Claude Code: `/opsx:propose`; Codex: `$openspec-propose`) y detente hasta que se apruebe.
- Si un cambio no altera comportamiento (refactor, tooling), el change usa `skip_specs: true`, pero igual pasa por proposal y tasks.
- Implementa solo lo que indica el `tasks.md` del change activo y marca cada tarea al terminarla. Si surge trabajo nuevo, detente y actualiza el change antes de continuar.
- No implementes comportamiento que no esté cubierto por un requirement y un scenario.
- Las reglas y el contexto detallados del proyecto están en `openspec/config.yaml`. Si algo aquí contradice ese archivo, gana `config.yaml`.

## Arquitectura

Solución en `src/` con Clean Architecture. Las dependencias apuntan hacia adentro:

- `GHL.Domain`: entidades e interfaces del dominio. No referencia ningún otro proyecto. Excepción documentada: referencia `Microsoft.Extensions.Identity.Stores` porque `ApplicationUser` hereda de `IdentityUser`. No agregar otras dependencias.
- `GHL.Application`: casos de uso, DTOs, interfaces y servicios. Solo depende de Domain.
- `GHL.Infrastructure`: EF Core, repositorios, migraciones. Implementa las interfaces de Domain y Application.
- `GHL.WebApi` y `GHL.BlazorApp`: capas de presentación. Consumen Application.

La lógica de negocio (cálculo de horas, reglas de jornada y pausas) vive en Domain o Application, nunca en componentes Blazor ni en controllers.

## Principios de código

Resumen de `.agent/rules/buenas-practicas-desarrollo.md` (la versión completa, que lee Antigravity), para que apliquen con cualquier asistente. Se aplican siempre dentro de un change de OpenSpec; no sustituyen ni relajan el flujo spec-first.

- Separación de responsabilidades: la interfaz solo muestra datos; la lógica de negocio no sabe cómo se muestra.
- Las dependencias externas van detrás de una interfaz; su implementación vive en Infrastructure.
- Inmutabilidad por defecto en DTOs, value objects y modelos de vista (usa `record`). Las entidades de EF Core solo cambian de estado mediante métodos del dominio (por ejemplo `ClockOut()`), no con setters públicos.
- Antes de borrar o refactorizar código que no creaste, explica por qué existía.
- Nombres descriptivos; comentarios solo para lógica de negocio compleja o decisiones no obvias.
- Cambios atómicos: cada cambio compila y funciona, sin TODOs que rompan la compilación.
- Interfaz: sin valores mágicos; colores y espaciados con variables CSS semánticas (por ejemplo `var(--color-danger)`). Extrae a un componente lo que se repita o supere unas 20 líneas de marcado. Cada componente maneja los estados Loading, Error, Empty y texto largo.
- Early return en lugar de `if` anidados. Nunca silencies un error: propágalo hasta una capa que informe al usuario.
- Antes de entregar, comprueba que el cambio respeta las capas y los tokens de diseño.

## Datos y seguridad

- Base de datos: SQL Server con EF Core. Todo cambio de esquema se hace con una migración de EF Core.
- Autenticación: ASP.NET Core Identity.
- Cada usuario solo accede a sus propios registros. Toda consulta de jornadas, pausas y perfiles filtra por el usuario autenticado en la capa Application. Nunca confíes en que la interfaz oculte datos de otros usuarios.
- Todos los timestamps se guardan en UTC. La conversión a hora local ocurre solo en la interfaz.
- Las horas trabajadas se calculan siempre como (salida - entrada) - total de pausas.
- Nunca guardes credenciales ni cadenas de conexión en el repositorio: usa User Secrets o variables de entorno.

## Comandos

- Compilar: `dotnet build`
- Pruebas: `dotnet test` (proyectos en `tests/`)
- Migraciones: `dotnet ef`, con `GHL.Infrastructure` como proyecto de migraciones.
- Validar specs de OpenSpec: `openspec validate --all`

## Convenciones

- Specs, proposals, design y tasks se escriben en español. El código (variables, funciones, comentarios técnicos) en inglés.
- La interfaz debe ser responsive (320px a 2560px) y cumplir WCAG 2.1 AA.

## Fuera de alcance por ahora

Vista de administrador o equipos, modo offline/PWA, integraciones con nómina o RRHH, notificaciones push.
