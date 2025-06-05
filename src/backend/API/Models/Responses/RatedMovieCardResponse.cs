namespace API.Models.Responses;

public class RatedMovieCardResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string PosterUrl { get; set; }
    public int OwnRating { get; set; }
    public double UserRating { get; set; }
}