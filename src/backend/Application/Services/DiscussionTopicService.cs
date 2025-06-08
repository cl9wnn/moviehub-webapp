using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Models;
using Domain.Utils;

namespace Application.Services;

public class DiscussionTopicService(IDiscussionTopicRepository topicRepository): IDiscussionTopicService
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
        
        return getResult.IsSuccess
            ? Result<DiscussionTopic>.Success(getResult.Data)
            : Result<DiscussionTopic>.Failure(getResult.ErrorMessage!)!;
    }

    public async Task<Result<DiscussionTopic>> CreateTopicAsync(DiscussionTopic topic, Guid userId)
    {
        topic.UserId = userId;
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
}