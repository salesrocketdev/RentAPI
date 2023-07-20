using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTOs.Request;
using Rent.Domain.DTOs.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Autenticação")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Realizar autenticação.",
            Description = "Realiza a autenticação com um usuário já registrado e retorna um token JWT."
        )]
        public async Task<ActionResult<TokenResponseDTO>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO.Email == null || loginRequestDTO.Password == null)
                    throw new Exception("Por favor informe um email e senha");

                var token = await _loginService.Authenticate(
                    loginRequestDTO.Email,
                    loginRequestDTO.Password
                );

                return Ok(token);
            }
            catch (Exception ex)
            {
                ApiResponse<TokenResponseDTO> response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }
    }
}
