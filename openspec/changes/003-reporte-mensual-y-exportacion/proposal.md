# Proposal: Reporte mensual, promedio semanal y exportación

## Why
El spec `reportes` ya describe el reporte mensual y la exportación a CSV o PDF, y el spec `calculos-automaticos` pide el promedio diario de la semana, pero no están implementados:
- No existe un reporte mensual: solo hay una barra de progreso contra 176 horas en `Statistics.razor`.
- No hay exportación de datos.
- `WeeklyReportDto` entrega el total semanal pero no el promedio diario.

## What Changes
- Agregar `GetMonthlyReportAsync(userId, year, month)` en Application, con horas por día, total del mes y días trabajados, y su vista con selector de mes y año.
- Agregar el promedio diario a `WeeklyReportDto` y mostrarlo en el reporte semanal.
- Agregar la exportación del historial a CSV y a PDF, respetando el filtro de fechas, con el nombre de archivo con la fecha de generación.

## Impact
- **Specs de referencia**: `specs/reportes/spec.md` (Reporte Mensual, Exportación de Datos) y `specs/calculos-automaticos/spec.md` (Estadísticas Semanales). Ya describen el comportamiento objetivo, por lo que este change los implementa sin modificarlos y no incluye deltas (`skip_specs: true`).
- **Decisión por defecto**: CSV primero y PDF después, en tareas separadas, para poder entregar valor sin esperar la elección de librería de PDF.
- **Fuera de alcance**: zona horaria y límite de consultas (change `004`).
- **Dependencia**: conviene aplicarlo después del change `004`, porque los totales por día dependen de cómo se calcula el día; si se aplica antes, habrá que revisar estos cálculos.
- **Riesgo**: bajo. Las librerías de PDF tienen licencias distintas; se verifica la licencia antes de adoptar una.
