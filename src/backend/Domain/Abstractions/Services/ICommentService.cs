using Domain.Models;
using Domain.Utils;

namespace Domain.Abstractions.Services;

public interface ICommentService: IEntityService<Comment>
{
    Task<Result<List<Comment>>> GetAllComments();
    Task<Result<Comment>> CreateTopicCommentAsync(Comment comment);
    Task<Result<Comment>> CreateReplyCommentAsync(Guid userId, Comment comment, string url);
    Task<Result> DeleteOwnCommentAsync(Guid id, Guid userId);
    Task<Result> LikeCommentAsync(Guid userId, Guid id);
    Task<Result> UnlikeCommentAsync(Guid userId, Guid id);
}