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
    [SwaggerTag("Funcionários")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(Roles = "Owner")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os funcionários cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de funcionários cadastrados no sistema."
        )]
        public async Task<ActionResult<List<ResponseEmployeeDTO>>> GetAllEmployees(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                ResponsePaginateDTO<ResponseEmployeeDTO> responsePaginateDTO =
                    await _employeeService.GetAllEmployees(pageNumber, pageSize);

                ApiResponse<List<ResponseEmployeeDTO>> response =
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

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter funcionário por ID.",
            Description = "Retorna um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseEmployeeDTO>> GetEmployeeById(int id)
        {
            try
            {
                ResponseEmployeeDTO responseEmployeeDTO = await _employeeService.GetEmployeeById(
                    id
                );

                ApiResultResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseEmployeeDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo funcionário.",
            Description = "Cria um novo funcionário."
        )]
        public async Task<ActionResult<ResponseEmployeeDTO>> CreateEmployee(
            CreateEmployeeDTO createEmployeeDTO
        )
        {
            try
            {
                ResponseEmployeeDTO createdEmployee = await _employeeService.AddEmployee(
                    createEmployeeDTO
                );

                ApiResultResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = createdEmployee
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CreateEmployeeDTO> response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um funcionário existente.",
            Description = "Atualiza um funcionário existente."
        )]
        public async Task<ActionResult<ResponseEmployeeDTO>> UpdateEmployee(
            UpdateEmployeeDTO updateEmployeeDTO
        )
        {
            try
            {
                ResponseEmployeeDTO updatedEmployee = await _employeeService.UpdateEmployee(
                    updateEmployeeDTO
                );

                ApiResultResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = updatedEmployee
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover funcionário por ID.",
            Description = "Remove um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseEmployeeDTO>> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);

                ApiResponse<ResponseEmployeeDTO> response =
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
