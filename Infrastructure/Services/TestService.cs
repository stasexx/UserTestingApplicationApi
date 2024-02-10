using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;

namespace Infrastructure.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;
    
    private readonly IMapper _mapper;

    public TestService(ITestRepository testRepository, IMapper mapper)
    {
        _testRepository = testRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<TestDto>> GetTestsPages(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _testRepository.GetPageAsync(pageNumber, pageSize, cancellationToken);
        var dtos = _mapper.Map<List<TestDto>>(entities);
        var count = await _testRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<TestDto>(dtos, pageNumber, pageSize, count);
    }
    
}