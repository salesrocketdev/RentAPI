using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(Login login);
    }
}
