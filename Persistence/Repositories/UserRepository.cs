using Application.IRepositories;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(DataContext db) : base(db) { }
}