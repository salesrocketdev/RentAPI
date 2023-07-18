using Microsoft.AspNetCore.Mvc;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Rent.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
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
        public async Task<ActionResult<List<CarDTO>>> GetAllCars(int pageNumber = 1, int pageSize = 10)
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

        [HttpPost]
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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
