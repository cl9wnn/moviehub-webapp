using Domain.Dtos;

namespace Domain.Models;

public class Comment
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public DiscussionTopic Topic { get; set; }
    public Guid TopicId { get; set; }
    public TopicUserDto User { get; set; }
    public Guid UserId { get; set; }
    public Comment? ParentComment { get; set; }
    public Guid? ParentCommentId { get; set; }
    public List<Comment> Replies { get; set; } = new();
}