# Tasks: Seguridad transversal

## 1. API protegida (GHL.WebApi)
- [ ] 1.1 Configurar la autenticación JWT Bearer con clave o autoridad leída de configuración fuera del repositorio
- [ ] 1.2 Agregar `UseAuthentication()` antes de `UseAuthorization()` y una política por defecto que exige usuario autenticado
- [ ] 1.3 Quitar `{userId}` de las rutas de `SessionsController` y obtener el usuario del claim `NameIdentifier`
- [ ] 1.4 Definir en Swagger el esquema Bearer para poder probar con token
- [ ] 1.5 Mover los orígenes de CORS a configuración
- [ ] 1.6 Verificar los 2 scenarios de "API Autenticada"

## 2. Autorización por defecto (GHL.BlazorApp)
- [ ] 2.1 Definir la política por defecto que exige usuario autenticado en `Program.cs`
- [ ] 2.2 Marcar con `[AllowAnonymous]` las páginas de cuenta y Error, y las acciones públicas de `AccountController`
- [ ] 2.3 Verificar que Login, Register y las páginas de recuperación siguen abiertas para un usuario anónimo
- [ ] 2.4 Verificar los 2 scenarios de "Páginas Protegidas por Defecto"

## 3. Aislamiento de datos
- [ ] 3.1 Cambiar `GetByIdAsync` a `GetByIdAsync(userId, id)` filtrando por propietario en repositorio e interfaz
- [ ] 3.2 Revisar que todos los métodos del servicio y del repositorio filtren por el `userId` recibido
- [ ] 3.3 Verificar los 2 scenarios de "Aislamiento de Datos por Usuario"

## 4. Mensajes de error
- [ ] 4.1 Reemplazar `ex.Message` por un mensaje genérico en Register, Settings y Home, registrando la excepción con `ILogger`
- [ ] 4.2 Reemplazar los `Console.WriteLine` de error en History y Statistics por `ILogger` y mostrar un mensaje al usuario
- [ ] 4.3 Mantener visibles los mensajes de las reglas de negocio de jornadas y pausas
- [ ] 4.4 Verificar los 2 scenarios de "Mensajes de Error sin Detalles Internos"

## 5. Pruebas
- [ ] 5.1 Crear el proyecto `tests/GHL.Application.Tests` (xUnit) y agregarlo a `GHL.sln`
- [ ] 5.2 Escribir pruebas de aislamiento: un usuario no puede obtener, iniciar pausa ni cerrar la jornada de otro
- [ ] 5.3 Escribir pruebas de las reglas de jornada: doble entrada, doble pausa y salida con pausa abierta
