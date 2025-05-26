namespace Infrastructure.Database.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int DurationAtMinutes { get; set; }
    public string AgeRating { get; set; }
    public double UserRating { get; set; }
    public string? PosterUrl { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<MovieDirectorEntity> Directors { get; set; } = new List<MovieDirectorEntity>();
    public ICollection<MovieWriterEntity> Writers { get; set; } = new List<MovieWriterEntity>();
    public ICollection<MovieActorEntity> MovieActors { get; set; } = new List<MovieActorEntity>();
    public ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public ICollection<MoviePhotoEntity>? Photos { get; set; } 
}