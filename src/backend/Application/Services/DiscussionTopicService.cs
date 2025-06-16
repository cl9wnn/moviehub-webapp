using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
using Domain.Models;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class DiscussionTopicService(IDiscussionTopicRepository topicRepository, IDistributedCacheService cacheService): IDiscussionTopicService
{
    public async Task<Result<List<DiscussionTopic>>> GetAllTopicsAsync()
    {
        var getResult = await topicRepository.GetAllAsync();
        
        return getResult.IsSuccess
            ? Result<List<DiscussionTopic>>.Success(getResult.Data.ToList())
            : Result<List<DiscussionTopic>>.Failure(getResult.ErrorMessage!)!;
    }
    public async Task<Result<DiscussionTopic>> GetByIdAsync(Guid id)
    {
        var getResult = await topicRepository.GetByIdAsync(id);

        if (!getResult.IsSuccess)
        {
            return Result<DiscussionTopic>.Failure(getResult.ErrorMessage!)!;
        }
        
        return Result<DiscussionTopic>.Success(getResult.Data);
    }

    public async Task<Result> ExistsAsync(Guid id)
    {
        var existsResult = await topicRepository.ExistsAsync(id);
        
        return existsResult.IsSuccess
            ? Result.Success()
            : Result.Failure(existsResult.ErrorMessage!)!;
    }

    public async Task<Result<DiscussionTopic>> CreateTopicAsync(DiscussionTopic topic)
    {
        var addResult = await topicRepository.AddAsync(topic);
        
        return addResult.IsSuccess
            ? Result<DiscussionTopic>.Success(addResult.Data)
            : Result<DiscussionTopic>.Failure(addResult.ErrorMessage!)!;
    }

    public async Task<Result> DeleteTopicAsync(Guid id)
    {
        var deleteResult = await topicRepository.DeleteAsync(id);
        
        return deleteResult.IsSuccess
            ? Result.Success()
            : Result.Failure(deleteResult.ErrorMessage!);
    }

    public async Task<Result<List<Comment>>> GetCommentsByTopicIdAsync(Guid id)
    {
        var getResult = await topicRepository.GetCommentsByTopicIdAsync(id);
        
        return getResult.IsSuccess
            ? Result<List<Comment>>.Success(getResult.Data.ToList())
            : Result<List<Comment>>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<PaginatedDto<DiscussionTopic>>> GetPaginatedTopicsAsync(Guid userId, int page, int pageSize)
    {
        var getResult = await topicRepository.GetPaginatedAsync(userId, page, pageSize);
        
        return getResult.IsSuccess
            ? Result<PaginatedDto<DiscussionTopic>>.Success(getResult.Data)
            : Result<PaginatedDto<DiscussionTopic>>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result> IncrementViewAsync(Guid topicId, Guid userId)
    {
         var viewFlagKey = $"topic:viewed:{topicId}:{userId}";
         var viewCountKey = $"topic:views:{topicId}";
         
         var alreadyViewed = await cacheService.GetAsync<bool>(viewFlagKey);

         if (alreadyViewed)
         {
             return Result.Failure("This topic has already been viewed.");
         }
         
         await cacheService.SetAsync(viewFlagKey, true, TimeSpan.FromHours(1));
         
         var count = await cacheService.GetAsync<int?>(viewCountKey) ?? 0;
         
         await cacheService.SetAsync(viewCountKey, count + 1);

         return Result.Success();
    }
}