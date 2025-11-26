CREATE OR ALTER PROCEDURE dbo.usp_Finding_Create
    @Description NVARCHAR(3000),
    @TypeId INT,
    @SeverityId INT,
    @Date DATETIME2,
    @AuditId INT,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.Findings (Description, TypeId, SeverityId, Date, AuditId)
        VALUES (@Description, @TypeId, @SeverityId, @Date, @AuditId);

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
