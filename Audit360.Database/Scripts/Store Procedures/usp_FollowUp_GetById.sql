IF OBJECT_ID(N'dbo.usp_FollowUp_GetById', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FollowUp_GetById;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FollowUp_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description, CommitmentDate, StatusId, FindingId
    FROM dbo.FollowUps
    WHERE Id = @Id;
END
GO
