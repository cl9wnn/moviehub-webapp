using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Repositories;

public interface ICommentRepository: IRepository<Guid, Comment>
{
    Task<Result<DiscussionTopic>> GetTopicByCommentId(Guid? parentId);
}