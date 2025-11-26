IF OBJECT_ID(N'dbo.usp_User_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_User_Update;
GO

CREATE OR ALTER PROCEDURE dbo.usp_User_Update
    @Id INT,
    @Email NVARCHAR(200),
    @FullName NVARCHAR(200),
    @IsActive BIT,
    @RoleId INT,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Users
        SET Email = @Email,
            FullName = @FullName,
            IsActive = @IsActive,
            RoleId = @RoleId
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
