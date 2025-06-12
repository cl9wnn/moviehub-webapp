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
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
        
        CreateMap<RegisterAdminRequest, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.AvatarUrl, opt => opt.Ignore())
            .ForMember(dest => dest.Bio, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore())
            .ForMember(dest => dest.FavoriteActors, opt => opt.Ignore())
            .ForMember(dest => dest.WatchList, opt => opt.Ignore())
            .ForMember(dest => dest.PreferredGenres, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
        
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
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
        
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
        
        CreateMap<int, TopicTag>()
            .ConvertUsing(id => new TopicTag { Id = id });
        
        CreateMap<CreateMovieRequest, Movie>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.GenreIds.Select(id => new Genre { Id = id }).ToList()))
            .ForMember(dest => dest.RatingCount, opt => opt.Ignore())
            .ForMember(dest => dest.RatingSum, opt => opt.Ignore())
            .ForMember(dest => dest.Topics, opt => opt.Ignore());

        CreateMap<PersonalizeUserRequest, PersonalizeUserDto>();
        
        CreateMap<CreateMovieActorByIdRequest, MovieActorDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom((_, _, _, context) =>
                (Guid)context.Items["MovieId"]));

        CreateMap<CreateCommentRequest, Comment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Likes, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Likes, opt => opt.Ignore())
            .ForMember(dest => dest.TopicId, opt => opt.Ignore())
            .ForMember(dest => dest.Topic, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
            .ForMember(dest => dest.ParentCommentId, opt => opt.Ignore())
            .ForMember(dest => dest.Replies, opt => opt.Ignore());

        CreateMap<CreateDiscussionTopicRequest, DiscussionTopic>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Views, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Movie, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.TagIds.Select(id => new TopicTag { Id = id }).ToList()))
            .ForMember(dest => dest.Comments, opt => opt.Ignore());
            
        CreateMap<Photo, string>().ConvertUsing(src => src.ImageUrl);
        
        CreateMap<Genre, string>().ConvertUsing(src => src.Name);
        
        CreateMap<Actor, ActorResponse>();
        
        CreateMap<Actor, ActorCardResponse>()
            .ForMember(dest => dest.CharacterName, opt => opt.Ignore());

        CreateMap<Actor, ActorSearchResponse>();
        
        CreateMap<ActorWithUserInfoDto, ActorWithUserInfoResponse>();
        
        CreateMap<Movie, MovieResponse>()
            .ForMember(dest => dest.MovieActors, opt => opt.MapFrom(src => src.Actors))
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));
        
        CreateMap<Movie, MovieSearchResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));
        
        CreateMap<Movie, RatedMovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)))
            .ForMember(dest => dest.OwnRating, opt => opt.Ignore());
        
        CreateMap<Movie, MovieCardResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)))
            .ForMember(dest => dest.CharacterName, opt => opt.Ignore());

        CreateMap<Movie, RecommendationMovieResponse>()
            .ForMember(dest => dest.UserRating, opt => opt.MapFrom(src => CalculateUserRating(src)));
        
        CreateMap<MovieRating, RatedMovieCardResponse>()
            .IncludeMembers(src => src.Movie)
            .ForMember(dest => dest.OwnRating, opt => opt.MapFrom(src => src.Rating));
        
        CreateMap<MovieWithUserInfoDto, MovieWithUserInfoResponse>();
        
        CreateMap<Person, PersonResponse>();

        CreateMap<User, UserResponse>()
            .ForMember(dest => dest.IsCurrentUser, opt => opt.Ignore());
        
        CreateMap<MovieActor, ActorCardResponse>()
            .IncludeMembers(src => src.Actor);
        
        CreateMap<MovieActor, MovieCardResponse>()
            .IncludeMembers(src => src.Movie);
        
        CreateMap<MovieActor, ActorSearchResponse>()
            .IncludeMembers(src => src.Actor);
        
        CreateMap<TopicTag, string>().ConvertUsing(src => src.Name);

        CreateMap<User, UserTopicResponse>();

        CreateMap<DiscussionTopic, UserDiscussionTopicResponse>();
            
        CreateMap<DiscussionTopic, DiscussionTopicResponse>();
        
        CreateMap<DiscussionTopic, MovieDiscussionTopicResponse>();

        CreateMap<DiscussionTopic, ListDiscussionTopicResponse>();
        
        CreateMap<Movie, TopicMovieResponse>();

        CreateMap<Comment, CommentResponse>();
        
        CreateMap<Comment, UserCommentResponse>();

        CreateMap<TopicMovieDto, TopicMovieResponse>();
        
        CreateMap<TopicUserDto, UserTopicResponse>();
    }
    
    private static double CalculateUserRating(Movie movie)
    {
        return movie.RatingCount == 0 ? 0 : Math.Round(movie.RatingSum / movie.RatingCount, 1);
    }
}