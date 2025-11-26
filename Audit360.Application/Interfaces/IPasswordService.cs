namespace Audit360.Application.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool Verify(string hash, string password);
    }
}
