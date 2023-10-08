using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;
using System;

namespace Rent.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(DataContext context)
            : base(context) { }

        public async Task<(List<Customer>, PaginationMeta)> GetAllCustomers(
            int pageNumber,
            int pageSize
        )
        {
            var query = _context.Customers
                .Include(x => x.Document)
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
            Customer? customer = await _context.Customers
                .Include(x => x.Document)
                .Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return customer ?? throw new Exception("Customer not found");
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
            query.Address = customer.Address;
            query.Name = customer.Name;
            query.Phone = customer.Phone;

            await _context.SaveChangesAsync();

            return query;
        }

        public async Task DeleteCustomer(int id)
        {
            var query =
                await _context.Customers.FindAsync(id)
                ?? throw new Exception("Customer not found.");
            _context.Customers.Remove(query);
            await _context.SaveChangesAsync();
        }
    }
}
