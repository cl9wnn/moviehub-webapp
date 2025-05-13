namespace Infrastructure.Database.Entities;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }  = DateTime.UtcNow;
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}