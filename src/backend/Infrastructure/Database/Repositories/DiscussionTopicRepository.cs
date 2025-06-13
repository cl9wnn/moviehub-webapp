using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database.Repositories;

public class DiscussionTopicRepository(AppDbContext dbContext, IMapper mapper, ILogger<DiscussionTopicRepository> logger) : IDiscussionTopicRepository
{
    private IQueryable<DiscussionTopicEntity> ActiveTopics => dbContext.DiscussionTopics.Where(t => !t.IsDeleted);

    public async Task<Result<ICollection<DiscussionTopic>>> GetAllAsync()
    {
        var topicEntities = await ActiveTopics
            .Include(t => t.Comments)
            .ThenInclude(c => c.User)
            .Include(t => t.Tags)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .AsNoTracking()
            .ToListAsync();

        var topics = mapper.Map<ICollection<DiscussionTopic>>(topicEntities);

        return Result<ICollection<DiscussionTopic>>.Success(topics);
    }

    public async Task<Result<PaginatedDto<DiscussionTopic>>> GetPaginatedAsync(Guid userId, int page, int pageSize) 
    {
        var skip = (page - 1) * pageSize;

        var preferredGenreIds = await dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.PreferredGenres.Select(g => g.Id))
            .ToListAsync();

        var baseQuery = ActiveTopics
            .Include(t => t.Comments).ThenInclude(c => c.User)
            .Include(t => t.Tags)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .ThenInclude(m => m.Genres) 
            .AsNoTracking();

        var totalCount = await baseQuery.CountAsync();

        IQueryable<DiscussionTopicEntity> orderedQuery;

        if (preferredGenreIds.Any())
        {
            orderedQuery = baseQuery
                .Select(t => new 
                {
                    Topic = t,
                    MatchScore = t.Movie.Genres
                        .Count(g => preferredGenreIds.Contains(g.Id))
                })
                .OrderByDescending(x => x.MatchScore) 
                .Select(x => x.Topic);
        }
        else
        {
            orderedQuery = baseQuery
                .OrderByDescending(t => t.CreatedAt);
        }

        var topicEntities = await orderedQuery
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var topics = mapper.Map<ICollection<DiscussionTopic>>(topicEntities);

        return Result<PaginatedDto<DiscussionTopic>>.Success(new PaginatedDto<DiscussionTopic>
        {
            Items = topics,
            TotalCount = totalCount
        });
    }

    public async Task<Result<List<Comment>>> GetCommentsByTopicIdAsync(Guid id)
    {
        var commentEntities = await dbContext.Comments
            .Where(c => c.TopicId == id && c.ParentCommentId == null && !c.IsDeleted)
            .Include(c => c.User)
            .Include(c => c.Likes)
            .Include(c => c.Replies.Where(r => !r.IsDeleted))
            .ThenInclude(r => r.User)
            .Include(c => c.Replies)
            .ThenInclude(r => r.Likes)
            .AsNoTracking()
            .ToListAsync();

        return Result<List<Comment>>.Success(mapper.Map<List<Comment>>(commentEntities));
    }

    public async Task<Result<DiscussionTopic>> GetByIdAsync(Guid id)
    {
        var topicEntity = await ActiveTopics
            .Include(t => t.Tags)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (topicEntity == null)
        {
            return Result<DiscussionTopic>.Failure("Topic not found!")!;
        }

        var allComments = await dbContext.Comments
            .Where(c => c.TopicId == id && !c.IsDeleted)
            .Include(c => c.User)
            .Include(c => c.Likes)
            .ToListAsync();

        topicEntity.Comments = BuildCommentTree(allComments);

        var topic = mapper.Map<DiscussionTopic>(topicEntity);

        return Result<DiscussionTopic>.Success(topic);
    }

    public async Task<Result> ExistsAsync(Guid id)
    {
        var exists = await dbContext.DiscussionTopics
            .AsNoTracking()
            .AnyAsync(c => c.Id == id && !c.IsDeleted);

        return exists
            ? Result.Success()
            : Result.Failure("Topic not found!");
    }

    public async Task<Result<DiscussionTopic>> AddAsync(DiscussionTopic topicDto)
    {
        var tagIds = topicDto.Tags.Select(t => t.Id).ToList();

        var tagEntities = await dbContext.TopicTags
            .Where(g => tagIds.Contains(g.Id))
            .ToListAsync();

        var missingIds = tagIds.Except(tagEntities.Select(g => g.Id)).ToList();

        if (missingIds.Count != 0)
        {
            return Result<DiscussionTopic>.Failure("Tags not found")!;
        }

        var topicEntity = mapper.Map<DiscussionTopicEntity>(topicDto);

        topicEntity.Tags = tagEntities;
        topicEntity.CreatedAt = DateTime.UtcNow;

        await dbContext.DiscussionTopics.AddAsync(topicEntity);
        await dbContext.SaveChangesAsync();

        var topic = mapper.Map<DiscussionTopic>(topicEntity);

        return Result<DiscussionTopic>.Success(topic);
    }

    public async Task<Result<DiscussionTopic>> UpdateAsync(DiscussionTopic topicDto)
    {
        var existingTopicEntity = await ActiveTopics
            .FirstOrDefaultAsync(t => t.Id == topicDto.Id);

        if (existingTopicEntity == null)
        {
            return Result<DiscussionTopic>.Failure("Topic not found")!;
        }

        mapper.Map(topicDto, existingTopicEntity);
        await dbContext.SaveChangesAsync();

        var updated = mapper.Map<DiscussionTopic>(existingTopicEntity);

        return Result<DiscussionTopic>.Success(updated);
    }

    public async Task<Result> DeleteAsync(Guid id)
    {
        var existingTopic = await ActiveTopics
            .FirstOrDefaultAsync(t => t.Id == id);

        if (existingTopic == null)
        {
            return Result.Failure("Topic not found");
        }

        existingTopic.IsDeleted = true;
        await dbContext.SaveChangesAsync();

        return Result.Success();
    }

    private List<CommentEntity> BuildCommentTree(List<CommentEntity> allComments)
    {
        var commentLookup = allComments.ToLookup(c => c.ParentCommentId);

        List<CommentEntity> Build(Guid? parentId)
        {
            return commentLookup[parentId]
                .OrderByDescending(c => c.CreatedAt)
                .Select(c =>
                {
                    c.Replies = Build(c.Id);
                    return c;
                })
                .ToList();
        }

        return Build(null);
    }
}