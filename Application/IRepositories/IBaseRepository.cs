using System.Linq.Expressions;

namespace Application.IRepositories;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}