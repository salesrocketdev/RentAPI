using Microsoft.EntityFrameworkCore;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using Rent.Infrastructure.Data;
using Rent.Core.Models;

namespace Rent.Infrastructure.Repositories
{
    public class OwnerRepository : BaseRepository, IOwnerRepository
    {
        public OwnerRepository(DataContext context)
            : base(context) { }

        public async Task<(List<Owner>, PaginationMeta)> GetAllOwners(int pageNumber, int pageSize)
        {
            var query = _context.Owners.Where(x => x.IsActive == true && x.IsDeleted == false);

            int totalItems = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var owners = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var pagination = new PaginationMeta
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize
            };

            return (owners, pagination);
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            Owner? owner = await _context.Owners
                .Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            return owner ?? throw new Exception("Owner not found");
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return owner;
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            var query =
                await _context.Owners.FindAsync(owner.Id)
                ?? throw new Exception("Owner not found.");

            query.Email = owner.Email;
            query.Address = owner.Address;
            query.Name = owner.Name;
            query.Phone = owner.Phone;

            await _context.SaveChangesAsync();

            return query;
        }

        public async Task DeleteOwner(int id)
        {
            var query =
                await _context.Owners.FindAsync(id) ?? throw new Exception("Owner not found.");
            _context.Owners.Remove(query);
            await _context.SaveChangesAsync();
        }
    }
}
