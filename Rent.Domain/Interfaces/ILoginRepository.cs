using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login> Authenticate(string email, string password);
        Task<(List<Login>, PaginationMeta)> GetAllLogins(int pageNumber, int pageSize);
        Task<Login> GetLoginById(int id);
        Task<Login> AddLogin(Login login);
        Task<Login> UpdateLogin(Login login);
        Task DeleteLogin(int id);
    }
}
