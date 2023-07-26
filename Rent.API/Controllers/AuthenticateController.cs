using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request;
using Rent.Domain.DTO.Response;
using Rent.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Autenticação")]
    [ApiController]
    public class Authenticate : ControllerBase
    {
        private readonly Rent.Domain.Interfaces.Services.IAuthenticationService _authenticationService;
        private readonly IOwnerService _ownerService;
        private readonly IEmployeeService _employeeService;
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public Authenticate(
            Rent.Domain.Interfaces.Services.IAuthenticationService authenticationService,
            IOwnerService ownerService,
            IEmployeeService employeeService,
            ICustomerService customerService,
            IMapper mapper
        )
        {
            _authenticationService = authenticationService;
            _ownerService = ownerService;
            _employeeService = employeeService;
            _customerService = customerService;
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

        [HttpGet("Me")]
          [SwaggerOperation(
            Summary = "Buscar dados do usuário logado.",
            Description = "Realiza uma busca de usuário baseado no token JWT do usuário logado."
        )]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            // Extrair o token do header de autorização
            string? token = HttpContext.Request.Headers["Authorization"]
                .FirstOrDefault()
                ?.Replace("Bearer ", "");

            if (token == null)
            {
                return BadRequest("Token não fornecido.");
            }

            // Busca tipo de usuário
            UserMeta userMeta = _authenticationService.GetUserMeta(token);

            if(userMeta.UserType == null)
                return BadRequest("Tipo de usuário não encontrado.");

            if(userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 1))){
                var owner = await _ownerService.GetOwnerById(userMeta.ParentId);

                return Ok(owner);
            }

            if(userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 2))){
                var owner = await _employeeService.GetEmployeeById(userMeta.ParentId);

                return Ok(owner);
            }

            if(userMeta.UserType.Equals(Enum.GetName(typeof(Domain.Enums.UserType), 3))){
                var owner = await _customerService.GetCustomerById(userMeta.ParentId);

                return Ok(owner);
            }

            return BadRequest("Não encontrado.");
        }
    }
}
