using Application.Models.Dtos;

namespace Application.IServices;

public interface IUserTestService
{
    Task<UserTestDto> UpdateTestCompletedStatus(Guid testId, Guid userId, bool newStatus,
        CancellationToken cancellationToken);

    Task<UserTestDto> AddUserTest(Guid testId, Guid userId, CancellationToken cancellationToken);
}