using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<(List<Login>, PaginationMeta)> GetAllLogins(int pageNumber, int pageSize);
        Task<Login> AddLogin(Login login);
        Task<Login> GetLoginById(int id);
        Task<Login> UpdateLogin(Login login);
        Task DeleteLogin(int id);
    }
}
