# Design: Zona horaria y consultas completas

## Decisiones

### 1. Zona horaria de la aplicación
- Nueva opción `App:TimeZoneId` (por defecto `America/Bogota`), leída con una clase de opciones tipada.
- `ITimeZoneProvider` en `GHL.Application/Interfaces` con `TimeZoneInfo Zone`, y utilidades para obtener el inicio del día, de la semana y del mes en esa zona, y convertirlos a UTC.
- En .NET 6 o superior, `TimeZoneInfo.FindSystemTimeZoneById("America/Bogota")` funciona también en Windows siempre que ICU esté disponible. Se verifica en el arranque y la aplicación falla con un mensaje claro si el identificador no existe.
- Los timestamps siguen guardándose en UTC; solo cambia cómo se calculan los límites de los periodos.

### 2. Inicio de semana
- Hoy la semana empieza en domingo (`DayOfWeek`). Se mantiene ese criterio y se hace configurable (`App:WeekStartsOn`) para poder pasarlo a lunes sin tocar código.

### 3. Consultas de estadísticas
- Nuevo método de repositorio `GetSessionsInRangeAsync(userId, startUtc, endUtc)` sin paginación, que incluye las pausas, usado por el dashboard y los reportes.
- `GetSessionsByUserIdAsync` con paginación queda solo para el historial.
- Un mes con jornadas diarias y pocas pausas suma cientos de filas como mucho, por lo que cargar el rango completo es aceptable. Si el volumen creciera, la agregación se movería a SQL.

### 4. Impacto en otros changes
- El reporte mensual del change `003` usa estos límites de día; se recomienda aplicar este change antes.
