using Domain.Dtos;

namespace Domain.Models;

public class DiscussionTopic
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public TopicMovieDto Movie { get; set; }
    public Guid MovieId { get; set; }
    
    public TopicUserDto User { get; set; }
    public Guid UserId { get; set; }

    public List<TopicTag> Tags { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}