# Tasks: Setup Inicial y Autenticación

## Configuración de proyecto
- [ ] Inicializar repositorio Git
- [ ] Crear proyecto Next.js con Tailwind CSS
- [ ] Configurar ESLint y Prettier
- [ ] Instalar `@supabase/supabase-js`

## Supabase
- [ ] Crear proyecto en Supabase
- [ ] Crear tabla `profiles` (id, full_name, avatar_url, created_at, updated_at)
- [ ] Crear tabla `work_sessions` (id, user_id, clock_in, clock_out, total_worked_minutes, total_break_minutes)
- [ ] Crear tabla `breaks` (id, session_id, start_time, end_time, duration_minutes)
- [ ] Configurar políticas RLS en las tres tablas (usuario solo accede a sus propios registros)
- [ ] Configurar templates de email (verificación y recuperación de contraseña)

## Autenticación (según openspec/specs/autenticacion/spec.md)
- [ ] Pantalla y formulario de registro
- [ ] Pantalla y formulario de login
- [ ] Flujo de recuperación de contraseña
- [ ] Botón y lógica de logout
- [ ] Middleware/guard de rutas protegidas
- [ ] Validar los 3 scenarios de "Registro de Usuario"
- [ ] Validar los 3 scenarios de "Inicio de Sesión"
- [ ] Validar los 2 scenarios de "Recuperación de Contraseña"
- [ ] Validar el scenario de "Cierre de Sesión"

## Shell de aplicación
- [ ] Layout general (navbar, sidebar si aplica)
- [ ] Ruta de dashboard vacía (placeholder para siguiente change)