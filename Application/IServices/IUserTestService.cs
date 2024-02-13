using Application.Models.Dtos;

namespace Application.IServices;

public interface IUserTestService
{
    Task<UserTestDto> AddUserTest(Guid testId, Guid userId, CancellationToken cancellationToken);

    Task<UserTestDto> UpdateTestCompletedStatus(Guid testId, Guid userId, double score,
        CancellationToken cancellationToken);
}