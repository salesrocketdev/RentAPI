using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rent.API.DTOs;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;

        public AuthController(ILoginService loginService, IMapper mapper)
        {
            _loginService = loginService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<TokenResponse>> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var token = await _loginService.Authenticate(loginRequestDTO.Email, loginRequestDTO.Password);

                //ApiResponse<TokenResponse> response = new ApiResponse<TokenResponse>
                //{
                //    Code = 1,
                //    Message = "Success.",
                //    Data = token
                //};

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