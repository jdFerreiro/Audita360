CREATE OR ALTER PROCEDURE dbo.usp_AuditStatus_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.AuditStatuses ORDER BY Id;
END
GO
