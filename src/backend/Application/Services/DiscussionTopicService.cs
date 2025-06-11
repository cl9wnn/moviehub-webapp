using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using Domain.Dtos;
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

    public async Task<Result<PaginatedDto<DiscussionTopic>>> GetPaginatedTopicsAsync(int page, int pageSize)
    {
        var getResult = await topicRepository.GetPaginatedAsync(page, pageSize);
        
        return getResult.IsSuccess
            ? Result<PaginatedDto<DiscussionTopic>>.Success(getResult.Data)
            : Result<PaginatedDto<DiscussionTopic>>.Failure(getResult.ErrorMessage!)!;
    }
}