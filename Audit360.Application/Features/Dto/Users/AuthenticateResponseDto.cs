namespace Audit360.Application.Features.Dto.Users
{
    public record AuthenticateResponseDto(string Token, DateTime ExpiresAt);
}
