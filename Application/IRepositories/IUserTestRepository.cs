using Domain.Entities;

namespace Application.IRepositories;

public interface IUserTestRepository : IBaseRepository<UserTest>
{
    Task<UserTest> UpdateCompletedStatus(Guid testId, Guid userId, bool newStatus, CancellationToken cancellationToken);
}