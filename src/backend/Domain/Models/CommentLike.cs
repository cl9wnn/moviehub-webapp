namespace Domain.Models;

public class CommentLike
{
    public Guid Id { get; set; }
    
    public Comment Comment { get; set; }
    public User User { get; set; }
}