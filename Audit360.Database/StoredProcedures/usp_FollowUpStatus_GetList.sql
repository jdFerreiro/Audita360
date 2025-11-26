CREATE OR ALTER PROCEDURE dbo.usp_FollowUpStatus_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FollowUpStatuses ORDER BY Id;
END
GO
