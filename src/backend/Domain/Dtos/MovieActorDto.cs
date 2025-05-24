namespace Domain.Dtos;

public class MovieActorDto
{
    public Guid ActorId { get; set; }
    public Guid MovieId { get; set; }
    public string CharacterName { get; set; }
}