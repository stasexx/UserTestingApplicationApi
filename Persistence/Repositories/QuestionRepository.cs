using Application.IRepositories;
using Domain.Entities;
using Persistence.Database;

namespace Persistence.Repositories;

public class QuestionRepository: BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(DataContext db) : base(db) { }
}