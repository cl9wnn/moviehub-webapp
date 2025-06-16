using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface IDiscussionTopicService: IEntityService<DiscussionTopic>
{
    Task<Result<List<DiscussionTopic>>> GetAllTopicsAsync();
    Task<Result<DiscussionTopic>> CreateTopicAsync(DiscussionTopic topic);
    Task<Result> DeleteTopicAsync(Guid id);
    Task<Result<List<Comment>>> GetCommentsByTopicIdAsync(Guid id);
    Task<Result<PaginatedDto<DiscussionTopic>>> GetPaginatedTopicsAsync(Guid userId, int page, int pageSize);
    Task<Result> IncrementViewAsync(Guid topicId, Guid userId);
}