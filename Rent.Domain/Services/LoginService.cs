using AutoMapper;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly ITokenService _tokenService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public LoginService(ITokenService tokenService, ILoginRepository loginRepository, IMapper mapper)
        {
            _tokenService = tokenService;
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        public async Task<TokenResponse> Authenticate(string email, string password)
        {
            Login auth = await _loginRepository.Authenticate(email, password);
            LoginRequest authDTO = _mapper.Map<LoginRequest>(auth);
            var token = _tokenService.GenerateToken(authDTO);

            TokenResponse tokenResponse = new TokenResponse()
            {
                Token = token,
                ExpiresAt = DateTime.Now.AddHours(1)
            };

            return tokenResponse;
        }
    }
}
