# Proposal: Alinear la autenticación con el spec

## Why
La autenticación ya está parcialmente construida (Identity con cookies, páginas Login, Register y Logout), pero no cumple el spec `autenticacion` ni buenas prácticas de seguridad. Una auditoría del código encontró:
- No existe la recuperación de contraseña: el enlace "¿Olvidaste?" apunta a `/Account/ForgotPassword`, una página que no existe, y no hay forma de enviar correos.
- No se verifica el email: `RequireConfirmedAccount = false` y no se envía ningún correo, aunque el spec lo exige.
- El login redirige a cualquier `returnUrl` recibido (redirección abierta) y no bloquea cuentas tras intentos fallidos.
- El cierre de sesión se hace con GET, por lo que un enlace externo puede cerrar la sesión de un usuario.
- `UseAntiforgery()` está antes de `UseAuthentication()`, al revés de lo recomendado, y el controlador de cuenta no valida el token antiforgery.
- El spec de contraseña dice solo "8 caracteres", pero el código y el SRS exigen mayúscula, minúscula, número y símbolo.

## What Changes
- Implementar recuperación de contraseña (páginas ForgotPassword y ResetPassword) y verificación de email (ConfirmEmail y reenvío), con un servicio de correo abstracto en Application y una implementación SMTP en Infrastructure.
- Activar la confirmación de email para iniciar sesión y exigir confirmación al cambiar el email.
- Usar `LocalRedirect` en el login, activar el bloqueo por intentos fallidos y pasar el cierre de sesión a POST con antiforgery.
- Corregir el orden de middleware y validar antiforgery en `AccountController`.
- Alinear el spec de Registro con la política de contraseña real (delta `MODIFIED`).

## Impact
- **Capabilities afectadas**: `autenticacion` (ADDED y MODIFIED, ver `specs/autenticacion/spec.md`).
- **Decisión tomada por defecto**: se implementa la verificación de email, con SMTP configurable y un modo de desarrollo que escribe el enlace en el log. Si se prefiere no exigirla por ahora, hay que quitar los requirements de verificación del delta antes de aplicar.
- **Fuera de alcance**: seguridad de `GHL.WebApi` y autorización por defecto (change `002`), zona horaria (`004`), limpieza de arquitectura (`005`).
- **Riesgo**: medio. Activar la confirmación de email deja sin acceso a los usuarios existentes que no la tengan; ver la migración de datos en `design.md`.
