using BackgroundTaskExecutor;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Services.Abstraction;

namespace Volunterio.Domain.Services.Realization;

internal class NotificationService(
    IWebPushService webPushService,
    IRepository<PushSubscription> pushSubscriptionRepository
) : INotificationService
{
    [BackgroundExecution("Test")]
    public async Task SendExampleNotification(
        CancellationToken cancellationToken = default
    )
    {
        var credentials = await pushSubscriptionRepository.NoTrackingQuery().ToListAsync(cancellationToken);

        try
        {
            await webPushService.SendNotificationAsync(
                credentials,
                NotificationSubject.Test,
                NotificationContent.Test,
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}