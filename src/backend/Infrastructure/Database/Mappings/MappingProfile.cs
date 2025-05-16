using AutoMapper;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Mappings;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
    }
}