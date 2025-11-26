CREATE OR ALTER PROCEDURE dbo.usp_AuditStatus_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description
    FROM dbo.AuditStatuses
    WHERE Id = @Id;
END
GO
