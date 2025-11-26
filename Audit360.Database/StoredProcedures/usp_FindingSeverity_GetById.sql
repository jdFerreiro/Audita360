CREATE OR ALTER PROCEDURE dbo.usp_FindingSeverity_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FindingSeverities WHERE Id = @Id;
END
GO
