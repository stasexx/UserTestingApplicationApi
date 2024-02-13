using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;

namespace Infrastructure.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    private readonly IOptionRepository _optionRepository;

    private readonly IMapper _mapper;

    public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IOptionRepository optionRepository)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
        _optionRepository = optionRepository;
    }
    
    public async Task<PagedList<QuestionDto>> GetQuestionsPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _questionRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<QuestionDto>>(entities);
        var count = await _questionRepository.GetTotalCountAsync(cancellationToken);
        
        return new PagedList<QuestionDto>(dtos, pageNumber, pageSize, count);
    }
    
    public async Task<List<QuestionOptionDto>> GetQuestionsWithOptionsByTestAsync(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var questionsEntities = await _questionRepository.GetPageAsync(pageNumber, pageSize, 
            q => q.TestId == testId, cancellationToken);
    
        var questionsDtos = _mapper.Map<List<QuestionDto>>(questionsEntities);
    
        var questionOptionDtos = new List<QuestionOptionDto>();

        foreach (var questionDto in questionsDtos)
        {
            var optionsEntities = await _optionRepository.GetPageAsync(pageNumber, pageSize, 
                o => o.QuestionId == questionDto.Id, cancellationToken);

            var optionsDtos = _mapper.Map<List<OptionDto>>(optionsEntities);

            questionOptionDtos.Add(new QuestionOptionDto
            {
                QuestionDto = questionDto,
                OptionDtos = optionsDtos.ToArray()
            });
        }

        return questionOptionDtos;
    }
}