# Capability: Perfil de Usuario

## Purpose
Permitir a los usuarios visualizar y actualizar su información personal básica.

## ADDED Requirements

### Requirement: Visualizar Perfil
El sistema DEBE mostrar al usuario su nombre completo, email y fecha de registro.

#### Scenario: Consulta de perfil
- **WHEN** el usuario abre la sección "Mi Perfil"
- **THEN** el sistema muestra nombre completo, email y fecha de registro

---

### Requirement: Editar Perfil
El sistema DEBE permitir actualizar el nombre completo y cambiar la contraseña.

#### Scenario: Actualización de nombre
- **WHEN** el usuario edita su nombre completo y guarda los cambios
- **THEN** el sistema actualiza el registro en la tabla profiles
- **AND** muestra confirmación del cambio

#### Scenario: Cambio de contraseña válido
- **WHEN** el usuario ingresa una nueva contraseña de al menos 8 caracteres y confirma
- **THEN** el sistema actualiza la contraseña en Supabase Auth
- **AND** muestra confirmación del cambio

#### Scenario: Cambio de contraseña inválido
- **WHEN** el usuario ingresa una nueva contraseña de menos de 8 caracteres
- **THEN** el sistema rechaza el cambio
- **AND** muestra un mensaje indicando el requisito mínimo