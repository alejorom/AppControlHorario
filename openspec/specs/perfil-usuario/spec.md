# Perfil de Usuario

## Purpose
Permitir a los usuarios visualizar y actualizar su información personal básica (nombre completo y contraseña), asociada a su cuenta de usuario.

## Requirements

### Requirement: Visualizar Perfil
El sistema SHALL mostrar al usuario su nombre completo, su email y su fecha de registro.

#### Scenario: Consulta de perfil
- **WHEN** el usuario abre la sección "Mi Perfil"
- **THEN** el sistema muestra nombre completo, email y fecha de registro

### Requirement: Editar Perfil
El sistema SHALL permitir actualizar el nombre completo y cambiar la contraseña.

#### Scenario: Actualización de nombre
- **WHEN** el usuario edita su nombre completo y guarda los cambios
- **THEN** el sistema actualiza el registro en la tabla profiles
- **AND** muestra confirmación del cambio

#### Scenario: Cambio de contraseña válido
- **WHEN** el usuario ingresa una nueva contraseña de al menos 8 caracteres y confirma
- **THEN** el sistema actualiza la contraseña de la cuenta
- **AND** muestra confirmación del cambio

#### Scenario: Cambio de contraseña inválido
- **WHEN** el usuario ingresa una nueva contraseña de menos de 8 caracteres
- **THEN** el sistema rechaza el cambio
- **AND** muestra un mensaje indicando el requisito mínimo
