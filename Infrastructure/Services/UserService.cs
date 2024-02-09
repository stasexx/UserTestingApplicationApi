using Application.IRepositories;
using Application.IServices;
using Application.Models.Dtos;
using Domain.Entities;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AddUserAsync(UserDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            var user = new User
            {
                Name = userDto.Name,
                UserTests = new List<UserTest>(),
                Id = Guid.NewGuid()
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}