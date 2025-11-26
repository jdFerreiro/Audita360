IF OBJECT_ID(N'dbo.usp_Finding_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Finding_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Finding_GetList
    @AuditId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description, TypeId, SeverityId, Date, AuditId
    FROM dbo.Findings
    WHERE (@AuditId IS NULL OR AuditId = @AuditId)
    ORDER BY Date DESC;
END
GO
