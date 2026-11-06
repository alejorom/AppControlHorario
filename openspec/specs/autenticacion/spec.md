# Autenticación

## Purpose
Gestionar el registro, inicio de sesión, recuperación de contraseña y cierre de sesión de los usuarios de la aplicación de control de horarios, a través de un único sistema de identidad. Esta capability es la base del sistema: ninguna otra capability puede operar sin un usuario autenticado.

## Requirements

### Requirement: Registro de Usuario
El sistema SHALL permitir a nuevos usuarios crear una cuenta mediante email y contraseña, y SHALL exigir una contraseña de al menos 8 caracteres que incluya una mayúscula, una minúscula, un número y un carácter especial.

#### Scenario: Registro exitoso
- **WHEN** un usuario envía el formulario de registro con un email no existente y una contraseña que cumple la política de seguridad
- **THEN** el sistema crea la cuenta del usuario
- **AND** el sistema envía un correo de verificación
- **AND** el usuario no puede iniciar sesión hasta confirmar su email

#### Scenario: Registro con email duplicado
- **WHEN** un usuario intenta registrarse con un email ya existente en el sistema
- **THEN** el sistema rechaza el registro
- **AND** muestra un mensaje de error indicando que el email ya está en uso

#### Scenario: Contraseña insegura
- **WHEN** un usuario intenta registrarse con una contraseña que tiene menos de 8 caracteres o que no incluye mayúscula, minúscula, número y carácter especial
- **THEN** el sistema rechaza el registro
- **AND** muestra un mensaje indicando los requisitos que no se cumplen

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
El sistema SHALL permitir a un usuario autenticado cerrar su sesión de forma segura, mediante una solicitud protegida contra falsificación entre sitios.

#### Scenario: Logout exitoso
- **WHEN** un usuario autenticado confirma "Cerrar sesión" desde la aplicación
- **THEN** el sistema invalida la sesión activa
- **AND** redirige a la pantalla de login

#### Scenario: Cierre de sesión solicitado desde otro sitio
- **WHEN** se recibe una solicitud de cierre de sesión sin el token de protección de la aplicación
- **THEN** el sistema rechaza la solicitud
- **AND** la sesión del usuario permanece activa

### Requirement: Bloqueo por Intentos Fallidos
El sistema SHALL bloquear temporalmente el inicio de sesión de una cuenta tras varios intentos fallidos consecutivos.

#### Scenario: Bloqueo tras intentos fallidos
- **WHEN** un usuario falla la contraseña 5 veces consecutivas
- **THEN** el sistema bloquea el inicio de sesión de esa cuenta durante 15 minutos
- **AND** muestra un mensaje que no confirma si la cuenta existe

#### Scenario: Credenciales correctas durante el bloqueo
- **WHEN** un usuario con la cuenta bloqueada envía credenciales correctas
- **THEN** el sistema rechaza el acceso
- **AND** muestra un mensaje indicando que debe intentarlo más tarde

### Requirement: Redirección Segura Tras el Inicio de Sesión
El sistema SHALL redirigir tras el inicio de sesión únicamente a direcciones dentro de la propia aplicación.

#### Scenario: Dirección de retorno local
- **WHEN** un usuario inicia sesión con una dirección de retorno que pertenece a la aplicación
- **THEN** el sistema redirige a esa dirección

#### Scenario: Dirección de retorno externa
- **WHEN** un usuario inicia sesión con una dirección de retorno que apunta a otro sitio
- **THEN** el sistema ignora esa dirección
- **AND** redirige al dashboard principal

### Requirement: Confirmación al Cambiar el Email
El sistema SHALL exigir la confirmación del nuevo email antes de aplicar un cambio de email en la cuenta.

#### Scenario: Solicitud de cambio de email
- **WHEN** un usuario autenticado solicita cambiar su email por uno no registrado
- **THEN** el sistema envía un correo de confirmación al nuevo email
- **AND** mantiene el email anterior hasta que el nuevo se confirme
