using System.Linq.Expressions;

namespace Rent.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity>
    where TEntity : class
{
    TEntity? Find(
        Expression<Func<TEntity, bool>> where,
        params Expression<Func<TEntity, object>>[] includes
    );
}
