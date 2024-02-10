using Application.Models.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile;

public class OptionProfile : Profile
{
    public OptionProfile()
    {
        CreateMap<Option, OptionDto>();
        CreateMap<OptionDto, Option>();
    }
}