namespace Audit360.Application.Features.Dto.Users
{
    public record UserReadDto(int Id, string Username, string Email, string FullName, bool IsActive, int RoleId, System.DateTime CreatedAt);
}