using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, Test>();
    }
}