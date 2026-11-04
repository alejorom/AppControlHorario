# Autenticación

## Purpose
Gestionar el registro, inicio de sesión, recuperación de contraseña y cierre de sesión de los usuarios de la aplicación de control de horarios, a través de un único sistema de identidad. Esta capability es la base del sistema: ninguna otra capability puede operar sin un usuario autenticado.

## Requirements

### Requirement: Registro de Usuario
El sistema SHALL permitir a nuevos usuarios crear una cuenta mediante email y contraseña.

#### Scenario: Registro exitoso
- **WHEN** un usuario envía el formulario de registro con un email no existente y una contraseña de al menos 8 caracteres
- **THEN** el sistema crea la cuenta del usuario
- **AND** el sistema envía un correo de verificación
- **AND** el usuario no puede iniciar sesión hasta confirmar su email

#### Scenario: Registro con email duplicado
- **WHEN** un usuario intenta registrarse con un email ya existente en el sistema
- **THEN** el sistema rechaza el registro
- **AND** muestra un mensaje de error indicando que el email ya está en uso

#### Scenario: Contraseña insegura
- **WHEN** un usuario intenta registrarse con una contraseña de menos de 8 caracteres
- **THEN** el sistema rechaza el registro
- **AND** muestra un mensaje indicando el requisito mínimo

### Requirement: Inicio de Sesión
El sistema SHALL permitir a usuarios registrados autenticarse con email y contraseña.

#### Scenario: Login exitoso
- **WHEN** un usuario envía credenciales válidas y su email está verificado
- **THEN** el sistema autentica al usuario
- **AND** redirige al dashboard principal

#### Scenario: Credenciales incorrectas
- **WHEN** un usuario envía un email o una contraseña incorrectos
- **THEN** el sistema rechaza el acceso
- **AND** muestra un mensaje de error genérico sin especificar cuál campo falló

#### Scenario: Email no verificado
- **WHEN** un usuario con email no verificado intenta iniciar sesión
- **THEN** el sistema rechaza el acceso
- **AND** ofrece reenviar el correo de verificación

### Requirement: Recuperación de Contraseña
El sistema SHALL permitir a un usuario restablecer su contraseña mediante un enlace enviado por correo y válido por 24 horas.

#### Scenario: Solicitud de recuperación
- **WHEN** un usuario solicita recuperar su contraseña ingresando su email
- **THEN** el sistema envía un correo con un enlace de restablecimiento
- **AND** el enlace expira 24 horas después de su generación

#### Scenario: Enlace expirado
- **WHEN** un usuario intenta usar un enlace de recuperación después de 24 horas
- **THEN** el sistema rechaza la solicitud
- **AND** ofrece generar un nuevo enlace

### Requirement: Cierre de Sesión
El sistema SHALL permitir a un usuario autenticado cerrar su sesión de forma segura.

#### Scenario: Logout exitoso
- **WHEN** un usuario autenticado hace clic en "Cerrar sesión"
- **THEN** el sistema invalida el token de sesión activo
- **AND** redirige a la pantalla de login
