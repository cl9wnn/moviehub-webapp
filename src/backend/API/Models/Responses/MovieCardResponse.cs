namespace API.Models.Responses;

public class MovieCardResponse
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string PosterUrl { get; set; }
    public double UserRating { get; set; }
    public string CharacterName { get; set; }
}