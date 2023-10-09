using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Services;
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

        public LoginsController(
            ILoginService loginService,
            IMapper mapper
        )
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

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter login por ID.",
            Description = "Retorna um login específico com base no seu ID."
        )]
        public async Task<ActionResult<LoginDTO>> GetLoginById(int id)
        {
            try
            {
                Login login = await _loginService.GetLoginById(id);
                LoginDTO loginDTO = _mapper.Map<LoginDTO>(login);

                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = loginDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpGet("{parentId}")]
        [SwaggerOperation(
            Summary = "Obter login por Parent ID.",
            Description = "Retorna um login específico com base no seu Parent ID."
        )]
        public async Task<ActionResult<LoginDTO>> GetLoginByParentId(int parentId)
        {
            try
            {
                Login login = await _loginService.GetLoginById(parentId);
                LoginDTO loginDTO = _mapper.Map<LoginDTO>(login);

                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = loginDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo login.", Description = "Cria um novo login.")]
        public async Task<ActionResult<LoginRequest>> CreateLogin(LoginRequest loginRequest)
        {
            try
            {
                Login login = _mapper.Map<Login>(loginRequest);

                Login addedLogin = await _loginService.AddLogin(login);

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

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um login existente.",
            Description = "Atualiza um login existente."
        )]
        public async Task<ActionResult<LoginDTO>> UpdateLogin(LoginDTO loginRequest)
        {
            try
            {
                Login login = _mapper.Map<Login>(loginRequest);

                Login updatedLogin = await _loginService.UpdateLogin(login);

                LoginDTO updatedLoginDTO = _mapper.Map<LoginDTO>(updatedLogin);

                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedLoginDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover login por ID.",
            Description = "Remove um login específico com base no seu ID."
        )]
        public async Task<ActionResult<LoginDTO>> DeleteLogin(int id)
        {
            try
            {
                await _loginService.DeleteLogin(id);

                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<LoginDTO> response = new ApiResponse<LoginDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
