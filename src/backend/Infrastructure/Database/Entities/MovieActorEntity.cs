namespace Infrastructure.Database.Entities;

public class MovieActorEntity
{
    public Guid MovieId { get; set; }
    public MovieEntity Movie { get; set; }

    public Guid ActorId { get; set; }
    public ActorEntity Actor { get; set; }

    public string CharacterName { get; set; } 
}