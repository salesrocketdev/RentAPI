using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(DataContext context)
            : base(context) { }

        public async Task<(List<Car>, PaginationMeta)> GetAllCars(
            int? brandId,
            int pageNumber,
            int pageSize
        )
        {
            var query = _context
                .Cars.Include(x => x.CarImages)
                .Include(x => x.Brand)
                .Where(x => x.IsActive == true && x.IsDeleted == false);

            if (brandId != 0)
            {
                query = query.Where(x => x.BrandId == brandId);
            }

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            List<Car> cars = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var pagination = new PaginationMeta
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return (cars, pagination);
        }

        public async Task<Car> GetCarById(int id)
        {
            Car? car = await _context
                .Cars.Include(x => x.CarImages)
                .Include(x => x.Brand)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            return car ?? throw new Exception("Car not found");
        }

        public async Task<Car> AddCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> UpdateCar(Car car)
        {
            var query =
                await _context.Cars.FindAsync(car.Id) ?? throw new Exception("Car not found.");

            query.Model = car.Model;
            query.Year = car.Year;
            query.Color = car.Color;
            query.SeatsNumber = car.SeatsNumber;
            query.Plate = car.Plate;
            query.DailyValue = car.DailyValue;
            query.Available = car.Available;
            query.BrandId = car.BrandId;
            query.FuelType = car.FuelType;
            query.TransmissionType = car.TransmissionType;
            query.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return query;
        }

        public async Task<bool> DeleteCar(int id)
        {
            var query = await _context.Cars.FindAsync(id) ?? throw new Exception("Car not found.");

            query.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
