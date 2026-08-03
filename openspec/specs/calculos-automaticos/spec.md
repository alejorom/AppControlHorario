# Capability: Cálculos Automáticos

## Purpose
Calcular de forma automática y precisa las horas trabajadas, el tiempo en pausas
y las estadísticas agregadas (semanales y mensuales) de cada usuario.

## ADDED Requirements

### Requirement: Cálculo de Horas Trabajadas por Jornada
El sistema DEBE calcular las horas netas trabajadas de cada jornada al momento
de marcar la salida.

#### Scenario: Cálculo al cerrar jornada
- **WHEN** se registra clock_out de una jornada
- **THEN** el sistema calcula total_worked_minutes = (clock_out - clock_in) - total_break_minutes
- **AND** presenta el resultado en formato horas y minutos (ej. "8h 30m")

---

### Requirement: Cálculo de Tiempo en Pausas
El sistema DEBE sumar la duración de todas las pausas asociadas a una jornada.

#### Scenario: Suma de pausas múltiples
- **WHEN** una jornada tiene dos o más pausas finalizadas
- **THEN** el sistema suma sus duraciones individuales
- **AND** actualiza total_break_minutes de la jornada correspondiente

---

### Requirement: Estadísticas Semanales
El sistema DEBE calcular el total y promedio diario de horas trabajadas por semana.

#### Scenario: Cálculo de cierre semanal
- **WHEN** finaliza una semana (domingo a medianoche)
- **THEN** el sistema calcula el total de horas trabajadas en esa semana
- **AND** calcula el promedio diario en base a los días con jornada registrada

---

### Requirement: Estadísticas Mensuales
El sistema DEBE calcular el total de horas, promedio diario y días trabajados por mes.

#### Scenario: Cálculo de cierre mensual
- **WHEN** cambia el mes en curso
- **THEN** el sistema calcula el total de horas trabajadas del mes anterior
- **AND** calcula el promedio diario y el número de días trabajados