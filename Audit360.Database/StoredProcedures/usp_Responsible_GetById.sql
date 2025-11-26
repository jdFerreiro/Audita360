CREATE OR ALTER PROCEDURE dbo.usp_Responsible_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Name, Email, Area
    FROM dbo.Responsibles
    WHERE Id = @Id;
END
GO
