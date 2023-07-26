using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
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
            Login login = await _loginRepository.GetLoginByEmail(email);

            if (login == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            var token = _tokenService.GenerateToken(login);

            TokenResponse tokenResponse = new TokenResponse()
            {
                Token = token,
                ExpiresAt = DateTime.Now.AddHours(1)
            };

            return tokenResponse;
        }

        public UserMeta GetUserMeta(string token)
        {
            // Decodificar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            // Obter o tipo de usuário (UserType) das reivindicações (claims)
            var userMeta = new UserMeta
            {
                UserType = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserType")?.Value,
                ParentId = int.Parse(
                    jwtToken.Claims.FirstOrDefault(x => x.Type == "ParentId")?.Value
                )
            };

            if (userMeta == null)
            {
                throw new Exception("Tipo de usuário não encontrado no token.");
            }

            return userMeta;
        }
    }
}
