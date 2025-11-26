CREATE OR ALTER PROCEDURE dbo.usp_Responsible_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Area
    FROM dbo.Responsibles
    ORDER BY Name;
END
GO
