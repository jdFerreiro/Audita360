-- Seed lookup data for Audit360
-- Idempotent inserts for Roles, AuditStatus, FindingType, FindingSeverity, FollowUpStatus

-- Roles
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE [Name] = 'User')
    INSERT INTO dbo.Roles ([Name], [Description]) VALUES ('User', 'Rol usuario básico');

IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE [Name] = 'Admin')
    INSERT INTO dbo.Roles ([Name], [Description]) VALUES ('Admin', 'Rol administrador');

-- AuditStatus
IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Pendiente')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Pendiente');
IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'En Proceso')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('En Proceso');
IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Finalizada')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Finalizada');
IF NOT EXISTS (SELECT 1 FROM dbo.AuditStatuses WHERE [Description] = 'Cancelada')
    INSERT INTO dbo.AuditStatuses ([Description]) VALUES ('Cancelada');

-- FindingType
IF NOT EXISTS (SELECT 1 FROM dbo.FindingTypes WHERE [Description] = 'observación')
    INSERT INTO dbo.FindingTypes ([Description]) VALUES ('observación');
IF NOT EXISTS (SELECT 1 FROM dbo.FindingTypes WHERE [Description] = 'no conformidad')
    INSERT INTO dbo.FindingTypes ([Description]) VALUES ('no conformidad');

-- FindingSeverity
IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'baja')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('baja');
IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'media')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('media');
IF NOT EXISTS (SELECT 1 FROM dbo.FindingSeverities WHERE [Description] = 'alta')
    INSERT INTO dbo.FindingSeverities ([Description]) VALUES ('alta');

-- FollowUpStatus
IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'pendiente')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('pendiente');
IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'en curso')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('en curso');
IF NOT EXISTS (SELECT 1 FROM dbo.FollowUpStatuses WHERE [Description] = 'completado')
    INSERT INTO dbo.FollowUpStatuses ([Description]) VALUES ('completado');
