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
    [Authorize]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Alugueis")]
    [ApiController]
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly IMapper _mapper;

        public RentalsController(IRentalService rentalService, IMapper mapper)
        {
            _rentalService = rentalService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os alugueis cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de alugueis cadastrados no sistema."
        )]
        public async Task<ActionResult<List<RentalDTO>>> GetAllRentals(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (employees, pagination) = await _rentalService.GetAllRentals(
                    pageNumber,
                    pageSize
                );
                List<RentalDTO> employeeDTOs = _mapper.Map<List<RentalDTO>>(employees);

                ApiResponse<List<RentalDTO>> response = new ApiResponse<List<RentalDTO>>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = employeeDTOs,
                    Pagination = pagination
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<List<RentalDTO>> response = new ApiResponse<List<RentalDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter aluguel por ID.",
            Description = "Retorna um aluguel específico com base no seu ID."
        )]
        public async Task<ActionResult<RentalDTO>> GetRentalById(int id)
        {
            try
            {
                Rental rental = await _rentalService.GetRentalById(id);
                RentalDTO RentalDTO = _mapper.Map<RentalDTO>(rental);

                ApiResponse<RentalDTO> response = new ApiResponse<RentalDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = RentalDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<RentalDTO> response = new ApiResponse<RentalDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo aluguel.",
            Description = "Cria um novo aluguel."
        )]
        public async Task<ActionResult<RentalDTO>> CreateRental(CreateRentalDTO rentalRequest)
        {
            try
            {
                Rental rental = _mapper.Map<Rental>(rentalRequest);

                Rental addedRental = await _rentalService.AddRental(rental);

                CreateRentalDTO addedRentalDTO = _mapper.Map<CreateRentalDTO>(addedRental);

                ApiResponse<CreateRentalDTO> response = new ApiResponse<CreateRentalDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedRentalDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<RentalDTO> response = new ApiResponse<RentalDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um aluguel existente.",
            Description = "Atualiza um aluguel existente."
        )]
        public async Task<ActionResult<RentalDTO>> UpdateRental(UpdateRentalDTO rentalRequest)
        {
            try
            {
                Rental rental = _mapper.Map<Rental>(rentalRequest);

                Rental updatedRental = await _rentalService.UpdateRental(rental);

                UpdateRentalDTO updatedRentalDTO = _mapper.Map<UpdateRentalDTO>(updatedRental);

                ApiResponse<UpdateRentalDTO> response = new ApiResponse<UpdateRentalDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedRentalDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<RentalDTO> response = new ApiResponse<RentalDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }
    }
}
