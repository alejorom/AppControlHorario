# Capability: Reportes

## Purpose
Proveer visualizaciones y exportaciones del historial de jornadas y estadísticas
de tiempo trabajado.

## ADDED Requirements

### Requirement: Historial de Jornadas
El sistema DEBE mostrar el listado completo de jornadas registradas por el usuario,
ordenado por fecha descendente y con filtros por rango de fechas.

#### Scenario: Consulta de historial
- **WHEN** el usuario abre la sección "Historial"
- **THEN** el sistema muestra las jornadas con fecha, entrada, salida, pausas y total trabajado
- **AND** ordena los resultados del más reciente al más antiguo

#### Scenario: Filtro por rango de fechas
- **WHEN** el usuario aplica un filtro de fecha inicio/fin
- **THEN** el sistema muestra solo las jornadas dentro de ese rango

---

### Requirement: Reporte Semanal
El sistema DEBE generar un gráfico visual de horas trabajadas por día dentro
de una semana seleccionada.

#### Scenario: Visualización de reporte semanal
- **WHEN** el usuario selecciona "Reporte Semanal" y una semana específica
- **THEN** el sistema muestra un gráfico de barras con horas por día
- **AND** muestra el total de horas de la semana

---

### Requirement: Reporte Mensual
El sistema DEBE generar un gráfico visual de horas trabajadas por día dentro
de un mes seleccionado.

#### Scenario: Visualización de reporte mensual
- **WHEN** el usuario selecciona "Reporte Mensual" y un mes/año específico
- **THEN** el sistema muestra un gráfico con horas por día
- **AND** muestra el total de horas y días trabajados del mes

---

### Requirement: Exportación de Datos
El sistema DEBE permitir exportar el historial de jornadas en formato CSV o PDF,
respetando los filtros de fecha aplicados.

#### Scenario: Exportación con filtro activo
- **WHEN** el usuario exporta datos con un filtro de fecha aplicado
- **THEN** el sistema genera un archivo que contiene solo las jornadas dentro de ese rango
- **AND** nombra el archivo incluyendo la fecha de generación