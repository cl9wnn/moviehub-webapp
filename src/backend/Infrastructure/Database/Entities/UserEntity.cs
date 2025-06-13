using Domain.Enums;

namespace Infrastructure.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string? Bio { get; set; }
    public UserRole Role { get; set; }
    public bool IsDeleted { get; set; }
    
    public RefreshTokenEntity RefreshToken { get; set; } 
    public ICollection<MovieEntity> WatchList { get; set; } = new List<MovieEntity>();
    public ICollection<ActorEntity> FavoriteActors { get; set; } = new List<ActorEntity>();
    public ICollection<GenreEntity> PreferredGenres { get; set; } = new List<GenreEntity>();
    public ICollection<MovieRatingEntity> MovieRatings { get; set; } = new List<MovieRatingEntity>();
    public ICollection<DiscussionTopicEntity> Topics { get; set; } = new List<DiscussionTopicEntity>();
    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    public ICollection<MovieEntity> NotInterestedMovies { get; set; } = new List<MovieEntity>();
}