CREATE OR ALTER PROCEDURE dbo.usp_FollowUp_Create
    @Description NVARCHAR(3000),
    @CommitmentDate DATETIME2,
    @StatusId INT,
    @FindingId INT,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.FollowUps (Description, CommitmentDate, StatusId, FindingId)
        VALUES (@Description, @CommitmentDate, @StatusId, @FindingId);

        SET @NewId = CAST(SCOPE_IDENTITY() AS INT);
    END TRY
    BEGIN CATCH
        DECLARE @ErrMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrState INT = ERROR_STATE();
        RAISERROR (@ErrMessage, @ErrSeverity, @ErrState);
    END CATCH
END
GO
