CREATE OR ALTER PROCEDURE dbo.usp_Audit_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, Area, StartDate, EndDate, StatusId, ResponsibleId
    FROM dbo.Audits
    WHERE Id = @Id;
END
GO
