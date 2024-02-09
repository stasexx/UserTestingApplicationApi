namespace Application.IRepositories;

public interface IBaseRepository<T>
{
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);
}