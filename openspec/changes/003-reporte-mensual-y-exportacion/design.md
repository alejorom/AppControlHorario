# Design: Reporte mensual, promedio semanal y exportación

## Decisiones

### 1. Reporte mensual
- Nuevo método `GetMonthlyReportAsync(string userId, int year, int month)` en `IWorkSessionService`, que devuelve un `MonthlyReportDto` con una entrada por día del mes, el total, los días trabajados y el promedio diario.
- Reutiliza la lógica de agrupación por día del reporte semanal, extraída a un método privado compartido para no duplicarla.
- La vista va en `Statistics.razor` con un selector de mes y año; las barras siguen el mismo estilo del reporte semanal.

### 2. Promedio diario semanal
- `WeeklyReportDto` incorpora `AverageDailyMinutes`, calculado sobre los días con jornada registrada, como indica el spec.

### 3. Exportación
- `IExportService` en `GHL.Application/Interfaces`, con `ExportSessionsCsvAsync` y `ExportSessionsPdfAsync`, que reciben el usuario y el rango de fechas y devuelven los bytes y el nombre sugerido del archivo.
- El CSV se escribe con codificación UTF-8 con BOM para que Excel respete las tildes.
- La descarga se sirve desde un endpoint protegido (`GET /Export/Sessions?start=&end=&format=`) en `GHL.BlazorApp`. El usuario sale de la sesión, no de la URL.
- Un endpoint es más simple y fiable que descargar mediante JS interop desde un circuito de Blazor Server.
- El nombre del archivo incluye la fecha de generación, por ejemplo `jornadas_2026-11-05.csv`.

### 4. Librería de PDF
- Se elige una librería de PDF para .NET antes de implementar la tarea de PDF. QuestPDF es una opción habitual, pero su licencia gratuita tiene condiciones según el tamaño de la organización: hay que verificarlas antes de adoptarla.
- Si la licencia no encaja, la alternativa es imprimir la vista del reporte con CSS de impresión.
- El CSV no depende de esta decisión.
