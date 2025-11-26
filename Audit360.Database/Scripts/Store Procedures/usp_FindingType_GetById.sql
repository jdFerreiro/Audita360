IF OBJECT_ID(N'dbo.usp_FindingType_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FindingType_GetById;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FindingType_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, [Description]
    FROM dbo.FindingTypes
    WHERE Id = @Id;
END
GO
