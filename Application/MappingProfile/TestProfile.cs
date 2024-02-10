using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class TestProfile : Profile
{
    public TestProfile()
    {
        CreateMap<Test, TestDto>();
        CreateMap<TestDto, Test>();
    }
}