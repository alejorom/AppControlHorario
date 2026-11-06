## Purpose
Definir las garantías de seguridad transversales de la aplicación de control de horarios: aislamiento de los datos de cada usuario, API autenticada, páginas protegidas por defecto y mensajes de error que no revelan detalles internos. Aplica a todas las demás capabilities.

## ADDED Requirements

### Requirement: Aislamiento de Datos por Usuario
El sistema SHALL permitir a cada usuario acceder únicamente a sus propios registros de jornadas, pausas y perfil.

#### Scenario: Consulta de datos propios
- **WHEN** un usuario autenticado consulta su historial, su jornada actual o sus estadísticas
- **THEN** el sistema devuelve solo registros que le pertenecen

#### Scenario: Acceso a una jornada de otro usuario
- **WHEN** un usuario autenticado intenta obtener o modificar una jornada que pertenece a otro usuario
- **THEN** el sistema rechaza la operación
- **AND** no revela si la jornada existe

### Requirement: API Autenticada
El sistema SHALL exigir autenticación en todos los endpoints de la API y SHALL determinar la identidad del usuario a partir de las credenciales, nunca de un identificador recibido en la dirección o en el cuerpo de la solicitud.

#### Scenario: Solicitud sin credenciales
- **WHEN** se recibe una solicitud a un endpoint de la API sin credenciales válidas
- **THEN** el sistema responde con un error de no autorizado
- **AND** no ejecuta ninguna operación

#### Scenario: Identidad tomada de las credenciales
- **WHEN** un usuario autenticado registra su entrada mediante la API
- **THEN** el sistema crea la jornada a nombre del usuario de las credenciales
- **AND** no acepta indicar otro usuario en la solicitud

### Requirement: Páginas Protegidas por Defecto
El sistema SHALL exigir un usuario autenticado en toda página de la aplicación, salvo en las páginas de cuenta marcadas explícitamente como públicas.

#### Scenario: Página nueva sin configuración de acceso
- **WHEN** un usuario anónimo solicita una página que no declara ninguna regla de acceso
- **THEN** el sistema lo redirige a la pantalla de login

#### Scenario: Páginas de cuenta públicas
- **WHEN** un usuario anónimo abre el login, el registro o la recuperación de contraseña
- **THEN** el sistema muestra la página sin exigir sesión

### Requirement: Mensajes de Error sin Detalles Internos
El sistema SHALL mostrar al usuario mensajes de error genéricos ante fallos inesperados y SHALL registrar el detalle técnico solo en el registro del sistema.

#### Scenario: Fallo inesperado al cargar datos
- **WHEN** ocurre una excepción inesperada al cargar o guardar datos
- **THEN** el sistema muestra al usuario un mensaje genérico de error
- **AND** registra la excepción completa en el log

#### Scenario: Base de datos no disponible al iniciar sesión
- **WHEN** un usuario intenta iniciar sesión y el sistema no puede conectarse a la base de datos
- **THEN** el sistema muestra un mensaje indicando que el servicio no está disponible temporalmente
- **AND** registra el detalle técnico en el log
- **AND** no revela el nombre del servidor ni de la base de datos

#### Scenario: Error de regla de negocio
- **WHEN** el usuario intenta una operación que una regla de negocio no permite, como marcar entrada con una jornada activa
- **THEN** el sistema muestra el mensaje de esa regla
