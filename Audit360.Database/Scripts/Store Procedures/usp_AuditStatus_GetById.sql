IF OBJECT_ID(N'dbo.usp_AuditStatus_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_AuditStatus_GetById;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AuditStatus_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description
    FROM dbo.AuditStatuses
    WHERE Id = @Id;
END
GO
