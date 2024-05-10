using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Core.Response.Result;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Response;
using Rent.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Marcas")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // [Authorize(Roles = "Owner, Employee, Customer")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos as marcas cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de marcas cadastrados no sistema."
        )]
        public async Task<ActionResult<List<ResponseBrandDTO>>> GetAllBrands()
        {
            try
            {
                List<ResponseBrandDTO> responseDTO = await _brandService.GetAllBrands();

                ApiResponse<List<ResponseBrandDTO>> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseDTO,
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return Ok(response);
            }
        }

        [Authorize(Roles = "Owner, Employee, Customer")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter marca por ID.",
            Description = "Retorna uma marca específica com base no seu ID."
        )]
        public async Task<ActionResult<ResponseBrandDTO>> GetBrandById(int id)
        {
            try
            {
                ResponseBrandDTO ResponseBrandDTO = await _brandService.GetBrandById(id);

                ApiResultResponse<ResponseBrandDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = ResponseBrandDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpPost]
        [SwaggerOperation(Summary = "Criar uma nova marca.", Description = "Cria uma novo marca.")]
        public async Task<ActionResult<ResponseBrandDTO>> CreateBrand(CreateBrandDTO createBrandDTO)
        {
            try
            {
                ResponseBrandDTO CreatedBrand = await _brandService.AddBrand(createBrandDTO);

                ApiResultResponse<ResponseBrandDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success",
                        Data = CreatedBrand
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }
    }
}
