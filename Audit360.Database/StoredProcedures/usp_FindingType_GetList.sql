CREATE OR ALTER PROCEDURE dbo.usp_FindingType_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FindingTypes ORDER BY Id;
END
GO
