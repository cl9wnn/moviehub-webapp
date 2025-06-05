using System.Security.Cryptography;
using API.Models.Requests;
using API.Models.Responses;
using AutoMapper;
using Domain.Dtos;
using Domain.Models;

namespace API.Mappings;

public class ApiMappingProfile: Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        
        CreateMap<RegisterAdminRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        
        CreateMap<LoginUserRequest, User>();

        CreateMap<CreateActorRequest, Actor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        
        CreateMap<string, Photo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src));

        CreateMap<CreateMovieActorRequest, MovieActor>();
        
        CreateMap<CreatePersonRequest, Person>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<int, Genre>()
            .ConvertUsing(id => new Genre { Id = id });
        
        CreateMap<CreateMovieRequest, Movie>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src =>
                src.GenreIds.Select(id => new Genre { Id = id }).ToList()));

        CreateMap<PersonalizeUserRequest, PersonalizeUserDto>();
        
        CreateMap<CreateMovieActorByIdRequest, MovieActorDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom((_, _, _, context) =>
                (Guid)context.Items["MovieId"]));
        
        CreateMap<Photo, string>().ConvertUsing(src => src.ImageUrl);
        
        CreateMap<Genre, string>().ConvertUsing(src => src.Name);
        
        CreateMap<Actor, ActorResponse>();
        
        CreateMap<Actor, ActorCardResponse>();
        
        CreateMap<ActorWithUserInfoDto, ActorWithUserInfoResponse>();
        
        CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));


        CreateMap<Movie, RatedMovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));

        CreateMap<MovieRating, RatedMovieCardResponse>()
            .IncludeMembers(src => src.Movie)
            .ForMember(dest => dest.OwnRating, opt => opt.MapFrom(src => src.Rating));
        
        CreateMap<Movie, MovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));

        CreateMap<MovieWithUserInfoDto, MovieWithUserInfoResponse>();
        
        CreateMap<Person, PersonResponse>();

        CreateMap<User, UserResponse>();
        
        CreateMap<MovieActor, ActorCardResponse>()
            .IncludeMembers(src => src.Actor);
        
        CreateMap<MovieActor, MovieCardResponse>()
            .IncludeMembers(src => src.Movie);
    }
    
    private static double CalculateUserRating(Movie movie)
    {
        return movie.RatingCount == 0 ? 0 : Math.Round(movie.RatingSum / movie.RatingCount, 1);
    }
}