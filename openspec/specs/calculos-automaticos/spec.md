# Cálculos Automáticos

## Purpose
Calcular de forma automática y precisa las horas trabajadas, el tiempo en pausas y las estadísticas agregadas semanales y mensuales de cada usuario, sin intervención manual. Los resultados alimentan el dashboard y los reportes.

## Requirements

### Requirement: Cálculo de Horas Trabajadas por Jornada
El sistema SHALL calcular las horas netas trabajadas de cada jornada al momento de marcar la salida.

#### Scenario: Cálculo al cerrar jornada
- **WHEN** se registra el clock_out de una jornada
- **THEN** el sistema calcula total_worked_minutes como (clock_out - clock_in) menos total_break_minutes
- **AND** presenta el resultado en formato horas y minutos (por ejemplo "8h 30m")

### Requirement: Cálculo de Tiempo en Pausas
El sistema SHALL sumar la duración de todas las pausas asociadas a una jornada.

#### Scenario: Suma de pausas múltiples
- **WHEN** una jornada tiene dos o más pausas finalizadas
- **THEN** el sistema suma sus duraciones individuales
- **AND** actualiza total_break_minutes de la jornada correspondiente

### Requirement: Estadísticas Semanales
El sistema SHALL calcular el total y el promedio diario de horas trabajadas por semana.

#### Scenario: Cálculo de cierre semanal
- **WHEN** se consulta una semana completa
- **THEN** el sistema calcula el total de horas trabajadas en esa semana
- **AND** calcula el promedio diario sobre los días con jornada registrada

### Requirement: Estadísticas Mensuales
El sistema SHALL calcular el total de horas, el promedio diario y los días trabajados por mes.

#### Scenario: Cálculo mensual
- **WHEN** se consulta un mes
- **THEN** el sistema calcula el total de horas trabajadas de ese mes
- **AND** calcula el promedio diario y el número de días trabajados
