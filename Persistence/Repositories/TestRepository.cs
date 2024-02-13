using Application.IRepositories;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class TestRepository : BaseRepository<Test>, ITestRepository
{
    public TestRepository(DataContext db) : base(db) { }
}