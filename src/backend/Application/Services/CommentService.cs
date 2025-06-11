using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CommentService(ICommentRepository commentRepository, ILogger<CommentService> logger): ICommentService
{
    public async Task<Result<List<Comment>>> GetAllComments()
    {
        var getResult = await commentRepository.GetAllAsync();
        
        return getResult.IsSuccess
            ? Result<List<Comment>>.Success(getResult.Data.ToList())
            : Result<List<Comment>>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<Comment>> CreateTopicCommentAsync(Comment comment)
    {
        logger.LogInformation("new comment: {@comment}", comment);
        var createResult = await commentRepository.AddAsync(comment);
        
        return createResult.IsSuccess
            ? Result<Comment>.Success(createResult.Data)
            : Result<Comment>.Failure(createResult.ErrorMessage!)!;
    }

    public async Task<Result<Comment>> CreateReplyCommentAsync(Comment comment)
    {
        var getTopicIdResult = await commentRepository.GetTopicByCommentId(comment.ParentCommentId);

        logger.LogInformation("topicId: {@topicId}", getTopicIdResult.Data.Id);
        if (!getTopicIdResult.IsSuccess)
        {
            return Result<Comment>.Failure("Topic of comment not found!")!;
        }
        
        comment.TopicId = getTopicIdResult.Data.Id;
        var createResult = await commentRepository.AddAsync(comment);
        
        return createResult.IsSuccess
            ? Result<Comment>.Success(createResult.Data)
            : Result<Comment>.Failure(createResult.ErrorMessage!)!;
    }

    public async Task<Result<Comment>> GetByIdAsync(Guid id)
    {
        var getResult = await commentRepository.GetByIdAsync(id);

        return getResult.IsSuccess
            ? Result<Comment>.Success(getResult.Data)
            : Result<Comment>.Failure(getResult.ErrorMessage!)!;
    }
    
    public async Task<Result<Comment>> CreateCommentAsync(Comment comment, Guid userId)
    {
        comment.UserId = userId;
        var addResult = await commentRepository.AddAsync(comment);
        
        return addResult.IsSuccess
            ? Result<Comment>.Success(addResult.Data)
            : Result<Comment>.Failure(addResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteOwnCommentAsync(Guid id, Guid userId)
    {
        var isOwnCommentResult = await commentRepository.IsOwnedByUser(id, userId);

        if (!isOwnCommentResult.IsSuccess)
        {
            return Result.Failure(isOwnCommentResult.ErrorMessage!)!;
        }
        
        var deleteResult = await commentRepository.DeleteAsync(id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result> LikeCommentAsync(Guid userId, Guid id)
    {
        var addResult = await commentRepository.LikeCommentAsync(userId, id);
        
        return addResult.IsSuccess
            ? Result.Success()
            : Result.Failure(addResult.ErrorMessage!);
    }

    public async Task<Result> UnlikeCommentAsync(Guid userId, Guid id)
    {
        var deleteResult = await commentRepository.UnlikeCommentAsync(userId, id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }
}