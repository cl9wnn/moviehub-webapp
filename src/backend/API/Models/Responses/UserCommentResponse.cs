namespace API.Models.Responses;

public class UserCommentResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid TopicId { get; set; }
    public int Likes { get; set; }
}