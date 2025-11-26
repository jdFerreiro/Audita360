IF OBJECT_ID(N'dbo.usp_FindingType_Create', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FindingType_Create;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FindingType_Create
    @Description NVARCHAR(100),
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.FindingTypes ([Description]) VALUES (@Description);
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
