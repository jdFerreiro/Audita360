IF OBJECT_ID(N'dbo.usp_User_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_User_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_User_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, Email, FullName, IsActive, CreatedAt FROM dbo.Users ORDER BY Username;
END
GO
