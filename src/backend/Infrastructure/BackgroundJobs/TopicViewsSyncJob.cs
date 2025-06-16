using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundJobs;

public class TopicViewsSyncJob(IDistributedCacheService cacheService, IDiscussionTopicRepository repository,
    ILogger<TopicViewsSyncJob> logger)
{
    private const string TopicsCacheKey = "topic:views:*";
    public async Task ExecuteAsync()
    {
        var keys = await cacheService.GetKeysByPatternAsync(TopicsCacheKey);
        
        foreach (var key in keys)
        {
            try
            {
                var topicIdStr = key.Split(":")[2];
                
                if (!Guid.TryParse(topicIdStr, out var topicId))
                {
                    logger.LogWarning("Invalid topicId in key: {Key}", key);
                    continue;
                }

                var count = await cacheService.GetAsync<int?>(key) ?? 0;

                var updateResult = await repository.UpdateViewsAsync(topicId, count);

                if (!updateResult.IsSuccess)
                {
                    return;
                }

                await cacheService.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to sync views for key: {Key}", key);
            }
        }
        
    }
}