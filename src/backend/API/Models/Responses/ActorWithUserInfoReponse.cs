namespace API.Models.Responses;

public class ActorWithUserInfoResponse
{
    public ActorResponse Actor {get; set;}
    public bool IsFavorite { get; set; }
}