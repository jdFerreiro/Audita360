namespace Audit360.Application.Features.Dto.FollowUps
{
    /// <summary>
    /// DTO de escritura para seguimiento.
    /// </summary>
    public record FollowUpWriteDto(string Notes, int FollowUpStatusId, int FindingId);
}