using AutoMapper;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Mappings;

public class InfrastructureMappingProfile: Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
    }
}