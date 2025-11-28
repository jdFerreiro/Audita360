Propuesta de est�ndares t�cnicos

Resumen

Documento que describe est�ndares recomendados para el proyecto Audit360 en las �reas de:
- Nombres y convenciones de c�digo
- Estructura de proyectos y carpetas
- Pruebas (unitarias, integraci�n, contract)
- Seguridad (autenticaci�n, autorizaci�n, manejo de secretos)

1. Convenciones de nombres

- C#:
  - PascalCase para clases, interfaces, enum, m�todos y propiedades.
  - camelCase para par�metros y variables locales.
  - Interfaces comienzan con `I` seguido de PascalCase (p. ej. `IUserRepository`).
  - Nombres de archivos coinciden con la clase p�blica principal (`UserService.cs` contiene `class UserService`).
  - Nombres de espacios de nombres basados en la estructura del proyecto: `Audit360.Infrastructure.Repositories.Write`.
- Base de datos:
  - Tablas en singular o plural (definir uno); usar prefijo opcional `tbl_` no recomendado.
  - Columnas en `PascalCase` o `snake_case` consistente (definir y aplicar).

2. Estructura de proyecto y carpetas

- Capas separadas (ya presentes):
  - `Audit360.Domain` - entidades, valores y l�gica de dominio.
  - `Audit360.Application` - DTOs, comandos, queries, handlers, interfaces.
  - `Audit360.Infrastructure` - implementaci�n de repositorios, acceso a datos, servicios externos.
  - `Audit360.API` - controllers, middleware, composici�n y configuraciones.
- Orden recomendado dentro de cada proyecto:
  - `Features` o `Modules` con subcarpetas por entidad (Users, Audits, Findings).
  - `DTOs` separados en `Read`/`Write` si aplica.
  - `Mapping` para perfiles AutoMapper.

3. Dise�o de APIs y contratos

- Usar RESTful endpoints; verbos HTTP correctos:
  - GET para lectura (200, 404 si no existe)
  - POST para creaci�n (201 Created con Location o 204 si no devuelve body)
  - PUT/PATCH para actualizaciones (204 NoContent o 200 con payload)
  - DELETE para eliminaci�n (204)
- Errores: devolver payload JSON con esquema `{ error: string, detail?: string, code?: string }`.
- Versionado: incluir `/api/v1/` cuando se lance primera versi�n p�blica.

4. Pruebas

- Tipos:
  - Unitarias: cubrir l�gica pura (handlers, servicios puros). Mockear dependencias con `Moq` o similar.
  - Integraci�n: usar `WebApplicationFactory<Program>` para probar endpoints y pipeline de middleware; inyectar `InMemory` o mocks para evitar tocar DB real cuando no sea necesario.
  - Contract/API: tests que validen contra especificaci�n OpenAPI/Swagger.
- Cobertura m�nima recomendada:
  - Unitarias >= 70% en capas cr�ticas.
  - Integraci�n para flujos cr�ticos (autenticaci�n, creaci�n de entidades claves).
- Nombres de tests: `MethodName_StateUnderTest_ExpectedBehavior`.

5. Seguridad

- Autenticaci�n y autorizaci�n:
  - JWT Bearer con tokens firmados (HS256 o RS256 preferido para producci�n).
  - Reglas de roles/claims en endpoints con `[Authorize(Roles = "Admin")]` o pol�ticas.
- Manejo de secretos:
  - No almacenar secretos en c�digo. Usar `User Secrets` en desarrollo y `Azure Key Vault` o similar en producci�n.
- Passwords:
  - Usar hashing fuerte (BCrypt/Argon2) con sal por usuario (ya se usa BCrypt).
  - No almacenar contrase�as en DTOs ni logs; aceptar `Password` en DTOs s�lo en entrada y transformar a `PasswordHash` antes de persistir.
- Validaci�n de entradas:
  - Usar FluentValidation para validar DTOs; centralizar reglas y devolver errores claros.
- Protecci�n contra inyecci�n SQL:
  - Preferir consultas parametrizadas y ORMs. Si se usan stored procedures, validar par�metros y mapear errores a respuestas amigables.

6. Manejo de errores y observabilidad

- Middleware global para:
  - Capturar errores de validaci�n (400)
  - Capturar errores de BD y mapear c�digos SQL a HTTP adecuados (409 para FK/unique, 500 para errores inesperados)
  - Registrar (log) con trazabilidad (correlation id)
- Logs:
  - Uso de `ILogger<T>` con niveles apropiados.
  - No loggear secretos.
- Telemetr�a:
  - Integraci�n con Application Insights / Prometheus seg�n entorno.

7. Revisi�n de c�digo y CI

- Repositorios con branch protection y PRs obligatorios.
- Revisi�n de c�digo por al menos un revisor.
- CI pipeline ejecuta:
  - Build
  - Unit tests
  - Linter/analyzers (p. ej. Roslyn analyzers, StyleCop)
  - Publicaci�n de cobertura de tests

8. Est�ndares de calidad de c�digo

- Formato y estilos con `.editorconfig` y Roslyn analyzers.
- Evitar m�todos largos (>50 LOC), preferir funciones peque�as y testables.
- Documentar APIs p�blicas con XML comments y mantener Swagger actualizado.

9. Plantilla de Pull Request

- Resumen de cambios.
- Issue / historia relacionada.
- C�mo probar localmente.
- Impacto en la base de datos y migraciones.

10. Ejemplos y checklist r�pido

- Crear usuario:
  - DTO m�nimo: `Username, Email, Password, FullName, RoleId`.
  - Validaciones: email formato, password min length 6, roleId > 0.
  - Hash password en handler antes de persistir.

- Tests:
  - Unit: mockear repositorio y password service para `UserCommandHandler`.
  - Integration: usar `WebApplicationFactory` y mockear repositorio para simular excepciones de BD cuando no se quiera depender de DB real.
