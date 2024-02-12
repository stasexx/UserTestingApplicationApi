using Application.Models.Dtos;
using Application.Paging;

namespace Application.IServices;

public interface IQuestionService
{
    Task<PagedList<QuestionDto>> GetQuestionsPages(int pageNumber, int pageSize, CancellationToken cancellationToken);

    Task<PagedList<QuestionDto>> GetQuestionsByTestIdPages(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task<List<QuestionOptionDto>> GetQuestionsWithOptionsByTestAsync(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}