using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using Volunterio.Data.Entities;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Services.Abstraction;

namespace Volunterio.Domain.Services.Realization;

internal class PushSubscriptionService(
    IRepository<PushSubscription> pushSubscriptionRepository,
    ICurrentUserService currentUserService
) : IPushSubscriptionService
{
    public async Task AddPushSubscriptionAsync(
        CreatePushSubscriptionModel createPushSubscriptionModel,
        CancellationToken cancellationToken = default
    )
    {
        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        var existingSubscription = await pushSubscriptionRepository
            .NoTrackingQuery()
            .FirstOrDefaultAsync(
                pushSubscription => pushSubscription.Endpoint == createPushSubscriptionModel.Endpoint
                    && pushSubscription.Auth == createPushSubscriptionModel.Keys.Auth
                    && pushSubscription.P256dh == createPushSubscriptionModel.Keys.P256dh
                    && pushSubscription.UserId == currentUser!.Id,
                cancellationToken
            );

        if (existingSubscription is not null)
        {
            return;
        }

        await pushSubscriptionRepository.AddAsync(
            new PushSubscription
            {
                Endpoint = createPushSubscriptionModel.Endpoint,
                ExpirationTime = createPushSubscriptionModel.ExpirationTime,
                P256dh = createPushSubscriptionModel.Keys.P256dh,
                Auth = createPushSubscriptionModel.Keys.Auth,
                UserId = currentUser!.Id
            },
            cancellationToken
        );

        await pushSubscriptionRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePushSubscriptionAsync(
        PushSubscription pushSubscription,
        CancellationToken cancellationToken
    )
    {
        var pushSubscriptionsToDelete = await pushSubscriptionRepository
            .Query()
            .Where(subscription => subscription.P256dh == pushSubscription.P256dh
                && subscription.Auth == pushSubscription.Auth
                && subscription.Endpoint == pushSubscription.Endpoint)
            .ToListAsync(cancellationToken);

        await pushSubscriptionRepository.DeleteRangeAsync(pushSubscriptionsToDelete, cancellationToken);

        await pushSubscriptionRepository.SaveChangesAsync(cancellationToken);
    }
}