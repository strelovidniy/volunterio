using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.NotificationSettings;

internal class NotificationSettingsToNotificationSettingsViewConverter
    : ITypeConverter<Data.Entities.NotificationSettings, NotificationSettingsView>
{
    public NotificationSettingsView Convert(
        Data.Entities.NotificationSettings notificationSettings,
        NotificationSettingsView notificationSettingsView,
        ResolutionContext context
    ) => new(
        notificationSettings.EnableNotifications,
        notificationSettings.EnableTagFiltration,
        notificationSettings.FilterTags,
        notificationSettings.EnableTitleFiltration,
        notificationSettings.FilterTitles,
        notificationSettings.EnableUpdateNotifications
    );
}