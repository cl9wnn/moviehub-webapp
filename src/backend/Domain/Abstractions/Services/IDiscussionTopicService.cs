using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IDiscussionTopicService: IEntityService<DiscussionTopic>
{
    Task<Result<List<DiscussionTopic>>> GetAllTopicsAsync();
    Task<Result<DiscussionTopic>> CreateTopicAsync(DiscussionTopic topic, Guid userId);
    Task<Result> DeleteTopicAsync(Guid id);
}