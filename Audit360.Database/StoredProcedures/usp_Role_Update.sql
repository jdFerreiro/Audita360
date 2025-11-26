CREATE OR ALTER PROCEDURE dbo.usp_Role_Update
    @Id INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(300) = NULL,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Roles SET Name = @Name, Description = @Description WHERE Id = @Id;
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
