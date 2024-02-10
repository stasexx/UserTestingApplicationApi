using Application.IRepositories;
using Application.Models.Dtos;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class UserTestRepository : BaseRepository<UserTest>, IUserTestRepository
{
    public UserTestRepository(DataContext db) : base(db) { }
    
    public async Task<UserTest> UpdateCompletedStatus(Guid testId, Guid userId, bool newStatus, CancellationToken cancellationToken)
    {
        
        var userTest = await GetOneAsync(x => x.TestId == testId && x.UserId == userId , cancellationToken);
    
        if (userTest != null)
        {
            userTest.IsCompleted = newStatus;
            
            await _db.SaveChangesAsync(cancellationToken);
        
            return userTest;
        }

        return userTest;
    }
}