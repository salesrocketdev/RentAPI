using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<RevokedToken> RevokeToken(RevokedToken revokedToken);
        Task<bool> IsTokenRevoked(string token);
    }
}
