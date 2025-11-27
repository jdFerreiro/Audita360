namespace Audit360.Application.Features.Dto.Audits
{
    public record AuditReadDto(int Id, string Title, string Area, System.DateTime StartDate, System.DateTime? EndDate, int StatusId, int ResponsibleId);
}