using AutoMapper;
using Domain.Abstractions.Repositories;
using Domain.Models;
using Domain.Utils;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories;

public class DiscussionTopicRepository(AppDbContext dbContext, IMapper mapper): IDiscussionTopicRepository
{
    private IQueryable<DiscussionTopicEntity> ActiveTopics => dbContext.DiscussionTopics.Where(t => !t.IsDeleted);
   
    public async Task<Result<ICollection<DiscussionTopic>>> GetAllAsync()
    {
        var topicEntities = await ActiveTopics
            .Include(t => t.Comments)
            .Include(t => t.Tags)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .AsNoTracking()
            .ToListAsync();
        
        var topics = mapper.Map<ICollection<DiscussionTopic>>(topicEntities);
        
        return Result<ICollection<DiscussionTopic>>.Success(topics);
    }
    
    public async Task<Result<DiscussionTopic>> GetByIdAsync(Guid id)
    {
        var topicEntity = await ActiveTopics
            .Include(t => t.Comments)
            .Include(t => t.Tags)
            .Include(t => t.User)
            .Include(t => t.Movie)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (topicEntity == null)
        {
            return Result<DiscussionTopic>.Failure("Topic not found!")!;
        }
        
        var topic = mapper.Map<DiscussionTopic>(topicEntity);
        
        return Result<DiscussionTopic>.Success(topic);
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
}