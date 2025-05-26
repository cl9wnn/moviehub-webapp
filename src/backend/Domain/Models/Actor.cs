namespace Domain.Models;

public class Actor
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Biography { get; set; }
    public DateOnly BirthDate { get; set; }
    public string? PhotoUrl { get; set; }
    public List<Photo>? Photos { get; set; } = new();
    public List<MovieActor> Movies { get; set; } = new();
}