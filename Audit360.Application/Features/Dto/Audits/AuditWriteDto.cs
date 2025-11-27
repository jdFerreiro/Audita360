namespace Audit360.Application.Features.Dto.Audits
{
    public record AuditWriteDto(string Title, string Area, System.DateTime StartDate, System.DateTime? EndDate, int StatusId, int ResponsibleId);
}