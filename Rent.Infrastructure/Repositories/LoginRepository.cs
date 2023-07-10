using Microsoft.EntityFrameworkCore;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using Rent.Infrastructure.Data;
using Rent.Core.Models;

namespace Rent.Infrastructure.Repositories
{
    public class LoginRepository : BaseRepository, ILoginRepository
    {
        public LoginRepository(DataContext context) : base(context)
        {

        }
        public async Task<Login> Authenticate(string email, string password)
        {
            Login? login = await _context.Logins
                .Where(x => x.Email == email && x.Password == password && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return login ?? throw new Exception("Login not found");
        }
    }
}
