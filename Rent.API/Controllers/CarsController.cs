using Microsoft.AspNetCore.Mvc;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using AutoMapper;
using Rent.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Rent.Domain.DTO.Response;
using Rent.Domain.Interfaces.Services;

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
        public async Task<ActionResult<List<CarDTO>>> GetAllCars(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (cars, pagination) = await _carService.GetAllCars(pageNumber, pageSize);
                List<CarDTO> carsDTOs = _mapper.Map<List<CarDTO>>(cars);

                ApiResponse<List<CarDTO>> response = new ApiResponse<List<CarDTO>>
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
                ApiResponse<List<CarDTO>> response = new ApiResponse<List<CarDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter carro por ID.",
            Description = "Retorna um carro específico com base no seu ID."
        )]
        public async Task<ActionResult<CarDTO>> GetCarById(int id)
        {
            try
            {
                Car car = await _carService.GetCarById(id);
                CarDTO carDTO = _mapper.Map<CarDTO>(car);

                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = carDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpPost]
        [SwaggerOperation(Summary = "Criar um novo carro.", Description = "Cria um novo carro.")]
        public async Task<ActionResult<CarDTO>> CreateCar(CarDTO carsRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Car cars = _mapper.Map<Car>(carsRequest);

                // Adicionar o carro usando o serviço
                Car addedCars = await _carService.AddCar(cars);

                // Mapear o carro adicionado de volta para CarDTO
                CarDTO addedCarDTO = _mapper.Map<CarDTO>(addedCars);

                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedCarDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um carro existente.",
            Description = "Atualiza um carro existente."
        )]
        public async Task<ActionResult<CarDTO>> UpdateCar(CarDTO carsRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Car cars = _mapper.Map<Car>(carsRequest);

                // Adicionar o carro usando o serviço
                Car updatedCars = await _carService.UpdateCar(cars);

                // Mapear o carro adicionado de volta para CarDTO
                CarDTO updatedCarDTO = _mapper.Map<CarDTO>(updatedCars);

                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedCarDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover carro por ID.",
            Description = "Remove um carro específico com base no seu ID."
        )]
        public async Task<ActionResult<CarDTO>> DeleteCar(int id)
        {
            try
            {
                await _carService.DeleteCar(id);

                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CarDTO> response = new ApiResponse<CarDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
