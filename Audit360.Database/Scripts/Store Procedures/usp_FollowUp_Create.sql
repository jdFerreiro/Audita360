IF OBJECT_ID(N'dbo.usp_FollowUp_Create', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_FollowUp_Create;
GO

CREATE OR ALTER PROCEDURE dbo.usp_FollowUp_Create
    @Description NVARCHAR(3000),
    @ResponsibleId INT,
    @DueDate DATETIME2,
    @AuditId INT,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO dbo.FollowUps (Description, ResponsibleId, DueDate, AuditId)
        VALUES (@Description, @ResponsibleId, @DueDate, @AuditId);

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
