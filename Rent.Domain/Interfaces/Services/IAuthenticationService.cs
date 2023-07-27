using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Authenticate(string email, string password);
        Task<RevokedToken> RevokeToken(RevokedToken revokedToken);
        UserMeta GetUserMeta(string token);
    }
}
