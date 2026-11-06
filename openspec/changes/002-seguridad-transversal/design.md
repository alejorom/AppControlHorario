# Design: Seguridad transversal

## Decisiones

### 1. Identidad en la API
- Los endpoints dejan de recibir `{userId}` y lo obtienen del claim `NameIdentifier` del token. Se agrega un método de apoyo en el controlador (o un `ICurrentUser` en Application) que lee ese claim y falla con 401 si no existe.
- Autenticación por JWT Bearer, con la clave o autoridad en configuración (User Secrets o variables de entorno).
- La política por defecto exige usuario autenticado en todos los endpoints (`FallbackPolicy`), de modo que un endpoint nuevo nace protegido.
- No se implementa la emisión de tokens: sin cliente, no hay quién los pida. Esto es una decisión consciente y queda documentada en el proposal.
- Las rutas cambian (por ejemplo `POST /api/sessions/clock-in`), lo que rompe a cualquier consumidor externo; hoy no hay ninguno.

### 2. Autorización por defecto en Blazor
- `AddAuthorizationBuilder().SetFallbackPolicy(...)` con `RequireAuthenticatedUser()`.
- Se marcan con `[AllowAnonymous]` las páginas de cuenta (Login, Register, Logout, ForgotPassword, ResetPassword, ConfirmEmail) y Error, y las acciones públicas de `AccountController`.
- `MainLayout` mantiene su redirección como respaldo.

### 3. Aislamiento de datos
- Se conserva el patrón actual: todos los métodos del repositorio reciben el `userId` y filtran por él, y las páginas lo obtienen del claim de la sesión.
- `GetByIdAsync(id)` se reemplaza por `GetByIdAsync(userId, id)`, que filtra por propietario. Hoy no se usa en el código, pero es un riesgo latente.
- No se agregan filtros globales de EF Core: el patrón explícito ya existe en toda la capa y añadir otro mecanismo duplicaría la fuente de verdad.

### 4. Mensajes de error
- Los componentes muestran un mensaje genérico al usuario y registran la excepción con `ILogger`. Se eliminan los `Console.WriteLine` de error.
- El login (`AccountController`) captura los fallos de acceso a datos, como una base de datos inaccesible o un certificado de SQL Server no confiable. Muestra "El servicio no está disponible temporalmente" y registra la excepción. Sin esto, el usuario ve el error crudo en desarrollo o una página genérica en producción sin saber qué ocurrió.
- Los errores de regla de negocio esperados (por ejemplo "Ya existe una jornada activa") sí se muestran al usuario, porque son mensajes pensados para él y no detalles internos.

### 5. Pruebas
- Proyecto xUnit en `tests/GHL.Application.Tests`, con `UseInMemory` o SQLite en memoria para el repositorio.
- Primeras pruebas: un usuario no puede obtener, modificar ni cerrar sesiones de otro.
