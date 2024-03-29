﻿using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly ISecurityService _securityService;

        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
        private readonly IOwnerService _ownerService;
        private readonly ILoginRepository _loginRepository;

        public LoginService(
            ISecurityService securityService,
            ICustomerService customerService,
            IEmployeeService employeeService,
            IOwnerService ownerService,
            ILoginRepository loginRepository
        )
        {
            _securityService = securityService;
            _customerService = customerService;
            _employeeService = employeeService;
            _ownerService = ownerService;
            _loginRepository = loginRepository;
        }

        public async Task<(List<Login>, PaginationMeta)> GetAllLogins(int pageNumber, int pageSize)
        {
            return await _loginRepository.GetAllLogins(pageNumber, pageSize);
        }

        public async Task<Login> GetLoginById(int id)
        {
            return await _loginRepository.GetLoginById(id);
        }

        public async Task<Login> GetLoginByParentId(int parentId)
        {
            return await _loginRepository.GetLoginByParentId(parentId);
        }

        public async Task<Login> AddLogin(Login login)
        {
            if (login.UserType == Enums.UserType.Owner)
            {
                var owner = await _ownerService.GetOwnerById(login.ParentId);

                if (owner == null)
                    throw new Exception("Owner não existe.");

                login.Email = owner.Email;
            }

            if (login.UserType == Enums.UserType.Employee)
            {
                var employee = await _employeeService.GetEmployeeById(login.ParentId);

                if (employee == null)
                    throw new Exception("Employee não existe.");

                login.Email = employee.Email;
            }

            if (login.UserType == Enums.UserType.Customer)
            {
                var customer = await _customerService.GetCustomerById(login.ParentId);

                if (customer == null)
                    throw new Exception("Customer não existe.");

                login.Email = customer.Email;
            }

            if (login.Password == null) throw new Exception("Senha inválida.");
        
            var hash = _securityService.HashPassword(login.Password, out var salt);

            login.PasswordHash = hash;
            login.PasswordSalt = salt;

            return await _loginRepository.AddLogin(login);
        }

        public async Task<Login> UpdateLogin(Login login)
        {
            return await _loginRepository.UpdateLogin(login);
        }

        public async Task DeleteLogin(int id)
        {
            await _loginRepository.DeleteLogin(id);
        }
    }
}
