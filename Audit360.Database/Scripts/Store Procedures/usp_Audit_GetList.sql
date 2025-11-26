IF OBJECT_ID(N'dbo.usp_Audit_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Audit_GetList;
GO

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
