using System;

namespace Audit360.Application.Features.Dto.Audits
{
    public record AuditFinalizedSummaryReadDto(
        int AuditId,
        string? Title,
        string? Area,
        DateTime StartDate,
        DateTime? EndDate,
        int? ResponsibleId,
        string? ResponsibleName,
        int TotalFindings,
        int FindingsBaja,
        int FindingsMedia,
        int FindingsAlta,
        int FindingsWithCompletedFollowUp,
        decimal PercentFindingsWithCompletedFollowUp
    );
}
