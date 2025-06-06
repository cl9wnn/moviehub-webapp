using AutoMapper;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Database.Entities;

namespace Infrastructure.Database.Mappings;

public class InfrastructureMappingProfile: Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<User, UserEntity>()
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<RefreshToken, RefreshTokenEntity>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Actor, ActorEntity>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Movies))
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Movie, MovieEntity>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<MovieActorDto, MovieActorEntity>()
            .ForMember(dest => dest.Movie, opt => opt.Ignore())
            .ForMember(dest => dest.Actor, opt => opt.Ignore());
        
        CreateMap<Person, MovieDirectorEntity>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Person, MovieWriterEntity>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Genre, GenreEntity>()
            .ForMember(dest => dest.Movies, opt => opt.Ignore())
            .ForMember(dest => dest.Users, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<Photo, MoviePhotoEntity>()
            .ForMember(dest => dest.MovieId, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore())
            .ReverseMap();       
        
        CreateMap<Photo, ActorPhotoEntity>()
            .ForMember(dest => dest.ActorId, opt => opt.Ignore())
            .ForMember(dest => dest.Actor, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<MovieActor, MovieActorEntity>()
            .ReverseMap();

        CreateMap<MovieRatingEntity, MovieRating>()
            .ReverseMap();
    }
}