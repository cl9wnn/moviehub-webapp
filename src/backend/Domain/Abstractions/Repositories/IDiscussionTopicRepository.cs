using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IDiscussionTopicRepository: IRepository<Guid, DiscussionTopic>
{
    Task<Result<List<Comment>>> GetCommentsByTopicIdAsync(Guid id);
}