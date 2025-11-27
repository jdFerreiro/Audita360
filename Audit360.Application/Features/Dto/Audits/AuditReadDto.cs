namespace Audit360.Application.Features.Dto.Audits
{
    /// <summary>
    /// DTO de lectura para una auditoría.
    /// </summary>
    public record AuditReadDto(int Id, string Title, string Area, System.DateTime StartDate, System.DateTime? EndDate, int StatusId, int ResponsibleId);
}