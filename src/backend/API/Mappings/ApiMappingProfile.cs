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
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Bio, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.FavoriteActors, opt => opt.Ignore())
            .ForMember(dest => dest.WatchList, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredGenres, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore());
        
        CreateMap<RegisterAdminRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Bio, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.FavoriteActors, opt => opt.Ignore())
            .ForMember(dest => dest.WatchList, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredGenres, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore());
        
        CreateMap<LoginUserRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Bio, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.FavoriteActors, opt => opt.Ignore())
            .ForMember(dest => dest.WatchList, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredGenres, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore());
        
        CreateMap<CreateActorRequest, Actor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Movies, opt => opt.Ignore());
        
        CreateMap<string, Photo>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src));

        CreateMap<CreateMovieActorRequest, MovieActor>()
            .ForMember(dest => dest.Movie, opt => opt.Ignore());
        
        CreateMap<CreatePersonRequest, Person>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));

        CreateMap<int, Genre>()
            .ConvertUsing(id => new Genre { Id = id });
        
        CreateMap<CreateMovieRequest, Movie>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new Genre { Id = id }).ToList()))
            .ForMember(dest => dest.RatingCount, opt => opt.Ignore())
            .ForMember(dest => dest.RatingSum, opt => opt.Ignore());

        CreateMap<PersonalizeUserRequest, PersonalizeUserDto>();
        
        CreateMap<CreateMovieActorByIdRequest, MovieActorDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom((_, _, _, context) =>
                (Guid)context.Items["MovieId"]));
        
        CreateMap<Photo, string>().ConvertUsing(src => src.ImageUrl);
        
        CreateMap<Genre, string>().ConvertUsing(src => src.Name);
        
        CreateMap<Actor, ActorResponse>();
        
        CreateMap<Actor, ActorCardResponse>()
            .ForMember(dest => dest.CharacterName, opt => opt.Ignore());
        
        CreateMap<ActorWithUserInfoDto, ActorWithUserInfoResponse>();
        
        CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));

        CreateMap<Movie, RatedMovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)))
            .ForMember(dest => dest.OwnRating, opt => opt.Ignore());

        CreateMap<MovieRating, RatedMovieCardResponse>()
            .IncludeMembers(src => src.Movie)
            .ForMember(dest => dest.OwnRating, opt => opt.MapFrom(src => src.Rating));
        
        CreateMap<Movie, MovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)))
            .ForMember(dest => dest.CharacterName, opt => opt.Ignore());

        CreateMap<MovieWithUserInfoDto, MovieWithUserInfoResponse>();
        
        CreateMap<Person, PersonResponse>();

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.IsCurrentUser, opt => opt.Ignore());
        
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