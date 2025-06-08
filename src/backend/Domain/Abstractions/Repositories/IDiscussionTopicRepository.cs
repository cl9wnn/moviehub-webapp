using Domain.Models;

namespace Domain.Abstractions.Repositories;

public interface IDiscussionTopicRepository: IRepository<Guid, DiscussionTopic>
{
    
}