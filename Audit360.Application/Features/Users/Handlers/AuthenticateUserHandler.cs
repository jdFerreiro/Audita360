using MediatR;
using Audit360.Application.Features.Users.Commands;
using Audit360.Application.Features.Dto.Users;
using Audit360.Application.Interfaces.Repositories;
using Audit360.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Audit360.Application.Configurations;

namespace Audit360.Application.Features.Users.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, AuthenticateResponseDto?>
    {
        private readonly IUserReadRepository _readRepo;
        private readonly IPasswordService _passwordService;
        private readonly JwtOptions _jwtOptions;

        public AuthenticateUserHandler(IUserReadRepository readRepo, IPasswordService passwordService, JwtOptions jwtOptions)
        {
            _readRepo = readRepo;
            _passwordService = passwordService;
            _jwtOptions = jwtOptions;
        }

        public async Task<AuthenticateResponseDto?> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _readRepo.GetByEmailAsync(request.Email);
            if (user == null) return null;

            if (!_passwordService.Verify(user.PasswordHash, request.Password))
                return null;

            var secret = _jwtOptions?.Secret;
            var issuer = _jwtOptions?.Issuer;
            var audience = _jwtOptions?.Audience;

            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("JWT secret is not configured.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Role, user.Role?.Name ?? string.Empty)
            };

            var expires = DateTime.UtcNow.AddHours(1);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticateResponseDto(tokenStr, expires);
        }
    }
}
