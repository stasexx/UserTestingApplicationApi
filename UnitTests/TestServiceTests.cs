using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.IRepositories;
using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Services;
using Moq;

namespace UnitTests;

public class TestServiceTests
{
    private readonly Mock<ITestRepository> _mockTestRepository;
    
    private readonly Mock<IUserTestRepository> _mockUserTestRepository;
    
    private readonly Mock<IMapper> _mockMapper;
    
    private readonly TestService _testService;

    public TestServiceTests()
    {
        _mockTestRepository = new Mock<ITestRepository>();
        _mockUserTestRepository = new Mock<IUserTestRepository>();
        _mockMapper = new Mock<IMapper>();

        _testService = new TestService(_mockTestRepository.Object, _mockMapper.Object, _mockUserTestRepository.Object);
    }

    [Fact]
    public async Task GetTestsPages_ReturnsCorrectData()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var pageNumber = 1;
        var pageSize = 10;
        var cancellationToken = new CancellationToken();
        
        var userTests = new List<UserTest> 
        { 
            new UserTest { TestId = Guid.NewGuid(), UserId = userId, IsCompleted = true, Score = 100 } 
        };

        var tests = new List<Test> 
        { 
            new Test { Id = userTests.First().TestId, Title = "Test 1" } 
        };

        _mockUserTestRepository.Setup(x =>
                x.GetPageAsync(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<Expression<Func<UserTest, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userTests);

        _mockTestRepository.Setup(x =>
                x.GetPageAsync(It.IsAny<int>(), It.IsAny<int>(),
                    It.IsAny<Expression<Func<Test, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tests);

        _mockMapper.Setup(m => m.Map<List<TestDto>>(It.IsAny<List<Test>>()))
            .Returns(tests.Select(t => new TestDto { Id = t.Id, Name = t.Title, IsCompleted = true, Score = 100 }).ToList());

        // Act
        var result = await _testService.GetTestsPages(userId, pageNumber, pageSize, cancellationToken);

        // Assert
        Assert.Single(result.Items);
        var testDto = result.Items.First();
        Assert.Equal(tests.First().Id, testDto.Id);
        Assert.Equal(tests.First().Title, testDto.Name);
        Assert.True(testDto.IsCompleted);
        Assert.Equal(100, testDto.Score);
    }
}
