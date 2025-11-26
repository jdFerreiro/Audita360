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
