IF OBJECT_ID(N'dbo.usp_Finding_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Finding_Update;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Finding_Update
    @Id INT,
    @Description NVARCHAR(3000),
    @TypeId INT,
    @SeverityId INT,
    @Date DATETIME2,
    @AuditId INT,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Findings
        SET Description = @Description,
            TypeId = @TypeId,
            SeverityId = @SeverityId,
            Date = @Date,
            AuditId = @AuditId
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
