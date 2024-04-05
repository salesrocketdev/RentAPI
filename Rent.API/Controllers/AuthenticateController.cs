using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Domain.DTO.Request;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Autenticação")]
    [ApiController]
    public class Authenticate : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IOwnerService _ownerService;
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;

        public Authenticate(
            IAuthenticationService authenticationService,
            IOwnerService ownerService,
            IEmployeeService employeeService,
            ICustomerService customerService
        )
        {
            _authenticationService = authenticationService;
            _ownerService = ownerService;
            _employeeService = employeeService;
            _customerService = customerService;
        }

        [HttpPost("Login")]
        [SwaggerOperation(
            Summary = "Realizar autenticação.",
            Description = "Realiza a autenticação com um usuário já registrado e retorna um token JWT."
        )]
        public async Task<ActionResult> AuthenticateUser(AuthenticateDTO authenticateDTO)
        {
            try
            {
                if (authenticateDTO.Email == null || authenticateDTO.Password == null)
                    throw new Exception("Por favor informe um email e senha");

                ResponseTokenDTO responseToken = await _authenticationService.Authenticate(
                    authenticateDTO.Email,
                    authenticateDTO.Password
                );

                return Ok(responseToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Me")]
        [SwaggerOperation(
            Summary = "Buscar dados do usuário logado.",
            Description = "Realiza uma busca de usuário baseado no token JWT do usuário logado."
        )]
        public async Task<IActionResult> Me()
        {
            try
            {
                // Extrair o token do header de autorização
                string? token = HttpContext
                    .Request.Headers["Authorization"]
                    .FirstOrDefault()
                    ?.Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token não fornecido.");
                }

                // Busca tipo de usuário
                UserMeta userMeta = _authenticationService.GetUserMeta(token);

                if (userMeta.UserType == null)
                    return BadRequest("Tipo de usuário não encontrado.");

                if (userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 1)))
                {
                    var owner = await _ownerService.GetOwnerById(userMeta.ParentId);

                    return Ok(owner);
                }

                if (userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 2)))
                {
                    var owner = await _employeeService.GetEmployeeById(userMeta.ParentId);

                    return Ok(owner);
                }

                if (userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 3)))
                {
                    var owner = await _customerService.GetCustomerById(userMeta.ParentId);

                    return Ok(owner);
                }

                return BadRequest("Não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Refresh")]
        [SwaggerOperation(
            Summary = "Atualiza token do usuário.",
            Description = "Atualiza o token de autenticação do usuário."
        )]
        public async Task<IActionResult> Refresh()
        {
            try
            {
                // Extrair o token do header de autorização
                string? token = HttpContext
                    .Request.Headers["Authorization"]
                    .FirstOrDefault()
                    ?.Replace("Bearer ", "");

                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest("Token de autenticação não fornecido.");
                }

                var refreshedToken = await _authenticationService.Refresh(token);

                return Ok(refreshedToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Logout")]
        [SwaggerOperation(
            Summary = "Logout do usuário.",
            Description = "Revoga o token de autenticação do usuário."
        )]
        public async Task<IActionResult> Logout()
        {
            // Extrair o token do header de autorização
            string? token = HttpContext
                .Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Split(" ")
                .Last();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token de autenticação não fornecido.");
            }

            // bool logoutResult = await _authenticationService.RevokeToken(token);
            bool logoutResult = true;

            if (logoutResult)
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.ReadJwtToken(token);

                var revokedToken = new RevokedToken
                {
                    TokenId = jwtToken.Id,
                    ExpirationDate = jwtToken.ValidTo
                };

                await _authenticationService.RevokeToken(revokedToken);

                return Ok("Logout realizado com sucesso.");
            }
            else
            {
                return BadRequest("Falha ao realizar o logout.");
            }
        }
    }
}
