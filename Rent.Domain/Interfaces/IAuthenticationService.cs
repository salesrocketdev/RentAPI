using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        Task<TokenResponse> Authenticate(string email, string password);
    }
}
