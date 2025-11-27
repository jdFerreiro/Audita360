-- Post-deployment aggregator: incluye vistas, luego procedimientos, luego seeds
-- Ejecutar como único Post-Deployment script para evitar la validación/compilación de SSDT
-- Rutas relativas desde este archivo (ubicado en Scripts/Post-Deployment)

SET NOCOUNT ON;

-- Crear Base de datos y tablas si no existe (para entornos de prueba)
:r "..\Pre-Deployment\Create_Database.sql"
:r "..\Pre-Deployment\Create_Schema_Entities.sql" 

-- Vistas
:r "..\Views\vw_AuditFinalizedSummary.sql"

GO

-- Procedimientos almacenados (orden aproximado según .sqlproj)
:r "..\Store Procedures\usp_Audit_Create.sql"
:r "..\Store Procedures\usp_Audit_Delete.sql"
:r "..\Store Procedures\usp_AuditStatus_Create.sql"
:r "..\Store Procedures\usp_AuditStatus_Update.sql"
:r "..\Store Procedures\usp_AuditStatus_GetList.sql"
:r "..\Store Procedures\usp_AuditStatus_GetById.sql"
:r "..\Store Procedures\usp_AuditStatus_Delete.sql"
:r "..\Store Procedures\usp_Audit_Update.sql"
:r "..\Store Procedures\usp_Audit_GetList.sql"
:r "..\Store Procedures\usp_Audit_GetById.sql"

:r "..\Store Procedures\usp_User_Create.sql"
:r "..\Store Procedures\usp_User_Update.sql"
:r "..\Store Procedures\usp_User_GetList.sql"
:r "..\Store Procedures\usp_User_GetById.sql"
:r "..\Store Procedures\usp_User_Delete.sql"

:r "..\Store Procedures\usp_Role_Create.sql"
:r "..\Store Procedures\usp_Role_Update.sql"
:r "..\Store Procedures\usp_Role_GetList.sql"
:r "..\Store Procedures\usp_Role_GetById.sql"
:r "..\Store Procedures\usp_Role_Delete.sql"

:r "..\Store Procedures\usp_Responsible_Create.sql"
:r "..\Store Procedures\usp_Responsible_Update.sql"
:r "..\Store Procedures\usp_Responsible_GetList.sql"
:r "..\Store Procedures\usp_Responsible_GetById.sql"
:r "..\Store Procedures\usp_Responsible_Delete.sql"

:r "..\Store Procedures\usp_FollowUp_Create.sql"
:r "..\Store Procedures\usp_FollowUp_Update.sql"
:r "..\Store Procedures\usp_FollowUp_GetList.sql"
:r "..\Store Procedures\usp_FollowUp_GetById.sql"
:r "..\Store Procedures\usp_FollowUp_Delete.sql"

:r "..\Store Procedures\usp_FollowUpStatus_Create.sql"
:r "..\Store Procedures\usp_FollowUpStatus_Update.sql"
:r "..\Store Procedures\usp_FollowUpStatus_GetList.sql"
:r "..\Store Procedures\usp_FollowUpStatus_Delete.sql"

:r "..\Store Procedures\usp_Finding_Create.sql"
:r "..\Store Procedures\usp_Finding_Update.sql"
:r "..\Store Procedures\usp_Finding_GetList.sql"
:r "..\Store Procedures\usp_Finding_GetById.sql"
:r "..\Store Procedures\usp_Finding_Delete.sql"

:r "..\Store Procedures\usp_FindingType_Create.sql"
:r "..\Store Procedures\usp_FindingType_Update.sql"
:r "..\Store Procedures\usp_FindingType_GetList.sql"
:r "..\Store Procedures\usp_FindingType_GetById.sql"
:r "..\Store Procedures\usp_FindingType_Delete.sql"

:r "..\Store Procedures\usp_FindingSeverity_Create.sql"
:r "..\Store Procedures\usp_FindingSeverity_Update.sql"
:r "..\Store Procedures\usp_FindingSeverity_GetList.sql"
:r "..\Store Procedures\usp_FindingSeverity_GetById.sql"
:r "..\Store Procedures\usp_FindingSeverity_Delete.sql"

GO

-- Seeds (referenciado desde aquí)
:r "..\Seed Data\Script.LookupData.sql"

GO

PRINT N'Post-deployment aggregator finished.'
