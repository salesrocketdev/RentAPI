using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        Task<(List<Rental>, PaginationMeta)> GetAllRentals(int pageNumber, int pageSize);
        Task<Rental> GetRentalById(int id);
        Task<Rental> AddRental(Rental rental);
        Task<Rental> UpdateRental(Rental rental);
    }
}
