namespace Domain.Models;

public class Photo
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; }
}