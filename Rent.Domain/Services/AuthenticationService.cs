using System.IdentityModel.Tokens.Jwt;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly ISecurityService _securityService;
        private readonly ILoginRepository _loginRepository;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationService(
            ITokenService tokenService,
            ISecurityService securityService,
            ILoginRepository loginRepository,
            IAuthenticationRepository authenticationRepository
        )
        {
            _tokenService = tokenService;
            _securityService = securityService;
            _loginRepository = loginRepository;
            _authenticationRepository = authenticationRepository;
        }

        public async Task<ResponseTokenDTO> Authenticate(string email, string password)
        {
            Login login = await _loginRepository.GetLoginByEmail(email);

            if (login == null)
                throw new Exception("Usuário não encontrado.");

            bool verificationResult = _securityService.VerifyPassword(
                password,
                login.PasswordHash,
                login.PasswordSalt
            );

            if (verificationResult == false)
                throw new Exception("Senha incorreta.");

            string token = _tokenService.GenerateToken(login);

            ResponseTokenDTO responseToken =
                new() { Token = token, ExpiresAt = DateTime.Now.AddHours(1) };

            return responseToken;
        }

        public UserMeta GetUserMeta(string token)
        {
            // Decodificar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken =
                tokenHandler.ReadToken(token) as JwtSecurityToken
                ?? throw new Exception("Jwt Inválido.");

            // Obter o tipo de usuário (UserType) das reivindicações (claims)
            var userMeta = new UserMeta
            {
                UserType = jwtToken.Claims.FirstOrDefault(x => x.Type == "UserType")?.Value,
                ParentId = Convert.ToInt16(
                    jwtToken.Claims.FirstOrDefault(x => x.Type == "ParentId")?.Value
                )
            };

            if (userMeta == null)
            {
                throw new Exception("Tipo de usuário não encontrado no token.");
            }

            return userMeta;
        }

        public async Task<ResponseTokenDTO> Refresh(string token)
        {
            // Decodificar o token
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var guidToken = jwtToken.Claims.FirstOrDefault(x => x.Type == "jti")?.Value;
            var emailToken = jwtToken.Claims.FirstOrDefault(x => x.Type == "email")?.Value;

            if (!string.IsNullOrEmpty(guidToken))
            {
                // Checa se o token atual já foi revogado
                if (await IsTokenRevoked(guidToken))
                    throw new Exception("Token inválido.");
                else
                {
                    // Invalidar token original
                    var revokedToken = new RevokedToken
                    {
                        TokenId = jwtToken.Id,
                        ExpirationDate = jwtToken.ValidTo
                    };

                    await RevokeToken(revokedToken);
                }
            }

            Login login = await _loginRepository.GetLoginByEmail(emailToken);

            var refreshToken = _tokenService.GenerateToken(login);

            ResponseTokenDTO responseToken =
                new() { Token = refreshToken, ExpiresAt = DateTime.Now.AddHours(1) };

            return responseToken;
        }

        public async Task<bool> IsTokenRevoked(string token)
        {
            return await _authenticationRepository.IsTokenRevoked(token);
        }

        public async Task<RevokedToken> RevokeToken(RevokedToken revokedToken)
        {
            return await _authenticationRepository.RevokeToken(revokedToken);
        }
    }
}
