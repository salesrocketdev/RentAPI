﻿using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<(List<Customer>, PaginationMeta)> GetAllCustomers(int pageNumber, int pageSize);
        Task<Customer> GetCustomerById(int id);
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task DeleteCustomer(int id);
    }
}