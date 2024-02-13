using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Application.IRepositories;
using Persistence.Database;

namespace Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected DataContext _db;
    
    public BaseRepository(DataContext db)
    {
        this._db = db;
    }
    
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _db.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return entity;
    }
    
    public async Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>().Where(predicate).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetPageAsync(int pageNumber, int pageSize, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>()
            .Where(predicate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<int> GetTotalCountAsync(CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>().CountAsync(cancellationToken);
    }
}