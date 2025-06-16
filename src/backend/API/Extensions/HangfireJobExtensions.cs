using Hangfire;
using Infrastructure.BackgroundJobs;

namespace API.Extensions;

public static class HangfireJobsExtensions
{
    public static void RegisterRecurringJobs()
    {
        RecurringJob.AddOrUpdate<TopicViewsSyncJob>(
            "sync-topic-views",
            job => job.ExecuteAsync(),
            "0 */20 * * * *"
        );
    }
}