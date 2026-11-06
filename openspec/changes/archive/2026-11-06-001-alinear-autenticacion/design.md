# Design: Alinear la autenticación

## Contexto
Blazor Web App con renderizado interactivo de servidor, ASP.NET Core Identity con cookies, EF Core sobre SQL Server. Las decisiones de arquitectura ya están tomadas por el código existente: Blazor Server, acceso directo a la capa Application y sesión por cookie de Identity. Este change no las modifica.

## Decisiones

### 1. Cómo se escribe la cookie de sesión
El formulario de login ya hace POST a `AccountController` (`/Account/PerformLogin`), que es el patrón correcto en Blazor Server: un componente interactivo no puede escribir cookies una vez abierto el circuito. Se mantiene.
- Se agrega `[ValidateAntiForgeryToken]` en las acciones POST del controlador y el formulario incluye el token, obtenido con `AntiforgeryStateProvider` en los componentes interactivos.
- El cierre de sesión pasa de GET (`PerformLogout`) a POST. El botón de `MainLayout` se cambia por un formulario con token.

### 2. Servicio de correo
- Interfaz `IEmailService` en `GHL.Application/Interfaces` (envío de un mensaje con destinatario, asunto y cuerpo HTML).
- Implementación SMTP en `GHL.Infrastructure` con MailKit, configurada por `appsettings` (host, puerto, usuario, remitente). El usuario y la contraseña SMTP van en User Secrets o variables de entorno, no en el repositorio.
- Si SMTP no está configurado (desarrollo), se registra una implementación que escribe el contenido del correo, incluido el enlace, en el log. Así el flujo se puede probar sin proveedor.

### 3. Tokens de verificación y recuperación
- Se usan los tokens de Identity (`GenerateEmailConfirmationTokenAsync`, `GeneratePasswordResetTokenAsync`), codificados en Base64Url dentro del enlace.
- La vigencia se fija en 24 horas con `DataProtectionTokenProviderOptions.TokenLifespan`, para cumplir el spec de recuperación.
- La página de ForgotPassword responde siempre lo mismo exista o no el email, para no revelar qué cuentas existen.

### 4. Bloqueo por intentos fallidos
- `Lockout.MaxFailedAccessAttempts = 5` y `DefaultLockoutTimeSpan = 15 minutos`, con `AllowedForNewUsers = true`.
- `PasswordSignInAsync` pasa a `lockoutOnFailure: true`. El mensaje al usuario sigue siendo genérico.

### 5. Redirección tras el login
- Se valida `returnUrl` con `Url.IsLocalUrl`; si no es local, se redirige a `/`. Se usa `LocalRedirect`.

### 6. Orden de middleware en `GHL.BlazorApp/Program.cs`
`UseAuthentication()`, `UseAuthorization()` y después `UseAntiforgery()`.

### 7. Usuarios existentes
Al activar `RequireConfirmedAccount`, los usuarios ya creados quedan sin acceso porque su email no está confirmado. Se incluye una migración de datos que marca `EmailConfirmed = true` para los usuarios existentes al momento del despliegue.

## Riesgos
- Sin proveedor SMTP real en producción, nadie puede completar el registro. Se debe configurar antes del despliegue.
- Los enlaces de correo necesitan la URL pública de la aplicación; se toma de configuración (`App:BaseUrl`), no de la solicitud.
