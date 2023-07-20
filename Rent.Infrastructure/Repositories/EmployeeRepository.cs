using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(DataContext context)
            : base(context) { }

        public async Task<(List<Employee>, PaginationMeta)> GetAllEmployees(
            int pageNumber,
            int pageSize
        )
        {
            var query = _context.Employees.Where(x => x.IsActive == true && x.IsDeleted == false);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagination = new PaginationMeta
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return (employees, pagination);
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            Employee? employee = await _context.Employees
                .Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return employee ?? throw new Exception("Employee not found");
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var query =
                await _context.Employees.FindAsync(employee.Id)
                ?? throw new Exception("Employee not found.");

            query.Name = employee.Name;
            query.Age = employee.Age;
            query.Address = employee.Address;
            query.Phone = employee.Phone;
            query.Email = employee.Email;

            return query;
        }

        public async Task DeleteEmployee(int id)
        {
            var query =
                await _context.Employees.FindAsync(id)
                ?? throw new Exception("Customer not found.");

            _context.Employees.Remove(query);
            await _context.SaveChangesAsync();
        }
    }
}
