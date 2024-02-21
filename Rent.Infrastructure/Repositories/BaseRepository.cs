using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected readonly DataContext _context;

        protected DbSet<TEntity> DbSet
        {
            get { return _context.Set<TEntity>(); }
        }

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public TEntity? Find(
            Expression<Func<TEntity, bool>> where,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            try
            {
                var query = DbSet.AsNoTracking().AsQueryable();

                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

                return query.FirstOrDefault(where);
            }
            catch
            {
                throw;
            }
        }
    }
}
