namespace Domain.Models;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public DiscussionTopic Topic { get; set; }
    public User User { get; set; }
    public Comment? ParentComment { get; set; }
    public Guid? ParentCommentId { get; set; }

    public List<Comment> Replies { get; set; } = new();
    public int Likes { get; set; }
}