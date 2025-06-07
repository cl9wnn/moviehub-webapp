namespace API.Models.Responses;

public class MovieSearchResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public int UserRating { get; set; }
    public List<ActorSearchResponse> Actors { get; set; }
}