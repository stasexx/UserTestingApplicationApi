using Application.Models.Dtos;
using Application.Models.Identity;
using Domain.Entities;

namespace Application.IServices;

public interface IUserService
{
    Task<User> AddUserAsync(UserDto userDto, CancellationToken cancellationToken);

    Task<TokensModel> LoginAsync(UserDto userDto, CancellationToken cancellationToken);
}