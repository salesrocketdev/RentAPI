using AutoMapper;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(
            ITokenService tokenService,
            ILoginRepository loginRepository,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Authenticate(string email, string password)
        {
            Login login = await _loginRepository.Authenticate(email, password);
            var token = _tokenService.GenerateToken(login);

            TokenResponse tokenResponse = new TokenResponse()
            {
                Token = token,
                ExpiresAt = DateTime.Now.AddHours(1)
            };

            return tokenResponse;
        }
    }
}
