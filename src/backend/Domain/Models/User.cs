using Domain.Enums;

namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public UserRole Role { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string Password { get; set; }
    public List<Actor> FavoriteActors { get; set; } = new();
    public List<Movie> WatchList { get; set; } = new();
    public List<Genre> PreferredGenres { get; set; } = new();
    public List<MovieRating> MovieRatings { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<DiscussionTopic> Topics { get; set; } = new();
}