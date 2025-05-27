namespace API.Models.Responses;

public class MovieResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int DurationAtMinutes { get; set; }
    public string AgeRating { get; set; }
    public double UserRating { get; set; }
    public int RatingCount { get; set; }
    public string PosterUrl { get; set; }
    
    public List<PersonResponse> Directors { get; set; } 
    public List<PersonResponse> Writers { get; set; } 
    public List<ActorCardResponse> MovieActors { get; set; } 
    public List<string> Genres { get; set; } 
    public List<string> Photos { get; set; }
}