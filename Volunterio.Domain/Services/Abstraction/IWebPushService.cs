using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Domain.Services.Abstraction;

internal interface IWebPushService
{
    public Task SendNotificationAsync(
        IEnumerable<PushSubscription> recipients,
        NotificationSubject subject,
        NotificationContent content,
        CancellationToken cancellationToken = default
    );
}