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
