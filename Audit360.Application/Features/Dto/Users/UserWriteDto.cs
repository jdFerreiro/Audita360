namespace Audit360.Application.Features.Dto.Users
{
    public record UserWriteDto(string Username, string Email, string Password, string FullName, bool IsActive, int RoleId);
}