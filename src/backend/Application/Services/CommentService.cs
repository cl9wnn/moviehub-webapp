using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services;

public class CommentService(ICommentRepository commentRepository, IEmailService emailService): ICommentService
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
        var createResult = await commentRepository.AddAsync(comment);
        
        return createResult.IsSuccess
            ? Result<Comment>.Success(createResult.Data)
            : Result<Comment>.Failure(createResult.ErrorMessage!)!;
    }

    public async Task<Result<Comment>> CreateReplyCommentAsync(Guid userId, Comment comment, string url)
    {
        var topicResult = await commentRepository.GetTopicByCommentId(comment.ParentCommentId);

        if (!topicResult.IsSuccess)
        {
            return Result<Comment>.Failure(topicResult.ErrorMessage!)!;
        }
        var parentResult = await commentRepository.GetCommentById(comment.ParentCommentId);

        if (!parentResult.IsSuccess)
        {
            return Result<Comment>.Failure(parentResult.ErrorMessage!)!;
        }
        comment.TopicId = topicResult.Data.Id;

        var addResult = await commentRepository.AddAsync(comment);

        if (addResult.IsSuccess)
        {
            await NotifyParentCommentAuthor(parentResult.Data, comment.TopicId, userId, url, addResult.Data.Content);
        }

        return addResult.IsSuccess
            ? Result<Comment>.Success(addResult.Data)
            : Result<Comment>.Failure(addResult.ErrorMessage!)!;
    }

    public async Task<Result<Comment>> GetByIdAsync(Guid id)
    {
        var getResult = await commentRepository.GetByIdAsync(id);

        return getResult.IsSuccess
            ? Result<Comment>.Success(getResult.Data)
            : Result<Comment>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result> ExistsAsync(Guid id)
    {
        var existsResult = await commentRepository.ExistsAsync(id);
        
        return existsResult.IsSuccess
            ? Result.Success()
            : Result.Failure(existsResult.ErrorMessage!)!;
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
    
    private async Task NotifyParentCommentAuthor(Comment parentComment, Guid topicId, Guid currentUserId, string url, string replyMessageText)
    {
        if (parentComment.User.Id == currentUserId)
        {
            return;
        }
        
        var topicUrl = $"{url}/topics/{topicId}";

        await emailService.SendEmailAsync(
            parentComment.User.Email,
            "💬 Вам ответили на комментарий!",
            $"Здравствуйте, {parentComment.User.Username}!\n\n" +
            $"💬 Новый ответ на ваш комментарий:\n" +
            $"\"{replyMessageText}\"\n\n" +
            $"📌 Посмотреть обсуждение можно по ссылке:\n\n{topicUrl}\n\n" +
            $"С уважением,\nКоманда MovieHub 🎬"
        );
    }
}