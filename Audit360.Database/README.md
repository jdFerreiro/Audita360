# Proyecto `Audit360.Database` — Estado y ejecución de scripts

Este proyecto de base de datos está activo y contiene todos los scripts necesarios para crear y poblar la base de datos del sistema Audit360. A continuación se describe qué se ha creado y cómo ejecutar los scripts de forma ordenada.

## Qué incluye (resumen)
- Scripts de datos iniciales (seed) para tablas de referencia, roles y un usuario administrador.
- Scripts para la creación de procedimientos almacenados.
- Scripts para la creación de vistas.
- Un paquete desplegable (`.dacpac`) si se genera desde la build del proyecto (opcional).

## Orden recomendado de ejecución
1. Vistas, funciones y triggers
2. Procedimientos almacenados
3. Scripts de datos (`seed`)

Seguir este orden evita errores por dependencias entre objetos.

## Métodos para ejecutar los scripts
A continuación se describen formas habituales de desplegar los scripts.

- Visual Studio (recomendado para desarrollo):
  1. Abrir la solución en Visual Studio.
  2. Abrir el proyecto `Audit360.Database`.
  3. Click derecho en el proyecto -> `Publish`.
  4. Configurar la conexión de destino (servidor y base de datos) y las opciones de publicación.
  5. Ejecutar `Publish` para desplegar el `.dacpac` o los scripts.

- SQL Server Management Studio (SSMS):
  1. Conectar al servidor destino.
  2. Abrir cada archivo `.sql` en el orden recomendado.
  3. Ejecutar los scripts o usar la opción `SQLCMD Mode` para scripts con variables.

- Línea de comandos con `sqlcmd` (ejemplo):
  `sqlcmd -S <servidor> -d <base_datos> -U <usuario> -P <contraseña> -i <ruta_al_script>.sql`

- Despliegue con `SqlPackage` usando `.dacpac` (ejemplo):
  `SqlPackage /Action:Publish /SourceFile:<ruta>.dacpac /TargetServerName:<servidor> /TargetDatabaseName:<base_datos> /TargetUser:<usuario> /TargetPassword:<contraseña>`

## Variables y configuración
- Revisar si los scripts usan variables (p. ej. `SQLCMD` variables) y definirlas al ejecutar.
- Para entornos (Dev/Test/Prod) mantener ficheros de configuración o perfiles de publicación separados.

## Recomendaciones y buenas prácticas
- Ejecutar los scripts en una base de datos de pruebas antes de producción.
- Agrupar cambios y versionarlos en el control de código.
- Hacer copias de seguridad antes de ejecutar en entornos productivos.
- Ejecutar los `seed` solo cuando se requiera; evitar duplicados con comprobaciones `IF NOT EXISTS`.
