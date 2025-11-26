IF OBJECT_ID(N'dbo.usp_Responsible_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Responsible_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Responsible_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Area
    FROM dbo.Responsibles
    ORDER BY Name;
END
GO
