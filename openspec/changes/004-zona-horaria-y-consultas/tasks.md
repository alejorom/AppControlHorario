# Tasks: Zona horaria y consultas completas

## 1. Zona horaria
- [ ] 1.1 Crear las opciones tipadas `App:TimeZoneId` (por defecto `America/Bogota`) y `App:WeekStartsOn` y agregarlas a `appsettings.json`
- [ ] 1.2 Definir `ITimeZoneProvider` en Application con inicio de día, semana y mes en la zona configurada y su conversión a UTC
- [ ] 1.3 Verificar en el arranque que la zona existe y fallar con un mensaje claro si no
- [ ] 1.4 Registrar el proveedor en `DependencyInjection.cs` de Application
- [ ] 1.5 Escribir pruebas del proveedor con fechas cercanas a medianoche y al cambio de mes

## 2. Cálculos con la zona horaria
- [ ] 2.1 Reemplazar `DateTime.UtcNow.Date`, `weekStart` y `monthStart` en `GetDashboardStatsAsync` por los límites del proveedor
- [ ] 2.2 Reemplazar las agrupaciones por `.Date` en `GetDashboardStatsAsync` y `GetWeeklyReportAsync` por agrupación en la zona local
- [ ] 2.3 Mostrar las horas de entrada y salida de History y Home en la zona local
- [ ] 2.4 Verificar los 2 scenarios de "Periodos según la Zona Horaria Configurada"

## 3. Consultas sin límite
- [ ] 3.1 Agregar `GetSessionsInRangeAsync(userId, startUtc, endUtc)` a `IWorkSessionRepository` y a su implementación, incluyendo las pausas
- [ ] 3.2 Usarlo en `GetDashboardStatsAsync` y `GetWeeklyReportAsync` en lugar de la consulta paginada con tamaño 100
- [ ] 3.3 Dejar la paginación solo en `GetHistoryAsync`
- [ ] 3.4 Escribir una prueba con más de 100 jornadas en el mes
- [ ] 3.5 Verificar el scenario de "Estadísticas sin Límite de Registros"
