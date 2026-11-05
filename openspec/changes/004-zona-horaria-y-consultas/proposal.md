# Proposal: Zona horaria y consultas completas

## Why
La auditoría encontró dos fallos en las estadísticas:
- "Hoy", "esta semana" y "este mes" se calculan en UTC (`DateTime.UtcNow.Date`). En Bogotá (UTC-5), una jornada que empieza a las 8 pm se cuenta para el día siguiente, y las jornadas cercanas a la medianoche caen en el periodo equivocado.
- El dashboard y el reporte semanal leen como máximo 100 jornadas (`pageSize` fijo en 100), así que las cifras del mes quedan truncadas en cuanto hay más.

## What Changes
- Calcular los límites de día, semana y mes en la zona horaria configurada, y convertirlos a UTC para consultar, ya que los timestamps siguen guardándose en UTC.
- Reemplazar las comparaciones `.Date` en memoria por rangos calculados en la zona horaria.
- Agregar una consulta de rango sin paginación para las estadísticas y los reportes, y dejar la paginación solo para el historial.

## Impact
- **Capabilities afectadas**: `calculos-automaticos` (ADDED, ver `specs/calculos-automaticos/spec.md`).
- **Decisión por defecto**: una sola zona horaria para toda la aplicación, configurable (`America/Bogota`). La zona por usuario queda para un change posterior si hace falta.
- **Fuera de alcance**: zona horaria por usuario y selector de zona en Settings.
- **Riesgo**: bajo. No cambia el esquema de datos.
