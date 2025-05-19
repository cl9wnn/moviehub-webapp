namespace Infrastructure.Database.Entities;

public class MoviePhotoEntity: PhotoEntity
{
    public Guid MovieId { get; set; }
    public MovieEntity Movie { get; set; }
}