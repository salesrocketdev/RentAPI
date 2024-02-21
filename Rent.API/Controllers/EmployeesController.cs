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
    [SwaggerTag("Funcionários")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
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
                var (employees, pagination) = await _employeeService.GetAllEmployees(
                    pageNumber,
                    pageSize
                );
                List<ResponseEmployeeDTO> employeeDTOs = _mapper.Map<List<ResponseEmployeeDTO>>(
                    employees
                );

                ApiResponse<List<ResponseEmployeeDTO>> response =
                    new()
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
                ApiResponse<List<ResponseEmployeeDTO>> response =
                    new() { Code = 0, Message = ex.Message, };

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
                Employee employee = await _employeeService.GetEmployeeById(id);
                ResponseEmployeeDTO employeeDTO = _mapper.Map<ResponseEmployeeDTO>(employee);

                ApiResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = employeeDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseEmployeeDTO> response =
                    new() { Code = 0, Message = ex.Message };

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
                Employee mappedEmployee = _mapper.Map<Employee>(createEmployeeDTO);

                Employee createdEmployee = await _employeeService.AddEmployee(mappedEmployee);

                ApiResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<ResponseEmployeeDTO>(createdEmployee)
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
                Employee mappedEmployee = _mapper.Map<Employee>(updateEmployeeDTO);

                Employee updatedEmployee = await _employeeService.UpdateEmployee(mappedEmployee);

                ApiResponse<ResponseEmployeeDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<ResponseEmployeeDTO>(updatedEmployee)
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseEmployeeDTO> response =
                    new() { Code = 0, Message = ex.Message, };

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
                ApiResponse<ResponseEmployeeDTO> response =
                    new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }
    }
}
