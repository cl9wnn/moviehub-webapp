using API.Models;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Mappings;

public class ApiMappingProfile: Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        CreateMap<LoginUserRequest, User>();
    }
}