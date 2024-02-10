using Application.IRepositories.Identity;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories.Identity;

public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
{
    public RefreshTokensRepository(DataContext db) : base(db) { }
}