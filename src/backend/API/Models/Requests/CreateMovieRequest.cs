namespace API.Models.Requests;

public class CreateMovieRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int DurationAtMinutes { get; set; }
    public string AgeRating { get; set; }
    public string? PosterUrl { get; set; }
    public List<CreatePersonRequest> Directors { get; set; } = new();
    public List<CreatePersonRequest> Writers { get; set; } = new();
    public List<int> GenreIds { get; set; } = new();
    public List<string>? Photos { get; set; }
    public List<CreateMovieActorRequest> Actors { get; set; } = new();
}