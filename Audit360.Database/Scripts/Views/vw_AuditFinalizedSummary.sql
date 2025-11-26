IF OBJECT_ID(N'dbo.vw_AuditFinalizedSummary', N'V') IS NOT NULL
    DROP VIEW dbo.vw_AuditFinalizedSummary;
GO

CREATE OR ALTER VIEW dbo.vw_AuditFinalizedSummary
AS
SELECT
    a.Id AS AuditId,
    a.Title,
    a.Area,
    a.StartDate,
    a.EndDate,
    r.Id AS ResponsibleId,
    r.Name AS ResponsibleName,
    COUNT(f.Id) AS TotalFindings,
    SUM(CASE WHEN fs.Description = 'baja' THEN 1 ELSE 0 END) AS FindingsBaja,
    SUM(CASE WHEN fs.Description = 'media' THEN 1 ELSE 0 END) AS FindingsMedia,
    SUM(CASE WHEN fs.Description = 'alta' THEN 1 ELSE 0 END) AS FindingsAlta,
    SUM(CASE WHEN fc.HasCompleted = 1 THEN 1 ELSE 0 END) AS FindingsWithCompletedFollowUp,
    CASE WHEN COUNT(f.Id) = 0 THEN CAST(0 AS DECIMAL(5,2))
         ELSE CAST(100.0 * SUM(CASE WHEN fc.HasCompleted = 1 THEN 1 ELSE 0 END) / COUNT(f.Id) AS DECIMAL(5,2))
    END AS PercentFindingsWithCompletedFollowUp
FROM dbo.Audits a
INNER JOIN dbo.AuditStatuses ast ON a.StatusId = ast.Id AND ast.Description = 'Finalizada'
LEFT JOIN dbo.Responsibles r ON a.ResponsibleId = r.Id
LEFT JOIN dbo.Findings f ON f.AuditId = a.Id
LEFT JOIN dbo.FindingSeverities fs ON f.SeverityId = fs.Id
LEFT JOIN (
    SELECT fu.FindingId, MAX(CASE WHEN fus.Description = 'completado' THEN 1 ELSE 0 END) AS HasCompleted
    FROM dbo.FollowUps fu
    INNER JOIN dbo.FollowUpStatuses fus ON fu.StatusId = fus.Id
    GROUP BY fu.FindingId
) fc ON f.Id = fc.FindingId
GROUP BY a.Id, a.Title, a.Area, a.StartDate, a.EndDate, r.Id, r.Name;
