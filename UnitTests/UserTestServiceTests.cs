using Moq;
using AutoMapper;
using Domain.Entities;
using Application.IRepositories;
using Infrastructure.Services;
using Application.Models.Dtos;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTests;

public class UserTestServiceTests
{
    private readonly Mock<IUserTestRepository> _mockUserTestRepository;
    
    private readonly Mock<IMapper> _mockMapper;
    
    private readonly UserTestService _service;

    public UserTestServiceTests()
    {
        _mockUserTestRepository = new Mock<IUserTestRepository>();
        _mockMapper = new Mock<IMapper>();
        _service = new UserTestService(_mockUserTestRepository.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task UpdateTestCompletedStatus_ReturnsUpdatedUserTestDto()
    {
        // Arrange
        var testId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var score = 100.0;
        var cancellationToken = new CancellationToken();

        var userTest = new UserTest
        {
            TestId = testId,
            UserId = userId,
            Score = score,
            IsCompleted = true
        };

        _mockUserTestRepository.Setup(repo => repo.UpdateCompletedStatus(testId, userId, score, cancellationToken))
            .ReturnsAsync(userTest);

        var expectedDto = new UserTestDto
        {
            Score = score,
            IsCompleted = true
        };

        _mockMapper.Setup(mapper => mapper.Map<UserTestDto>(It.IsAny<UserTest>()))
            .Returns(expectedDto);

        // Act
        var result = await _service.UpdateTestCompletedStatus(testId, userId, score, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDto.Score, result.Score);
        Assert.Equal(expectedDto.IsCompleted, result.IsCompleted);

        _mockUserTestRepository.Verify(repo => repo.UpdateCompletedStatus(testId, userId, score, cancellationToken), Times.Once);
        _mockMapper.Verify(mapper => mapper.Map<UserTestDto>(userTest), Times.Once);
    }
}