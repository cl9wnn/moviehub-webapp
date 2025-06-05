namespace Infrastructure.Database.Entities;

public class MovieRatingEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid MovieId { get; set; }
    public MovieEntity Movie { get; set; }
    public int Rating { get; set; }
}