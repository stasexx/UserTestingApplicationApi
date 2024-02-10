using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;

namespace Infrastructure.Services;

public class OptionService : IOptionService
{
    private readonly IOptionRepository _optionRepository;

    private readonly IMapper _mapper;

    public OptionService(IOptionRepository optionRepository, IMapper mapper)
    {
        _optionRepository = optionRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<OptionDto>> GetOptionsPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _optionRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<OptionDto>>(entities);
        var count = await _optionRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<OptionDto>(dtos, pageNumber, pageSize, count);
    }
    
    public async Task<PagedList<OptionDto>> GetOptionsByTestIdPages(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities =
            await _optionRepository.GetPageAsync(pageNumber, pageSize, q => q.QuestionId == testId,
                cancellationToken);
        var dtos = _mapper.Map<List<OptionDto>>(entities);
        var count = await _optionRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<OptionDto>(dtos, pageNumber, pageSize, count);
    }
}