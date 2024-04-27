using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Settings.Abstraction;
using WebPush;
using PushSubscription = Volunterio.Data.Entities.PushSubscription;

namespace Volunterio.Domain.Services.Realization;

internal class WebPushService(
    IWebPushSettings webPushSettings
) : IWebPushService
{
    public async Task SendNotificationAsync(
        IEnumerable<PushSubscription> recipients,
        NotificationSubject subject,
        NotificationContent content,
        CancellationToken cancellationToken = default
    )
    {
        var client = new WebPushClient();

        await Task.WhenAll(
            recipients.Select(recipient => client.SendNotificationAsync(
                new WebPush.PushSubscription(
                    recipient.Endpoint,
                    recipient.P256dh,
                    recipient.Auth
                ),
                """{"notification":{"actions":[{"action":"openRequest","title":"Open"}],"data":{"onActionClick":{"default":{"operation":"openWindow"},"openRequest":{"operation":"openWindow","url":"/requests"}}}}}""",
                new VapidDetails(
                    "mailto:admin@example.com",
                    webPushSettings.PublicKey,
                    webPushSettings.PrivateKey
                ),
                cancellationToken
            ))
        );
    }
}