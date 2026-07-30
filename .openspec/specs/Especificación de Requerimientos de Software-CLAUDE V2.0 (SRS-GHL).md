# **Especificación de Requerimientos de Software (SRS)**

## **Sistema de Gestión de Horarios Laborales**

**Versión:** 1.0  
 **Fecha:** 20 de enero de 2026  
 **Proyecto:** Aplicación Web de Control de Horarios

---

## **1\. Introducción**

### **1.1 Propósito**

Este documento describe los requerimientos funcionales y no funcionales para el desarrollo de una aplicación web moderna de gestión de horarios laborales. El sistema permitirá a los usuarios registrar sus jornadas de trabajo, gestionar pausas y generar reportes de tiempo laborado.

### **1.2 Alcance**

La aplicación proporcionará una solución completa para el control de tiempo laboral, incluyendo registro de entradas y salidas, gestión de pausas, cálculo automático de horas trabajadas y generación de reportes visuales. El sistema se implementará como aplicación web con backend en Supabase.

### **1.3 Definiciones y Acrónimos**

* **SRS**: Especificación de Requerimientos de Software  
* **UI**: User Interface (Interfaz de Usuario)  
* **UX**: User Experience (Experiencia de Usuario)  
* **RLS**: Row Level Security (Seguridad a Nivel de Fila)  
* **PWA**: Progressive Web App

---

## **2\. Descripción General**

### **2.1 Perspectiva del Producto**

Sistema web independiente que utiliza Supabase como plataforma backend para autenticación, base de datos y almacenamiento. La aplicación será accesible desde navegadores web en dispositivos desktop, tablet y móvil.

### **2.2 Funciones del Producto**

* Autenticación y gestión de usuarios  
* Registro de entradas y salidas laborales  
* Gestión de pausas durante la jornada  
* Cálculo automático de horas trabajadas  
* Generación de reportes visuales  
* Dashboard con estadísticas de tiempo trabajado  
* Histórico de jornadas laborales

### **2.3 Características de Usuarios**

* **Usuario Final**: Empleados que necesitan registrar sus horarios laborales  
* **Administrador**: Supervisores que pueden visualizar reportes de equipos (fase posterior)

### **2.4 Restricciones**

* Debe utilizar Supabase como backend exclusivo  
* Debe ser una aplicación web responsive  
* Debe funcionar en navegadores modernos (Chrome, Firefox, Safari, Edge)  
* No requiere funcionalidad offline en versión inicial

---

## **3\. Requerimientos Funcionales**

### **3.1 Módulo de Autenticación**

#### **RF-001: Registro de Usuario**

**Prioridad**: Alta  
 **Descripción**: El sistema debe permitir a nuevos usuarios crear una cuenta utilizando email y contraseña.

**Criterios de Aceptación**:

* El usuario puede registrarse con email y contraseña  
* El email debe ser único en el sistema  
* La contraseña debe tener mínimo 8 caracteres  
* La contraseña debe contener al menos una mayúscula. 
* La contraseña debe contener al menos una minúscula. 
* La contraseña debe contener al menos un número. 
* La contraseña debe contener al menos un caracter especial. 
* El sistema debe enviar email de verificación  
* El usuario debe confirmar su email antes de acceder al sistema

#### **RF-002: Inicio de Sesión**

**Prioridad**: Alta  
 **Descripción**: Los usuarios registrados deben poder autenticarse en el sistema.

**Criterios de Aceptación**:

* Login con email y contraseña  
* Validación de credenciales contra Supabase Auth  
* Redirección al dashboard después de login exitoso  
* Mensaje de error claro en caso de credenciales incorrectas  
* Opción de "Recordar sesión"

#### **RF-003: Recuperación de Contraseña**

**Prioridad**: Media  
 **Descripción**: El sistema debe permitir recuperar contraseñas olvidadas.

**Criterios de Aceptación**:

* Opción "Olvidé mi contraseña" en pantalla de login  
* Envío de email con enlace de recuperación  
* Formulario para establecer nueva contraseña  
* Expiración del enlace después de 24 horas

#### **RF-004: Cierre de Sesión**

**Prioridad**: Alta  
 **Descripción**: Los usuarios deben poder cerrar sesión de forma segura.

**Criterios de Aceptación**:

* Botón de logout visible en la interfaz  
* Limpieza completa de sesión  
* Redirección a página de login

### **3.2 Módulo de Registro de Jornadas**

#### **RF-005: Marcar Entrada**

**Prioridad**: Alta  
 **Descripción**: El usuario debe poder registrar el inicio de su jornada laboral.

**Criterios de Aceptación**:

* Botón prominente "Marcar Entrada" en dashboard  
* Registro de fecha y hora exacta automáticamente  
* Confirmación visual del registro  
* No se puede marcar entrada si ya existe una jornada activa  
* El registro debe guardarse en la base de datos de Supabase

#### **RF-006: Marcar Salida**

**Prioridad**: Alta  
 **Descripción**: El usuario debe poder registrar el fin de su jornada laboral.

**Criterios de Aceptación**:

* Botón "Marcar Salida" visible solo cuando hay jornada activa  
* Registro de fecha y hora exacta automáticamente  
* Cálculo automático de horas trabajadas  
* Cierre de la jornada actual  
* Actualización del registro en base de datos

#### **RF-007: Iniciar Pausa**

**Prioridad**: Alta  
 **Descripción**: Durante una jornada activa, el usuario debe poder iniciar pausas.

**Criterios de Aceptación**:

* Botón "Iniciar Pausa" disponible durante jornada activa  
* Registro de hora de inicio de pausa  
* Estado visual que indica pausa activa  
* Solo puede haber una pausa activa a la vez

#### **RF-008: Finalizar Pausa**

**Prioridad**: Alta  
 **Descripción**: El usuario debe poder finalizar una pausa activa.

**Criterios de Aceptación**:

* Botón "Finalizar Pausa" visible durante pausa activa  
* Registro de hora de fin de pausa  
* Cálculo automático de duración de pausa  
* Actualización de tiempo total en pausa  
* Vuelta al estado de jornada activa

#### **RF-009: Visualizar Jornada Actual**

**Prioridad**: Alta  
 **Descripción**: El usuario debe ver información en tiempo real de su jornada actual.

**Criterios de Aceptación**:

* Mostrar hora de entrada  
* Mostrar tiempo transcurrido  
* Mostrar tiempo total en pausas  
* Mostrar tiempo trabajado efectivo  
* Actualización en tiempo real cada minuto

### **3.3 Módulo de Cálculos Automáticos**

#### **RF-010: Cálculo de Horas Trabajadas**

**Prioridad**: Alta  
 **Descripción**: El sistema debe calcular automáticamente las horas trabajadas en cada jornada.

**Criterios de Aceptación**:

* Cálculo: Hora salida \- Hora entrada \- Tiempo total pausas  
* Formato en horas y minutos (ej: 8h 30m)  
* Precisión de 1 minuto  
* Cálculo inmediato al marcar salida

#### **RF-011: Cálculo de Tiempo en Pausas**

**Prioridad**: Alta  
 **Descripción**: El sistema debe sumar todas las pausas de una jornada.

**Criterios de Aceptación**:

* Suma total de todas las pausas del día  
* Formato en horas y minutos  
* Actualización después de cada pausa

#### **RF-012: Cálculo de Estadísticas Semanales**

**Prioridad**: Media  
 **Descripción**: El sistema debe calcular totales semanales de tiempo trabajado.

**Criterios de Aceptación**:

* Total de horas trabajadas en la semana  
* Promedio diario de la semana  
* Cálculo automático cada domingo

#### **RF-013: Cálculo de Estadísticas Mensuales**

**Prioridad**: Media  
 **Descripción**: El sistema debe calcular totales mensuales de tiempo trabajado.

**Criterios de Aceptación**:

* Total de horas del mes  
* Promedio diario del mes  
* Días trabajados en el mes  
* Actualización al cambiar de mes

### **3.4 Módulo de Reportes**

#### **RF-014: Historial de Jornadas**

**Prioridad**: Alta  
 **Descripción**: El usuario debe poder visualizar el historial completo de sus jornadas.

**Criterios de Aceptación**:

* Lista de todas las jornadas registradas  
* Mostrar fecha, entrada, salida, pausas y total trabajado  
* Ordenamiento por fecha (más reciente primero)  
* Paginación o scroll infinito  
* Filtros por rango de fechas

#### **RF-015: Reporte Semanal**

**Prioridad**: Media  
 **Descripción**: Generar reporte visual del tiempo trabajado en la semana.

**Criterios de Aceptación**:

* Gráfico de barras con horas por día  
* Total de horas de la semana  
* Comparación con semana anterior  
* Selector de semana  
* Opción de exportar a PDF

#### **RF-016: Reporte Mensual**

**Prioridad**: Media  
 **Descripción**: Generar reporte visual del tiempo trabajado en el mes.

**Criterios de Aceptación**:

* Gráfico de barras o líneas con horas por día  
* Total de horas del mes  
* Días trabajados vs días totales  
* Selector de mes y año  
* Opción de exportar a PDF

#### **RF-017: Dashboard de Estadísticas**

**Prioridad**: Media  
 **Descripción**: Mostrar resumen visual de estadísticas principales.

**Criterios de Aceptación**:

* Horas trabajadas hoy  
* Horas trabajadas esta semana  
* Horas trabajadas este mes  
* Promedio diario del mes  
* Gráficos visuales atractivos

#### **RF-018: Exportar Datos**

**Prioridad**: Baja  
 **Descripción**: Permitir exportar los datos de jornadas en formatos estándar.

**Criterios de Aceptación**:

* Exportar a CSV  
* Exportar a PDF  
* Incluir filtros de fecha en exportación  
* Nombre de archivo con fecha de generación

### **3.5 Módulo de Perfil de Usuario**

#### **RF-019: Visualizar Perfil**

**Prioridad**: Baja  
 **Descripción**: El usuario puede ver su información de perfil.

**Criterios de Aceptación**:

* Mostrar nombre completo  
* Mostrar email  
* Mostrar fecha de registro

#### **RF-020: Editar Perfil**

**Prioridad**: Baja  
 **Descripción**: El usuario puede actualizar su información personal.

**Criterios de Aceptación**:

* Editar nombre completo  
* Cambiar contraseña  
* Validación de nueva contraseña  
* Confirmación de cambios guardados

---

## **4\. Requerimientos No Funcionales**

### **4.1 Rendimiento**

#### **RNF-001: Tiempo de Carga**

El sistema debe cargar la página principal en menos de 3 segundos con conexión de banda ancha estándar.

#### **RNF-002: Tiempo de Respuesta**

Las operaciones de registro de entrada/salida/pausas deben completarse en menos de 2 segundos.

#### **RNF-003: Capacidad Concurrente**

El sistema debe soportar al menos 100 usuarios concurrentes sin degradación significativa del rendimiento.

### **4.2 Seguridad**

#### **RNF-004: Autenticación**

Toda la autenticación debe manejarse a través de Supabase Auth con tokens JWT seguros.

#### **RNF-005: Protección de Datos**

Implementar Row Level Security (RLS) en Supabase para que los usuarios solo accedan a sus propios datos.

#### **RNF-006: Comunicación Segura**

Todas las comunicaciones entre cliente y servidor deben usar HTTPS.

#### **RNF-007: Validación de Datos**

Toda entrada de usuario debe ser validada tanto en cliente como en servidor.

### **4.3 Usabilidad**

#### **RNF-008: Diseño Responsive**

La interfaz debe ser completamente funcional en dispositivos con resoluciones desde 320px hasta 2560px de ancho.

#### **RNF-009: Navegadores Soportados**

Compatibilidad con las últimas dos versiones de Chrome, Firefox, Safari y Edge.

#### **RNF-010: Accesibilidad**

La aplicación debe cumplir con estándares WCAG 2.1 nivel AA para accesibilidad web.

#### **RNF-011: Interfaz Intuitiva**

Los usuarios deben poder registrar entrada/salida sin necesidad de tutorial o documentación.

### **4.4 Disponibilidad**

#### **RNF-012: Tiempo de Actividad**

El sistema debe mantener 99% de disponibilidad, aprovechando la infraestructura de Supabase.

#### **RNF-013: Backup**

Supabase debe configurarse con backups automáticos diarios de la base de datos.

### **4.5 Mantenibilidad**

#### **RNF-014: Código Limpio**

El código debe seguir estándares de la industria y mejores prácticas del framework elegido.

#### **RNF-015: Documentación**

El código debe incluir comentarios descriptivos y documentación técnica de configuración.

#### **RNF-016: Versionamiento**

El código fuente debe mantenerse en sistema de control de versiones Git.

### **4.6 Escalabilidad**

#### **RNF-017: Arquitectura Modular**

El sistema debe diseñarse con componentes modulares para facilitar futuras expansiones.

#### **RNF-018: Base de Datos**

La estructura de base de datos debe permitir crecimiento sin reestructuraciones mayores.

---

## **5\. Especificaciones de Base de Datos**

### **5.1 Esquema de Tablas en Supabase**

#### **Tabla: profiles**

\- id (uuid, PK, FK a auth.users)  
\- full\_name (text)  
\- avatar\_url (text, nullable)  
\- created\_at (timestamp)  
\- updated\_at (timestamp)

#### **Tabla: work\_sessions**

\- id (uuid, PK)  
\- user\_id (uuid, FK a auth.users)  
\- clock\_in (timestamp)  
\- clock\_out (timestamp, nullable)  
\- total\_worked\_minutes (integer, nullable)  
\- total\_break\_minutes (integer, nullable)  
\- created\_at (timestamp)  
\- updated\_at (timestamp)

#### **Tabla: breaks**

\- id (uuid, PK)  
\- session\_id (uuid, FK a work\_sessions)  
\- start\_time (timestamp)  
\- end\_time (timestamp, nullable)  
\- duration\_minutes (integer, nullable)  
\- created\_at (timestamp)

### **5.2 Políticas RLS**

Cada tabla debe tener políticas RLS que aseguren:

* Los usuarios solo pueden leer sus propios registros  
* Los usuarios solo pueden crear registros asociados a su user\_id  
* Los usuarios solo pueden actualizar sus propios registros  
* Los usuarios solo pueden eliminar sus propios registros

---

## **6\. Especificaciones de Interfaz de Usuario**

### **6.1 Paleta de Colores**

* Colores primarios modernos y profesionales  
* Alto contraste para accesibilidad  
* Modo claro como estándar (modo oscuro en fase posterior)

### **6.2 Tipografía**

* Fuente sans-serif moderna y legible  
* Tamaños responsive que se ajusten a diferentes dispositivos

### **6.3 Componentes Principales**

#### **Dashboard Principal**

* Tarjeta de estado actual (En trabajo / En pausa / Fuera de jornada)  
* Botones principales de acción (Entrada/Salida/Pausa)  
* Resumen de tiempo trabajado hoy  
* Widget de estadísticas rápidas

#### **Pantalla de Historial**

* Tabla o lista de jornadas  
* Filtros de fecha  
* Indicadores visuales de horas trabajadas

#### **Pantalla de Reportes**

* Gráficos interactivos  
* Selectores de período  
* Métricas destacadas

---

## **7\. Stack Tecnológico Recomendado**

#### **7.1 Frontend (El "Canvas" del Agente)**

* **Framework:** **Blazor WebAssembly** o **Server** (ideal para que el agente maneje lógica en C\# tanto en cliente como en servidor).  
* **Librerías de UI:** **MudBlazor** o **Radzen** (componentes pre-hechos que los agentes saben implementar muy rápido).  
* **Interacción:** Agentes de Antigravity especializados en UX para validar la accesibilidad y el diseño en el navegador integrado.

#### **7.2 Backend (Lógica de Negocio y Orquestación)**

* **Arquitectura:** **Web API** con **.NET 8+**.  
* **Patrón:** **Clean Architecture** (puedes pedirle al Agente de Antigravity que cree las capas de *Domain, Application, Infrastructure y Web API* desde el inicio).  
* **Integraciones:** Configuración de clientes para **servicios SOAP** (vía Axway) gestionada por agentes de integración.

#### **7.3 Datos (Persistencia)**

* **ORM:** **Entity Framework Core**.  
* **Bases de Datos:** **SQL Server** o **Oracle**. En Antigravity, puedes pedirle a un agente que analice tus esquemas actuales de base de datos para generar automáticamente las clases de entidad y el `DbContext`.

#### **7.4 DevOps y Despliegue (Gestión de Hitos)**

* **Control de Versiones:** Los agentes de Antigravity pueden realizar *commits* y gestionar ramas basados en el cumplimiento de los **hitos (Milestones)** que tú definas.  
* **Terminal:** El agente puede ejecutar comandos de `dotnet CLI` (migraciones, builds, tests) de forma autónoma para verificar que cada cambio es funcional.

---

## **8\. Casos de Uso Principales**

### **8.1 Caso de Uso: Registrar Jornada Completa**

**Actor**: Usuario autenticado

**Flujo Principal**:

1. Usuario inicia sesión en la aplicación  
2. Usuario ve dashboard con botón "Marcar Entrada"  
3. Usuario hace clic en "Marcar Entrada"  
4. Sistema registra hora actual como inicio de jornada  
5. Usuario trabaja durante varias horas  
6. Usuario hace clic en "Iniciar Pausa"  
7. Sistema registra inicio de pausa  
8. Usuario hace clic en "Finalizar Pausa"  
9. Sistema registra fin de pausa y calcula duración  
10. Al finalizar el día, usuario hace clic en "Marcar Salida"  
11. Sistema registra hora de salida, calcula total trabajado y muestra resumen

**Postcondición**: Jornada completa registrada con entrada, salida, pausas y cálculo de horas trabajadas.

### **8.2 Caso de Uso: Consultar Reporte Semanal**

**Actor**: Usuario autenticado

**Flujo Principal**:

1. Usuario navega a sección "Reportes"  
2. Usuario selecciona "Reporte Semanal"  
3. Sistema muestra gráfico con horas trabajadas por día  
4. Sistema muestra total de horas de la semana  
5. Usuario puede cambiar de semana usando controles  
6. Usuario puede exportar reporte a PDF

**Postcondición**: Usuario visualiza y puede exportar sus estadísticas semanales.

---

## **9\. Criterios de Aceptación del Proyecto**

El proyecto se considerará completo cuando:

1. Todos los requerimientos funcionales de prioridad Alta estén implementados y probados  
2. La aplicación sea completamente responsive en dispositivos móviles, tablets y desktop  
3. La autenticación con Supabase funcione correctamente  
4. Los cálculos automáticos de tiempo sean precisos  
5. Los usuarios puedan registrar entrada, pausas y salida sin errores  
6. El historial de jornadas se muestre correctamente  
7. Al menos un tipo de reporte visual esté implementado  
8. La aplicación pase pruebas de seguridad básicas  
9. El código esté documentado y en repositorio de control de versiones

---

## **10\. Fases de Desarrollo Sugeridas**

### **Fase 1: Fundamentos (Sprint 1-2)**

* Configuración de proyecto y Supabase  
* Sistema de autenticación completo  
* Estructura de base de datos  
* UI básica y navegación

### **Fase 2: Funcionalidad Core (Sprint 3-4)**

* Registro de entrada/salida  
* Gestión de pausas  
* Cálculos automáticos  
* Vista de jornada actual

### **Fase 3: Reportes y Visualización (Sprint 5-6)**

* Historial de jornadas  
* Dashboard con estadísticas  
* Reportes semanales y mensuales  
* Gráficos visuales

### **Fase 4: Refinamiento (Sprint 7\)**

* Exportación de datos  
* Optimizaciones de rendimiento  
* Corrección de bugs  
* Mejoras de UX

---

## **11\. Riesgos y Mitigaciones**

### **Riesgo 1: Limitaciones de Plan Gratuito de Supabase**

**Mitigación**: Monitorear uso de recursos y optimizar queries. Planificar upgrade si es necesario.

### **Riesgo 2: Precisión en Cálculos de Tiempo**

**Mitigación**: Implementar pruebas unitarias exhaustivas para funciones de cálculo. Usar bibliotecas probadas para manejo de fechas.

### **Riesgo 3: Experiencia en Móviles**

**Mitigación**: Diseño mobile-first y testing continuo en dispositivos reales.

### **Riesgo 4: Zonas Horarias**

**Mitigación**: Almacenar todos los timestamps en UTC y convertir a zona horaria local del usuario en la interfaz.

---

## **12\. Apéndices**

### **A. Glosario de Términos**

* **Jornada Laboral**: Período de tiempo entre entrada y salida  
* **Pausa**: Interrupción temporal durante la jornada laboral  
* **Tiempo Efectivo**: Tiempo trabajado menos pausas  
* **Dashboard**: Pantalla principal de la aplicación

---

**Fin del Documento**

---

