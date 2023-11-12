using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Repositories
{
    public interface IRentalRepository
    {
        Task<(List<Rental>, PaginationMeta)> GetAllRentals(int pageNumber, int pageSize);
        Task<Rental> GetRentalById(int id);
        Task<Rental> AddRental(Rental owner);
        Task<Rental> UpdateRental(Rental owner);
    }
}
