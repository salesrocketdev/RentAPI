using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface ILoginRepository
    {
        Task<Login> Authenticate(string email, string password);
    }
}
