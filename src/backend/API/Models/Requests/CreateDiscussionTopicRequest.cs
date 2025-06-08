namespace API.Models.Requests;

public class CreateDiscussionTopicRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid MovieId { get; set; }
    public List<int> TagIds { get; set; }
}