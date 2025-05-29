namespace API.Models.Responses;

public class MovieCardResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string PosterUrl { get; set; }
    public double UserRating { get; set; }
    public int RatingCount { get; set; }
    public string CharacterName { get; set; }
}