using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Authenticate(string email, string password);
    }
}
