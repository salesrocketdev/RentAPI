using Microsoft.EntityFrameworkCore;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using Rent.Infrastructure.Data;
using Rent.Core.Models;

namespace Rent.Infrastructure.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(DataContext context) : base(context)
        {

        }
        public async Task<(List<Car>, PaginationMeta)> GetAllCars(int pageNumber, int pageSize)
        {
            var query = _context.Cars
                .Where(x => x.IsActive == true && x.IsDeleted == false);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var cars = await query
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
            Car? car = await _context.Cars.FindAsync(id);
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
            var query = await _context.Cars.FindAsync(car.Id) ?? throw new Exception("Car not found.");
            query.Available = car.Available;
            query.Brand = car.Brand;
            query.Color = car.Color;
            query.Model = car.Model;
            query.Plate = car.Plate;
            query.Year = car.Year;

            await _context.SaveChangesAsync();

            return query;
        }
        public async Task DeleteCar(int id)
        {
            var query = await _context.Cars.FindAsync(id) ?? throw new Exception("Car not found.");
            query.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}
