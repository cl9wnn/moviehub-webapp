namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string AvatarUrl { get; set; }
    public string Bio { get; set; }
    public DateOnly RegistrationDate { get; set; }
    public string Password { get; set; }
    public List<Actor> FavoriteActors { get; set; }
    public List<Movie> WatchList { get; set; }
    public List<Genre> PreferredGenres { get; set; }
}