# Capability: Registro de Jornadas

## Purpose
Permitir a los usuarios registrar el inicio y fin de su jornada laboral, así como
gestionar pausas durante la misma, guardando cada evento en Supabase.

## ADDED Requirements

### Requirement: Marcar Entrada
El sistema DEBE permitir registrar el inicio de una jornada laboral con fecha y hora
exactas, siempre que no exista una jornada activa para ese usuario.

#### Scenario: Entrada sin jornada activa
- **WHEN** el usuario hace clic en "Marcar Entrada"
- **AND** no tiene ninguna jornada abierta (sin clock_out registrado)
- **THEN** el sistema crea un registro de work_session con clock_in = hora actual
- **AND** muestra confirmación visual del registro

#### Scenario: Intento de doble entrada
- **WHEN** el usuario hace clic en "Marcar Entrada"
- **AND** ya existe una jornada activa
- **THEN** el sistema rechaza la acción
- **AND** muestra un mensaje indicando que ya hay una jornada en curso

---

### Requirement: Marcar Salida
El sistema DEBE permitir registrar el fin de una jornada activa y calcular automáticamente
las horas trabajadas.

#### Scenario: Salida con jornada activa
- **WHEN** el usuario hace clic en "Marcar Salida"
- **AND** existe una jornada activa sin pausas abiertas
- **THEN** el sistema registra clock_out = hora actual
- **AND** calcula total_worked_minutes = (clock_out - clock_in) - total_break_minutes
- **AND** cierra la jornada

#### Scenario: Intento de salida con pausa activa
- **WHEN** el usuario hace clic en "Marcar Salida"
- **AND** existe una pausa sin finalizar
- **THEN** el sistema rechaza la acción
- **AND** solicita finalizar la pausa antes de marcar salida

---

### Requirement: Iniciar Pausa
El sistema DEBE permitir iniciar una pausa durante una jornada activa.

#### Scenario: Inicio de pausa válido
- **WHEN** el usuario hace clic en "Iniciar Pausa"
- **AND** tiene una jornada activa sin pausa abierta
- **THEN** el sistema crea un registro de break con start_time = hora actual
- **AND** actualiza el estado visual a "En pausa"

#### Scenario: Intento de doble pausa
- **WHEN** el usuario hace clic en "Iniciar Pausa"
- **AND** ya existe una pausa activa
- **THEN** el sistema rechaza la acción

---

### Requirement: Finalizar Pausa
El sistema DEBE permitir finalizar una pausa activa y calcular su duración.

#### Scenario: Fin de pausa válido
- **WHEN** el usuario hace clic en "Finalizar Pausa"
- **AND** existe una pausa activa
- **THEN** el sistema registra end_time = hora actual
- **AND** calcula duration_minutes de esa pausa
- **AND** suma esa duración a total_break_minutes de la jornada
- **AND** actualiza el estado visual a "En jornada"

---

### Requirement: Visualizar Jornada Actual
El sistema DEBE mostrar en tiempo real el estado de la jornada activa del usuario.

#### Scenario: Vista de jornada en curso
- **WHEN** el usuario tiene una jornada activa
- **THEN** el sistema muestra hora de entrada, tiempo transcurrido, tiempo total en pausas y tiempo trabajado efectivo
- **AND** actualiza estos valores al menos cada minuto