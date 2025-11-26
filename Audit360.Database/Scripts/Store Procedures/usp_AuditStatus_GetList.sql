IF OBJECT_ID(N'dbo.usp_AuditStatus_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_AuditStatus_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AuditStatus_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.AuditStatuses ORDER BY Id;
END
GO
