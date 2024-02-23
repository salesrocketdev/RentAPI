using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Core.Response.Result;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
    [SwaggerTag("Carros")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarsController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os carros cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de carros cadastrados no sistema."
        )]
        public async Task<ActionResult<ResponsePaginateDTO<ResponseCarDTO>>> GetAllCars(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                ResponsePaginateDTO<ResponseCarDTO> responsePaginateDTO =
                    await _carService.GetAllCars(pageNumber, pageSize);

                ApiResponse<List<ResponseCarDTO>> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responsePaginateDTO.Data,
                        Pagination = responsePaginateDTO.PaginationMeta
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter carro por ID.",
            Description = "Retorna um carro específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseCarDTO>> GetCarById(int id)
        {
            try
            {
                ResponseCarDTO responseCarDTO = await _carService.GetCarById(id);

                ApiResultResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseCarDTO
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
        [SwaggerOperation(Summary = "Criar um novo carro.", Description = "Cria um novo carro.")]
        public async Task<ActionResult<ResponseCarDTO>> CreateCar(CreateCarDTO createCarDTO)
        {
            try
            {
                ResponseCarDTO createdCar = await _carService.AddCar(createCarDTO);

                ApiResultResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = createdCar
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
        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um carro existente.",
            Description = "Atualiza um carro existente."
        )]
        public async Task<ActionResult<ResponseCarDTO>> UpdateCar(UpdateCarDTO updateCarDTO)
        {
            try
            {
                ResponseCarDTO updatedCar = await _carService.UpdateCar(updateCarDTO);

                ApiResultResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = updatedCar
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
        [HttpPost("{id}/Image")]
        [SwaggerOperation(
            Summary = "Adiciona imagem a um carro existente.",
            Description = "Adiciona imagem a um carro existente."
        )]
        public async Task<ActionResult> AddCarImage(int id, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                    return BadRequest("Arquivo de imagem inválido");

                ResponseCarDTO responseCarDTO = await _carService.GetCarById(id);

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    await _carService.UploadImage(responseCarDTO.Id, memoryStream);
                }

                ApiResponse<ResponseCarDTO> response =
                    new() { Code = 1, Message = "Imagem salva com sucesso" };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover carro por ID.",
            Description = "Remove um carro específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseCarDTO>> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCar(id);

                ApiResultResponse<ResponseCarDTO> response =
                    new() { Code = 1, Message = "Success.", };

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
