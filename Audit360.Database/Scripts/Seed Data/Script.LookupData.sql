/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Seed lookup data for Audit360
-- Idempotent inserts for Roles, AuditStatus, FindingType, FindingSeverity, FollowUpStatus

SET NOCOUNT ON;

-- Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE [Name] = 'User')
    INSERT INTO dbo.Roles ([Name], [Description]) VALUES ('User', 'Rol usuario básico');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE [Name] = 'Admin')
    INSERT INTO dbo.Roles ([Name], [Description]) VALUES ('Admin', 'Rol administrador');
GO;

-- AuditStatus
IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Pendiente')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Pendiente');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'En Proceso')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('En Proceso');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Finalizada')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Finalizada');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Cancelada')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Cancelada');
GO;

-- FindingType
IF NOT EXISTS (SELECT 1 FROM dbo.FindingTypes WHERE [Description] = 'Observación')
    INSERT INTO dbo.FindingTypes ([Description]) VALUES ('Observación');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.FindingTypes WHERE [Description] = 'No conformidad')
    INSERT INTO dbo.FindingTypes ([Description]) VALUES ('No conformidad');
GO;

-- FindingSeverity
IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'Baja')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('Baja');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'Media')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('Media');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'Alta')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('Alta');
GO;

-- FollowUpStatus
IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'Pendiente')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('Pendiente');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'En curso')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('En curso');
GO;

IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'Completado')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('Completado');
GO;
