namespace Infrastructure.Database.Entities;

public class ActorEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? PhotoUrl { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<ActorPhotoEntity>? Photos { get; set; } 
    public ICollection<MovieActorEntity> MovieActors { get; set; } = new List<MovieActorEntity>();
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}