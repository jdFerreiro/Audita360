namespace Audit360.Application.Features.Dto.Audits
{
    /// <summary>
    /// DTO de escritura para crear o actualizar una auditoría.
    /// </summary>
    public record AuditWriteDto(string Title, string Area, System.DateTime StartDate, System.DateTime? EndDate, int StatusId, int ResponsibleId);
}