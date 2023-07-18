using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTOs.Request;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                if (loginRequestDTO.Email == null || loginRequestDTO.Password == null)
                    throw new Exception("Por favor informe um email e senha");

                var token = await _loginService.Authenticate(loginRequestDTO.Email, loginRequestDTO.Password);

                return Ok(token);
            }
            catch (Exception ex)
            {
                ApiResponse<TokenResponse> response = new ApiResponse<TokenResponse>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }
    }
}