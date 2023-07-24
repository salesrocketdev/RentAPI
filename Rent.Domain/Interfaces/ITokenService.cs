
using Rent.Domain.DTO.Request;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Login login);
    }
}
