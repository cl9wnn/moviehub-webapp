namespace API.Models.Responses;

public class TopicMovieResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string PosterUrl { get; set; }
}