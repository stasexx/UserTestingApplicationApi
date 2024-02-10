using Application.IRepositories;
using Application.Models.Dtos;
using Application.Paging;
using Domain.Entities;

namespace Application.IServices;

public interface ITestService
{
    Task<PagedList<TestDto>> GetTestsPages(Guid userId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}