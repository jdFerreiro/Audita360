CREATE OR ALTER PROCEDURE dbo.usp_FindingType_Delete
    @Id INT,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM dbo.FindingTypes WHERE Id = @Id;
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
