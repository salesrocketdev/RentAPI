using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<ResponsePaginateDTO<ResponseEmployeeDTO>> GetAllEmployees(
            int pageNumber,
            int pageSize
        );
        Task<ResponseEmployeeDTO> GetEmployeeById(int id);
        Task<ResponseEmployeeDTO> AddEmployee(CreateEmployeeDTO employee);
        Task<ResponseEmployeeDTO> UpdateEmployee(UpdateEmployeeDTO employee);
        Task<bool> DeleteEmployee(int id);
    }
}
