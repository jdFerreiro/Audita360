IF OBJECT_ID(N'dbo.usp_AuditStatus_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_AuditStatus_Update;
GO

CREATE OR ALTER PROCEDURE dbo.usp_AuditStatus_Update
    @Id INT,
    @Description NVARCHAR(100),
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.AuditStatuses SET Description = @Description WHERE Id = @Id;
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
