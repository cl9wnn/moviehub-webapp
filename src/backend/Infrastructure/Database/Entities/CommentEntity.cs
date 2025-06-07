namespace Infrastructure.Database.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    
    public DiscussionTopicEntity Topic { get; set; }
    public Guid TopicId { get; set; }
    
    public UserEntity User { get; set; }
    public Guid UserId { get; set; }
    
    public Guid? ParentCommentId { get; set; }   
    public CommentEntity? ParentComment { get; set; }
    public ICollection<CommentEntity> Replies { get; set; } =  new List<CommentEntity>();
    
    public ICollection<CommentLikeEntity> Likes { get; set; } = new List<CommentLikeEntity>();
}