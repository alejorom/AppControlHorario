# Tasks: Alinear la autenticación

## 1. Base de seguridad del login
- [ ] 1.1 Corregir el orden de middleware en `GHL.BlazorApp/Program.cs`: `UseAuthentication`, `UseAuthorization`, `UseAntiforgery`
- [ ] 1.2 Agregar `[ValidateAntiForgeryToken]` a las acciones POST de `AccountController` y enviar el token desde el formulario de `Login.razor` con `AntiforgeryStateProvider`
- [ ] 1.3 Reemplazar `Redirect(returnUrl)` por una validación con `Url.IsLocalUrl` y `LocalRedirect`, con `/` como destino por defecto
- [ ] 1.4 Configurar las opciones de bloqueo (5 intentos, 15 minutos) y pasar `PasswordSignInAsync` a `lockoutOnFailure: true`
- [ ] 1.5 Cambiar el cierre de sesión de GET a POST con token y actualizar los dos botones de `MainLayout.razor`
- [ ] 1.6 Verificar los 2 scenarios de "Bloqueo por Intentos Fallidos"
- [ ] 1.7 Verificar los 2 scenarios de "Redirección Segura Tras el Inicio de Sesión"
- [ ] 1.8 Verificar los 2 scenarios de "Cierre de Sesión"

## 2. Servicio de correo
- [ ] 2.1 Definir `IEmailService` en `GHL.Application/Interfaces`
- [ ] 2.2 Implementar el envío SMTP con MailKit en `GHL.Infrastructure`, con opciones tipadas leídas de configuración
- [ ] 2.3 Implementar el servicio de desarrollo que escribe el correo y su enlace en el log, y elegirlo cuando SMTP no está configurado
- [ ] 2.4 Agregar `App:BaseUrl` a la configuración y mover las credenciales SMTP a User Secrets o variables de entorno
- [ ] 2.5 Registrar los servicios en `DependencyInjection.cs` de Infrastructure

## 3. Verificación de email
- [ ] 3.1 Fijar `TokenLifespan` en 24 horas con `DataProtectionTokenProviderOptions`
- [ ] 3.2 Enviar el correo de verificación al crear la cuenta en `Register.razor`
- [ ] 3.3 Crear la página `ConfirmEmail.razor` (token Base64Url) y la acción de reenviar el correo de confirmación
- [ ] 3.4 Activar `SignIn.RequireConfirmedAccount` y mostrar en el login la opción de reenviar cuando el email no está verificado
- [ ] 3.5 Crear la migración de EF Core que marca `EmailConfirmed = true` en los usuarios existentes
- [ ] 3.6 Verificar los 3 scenarios de "Registro de Usuario"
- [ ] 3.7 Verificar el scenario de "Email no verificado" de "Inicio de Sesión"

## 4. Recuperación de contraseña
- [ ] 4.1 Crear `ForgotPassword.razor` que responde siempre igual exista o no el email y envía el correo con el enlace
- [ ] 4.2 Crear `ResetPassword.razor` con validación de la política de contraseña y manejo de enlace expirado
- [ ] 4.3 Verificar el enlace "¿Olvidaste?" de `Login.razor`
- [ ] 4.4 Verificar los 2 scenarios de "Recuperación de Contraseña"

## 5. Cambio de email
- [ ] 5.1 Reemplazar en `Settings.razor` el cambio directo de email por el flujo de `GenerateChangeEmailTokenAsync` con correo de confirmación al nuevo email
- [ ] 5.2 Crear la página que confirma el cambio de email
- [ ] 5.3 Verificar el scenario de "Confirmación al Cambiar el Email"
