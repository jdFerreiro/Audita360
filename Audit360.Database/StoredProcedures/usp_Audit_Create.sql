CREATE OR ALTER PROCEDURE dbo.usp_Audit_Create
    @Title NVARCHAR(200),
    @Area NVARCHAR(100),
    @StartDate DATETIME2,
    @EndDate DATETIME2 = NULL,
    @StatusId INT,
    @ResponsibleId INT,
    @NewId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        INSERT INTO dbo.Audits (Title, Area, StartDate, EndDate, StatusId, ResponsibleId)
        VALUES (@Title, @Area, @StartDate, @EndDate, @StatusId, @ResponsibleId);

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
