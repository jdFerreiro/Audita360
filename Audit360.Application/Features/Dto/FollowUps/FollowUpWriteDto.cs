namespace Audit360.Application.Features.Dto.FollowUps
{
    /// <summary>
    /// DTO de escritura para seguimiento.
    /// </summary>
    public record FollowUpWriteDto(string Description, System.DateTime CommitmentDate, int StatusId, int FindingId);
}