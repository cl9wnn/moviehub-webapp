namespace Domain.Models;

public class MovieRating
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid MovieId { get; set; }
    public Movie Movie { get; set; }
    public int Rating { get; set; }
}