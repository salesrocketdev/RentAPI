namespace Rent.Domain.Interfaces.Services
{
    public interface ISecurityService
    {
        byte[] HashPassword(string password, out byte[] salt);
        bool VerifyPassword(string password, byte[]? storedHash, byte[]? salt);
        string GenerateRandomPassword(int length);
        
    }
}
