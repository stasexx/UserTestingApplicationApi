using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Application.Paging;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserTestService : IUserTestService
{
    private readonly IUserTestRepository _userTestRepository;

    private readonly IMapper _mapper;

    public UserTestService(IUserTestRepository userTestRepository, IMapper mapper)
    {
        _userTestRepository = userTestRepository;
        _mapper = mapper;
    }
    
    public async Task<PagedList<UserTestDto>> GetUserTestsByUserIdPages(Guid userId, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        var entities =
            await _userTestRepository.GetPageAsync(pageNumber, pageSize, u => u.UserId == userId,
                cancellationToken);
        var dtos = _mapper.Map<List<UserTestDto>>(entities);
        var count = await _userTestRepository.GetTotalCountAsync(cancellationToken);
        return new PagedList<UserTestDto>(dtos, pageNumber, pageSize, count);
    }

    public async Task<UserTestDto> UpdateTestCompletedStatus(Guid testId, Guid userId, bool newStatus, CancellationToken cancellationToken)
    {
        var userTest = await _userTestRepository.UpdateCompletedStatus(testId, userId, newStatus, cancellationToken);

        if (userTest == null)
        {
            throw new ArgumentNullException("UserTest");
        }
        
        var dto = _mapper.Map<UserTestDto>(userTest);

        return dto;
    }
    
    public async Task<UserTestDto> AddUserTest(Guid testId, Guid userId, CancellationToken cancellationToken)
    {
        if (testId==null && userId==null)
        {
            throw new ArgumentNullException("Id");
        }

        var userTest = new UserTest
        {
            Id = Guid.NewGuid(),
            IsCompleted = false,
            Score = 0,
            TestId = testId,
            UserId = userId
        };
        
        var userTestUpdated = await _userTestRepository.AddAsync(userTest, cancellationToken);

        if (userTest == null)
        {
            throw new ArgumentNullException("UserTest");
        }
        
        var dto = _mapper.Map<UserTestDto>(userTestUpdated);

        return dto;
    }
}