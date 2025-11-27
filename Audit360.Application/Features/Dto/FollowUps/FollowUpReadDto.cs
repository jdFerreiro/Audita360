namespace Audit360.Application.Features.Dto.FollowUps
{
    /// <summary>
    /// DTO de lectura para seguimiento.
    /// </summary>
    public record FollowUpReadDto(int Id, string Notes, System.DateTime? CommitmentDate, int FollowUpStatusId, int FindingId);
}