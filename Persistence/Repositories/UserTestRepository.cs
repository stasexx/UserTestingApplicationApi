using Application.IRepositories;
using Application.Models.Dtos;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class UserTestRepository : BaseRepository<UserTest>, IUserTestRepository
{
    public UserTestRepository(DataContext db) : base(db) { }
    
    public async Task<UserTest> UpdateCompletedStatus(Guid testId, Guid userId,double score, CancellationToken cancellationToken)
    {
        
        var userTest = await GetOneAsync(x => x.TestId == testId && x.UserId == userId , cancellationToken);
    
        if (userTest != null)
        {
            userTest.IsCompleted = true;
            userTest.Score = score;
            
            await _db.SaveChangesAsync(cancellationToken);
        
            return userTest;
        }

        return userTest;
    }
}