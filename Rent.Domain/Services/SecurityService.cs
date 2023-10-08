using System.Security.Cryptography;
using System.Text;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class SecurityService : ISecurityService
    {
        private const int keySize = 64;
        private const int iterations = 350000;
        private readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

        public byte[] HashPassword(string password, out byte[] salt)
        {
            salt = RandomNumberGenerator.GetBytes(keySize);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );
            return hash;
        }

        public bool VerifyPassword(string password, byte[]? storedHash, byte[]? salt)
        {
            if (password == null)
                throw new Exception("Erro");

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize
            );
            return CryptographicOperations.FixedTimeEquals(hashToCompare, storedHash);
        }

        public string GenerateRandomPassword(int length)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var passwordChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                passwordChars[i] = allowedChars[random.Next(allowedChars.Length)];
            }

            return new string(passwordChars);
        }
    }
}
