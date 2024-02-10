using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class UserTestProfile : Profile
{
    public UserTestProfile()
    {
        CreateMap<UserTest, UserTestDto>();
        CreateMap<UserTestDto, UserTest>();
    }
}