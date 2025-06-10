using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface ICommentService: IEntityService<Comment>
{
    Task<Result<List<Comment>>> GetAllComments();
    Task<Result<Comment>> CreateTopicCommentAsync(Comment comment);
    Task<Result<Comment>> CreateReplyCommentAsync(Comment comment);
    Task<Result> DeleteCommentAsync(Guid id);
}