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
        var userTestsPage = await _userTestRepository.GetPageAsync(pageNumber, pageSize, x => x.UserId == userId, cancellationToken);
        
        var testIds = userTestsPage.Select(ut => ut.TestId).Distinct();
        var tests = await _testRepository.GetPageAsync(pageNumber, pageSize, test => testIds.Contains(test.Id), cancellationToken);
        
        var userTestsMap = userTestsPage.ToDictionary(ut => ut.TestId, ut => ut);
    
        var dtos = tests.Select(test =>
        {
            var isCompleted = userTestsMap.TryGetValue(test.Id, out var userTest) && userTest.IsCompleted;
            var score = userTestsMap.TryGetValue(test.Id, out userTest) ? userTest.Score : 0;
        
            return new TestDto
            {
                Id = test.Id,
                Name = test.Tittle,
                IsCompleted = isCompleted,
                Score = score
            };
        }).ToList();
        
        var count = userTestsPage.Count;
    
        return new PagedList<TestDto>(dtos, pageNumber, pageSize, count);
    }
    
}