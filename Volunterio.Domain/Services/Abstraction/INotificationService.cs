using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;

namespace Volunterio.Domain.Services.Abstraction;

internal interface INotificationService
{
    public Task SendNotificationAsync(
        IEnumerable<PushSubscription> recipients,
        NotificationTitle title,
        NotificationContent content,
        string pageUrl,
        CancellationToken cancellationToken = default
    );
}