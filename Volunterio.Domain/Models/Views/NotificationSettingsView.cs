namespace Volunterio.Domain.Models.Views;

public record NotificationSettingsView(
    bool EnableNotifications,
    bool EnableTagFiltration,
    IEnumerable<string>? FilterTags,
    bool EnableTitleFiltration,
    IEnumerable<string>? FilterTitles,
    bool EnableUpdateNotifications
);