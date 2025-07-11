namespace Infrastructure.Database.Entities;

public class MovieEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public int DurationAtMinutes { get; set; }
    public string AgeRating { get; set; }
    public int RatingCount { get; set; }
    public double RatingSum { get; set; }
    public string? PosterUrl { get; set; }
    public bool IsDeleted { get; set; }
    
    public ICollection<MovieDirectorEntity> Directors { get; set; } = new List<MovieDirectorEntity>();
    public ICollection<MovieWriterEntity> Writers { get; set; } = new List<MovieWriterEntity>();
    public ICollection<MovieActorEntity> MovieActors { get; set; } = new List<MovieActorEntity>();
    public ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public ICollection<MoviePhotoEntity>? Photos { get; set; } = new List<MoviePhotoEntity>();
    public ICollection<UserEntity> UsersWatchList { get; set; } = new List<UserEntity>();
    public ICollection<MovieRatingEntity> MovieRatings { get; set; } = new List<MovieRatingEntity>();
    public ICollection<DiscussionTopicEntity> Topics { get; set; } = new List<DiscussionTopicEntity>();
    public ICollection<UserEntity> UsersNotInterested { get; set; } = new List<UserEntity>();
}