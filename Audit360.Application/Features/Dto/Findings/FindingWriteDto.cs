namespace Audit360.Application.Features.Dto.Findings
{
    /// <summary>
    /// DTO de escritura para crear o actualizar un hallazgo.
    /// </summary>
    public record FindingWriteDto(string Description, System.DateTime Date, int FindingTypeId, int SeverityId, int AuditId);
}