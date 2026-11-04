# Tasks: Setup Inicial y Autenticación

## 1. Configuración de proyecto
- [ ] 1.1 Inicializar repositorio Git
- [ ] 1.2 Verificar las referencias entre proyectos de la solución GHL (Domain sin dependencias; Application depende de Domain; Infrastructure implementa interfaces; WebApi y BlazorApp consumen Application)
- [ ] 1.3 Instalar paquetes: `Microsoft.EntityFrameworkCore.SqlServer` y ASP.NET Core Identity (EF Core) en GHL.Infrastructure
- [ ] 1.4 Registrar servicios en `DependencyInjection.cs` de Application e Infrastructure y configurar la cadena de conexión a SQL Server fuera del código (User Secrets / variables de entorno)

## 2. Base de datos (SQL Server)
- [ ] 2.1 Definir la instancia de SQL Server para desarrollo (local o LocalDB) y probar la conexión
- [ ] 2.2 Crear entidad `Profile` en GHL.Domain y su tabla `Profiles` (Id, UserId, FullName, AvatarUrl, CreatedAt, UpdatedAt)
- [ ] 2.3 Crear entidad `WorkSession` y su tabla `WorkSessions` (Id, UserId, ClockIn, ClockOut, TotalWorkedMinutes, TotalBreakMinutes)
- [ ] 2.4 Crear entidad `Break` y su tabla `Breaks` (Id, WorkSessionId, StartTime, EndTime, DurationMinutes)
- [ ] 2.5 Crear el `DbContext` (basado en `IdentityDbContext`) y la migración inicial de EF Core en GHL.Infrastructure, con timestamps en UTC
- [ ] 2.6 Definir en Application el acceso al usuario autenticado y aplicar global query filters para que los repositorios filtren siempre por su id
- [ ] 2.7 Implementar el envío de correos (verificación y recuperación de contraseña) mediante `IEmailSender` y un proveedor SMTP configurable

## 3. Autenticación (según specs/autenticacion/spec.md)
- [ ] 3.1 Componente Blazor y servicio de Application para registro
- [ ] 3.2 Componente Blazor y servicio de Application para login
- [ ] 3.3 Flujo de recuperación de contraseña
- [ ] 3.4 Botón y lógica de logout
- [ ] 3.5 Configurar ASP.NET Core Identity y el mecanismo de sesión (según `design.md`) en GHL.WebApi y GHL.BlazorApp, y proteger rutas en Blazor (`[Authorize]` / `AuthorizeRouteView`)
- [ ] 3.6 Verificar los 3 scenarios de "Registro de Usuario"
- [ ] 3.7 Verificar los 3 scenarios de "Inicio de Sesión"
- [ ] 3.8 Verificar los 2 scenarios de "Recuperación de Contraseña"
- [ ] 3.9 Verificar el scenario de "Cierre de Sesión"

## 4. Shell de aplicación
- [ ] 4.1 Layout general de Blazor (navbar y navegación)
- [ ] 4.2 Página de dashboard vacía (placeholder para el siguiente change)
