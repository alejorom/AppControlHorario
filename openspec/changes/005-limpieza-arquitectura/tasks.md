# Tasks: Limpieza de arquitectura y repositorio

## 1. Borrado de registros por las capas
- [ ] 1.1 Agregar `DeleteAllSessionsAsync(userId)` a `IWorkSessionRepository` y `IWorkSessionService` y su implementación, filtrando por usuario
- [ ] 1.2 Usarlo en `Settings.razor` y quitar la inyección de `IServiceScopeFactory` y el uso directo de `GHLDbContext`
- [ ] 1.3 Escribir una prueba que verifica que solo se borran las jornadas del usuario indicado

## 2. Identity en un solo lugar
- [ ] 2.1 Decidir dónde vive el registro de Identity (se propone `GHL.BlazorApp/Program.cs`, que es quien necesita cookies y `SignInManager`)
- [ ] 2.2 Quitar `AddIdentityCore` y la política de contraseña duplicada de `GHL.Infrastructure/DependencyInjection.cs`
- [ ] 2.3 Comprobar que `GHL.WebApi` compila y arranca sin el registro de Identity de Infrastructure
- [ ] 2.4 Verificar que el registro, el login y el cambio de contraseña siguen funcionando

## 3. Excepción documentada de Domain
- [ ] 3.1 Dejar anotado en `AGENTS.md` que `GHL.Domain` referencia `Microsoft.Extensions.Identity.Stores` porque `ApplicationUser` hereda de `IdentityUser`
- [ ] 3.2 Confirmar que Domain no referencia ningún otro paquete ni proyecto

## 4. Restos y orden del repositorio
- [ ] 4.1 Eliminar `NavMenu.razor` y `NavMenu.razor.css` tras comprobar que nada los usa
- [ ] 4.2 Ocultar en `Settings.razor` las preferencias de horas y notificaciones que no se guardan
- [ ] 4.3 Crear la carpeta `docs/` y mover a ella el SRS y `diseño_ux.png` desde `openspec/specs/`
- [ ] 4.4 Agregar al inicio del SRS una nota que indique que es histórico y que la fuente de verdad es `openspec/specs/`
- [ ] 4.5 Ejecutar `openspec validate --all` y comprobar que sigue pasando

## 5. Herramientas de asistente
- [ ] 5.1 Crear `.github/copilot-instructions.md` con las reglas esenciales de `AGENTS.md` (Copilot en Visual Studio no lee `AGENTS.md`)
- [ ] 5.2 Cambiar la directiva principal de `.agent/rules/buenas-practicas-desarrollo.md` para que exija trabajar con OpenSpec en lugar de maximizar la velocidad ("Vibe"), manteniendo el archivo donde Antigravity lo lee
- [ ] 5.3 Mantener `.agent/` y `.agents/` (Antigravity) y comprobar que `.github/` y `.claude/` conservan sus propios skills y prompts
- [ ] 5.4 Verificar que los comandos de OpenSpec funcionan en las herramientas que se usan (VS Code con Copilot o Claude Code; en Visual Studio, la CLI `openspec`)

## 6. Cadena de conexión
- [ ] 6.1 Dejar en `appsettings.json` de `GHL.BlazorApp` y de `GHL.WebApi` la misma cadena genérica de LocalDB, sin credenciales
- [ ] 6.2 Mover a User Secrets la cadena propia del equipo (hoy `Server=.` en `GHL.BlazorApp`) con `dotnet user-secrets`
- [ ] 6.3 Comprobar que ambos proyectos arrancan y se conectan a la misma base de datos
- [ ] 6.4 Verificar que ningún `appsettings*.json` contiene credenciales ni nombres de servidor propios de un equipo

## 7. README
- [ ] 7.1 Crear `README.md` en la raíz con descripción, stack, requisitos (SDK de .NET 8 y SQL Server o LocalDB) y estructura de la solución
- [ ] 7.2 Documentar la configuración de User Secrets y la aplicación de migraciones (`dotnet ef database update --project src/GHL.Infrastructure --startup-project src/GHL.BlazorApp`)
- [ ] 7.3 Documentar cómo ejecutar la app, el flujo de OpenSpec (`openspec validate --all` y los comandos `/opsx-*`) y el enlace a `AGENTS.md`
- [ ] 7.4 Seguir los pasos del README desde cero en una carpeta limpia para comprobar que funcionan
