# Audit360 — README

Este documento recoge la información esencial para entender las decisiones técnicas, los supuestos realizados y la guía mínima de despliegue y ejecución de la solución Audit360.

---

## 1. Justificación de decisiones técnicas

- Arquitectura en capas (API / Application / Infrastructure / Domain / Database)
  - Separación de responsabilidades: facilita pruebas unitarias, mantenimiento y reemplazo de implementaciones (repositorios, servicios).

- CQRS ligero con MediatR (Commands / Queries)
  - Centraliza la lógica de negocio en handlers; permite introducir behaviors transversales (validación, logging, caching) sin acoplar controladores.

- DTOs Read/Write y AutoMapper
  - Evita acoplar la API a las entidades de dominio y reduce código de mapeo manual en handlers.

- FluentValidation + ValidationBehavior (pipeline)
  - Valida comandos antes de ejecutar handlers; devuelve errores estructurados y evita duplicación de reglas.

- Repositorios Read/Write (EF Core + SPs cuando aplica)
  - Permite optimizar lecturas y centralizar llamadas a procedimientos almacenados en la base de datos.

- JWT (Bearer) para autenticación
  - Estándar interoperable para proteger endpoints REST y usar en Swagger (Authorise).

- Swagger (Swashbuckle)
  - Documentación automática y punto de entrada para pruebas manuales de la API.

- Middleware de manejo de errores y logging
  - Captura ValidationException y transforma en 400 con payload de errores; registra validaciones fallidas.

Decisiones orientadas a: mantenibilidad, testabilidad y despliegue simple en entornos modernos (.NET 10).

---

## 2. Supuestos realizados

- Base de datos: SQL Server (scripts y procedimientos en `Audit360.Database`).
- JWT secret y otras credenciales se proporcionan mediante variables de entorno o secret store en producción.
- Las contraseñas de usuarios son hasheadas por un servicio de infraestructura (`IPasswordService`).
- No se incluye gestión de identidad completa (IdentityServer/ASP.NET Identity) en esta entrega; la emisión de tokens debe añadirse o simularse para pruebas.
- CI/CD: se asume un pipeline que ejecuta build, tests y despliega artefactos; la infraestructura (k8s, VMs) queda fuera del repositorio.

---

## 3. Guía de despliegue y ejecución

Requisitos previos
- .NET 10 SDK instalado
- SQL Server accesible (local o remoto)
- Opcional: Visual Studio 2022/2026 o VS Code

Variables de configuración (mínimas)
- `ConnectionStrings__DefaultConnection` — cadena de conexión a SQL Server
- `Jwt__Secret` — clave simétrica para firmar JWT (mínimo 32 bytes)
- `Jwt__Issuer` y `Jwt__Audience` — valores configurados para validación de token

Despliegue de base de datos
1. Crear base de datos destino (ej. `Audit360`).
2. Configurar cadena de conexión en variables de entorno o `appsettings.json`.
3. Aplicar migraciones EF Core.
4. Ejecutar los scripts del proyecto `Audit360.Database` en el servidor (orden recomendado: vistas/funciones → procedimientos almacenados → seed de datos).

Ejecución local de la API
1. Establecer variables de entorno o editar `appsettings.Development.json` con la cadena de conexión y opciones JWT.
2. Desde la raíz de la solución ejecutar:
   - `dotnet restore`
   - `dotnet build`
3. Iniciar la API (desde `Audit360.API`):
   - `cd Audit360.API`
   - `dotnet run`
4. Acceder a Swagger UI en `https://localhost:{puerto}/swagger` y probar endpoints. Usar el botón "Authorize" para pegar `Bearer {token}`.

Generar/obtener token para pruebas
- Implementar o utilizar un endpoint de autenticación que valide credenciales y emita JWT firmado con `Jwt__Secret`.
- Para pruebas rápidas puedes usar jwt.io para generar un token con los claims `iss`/`aud` correctos y firmarlo con HS256 + `Jwt__Secret`.

Despliegue en producción (notas rápidas)
- No almacenar secretos en ficheros sin protección: usar Key Vault / Secrets Manager.
- Ejecutar scripts/migraciones de DB en pipeline antes de arrancar la API.
- Configurar HTTPS con certificados válidos y restringir puertos/ACLs.
- Habilitar logging centralizado (Serilog → sinks) y monitoreo (App Insights / Prometheus).

CI/CD (simulado)
- Incluido un flujo de CI/CD simulado en `.github/workflows/ci-cd.yml` que ejecuta:
  1. `dotnet restore`
  2. `dotnet build --configuration Release`
  3. `dotnet test` (unidad)
  4. `dotnet publish` del proyecto `Audit360.API`
  5. sube el artefacto publicado y realiza un paso de "deploy" simulado (copia de los ficheros publicados a carpeta `artifacts/deploy`)

Ejecutar pipeline localmente (simulación)
- Build y pruebas locales:
  - `dotnet restore`
  - `dotnet build -c Release`
  - `dotnet test`
- Publicar API localmente:
  - `dotnet publish ./Audit360.API/Audit360.API.csproj -c Release -o ./artifacts/publish`
- Simular deploy con el helper:
  - `bash tools/ci/deploy.sh`
  - Esto copiará el contenido de `./artifacts/publish` a `./artifacts/deploy_simulated`.

---

## 4. Estructura del repositorio (alto nivel)
- `Audit360.API` — Host HTTP, configuración DI, middlewares y Swagger
- `Audit360.Application` — DTOs, Handlers (MediatR), Validadores, AutoMapper profiles
- `Audit360.Infrastructure` — EF Core DbContext, Repositorios, Servicios infra
- `Audit360.Domain` — Entidades y modelos del dominio
- `Audit360.Database` — Scripts y definición de la base de datos
- `Audit360.UnitTests` / `Audit360.IntegrationTests` — pruebas (unitarias e integración)

---
