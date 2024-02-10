using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;

namespace Infrastructure.Services;

public class TestService : ITestService
{
    private readonly ITestRepository _testRepository;

    private readonly IUserTestRepository _userTestRepository;
    
    private readonly IMapper _mapper;

    public TestService(ITestRepository testRepository, IMapper mapper, IUserTestRepository userTestRepository)
    {
        _testRepository = testRepository;
        _mapper = mapper;
        _userTestRepository = userTestRepository;
    }
    
    public async Task<PagedList<TestDto>> GetTestsPages(Guid userId, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var userTests = await _userTestRepository.GetPageAsync(1, 50,
            x => x.UserId == userId, cancellationToken);
        var testIds = userTests.Select(ut => ut.TestId).Distinct().ToList();
        
        var entities = await _testRepository.GetPageAsync(1, testIds.Count, test => testIds.Contains(test.Id), cancellationToken);
        
        var dtos = _mapper.Map<List<TestDto>>(entities);
        var count = await _testRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<TestDto>(dtos, pageNumber, pageSize, count);
    }
    
}