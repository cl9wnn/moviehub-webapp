namespace Domain.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int DurationAtMinutes { get; set; }
    public string AgeRating { get; set; }
    public int RatingCount { get; set; }
    public double RatingSum { get; set; }
    public string PosterUrl { get; set; }
    
    public List<Person> Directors { get; set; } = new();
    public List<Person> Writers { get; set; } = new();
    public List<Genre> Genres { get; set; } = new();
    public List<Photo>? Photos { get; set; }
    public List<MovieActor> Actors { get; set; } = new();
}