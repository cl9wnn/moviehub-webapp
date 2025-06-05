namespace API.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public DateOnly RegistrationDate { get; set; } 
    public bool IsCurrentUser { get; set; }
    public List<ActorCardResponse> FavoriteActors { get; set; }
    public List<MovieCardResponse> WatchList { get; set; }
    public List<RatedMovieCardResponse> MovieRatings { get; set; }
}