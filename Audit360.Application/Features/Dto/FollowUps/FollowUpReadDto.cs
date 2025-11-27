namespace Audit360.Application.Features.Dto.FollowUps
{
    public record FollowUpReadDto(int Id, string Description, System.DateTime CommitmentDate, int StatusId, int FindingId);
}