using AutoMapper;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace API.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
    }
}