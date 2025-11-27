namespace Audit360.Application.Features.Dto.Users
{
    /// <summary>
    /// DTO de escritura para crear o actualizar un usuario.
    /// </summary>
    public record UserWriteDto(string Username, string Email, string Password, string FullName, bool IsActive, int RoleId);
}