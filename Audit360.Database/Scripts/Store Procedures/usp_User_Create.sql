IF OBJECT_ID(N'dbo.usp_User_Create', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_User_Create;
GO

CREATE OR ALTER PROCEDURE dbo.usp_User_Create
    @Username NVARCHAR(100),
    @Email NVARCHAR(200),
    @PasswordHash NVARCHAR(200),
    @FullName NVARCHAR(200),
    @IsActive BIT = 1,
    @RoleId INT,
    @CreatedAt DATETIME2 = NULL,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.Users (Username, Email, PasswordHash, FullName, IsActive, RoleId, CreatedAt)
        VALUES (@Username, @Email, @PasswordHash, @FullName, @IsActive, @RoleId, ISNULL(@CreatedAt, SYSUTCDATETIME()));

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
