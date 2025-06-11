using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface ICommentRepository: IRepository<Guid, Comment>
{
    Task<Result<DiscussionTopic>> GetTopicByCommentId(Guid? parentId);
    Task<Result> IsOwnedByUser(Guid id, Guid userId);
    Task<Result> LikeCommentAsync(Guid userId, Guid id);
    Task<Result> UnlikeCommentAsync(Guid userId, Guid id);
}