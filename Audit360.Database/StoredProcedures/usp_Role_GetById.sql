CREATE OR ALTER PROCEDURE dbo.usp_Role_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Description FROM dbo.Roles WHERE Id = @Id;
END
GO
