using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ResponsePaginateDTO<ResponseEmployeeDTO>> GetAllEmployees(
            int pageNumber,
            int pageSize
        )
        {
            (List<Employee>, PaginationMeta) employees = await _employeeRepository.GetAllEmployees(
                pageNumber,
                pageSize
            );

            var (Data, PaginationMeta) = employees;

            ResponsePaginateDTO<ResponseEmployeeDTO> responsePaginateDTO =
                new()
                {
                    Data = _mapper.Map<List<ResponseEmployeeDTO>>(Data),
                    PaginationMeta = PaginationMeta
                };

            return responsePaginateDTO;
        }

        public async Task<ResponseEmployeeDTO> GetEmployeeById(int id)
        {
            Employee employee = await _employeeRepository.GetEmployeeById(id);

            return _mapper.Map<ResponseEmployeeDTO>(employee);
        }

        public async Task<ResponseEmployeeDTO> AddEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            Employee mappedEmployee = _mapper.Map<Employee>(createEmployeeDTO);

            var newEmployee = await _employeeRepository.AddEmployee(mappedEmployee);

            return _mapper.Map<ResponseEmployeeDTO>(newEmployee);
        }

        public async Task<ResponseEmployeeDTO> UpdateEmployee(UpdateEmployeeDTO updateEmployeeDTO)
        {
            Employee mappedEmployee = _mapper.Map<Employee>(updateEmployeeDTO);

            var updatedEmployee = await _employeeRepository.UpdateEmployee(mappedEmployee);

            return _mapper.Map<ResponseEmployeeDTO>(updatedEmployee);
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            return await _employeeRepository.DeleteEmployee(id);
        }
    }
}
