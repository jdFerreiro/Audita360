IF OBJECT_ID(N'dbo.usp_Audit_Update', N'P') IS NOT NULL
    DROP PROCEDURE dbo.usp_Audit_Update;
GO

CREATE OR ALTER PROCEDURE dbo.usp_Audit_Update
    @Id INT,
    @Title NVARCHAR(200),
    @Area NVARCHAR(100),
    @StartDate DATETIME2,
    @EndDate DATETIME2 = NULL,
    @StatusId INT,
    @ResponsibleId INT,
    @RowsAffected INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE dbo.Audits
        SET Title = @Title,
            Area = @Area,
            StartDate = @StartDate,
            EndDate = @EndDate,
            StatusId = @StatusId,
            ResponsibleId = @ResponsibleId
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
