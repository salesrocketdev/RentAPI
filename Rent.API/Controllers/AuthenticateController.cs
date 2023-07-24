using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Autenticação")]
    [ApiController]
    public class Authenticate : ControllerBase
    {
        private readonly Rent.Domain.Interfaces.Services.IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public Authenticate(
            Rent.Domain.Interfaces.Services.IAuthenticationService authenticationService,
            IMapper mapper
        )
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        [HttpPost(Name = "AuthenticateUser")]
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

                var token = await _authenticationService.Authenticate(
                    authenticateDTO.Email,
                    authenticateDTO.Password
                );

                return Ok(token);
            }
            catch (Exception ex)
            {
                ApiResponse<AuthenticateDTO> response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }
    }
}
