namespace API.Models.Requests;

public class CreateActorRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; } 
    public DateOnly BirthDate { get; set; }
    public string PhotoUrl { get; set; }
    public List<string> Photos { get; set; } = new();
}