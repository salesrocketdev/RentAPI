using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Infrastructure.Data;
using Rent.Core.Models;
using Rent.Domain.Interfaces.Repositories;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Rent.Infrastructure.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(DataContext context)
            : base(context) { }

        public async Task<Login> Authenticate(string email, string password)
        {
            Login? login = await _context.Logins
                .Where(
                    x =>
                        x.Email == email
                        && x.Password == password
                        && x.IsActive == true
                        && x.IsDeleted == false
                )
                .FirstOrDefaultAsync();

            return login ?? throw new Exception("Login not found");
        }

        public async Task<(List<Login>, PaginationMeta)> GetAllLogins(int pageNumber, int pageSize)
        {
            var query = _context.Logins.Where(x => x.IsActive == true && x.IsDeleted == false);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var logins = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagination = new PaginationMeta
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return (logins, pagination);
        }

        public async Task<Login> GetLoginById(int id)
        {
            Login? login = await _context.Logins
                .Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return login ?? throw new Exception("Login not found");
        }

        public async Task<Login> AddLogin(Login login)
        {
            _context.Logins.Add(login);
            await _context.SaveChangesAsync();

            return login;
        }

        public async Task<Login> UpdateLogin(Login login)
        {
            var query =
                await _context.Logins.FindAsync(login.Id)
                ?? throw new Exception("Login not found.");

            query.Email = login.Email;
            query.Password = login.Password;

            await _context.SaveChangesAsync();

            return query;
        }

        public async Task DeleteLogin(int id)
        {
            var query =
                await _context.Logins.FindAsync(id) ?? throw new Exception("Login not found.");
            _context.Logins.Remove(query);

            await _context.SaveChangesAsync();
        }
    }
}
