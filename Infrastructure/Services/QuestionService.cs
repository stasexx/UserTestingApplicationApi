using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<QuestionDto>> GetQuestionsPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _questionRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<QuestionDto>>(entities);
        var count = await _questionRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<QuestionDto>(dtos, pageNumber, pageSize, count);
    }
    
    public async Task<PagedList<QuestionDto>> GetQuestionsByTestIdPages(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities =
            await _questionRepository.GetPageAsync(pageNumber, pageSize, q => q.TestId == testId,
                cancellationToken);
        var dtos = _mapper.Map<List<QuestionDto>>(entities);
        var count = await _questionRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<QuestionDto>(dtos, pageNumber, pageSize, count);
    }
}