using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;
using Rent.Infrastructure.Repositories;

namespace Rent.Infrastructure;

public class RentalRepository : BaseRepository<Rental>, IRentalRepository
{
    public RentalRepository(DataContext context)
        : base(context) { }

    public async Task<(List<Rental>, PaginationMeta)> GetAllRentals(int pageNumber, int pageSize)
    {
        var query = _context
            .Rentals.Include(x => x.Customer)
            .ThenInclude(c => c.Document)
            .Include(x => x.Employee)
            .Include(x => x.Car)
            .Where(x => x.IsActive == true && x.IsDeleted == false);

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var rentals = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var pagination = new PaginationMeta
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };

        return (rentals, pagination);
    }

    public async Task<Rental> GetRentalById(int id)
    {
        Rental? customer = await _context
            .Rentals.Include(x => x.Customer)
            .ThenInclude(c => c.Document)
            .Include(x => x.Employee)
            .Include(x => x.Car)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive == true && x.IsDeleted == false);

        return customer ?? throw new Exception("Rental not found");
    }

    public async Task<Rental> AddRental(Rental rental)
    {
        _context.Rentals.Add(rental);
        await _context.SaveChangesAsync();

        return rental;
    }

    public async Task<Rental> UpdateRental(Rental rental)
    {
        var query =
            await _context.Rentals.FindAsync(rental.Id) ?? throw new Exception("Rental not found.");

        query.CarId = rental.CarId;
        query.CustomerId = rental.CustomerId;
        query.StartDate = rental.StartDate;
        query.EndDate = rental.EndDate;

        await _context.SaveChangesAsync();

        return query;
    }
}
