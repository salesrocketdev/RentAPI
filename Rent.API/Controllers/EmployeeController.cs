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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os funcionários cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de funcionários cadastrados no sistema."
        )]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees(
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
                List<EmployeeDTO> employeeDTOs = _mapper.Map<List<EmployeeDTO>>(employees);

                ApiResponse<List<EmployeeDTO>> response = new ApiResponse<List<EmployeeDTO>>
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
                ApiResponse<List<EmployeeDTO>> response = new ApiResponse<List<EmployeeDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter funcionário por ID.",
            Description = "Retorna um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
        {
            try
            {
                Employee employee = await _employeeService.GetEmployeeById(id);
                EmployeeDTO employeeDTO = _mapper.Map<EmployeeDTO>(employee);

                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = employeeDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo funcionário.",
            Description = "Cria um novo funcionário."
        )]
        public async Task<ActionResult<EmployeeDTO>> CreateEmployee(EmployeeDTO employeeRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Employee employee = _mapper.Map<Employee>(employeeRequest);

                // Adicionar o carro usando o serviço
                Employee addedEmployee = await _employeeService.AddEmployee(employee);

                // Mapear o carro adicionado de volta para CarDTO
                EmployeeDTO addedEmployeeDTO = _mapper.Map<EmployeeDTO>(addedEmployee);

                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedEmployeeDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um funcionário existente.",
            Description = "Atualiza um funcionário existente."
        )]
        public async Task<ActionResult<EmployeeDTO>> UpdateEmployee(EmployeeDTO employeeRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Employee employee = _mapper.Map<Employee>(employeeRequest);

                // Adicionar o carro usando o serviço
                Employee updatedEmployee = await _employeeService.UpdateEmployee(employee);

                // Mapear o carro adicionado de volta para CarDTO
                EmployeeDTO updatedEmployeeDTO = _mapper.Map<EmployeeDTO>(updatedEmployee);

                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedEmployeeDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover funcionário por ID.",
            Description = "Remove um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<EmployeeDTO>> DeleteEmployee(int id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);

                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<EmployeeDTO> response = new ApiResponse<EmployeeDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
