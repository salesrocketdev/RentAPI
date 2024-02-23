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
    [Authorize]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Alugueis")]
    [ApiController]
    public class RentalsController : Controller
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os alugueis cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de alugueis cadastrados no sistema."
        )]
        public async Task<ActionResult<ResponsePaginateDTO<ResponseRentalDTO>>> GetAllRentals(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                ResponsePaginateDTO<ResponseRentalDTO> responsePaginateDTO =
                    await _rentalService.GetAllRentals(pageNumber, pageSize);

                ApiResponse<List<ResponseRentalDTO>> response =
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
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message };

                return Ok(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter aluguel por ID.",
            Description = "Retorna um aluguel específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseRentalDTO>> GetRentalById(int id)
        {
            try
            {
                ResponseRentalDTO responseRentalDTO = await _rentalService.GetRentalById(id);

                ApiResultResponse<ResponseRentalDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseRentalDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo aluguel.",
            Description = "Cria um novo aluguel."
        )]
        public async Task<ActionResult<ResponseRentalDTO>> CreateRental(
            CreateRentalDTO createRentalDTO
        )
        {
            try
            {
                ResponseRentalDTO createdRental = await _rentalService.AddRental(createRentalDTO);

                ApiResultResponse<ResponseRentalDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = createdRental
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um aluguel existente.",
            Description = "Atualiza um aluguel existente."
        )]
        public async Task<ActionResult<ResponseRentalDTO>> UpdateRental(
            UpdateRentalDTO updateRentalDTO
        )
        {
            try
            {
                ResponseRentalDTO updatedRental = await _rentalService.UpdateRental(
                    updateRentalDTO
                );

                ApiResultResponse<ResponseRentalDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = updatedRental
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }
    }
}
