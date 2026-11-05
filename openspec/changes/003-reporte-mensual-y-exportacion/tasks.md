# Tasks: Reporte mensual, promedio semanal y exportación

## 1. Promedio diario semanal
- [ ] 1.1 Agregar `AverageDailyMinutes` a `WeeklyReportDto` y calcularlo en `GetWeeklyReportAsync` sobre los días con jornada
- [ ] 1.2 Mostrar el promedio en el reporte semanal de `Statistics.razor`
- [ ] 1.3 Verificar el scenario de "Estadísticas Semanales" (total y promedio)

## 2. Reporte mensual
- [ ] 2.1 Crear `MonthlyReportDto` y `GetMonthlyReportAsync(userId, year, month)` en Application, extrayendo la agrupación por día compartida con el reporte semanal
- [ ] 2.2 Agregar la vista mensual en `Statistics.razor` con selector de mes y año
- [ ] 2.3 Mostrar total de horas y días trabajados del mes
- [ ] 2.4 Verificar el scenario de "Reporte Mensual"

## 3. Exportación a CSV
- [ ] 3.1 Definir `IExportService` en `GHL.Application/Interfaces` y su implementación de CSV (UTF-8 con BOM)
- [ ] 3.2 Crear el endpoint protegido `GET /Export/Sessions` que toma el usuario de la sesión y respeta el rango de fechas
- [ ] 3.3 Agregar el botón de exportar en `History.razor`, con los filtros de fecha activos
- [ ] 3.4 Nombrar el archivo con la fecha de generación
- [ ] 3.5 Verificar el scenario de "Exportación de Datos" con filtro activo

## 4. Exportación a PDF
- [ ] 4.1 Elegir la librería de PDF y verificar que su licencia es compatible con el uso previsto
- [ ] 4.2 Implementar la exportación a PDF en `IExportService`
- [ ] 4.3 Agregar la opción de formato PDF al botón de exportar
- [ ] 4.4 Verificar el scenario de "Exportación de Datos" con formato PDF
