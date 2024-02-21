using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public class CarImageRepository : BaseRepository<CarImage>, ICarImageRepository
    {
        public CarImageRepository(DataContext context)
            : base(context) { }

        public async Task<CarImage> AddCarImage(CarImage carImage)
        {
            _context.CarImages.Add(carImage);
            await _context.SaveChangesAsync();

            return carImage;
        }

        public async Task<List<CarImage>> GetImageByCarId(int carId)
        {
            var query = _context.CarImages.Where(x => x.CarId == carId);

            var carImages = await query.ToListAsync();

            return carImages;
        }
    }
}
