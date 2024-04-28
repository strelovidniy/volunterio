using Volunterio.Domain.Models.Update;

namespace Volunterio.Domain.Services.Abstraction;

public interface INotificationSettingsService
{
    public Task UpdateNotificationSettingAsync(
        UpdateNotificationSettingModel updateNotificationSettingModel,
        CancellationToken cancellationToken = default
    );
}