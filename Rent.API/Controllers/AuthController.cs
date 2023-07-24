using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request;
using Rent.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Autenticação")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public AuthController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost(Name = "Authenticate")]
        [SwaggerOperation(
            Summary = "Realizar autenticação.",
            Description = "Realiza a autenticação com um usuário já registrado e retorna um token JWT."
        )]
        public async Task<ActionResult> Authenticate(string email, string password)
        {
            try
            {
                if (email == null || password == null)
                    throw new Exception("Por favor informe um email e senha");

                var token = await _authenticationService.Authenticate(email, password);

                return Ok(token);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginRequest> response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }
    }
}
