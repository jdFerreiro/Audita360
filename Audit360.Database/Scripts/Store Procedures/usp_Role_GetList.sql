IF OBJECT_ID(N'dbo.usp_Role_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Role_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Role_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Description FROM dbo.Roles ORDER BY Name;
END
