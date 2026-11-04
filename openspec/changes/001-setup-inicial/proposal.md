# Proposal: Setup Inicial y Autenticación

## Why
El proyecto parte de cero (greenfield). Antes de construir el registro de jornadas, los cálculos y los reportes se necesita la base técnica: proyecto configurado, conexión a SQL Server y el flujo de autenticación funcionando, ya que todas las demás capabilities dependen de tener un usuario autenticado.

## What Changes
- Verificar la solución .NET existente (GHL.Domain, GHL.Application, GHL.Infrastructure, GHL.WebApi, GHL.BlazorApp) y sus referencias entre proyectos
- Configurar la conexión a SQL Server desde GHL.Infrastructure con EF Core
- Crear el esquema inicial de base de datos: `profiles`, `work_sessions`, `breaks`, como entidades de dominio y migración de EF Core, junto con las tablas de ASP.NET Core Identity
- Configurar el filtrado por usuario autenticado en la capa Application
- Configurar ASP.NET Core Identity y el flujo de sesión en GHL.WebApi y GHL.BlazorApp (el mecanismo de sesión, cookie o JWT, se define en `design.md`)
- Implementar la capability `autenticacion` según su spec vigente: registro, inicio de sesión, recuperación de contraseña y cierre de sesión
- Configurar navegación básica y layout (shell vacío del dashboard)

## Impact
- **Specs de referencia**: `specs/autenticacion/spec.md`. Ya describe el comportamiento objetivo, por lo que este change lo implementa sin modificarlo y no incluye delta specs (`skip_specs: true`).
- **Fuera de alcance**: registro de jornadas, cálculos, reportes y perfil (changes posteriores: `002-registro-jornadas`, `003-calculos-y-reportes`, etc.)
- **Riesgo**: bajo. No hay código previo que modificar.
