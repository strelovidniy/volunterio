using BackgroundTaskExecutor;
using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Settings.Abstraction;
using WebPush;
using PushSubscription = Volunterio.Data.Entities.PushSubscription;

namespace Volunterio.Domain.Services.Realization;

internal class NotificationService(
    IWebPushSettings webPushSettings,
    IUrlSettings urlSettings,
    IRepository<PushSubscription> pRepository,
    IRepository<HelpRequest> hRepository
) : INotificationService
{
    public async Task SendNotificationAsync(
        IEnumerable<PushSubscription> recipients,
        NotificationTitle title,
        NotificationContent content,
        string pageUrl,
        CancellationToken cancellationToken = default
    )
    {
        var client = new WebPushClient();

        string stringContent = content;
        string stringTitle = title;

        stringContent = stringContent.Replace("\"", "\\\"");
        stringTitle = stringTitle.Replace("\"", "\\\"");

        var jsonBody
            = $"{{\"notification\":{{\"data\":{{\"onActionClick\":{{\"default\":{{\"operation\":\"openWindow\",\"url\":\"${pageUrl}\"}}}}}},\"body\":\"{stringContent}\",\"title\":\"{stringTitle}\",\"icon\":\"{urlSettings.WebApiUrl.TrimEnd('/')}/api/v1/static-files/icon\",\"vibrate\":[100]}}}}";

        await Task.WhenAll(
            recipients.Select(recipient => client.SendNotificationAsync(
                new WebPush.PushSubscription(
                    recipient.Endpoint,
                    recipient.P256dh,
                    recipient.Auth
                ),
                jsonBody,
                new VapidDetails(
                    urlSettings.AppUrl,
                    webPushSettings.PublicKey,
                    webPushSettings.PrivateKey
                ),
                cancellationToken
            ))
        );
    }

    [BackgroundExecution("Test")]
    public async Task TestAsync(
        CancellationToken cancellationToken
    )
    {
        var credentials = await pRepository.NoTrackingQuery().ToListAsync(cancellationToken);
        var helpRequest = await hRepository.NoTrackingQuery().FirstOrDefaultAsync(cancellationToken);

        try
        {
            await SendNotificationAsync(
                credentials,
                NotificationTitle.NewHelpRequest,
                NotificationContent.NewHelpRequest(helpRequest!.Title),
                $"/#/requests/details?id={helpRequest.Id}",
                cancellationToken
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}