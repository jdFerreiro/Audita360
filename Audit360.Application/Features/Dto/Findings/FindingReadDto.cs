namespace Audit360.Application.Features.Dto.Findings
{
    /// <summary>
    /// DTO de lectura para un hallazgo.
    /// </summary>
    public record FindingReadDto(int Id, string Description, System.DateTime Date, int FindingTypeId, int SeverityId, int AuditId);
}