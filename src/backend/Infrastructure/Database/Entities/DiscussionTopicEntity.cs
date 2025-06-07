namespace Infrastructure.Database.Entities;

public class DiscussionTopicEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int Views { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    
    public MovieEntity Movie { get; set; }
    public Guid MovieId { get; set; }
    
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }

    public ICollection<TopicTagEntity> Tags { get; set; } = new List<TopicTagEntity>();
    public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}