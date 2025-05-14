namespace Domain.Models;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public Guid UserId { get; set; }

    public static RefreshToken Create(string token, Guid userId)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = token,
            Expires = DateTime.UtcNow.AddDays(7),
            Created = DateTime.UtcNow,
            UserId = userId,
        };
    }
}