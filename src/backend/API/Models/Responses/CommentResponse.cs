namespace API.Models.Responses;

public class CommentResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ParentCommentId { get; set; }
    public UserTopicResponse User { get; set; }
    public int Likes { get; set; }
    public List<CommentResponse> Replies { get; set; } = new();
}