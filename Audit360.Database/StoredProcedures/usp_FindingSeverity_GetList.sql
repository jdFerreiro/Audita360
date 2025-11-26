CREATE OR ALTER PROCEDURE dbo.usp_FindingSeverity_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FindingSeverities ORDER BY Id;
END
GO
