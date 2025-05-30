namespace Infrastructure.Database.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public RefreshTokenEntity RefreshToken { get; set; } 
    public bool IsDeleted { get; set; }
    
    public ICollection<MovieEntity> WatchList { get; set; } = new List<MovieEntity>();
    public ICollection<ActorEntity> FavoriteActors { get; set; } = new List<ActorEntity>();
    public ICollection<GenreEntity> PreferredGenres { get; set; } = new List<GenreEntity>();
}