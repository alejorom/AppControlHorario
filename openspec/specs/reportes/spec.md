# Reportes

## Purpose
Proveer visualizaciones y exportaciones del historial de jornadas y de las estadísticas de tiempo trabajado, de modo que el usuario pueda revisar su actividad por día, semana y mes.

## Requirements

### Requirement: Historial de Jornadas
El sistema SHALL mostrar el listado de jornadas registradas por el usuario, ordenado por fecha descendente y con filtros por rango de fechas.

#### Scenario: Consulta de historial
- **WHEN** el usuario abre la sección "Historial"
- **THEN** el sistema muestra las jornadas con fecha, entrada, salida, pausas y total trabajado
- **AND** ordena los resultados del más reciente al más antiguo

#### Scenario: Filtro por rango de fechas
- **WHEN** el usuario aplica un filtro de fecha inicio y fecha fin
- **THEN** el sistema muestra solo las jornadas dentro de ese rango

### Requirement: Reporte Semanal
El sistema SHALL generar un gráfico de las horas trabajadas por día dentro de una semana seleccionada.

#### Scenario: Visualización de reporte semanal
- **WHEN** el usuario selecciona "Reporte Semanal" y una semana específica
- **THEN** el sistema muestra un gráfico de barras con las horas por día
- **AND** muestra el total de horas de la semana

### Requirement: Reporte Mensual
El sistema SHALL generar un gráfico de las horas trabajadas por día dentro de un mes seleccionado.

#### Scenario: Visualización de reporte mensual
- **WHEN** el usuario selecciona "Reporte Mensual" y un mes y año específicos
- **THEN** el sistema muestra un gráfico con las horas por día
- **AND** muestra el total de horas y los días trabajados del mes

### Requirement: Exportación de Datos
El sistema SHALL permitir exportar el historial de jornadas en formato CSV o PDF, respetando los filtros de fecha aplicados.

#### Scenario: Exportación con filtro activo
- **WHEN** el usuario exporta datos con un filtro de fecha aplicado
- **THEN** el sistema genera un archivo que contiene solo las jornadas dentro de ese rango
- **AND** nombra el archivo incluyendo la fecha de generación
