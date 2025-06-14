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
            .ForMember(dest => dest.NotInterestedMovies, opt => opt.Ignore())
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
            .ForMember(dest => dest.UsersWatchList, opt => opt.Ignore())
            .ForMember(dest => dest.MovieRatings, opt => opt.Ignore())
            .ForMember(dest => dest.UsersNotInterested, opt => opt.Ignore())
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

        CreateMap<MovieRating, MovieRatingEntity>()
            .ReverseMap();

        CreateMap<DiscussionTopic, DiscussionTopicEntity>()
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
            .ReverseMap();
        
        CreateMap<TopicTag, TopicTagEntity>()
            .ForMember(dest => dest.Topics, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<CommentEntity, Comment>()
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
            .ForMember(dest => dest.Topic, opt => opt.Ignore());
        
        CreateMap<Comment, CommentEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
            .ForMember(dest => dest.Replies, opt => opt.Ignore())
            .ForMember(dest => dest.Topic, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.Likes, opt => opt.Ignore())
            .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

        CreateMap<UserEntity, TopicUserDto>()
            .ReverseMap(); 
        
        CreateMap<MovieEntity, TopicMovieDto>()
            .ReverseMap();
        
        CreateMap<CommentLike, CommentLikeEntity>()
            .ReverseMap();
    }
}