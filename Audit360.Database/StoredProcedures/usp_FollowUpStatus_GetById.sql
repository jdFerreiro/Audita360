CREATE OR ALTER PROCEDURE dbo.usp_FollowUpStatus_GetById
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Description FROM dbo.FollowUpStatuses WHERE Id = @Id;
END
GO
