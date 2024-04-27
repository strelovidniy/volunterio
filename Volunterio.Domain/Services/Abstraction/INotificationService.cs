namespace Volunterio.Domain.Services.Abstraction;

public interface INotificationService
{
    public Task SendExampleNotification(
        CancellationToken cancellationToken = default
    );
}