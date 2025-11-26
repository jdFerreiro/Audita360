IF OBJECT_ID(N'dbo.usp_FollowUp_GetList', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FollowUp_GetList;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FollowUp_GetList
    @FindingId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description, CommitmentDate, StatusId, FindingId
    FROM dbo.FollowUps
    WHERE (@FindingId IS NULL OR FindingId = @FindingId)
    ORDER BY CommitmentDate DESC;
END
GO
