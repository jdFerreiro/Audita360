CREATE OR ALTER PROCEDURE dbo.usp_Role_Create
    @Name NVARCHAR(100),
    @Description NVARCHAR(300) = NULL,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.Roles ([Name], [Description]) VALUES (@Name, @Description);
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
