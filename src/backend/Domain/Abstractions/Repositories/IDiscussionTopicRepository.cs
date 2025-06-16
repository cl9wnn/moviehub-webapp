using Domain.Dtos;
using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface IDiscussionTopicRepository: IRepository<Guid, DiscussionTopic>
{
    Task<Result<List<Comment>>> GetCommentsByTopicIdAsync(Guid id);
    Task<Result> UpdateViewsAsync(Guid id, int count);
    Task<Result<PaginatedDto<DiscussionTopic>>> GetPaginatedAsync(Guid userId, int page, int pageSize);

}