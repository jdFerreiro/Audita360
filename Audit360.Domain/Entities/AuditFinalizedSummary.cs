using System;

namespace Audit360.Domain.Entities
{
    // Entity mapped to database view vw_AuditFinalizedSummary
    public class AuditFinalizedSummary
    {
        public int AuditId { get; set; }
        public string? Title { get; set; }
        public string? Area { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? ResponsibleId { get; set; }
        public string? ResponsibleName { get; set; }
        public int TotalFindings { get; set; }
        public int FindingsBaja { get; set; }
        public int FindingsMedia { get; set; }
        public int FindingsAlta { get; set; }
        public int FindingsWithCompletedFollowUp { get; set; }
        public decimal PercentFindingsWithCompletedFollowUp { get; set; }
    }
}