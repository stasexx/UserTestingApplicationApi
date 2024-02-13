using Application.Models.Dtos;
using Application.Models.Identity;
using Domain.Entities;

namespace Application.IServices;

public interface IUserService
{
    Task<UserDto> GetUserAsync(string name, CancellationToken cancellationToken);

    Task<TokensModel> LoginAsync(UserLoginDto userDto, CancellationToken cancellationToken);
}