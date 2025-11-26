IF OBJECT_ID(N'dbo.usp_FollowUpStatus_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FollowUpStatus_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FollowUpStatus_GetList
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FollowUpStatuses ORDER BY Id;
END
GO
