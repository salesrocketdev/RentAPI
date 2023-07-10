using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface ITokenService
    {
        //Task<Login> Authenticate(string email, string password);
        string GenerateToken(LoginRequest loginRequest);
    }
}
