using Application.IRepositories;
using Persistence.Database;

namespace Persistence.Repositories;

public abstract class BaseRepository<T> : IBaseRepository<T>
{
    protected DataContext _db;
    
    public BaseRepository(DataContext db)
    {
        this._db = db;
    }
    
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await this.AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return entity;
    }
}