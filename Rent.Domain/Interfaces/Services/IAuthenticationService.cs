using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseTokenDTO> Authenticate(string email, string password);
        Task<ResponseTokenDTO> Refresh(string token);
        UserMeta GetUserMeta(string token);
        Task<bool> IsTokenRevoked(string token);
        Task<RevokedToken> RevokeToken(RevokedToken revokedToken);
    }
}
