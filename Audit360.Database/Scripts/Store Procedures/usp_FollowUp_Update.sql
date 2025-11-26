IF OBJECT_ID(N'dbo.usp_FollowUp_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FollowUp_Update;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FollowUp_Update
    @Id INT,
    @Description NVARCHAR(3000),
    @CommitmentDate DATETIME2,
    @StatusId INT,
    @FindingId INT,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.FollowUps
        SET Description = @Description,
            CommitmentDate = @CommitmentDate,
            StatusId = @StatusId,
            FindingId = @FindingId
        WHERE Id = @Id;

        SET @RowsAffected = @@ROWCOUNT;
    END TRY
    BEGIN CATCH
        DECLARE @ErrMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrState INT = ERROR_STATE();
        RAISERROR (@ErrMessage, @ErrSeverity, @ErrState);
    END CATCH
END
GO
