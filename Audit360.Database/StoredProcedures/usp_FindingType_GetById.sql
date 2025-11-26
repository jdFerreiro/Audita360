CREATE OR ALTER PROCEDURE dbo.usp_FindingType_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FindingTypes WHERE Id = @Id;
END
GO
