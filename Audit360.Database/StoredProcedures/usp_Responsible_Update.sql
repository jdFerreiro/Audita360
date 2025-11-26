CREATE OR ALTER PROCEDURE dbo.usp_Responsible_Update
    @Id INT,
    @Name NVARCHAR(150),
    @Email NVARCHAR(200),
    @Area NVARCHAR(100),
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Responsibles
        SET Name = @Name,
            Email = @Email,
            Area = @Area
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
