using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context)
            : base(context) { }

        public async Task<(List<Customer>, PaginationMeta)> GetAllCustomers(
            int pageNumber,
            int pageSize
        )
        {
            var query = _context
                .Customers.Include(x => x.Document)
                .Where(x => x.IsActive == true && x.IsDeleted == false);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var customers = await query
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

            return (customers, pagination);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            Customer? customer = await _context
                .Customers.Include(x => x.Document)
                .Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return customer ?? throw new Exception("Customer not found");
        }

        public async Task<Customer> GetCustomerByEmailOrTaxNumber(string email, string taxNumber)
        {
            Customer? customerVerification = await _context
                .Customers.Include(x => x.Document)
                .Where(x =>
                    x.Email == email && x.IsActive == true && x.IsDeleted == false
                    || x.Document.TaxNumber == taxNumber
                        && x.IsActive == true
                        && x.IsDeleted == false
                )
                .FirstOrDefaultAsync();

            return customerVerification ?? null;
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var query =
                await _context.Customers.FindAsync(customer.Id)
                ?? throw new Exception("Customer not found.");

            query.Email = customer.Email;
            query.Name = customer.Name;
            query.Phone = customer.Phone;
            query.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return query;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var query =
                await _context.Customers.FindAsync(id)
                ?? throw new Exception("Customer not found.");

            query.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
