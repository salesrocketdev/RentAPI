using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Logins")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public LoginsController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os logins cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de logins cadastrados no sistema."
        )]
        public async Task<ActionResult<List<LoginDTO>>> GetAllLogins(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (logins, pagination) = await _loginService.GetAllLogins(pageNumber, pageSize);
                List<LoginDTO> loginsDTOs = _mapper.Map<List<LoginDTO>>(logins);

                ApiResponse<List<LoginDTO>> response = new ApiResponse<List<LoginDTO>>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = loginsDTOs,
                    Pagination = pagination
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<List<LoginDTO>> response = new ApiResponse<List<LoginDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        // [HttpGet("{id}")]
        // [SwaggerOperation(
        //     Summary = "Obter login por ID.",
        //     Description = "Retorna um login específico com base no seu ID."
        // )]

        [HttpPost("CreateLogin")]
        [SwaggerOperation(Summary = "Criar um novo login.", Description = "Cria um novo login.")]
        public async Task<ActionResult<LoginRequest>> CreateLogin(LoginRequest loginRequest)
        {
            try
            {
                // Mapear a LoginDTO para a entidade Login
                Login login = _mapper.Map<Login>(loginRequest);

                // Adicionar o login usando o serviço
                Login addedLogin = await _loginService.AddLogin(login);

                // Mapear o login adicionado de volta para LoginDTO
                LoginRequest loginRequestDTO = _mapper.Map<LoginRequest>(addedLogin);

                ApiResponse<LoginRequest> response = new ApiResponse<LoginRequest>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = loginRequestDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginRequest> response = new ApiResponse<LoginRequest>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }
    }
}
