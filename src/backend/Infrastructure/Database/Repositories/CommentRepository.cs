using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Repositories;

public class CommentRepository(AppDbContext dbContext, IMapper mapper): ICommentRepository
{
    private IQueryable<CommentEntity> ActiveComments => dbContext.Comments.Where(c => !c.IsDeleted);
    
    public async Task<Result<ICollection<Comment>>> GetAllAsync()
    {
        var commentEntities = await ActiveComments
            .Include(c => c.User)
            .Include(c => c.Replies)
            .Include(c => c.Likes)
            .Include(c => c.ParentComment)
            .Include(c => c.Topic)
            .AsNoTracking()
            .ToListAsync();
        
        var comments = mapper.Map<ICollection<Comment>>(commentEntities);
        
        return Result<ICollection<Comment>>.Success(comments);
    }

    public async Task<Result<DiscussionTopic>> GetTopicByCommentId(Guid? id)
    {
        var commentEntity = await ActiveComments
            .Include(c => c.Topic)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (commentEntity == null)
        {
            return Result<DiscussionTopic>.Failure("Comment not found")!;
        }

        var topicEntity = commentEntity.Topic;
        
        return topicEntity == null
            ? Result<DiscussionTopic>.Failure("Topic not found")!
            : Result<DiscussionTopic>.Success(mapper.Map<DiscussionTopic>(topicEntity));
    }
    
    public async Task<Result> ExistsAsync(Guid id)
    {
        var exists = await dbContext.Comments
            .AsNoTracking()
            .AnyAsync(c => c.Id == id && !c.IsDeleted);

        return exists
            ? Result.Success()
            : Result.Failure("Comment not found!");
    }

    public async Task<Result> IsOwnedByUser(Guid id, Guid userId)
    {
        var commentEntity = await ActiveComments
            .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

        return commentEntity == null 
            ? Result.Failure("User is not the owner of the comment")
            : Result.Success();
    }

    public async Task<Result> LikeCommentAsync(Guid userId, Guid id)
    {
        var commentEntity = await ActiveComments
            .Include(c => c.Likes)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (commentEntity == null)
        {
            return Result.Failure("Comment not found");
        }

        if (commentEntity.Likes.Any(l => l.UserId == userId))
        {
            return Result.Failure("You already liked this comment");
        }

        var like = new CommentLikeEntity
        {
            Id = Guid.NewGuid(),
            CommentId = id,
            UserId = userId,
        };
        
        await dbContext.CommentLikes.AddAsync(like);
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> UnlikeCommentAsync(Guid userId, Guid id)
    {
        var likeEntity = await dbContext.CommentLikes
                .FirstOrDefaultAsync(c => c.CommentId == id && c.UserId == userId);

        if (likeEntity == null)
        {
            return Result.Failure("Comment is not liked yet");
        }
        
        dbContext.CommentLikes.Remove(likeEntity);
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<Comment>> GetByIdAsync(Guid id)
    {
        var commentEntity = await ActiveComments
            .Include(c => c.User)
            .Include(c => c.Replies)
                .ThenInclude(c => c.User)
            .Include(c => c.Likes)
            .Include(c => c.ParentComment)
            .Include(c => c.Topic)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (commentEntity == null)
        {
            return Result<Comment>.Failure("Comment not found!")!;
        }
        
        var comment = mapper.Map<Comment>(commentEntity);
        
        return Result<Comment>.Success(comment);
    }

    public async Task<Result<Comment>> AddAsync(Comment commentDto)
    {
        var commentEntity = mapper.Map<CommentEntity>(commentDto);
        
        commentEntity.CreatedAt = DateTime.UtcNow;
        
        await dbContext.Comments.AddAsync(commentEntity);
        await dbContext.SaveChangesAsync();
        
        var comment = mapper.Map<Comment>(commentEntity);
        
        return Result<Comment>.Success(comment);
    }

    public async Task<Result<Comment>> UpdateAsync(Comment commentDto)
    {
        var existingCommentEntity = await ActiveComments
            .FirstOrDefaultAsync(t => t.Id == commentDto.Id);
        
        if (existingCommentEntity == null)
        {
            return Result<Comment>.Failure("Comment not found")!;
        }
        
        mapper.Map(commentDto, existingCommentEntity);
        await dbContext.SaveChangesAsync();
        
        var updated = mapper.Map<Comment>(existingCommentEntity);
        
        return Result<Comment>.Success(updated);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var existingComment = await ActiveComments
            .FirstOrDefaultAsync(t => t.Id == id);

        if (existingComment == null)
        {
            return Result.Failure("Comment not found");
        }
        
        existingComment.IsDeleted = true;
        await dbContext.SaveChangesAsync();
        
        return Result.Success();
    }
}