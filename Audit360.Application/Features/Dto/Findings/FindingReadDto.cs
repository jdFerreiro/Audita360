namespace Audit360.Application.Features.Dto.Findings
{
    public record FindingReadDto(int Id, string Description, int TypeId, int SeverityId, System.DateTime Date, int AuditId);
}