CREATE OR ALTER PROCEDURE dbo.usp_Finding_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description, TypeId, SeverityId, Date, AuditId
    FROM dbo.Findings
    WHERE Id = @Id;
END
GO
