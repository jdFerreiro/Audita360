namespace Audit360.Application.Features.Dto.FollowUps
{
    public record FollowUpWriteDto(string Description, System.DateTime CommitmentDate, int StatusId, int FindingId);
}