using Application.Models.Dtos;
using Domain.Entities;

namespace Application.IServices;

public interface IUserService
{
    Task<User> AddUserAsync(UserDto userDto, CancellationToken cancellationToken);
}