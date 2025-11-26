IF OBJECT_ID(N'dbo.usp_FindingSeverity_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FindingSeverity_GetById;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FindingSeverity_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, [Description]
    FROM dbo.FindingSeverities
    WHERE Id = @Id;
END
GO
