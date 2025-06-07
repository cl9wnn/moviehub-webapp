namespace Infrastructure.Database.Entities;

public class TopicTagEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<DiscussionTopicEntity> Topics { get; set; } = new List<DiscussionTopicEntity>();
}