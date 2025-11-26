IF OBJECT_ID(N'dbo.usp_User_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_User_GetById;
GO

CREATE OR ALTER PROCEDURE dbo.usp_User_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Username, Email, FullName, IsActive, CreatedAt
    FROM dbo.Users
    WHERE Id = @Id;
END
GO
