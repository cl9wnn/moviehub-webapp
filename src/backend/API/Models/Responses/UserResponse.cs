namespace API.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public List<ActorCardResponse> FavoriteActors { get; set; }
    public List<MovieCardResponse> WatchList { get; set; }
}