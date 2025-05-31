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

        CreateMap<PreferredGenresRequest, List<Genre>>();
        
        CreateMap<CreateMovieActorByIdRequest, MovieActorDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom((_, _, _, context) =>
                (Guid)context.Items["MovieId"]));
        
        CreateMap<Photo, string>().ConvertUsing(src => src.ImageUrl);
        
        CreateMap<Genre, string>().ConvertUsing(src => src.Name);
        
        CreateMap<Actor, ActorResponse>();
        
        CreateMap<Actor, ActorCardResponse>();

        CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src =>
                src.RatingCount == 0 ? 0 : Math.Round(src.RatingSum / src.RatingCount, 1)));

        CreateMap<Movie, MovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src =>
            src.RatingCount == 0 ? 0 : Math.Round(src.RatingSum / src.RatingCount, 1)));
        
        CreateMap<Person, PersonResponse>();

        CreateMap<User, UserResponse>();
        
        CreateMap<MovieActor, ActorCardResponse>()
            .IncludeMembers(src => src.Actor);
        
        CreateMap<MovieActor, MovieCardResponse>()
            .IncludeMembers(src => src.Movie);
    }
}