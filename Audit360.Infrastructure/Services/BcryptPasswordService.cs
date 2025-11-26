using Audit360.Application.Interfaces;
using BCrypt.Net;

namespace Audit360.Infrastructure.Services
{
    public class BcryptPasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string hash, string password)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
        }
    }
}
