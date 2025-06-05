using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Mappings;

public class InfrastructureMappingProfile: Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<User, UserEntity>().ReverseMap();
        
        CreateMap<RefreshToken, RefreshTokenEntity>().ReverseMap();
        
        CreateMap<Actor, ActorEntity>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Movies))
            .ReverseMap();
        
        CreateMap<MovieActor, MovieActorEntity>()
            .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id))
            .ReverseMap();

        CreateMap<MovieRatingEntity, MovieRating>()
            .ReverseMap();
        
        CreateMap<Movie, MovieEntity>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ReverseMap();

        CreateMap<MovieActorDto, MovieActorEntity>();
        
        CreateMap<Person, MovieDirectorEntity>().ReverseMap();

        CreateMap<Person, MovieWriterEntity>().ReverseMap();
        
        CreateMap<Genre, GenreEntity>().ReverseMap();

        CreateMap<Photo, MoviePhotoEntity>().ReverseMap();
        
        CreateMap<Photo, ActorPhotoEntity>().ReverseMap();
    }
}