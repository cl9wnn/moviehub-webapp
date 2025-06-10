namespace API.Models.Responses;

public class MovieDiscussionTopicResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserTopicResponse User { get; set; }
}