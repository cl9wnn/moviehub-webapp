namespace API.Models.Responses;

public class ActorCardResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhotoUrl { get; set; }
    public string CharacterName { get; set; }
}