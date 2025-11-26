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
  5. Ejecutar `Generate Script`.
  6. Eliminar todas las instrucciones que defines las variables (p. ej. `:setvar`).
  7. Cambiar la línea que tiene USE [$(DatabaseName)]; por USE [Audita360]; o el nombre de la base de datos destino.
  8. Reemplaza los GO; por GO.
  9. Abre una nueva consulta en SSMS.
  10. Copia el contenido actualizado a la consulta en SSMS.
  11. Ejecuta el script en SSMS.
  12. Verifica que todo se haya ejecutado correctamente.
	1. Revisa los mensajes de salida en SSMS para detectar errores.
	2. Consulta las tablas que tengan los datos correctos.
	3. Verifica la existencia de procedimientos y vistas creadas.
