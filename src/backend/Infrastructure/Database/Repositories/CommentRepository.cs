using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Repositories;

public class CommentRepository(AppDbContext dbContext, IMapper mapper, ILogger<CommentRepository> logger): ICommentRepository
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
        
        logger.LogInformation("entity:{@commentEntity}", commentEntity);
        await dbContext.Comments.AddAsync(commentEntity);
        
        var deletedComments = dbContext.ChangeTracker.Entries<CommentEntity>()
            .Where(e => e.State == EntityState.Deleted)
            .Select(e => e.Entity.Id)
            .ToList();
        if (deletedComments.Any())
        {
            logger.LogWarning("Перед SaveChanges: будут удалены комментарии с Id: {DeletedIds}", deletedComments);
        }
        
        foreach (var entry in dbContext.ChangeTracker.Entries<CommentEntity>())
        {
            logger.LogInformation("ChangeTracker: Comment Id={Id}, State={State}, ParentCommentId={ParentId}",
                entry.Entity.Id, entry.State, entry.Entity.ParentCommentId);
        }
        
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