namespace Domain.Models;

public class MovieActor
{
    public Movie Movie { get; set; }
    public Actor Actor { get; set; }
    public string CharacterName { get; set; }
}