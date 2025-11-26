IF OBJECT_ID(N'dbo.usp_Responsible_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Responsible_GetById;
GO

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
