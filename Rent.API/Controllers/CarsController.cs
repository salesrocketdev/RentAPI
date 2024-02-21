using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
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
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os carros cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de carros cadastrados no sistema."
        )]
        public async Task<ActionResult<List<ResponseCarDTO>>> GetAllCars(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (cars, pagination) = await _carService.GetAllCars(pageNumber, pageSize);
                List<ResponseCarDTO> carsDTOs = _mapper.Map<List<ResponseCarDTO>>(cars);

                ApiResponse<List<ResponseCarDTO>> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = carsDTOs,
                        Pagination = pagination
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<List<ResponseCarDTO>> response =
                    new() { Code = 0, Message = ex.Message, };

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
                Car car = await _carService.GetCarById(id);

                ApiResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<ResponseCarDTO>(car)
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCarDTO> response = new() { Code = 0, Message = ex.Message };

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
                Car mappedCar = _mapper.Map<Car>(createCarDTO);

                Car createdCar = await _carService.AddCar(mappedCar);

                ApiResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<ResponseCarDTO>(createdCar)
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<object> response = new() { Code = 0, Message = ex.Message, };

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
                Car mappedCar = _mapper.Map<Car>(updateCarDTO);

                Car updatedCar = await _carService.UpdateCar(mappedCar);

                ApiResponse<ResponseCarDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<ResponseCarDTO>(updatedCar)
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCarDTO> response = new() { Code = 0, Message = ex.Message, };

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

                Car car = await _carService.GetCarById(id);

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    await _carService.UploadImage(car.Id, memoryStream);
                }

                ApiResponse<ResponseCarDTO> response =
                    new() { Code = 1, Message = "Imagem salva com sucesso" };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCarDTO> response = new() { Code = 0, Message = ex.Message, };

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

                ApiResponse<ResponseCarDTO> response = new() { Code = 1, Message = "Success.", };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCarDTO> response = new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }
    }
}
