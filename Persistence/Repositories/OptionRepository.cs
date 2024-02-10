using Application.IRepositories;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class OptionRepository : BaseRepository<Option>, IOptionRepository
{
    public OptionRepository(DataContext db) : base(db) { }
}
