using Volunterio.Data.Entities;
using Volunterio.Domain.Models.Create;

namespace Volunterio.Domain.Services.Abstraction;

internal interface IPushSubscriptionService
{
    public Task AddPushSubscriptionAsync(
        CreatePushSubscriptionModel createPushSubscriptionModel,
        CancellationToken cancellationToken = default
    );

    public Task DeletePushSubscriptionAsync(
        PushSubscription pushSubscription,
        CancellationToken cancellationToken
    );
}