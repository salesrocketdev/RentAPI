using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rent.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Login login)
        {
            byte[] key = Encoding.ASCII.GetBytes(
                _configuration.GetSection("AppSettings:Secret").Value
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("ParentId", login.ParentId.ToString()),
                        new Claim(ClaimTypes.Email, login.Email),
                        new Claim("UserType", login.UserType.ToString()),
                        new Claim(ClaimTypes.Role, login.UserType.ToString())
                    }
                ),
                //Expires = DateTime.UtcNow.AddHours(1),
                Expires = DateTime.Now.AddMinutes(3600),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
