using Application.Models.Dtos;
using Application.Paging;

namespace Application.IServices;

public interface IOptionService
{
    Task<PagedList<OptionDto>> GetOptionsByTestIdPages(Guid testId, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task<PagedList<OptionDto>> GetOptionsPages(int pageNumber, int pageSize,
        CancellationToken cancellationToken);
}