namespace Infrastructure.Database.Entities;

public class CommentLikeEntity
{
    public Guid Id { get; set; }
    
    public Guid CommentId { get; set; }
    public CommentEntity Comment { get; set; }
    
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}