CREATE OR ALTER PROCEDURE dbo.usp_Audit_GetList
    @StatusId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, Area, StartDate, EndDate, StatusId, ResponsibleId
    FROM dbo.Audits
    WHERE (@StatusId IS NULL OR StatusId = @StatusId)
    ORDER BY StartDate DESC;
END
GO
