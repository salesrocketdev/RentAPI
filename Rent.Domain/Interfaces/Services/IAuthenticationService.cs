using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Authenticate(string email, string password);
        Task<TokenResponse> Refresh(string token);
        UserMeta GetUserMeta(string token);
        Task<bool> IsTokenRevoked(string token);
        Task<RevokedToken> RevokeToken(RevokedToken revokedToken);
    }
}
