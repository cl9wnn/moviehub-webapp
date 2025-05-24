namespace API.Models.Responses;

public class ActorResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; } 
    public DateOnly BirthDate { get; set; }
    public string PhotoUrl { get; set; }
    public List<string> Photos { get; set; }
    public List<MovieCardResponse> Movies { get; set; }
}