CREATE OR ALTER PROCEDURE dbo.usp_User_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, Email, FullName, IsActive, CreatedAt
    FROM dbo.Users
    ORDER BY Username;
END
GO
