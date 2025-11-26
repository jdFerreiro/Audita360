IF OBJECT_ID(N'dbo.usp_Audit_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Audit_GetById;
GO

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
