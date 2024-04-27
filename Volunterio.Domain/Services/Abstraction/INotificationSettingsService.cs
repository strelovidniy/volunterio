using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Services.Abstraction
{
    public interface INotificationSettingsService
    {
        public Task UpdateNotificationSettingAsync(
            UpdateNotificationSettingModel updateNotificationSettingModel,
            CancellationToken cancellationToken = default
        );
    }
}
