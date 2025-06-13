namespace Domain.Dtos;

public class UserRecommendationDataDto
{
    public List<int> PreferredGenreIds { get; set; } = [];
    public List<int> WatchListGenreIds { get; set; } = [];
    public List<Guid> NotInterestedMovieIds { get; set; } = [];
    public List<int> GenresFromUserTopics { get; set; } = new();
}