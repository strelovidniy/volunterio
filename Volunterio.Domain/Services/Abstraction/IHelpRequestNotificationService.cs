using Volunterio.Data.Entities;

namespace Volunterio.Domain.Services.Abstraction;

internal interface IHelpRequestNotificationService
{
    public Task NotifyAboutHelpRequestUpdateAsync(
        HelpRequest helpRequest,
        CancellationToken cancellationToken = default
    );

    public Task NotifyAboutCreatingHelpRequestAsync(
        HelpRequest helpRequest,
        CancellationToken cancellationToken = default
    );
}