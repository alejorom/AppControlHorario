# Proposal: Setup Inicial y Autenticación

## Why
El proyecto parte de cero (greenfield). Antes de construir el registro de jornadas,
cálculos y reportes, se necesita la base técnica: proyecto configurado, conexión a
Supabase y el flujo completo de autenticación funcionando, ya que todas las demás
capabilities dependen de tener un usuario autenticado.

## What Changes
- Inicializar proyecto frontend (React/Next.js + Tailwind CSS)
- Configurar proyecto en Supabase (base de datos + Auth)
- Crear esquema inicial de base de datos: `profiles`, `work_sessions`, `breaks`
- Configurar políticas RLS para las tres tablas
- Implementar capability `autenticacion` completa:
  - Registro de usuario
  - Inicio de sesión
  - Recuperación de contraseña
  - Cierre de sesión
- Configurar navegación básica y layout de la aplicación (shell vacío del dashboard)

## Impact
- **Capabilities afectadas**: `autenticacion` (ADDED — implementación completa según spec)
- **Specs de referencia**: `openspec/specs/autenticacion/spec.md`
- **Fuera de alcance en este change**: registro de jornadas, cálculos, reportes y perfil
  (se abordarán en changes posteriores: `002-registro-jornadas`, `003-calculos-y-reportes`, etc.)
- **Riesgo**: bajo. No hay código previo que modificar (proyecto nuevo).