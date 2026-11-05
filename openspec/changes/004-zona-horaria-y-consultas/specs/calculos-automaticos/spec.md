## ADDED Requirements

### Requirement: Periodos según la Zona Horaria Configurada
El sistema SHALL calcular los límites de día, semana y mes de las estadísticas y los reportes en la zona horaria configurada de la aplicación, y no en UTC.

#### Scenario: Jornada de la noche
- **WHEN** un usuario inicia una jornada a las 8:00 pm hora local
- **THEN** el sistema la cuenta en el día local en que inició
- **AND** no la cuenta en el día siguiente

#### Scenario: Jornada cerca del cambio de mes
- **WHEN** un usuario inicia una jornada a las 11:00 pm hora local del último día del mes
- **THEN** el sistema la cuenta en ese mes
- **AND** no la cuenta en el mes siguiente

### Requirement: Estadísticas sin Límite de Registros
El sistema SHALL incluir todas las jornadas del periodo consultado en las estadísticas y los reportes, sin truncar el resultado por un número máximo de registros.

#### Scenario: Periodo con muchas jornadas
- **WHEN** un usuario tiene más de 100 jornadas en el mes consultado
- **THEN** el total de horas del mes incluye todas ellas
- **AND** el número de días trabajados refleja todos los días con jornada
