interface INotificationsConfig {
    enableNotifications: boolean;
    enableUpdateNotifications: boolean;
    enableTagFilter: boolean;
    tagFilters?: string[];
    enableTitleFilter: boolean;
    titleFilters?: string[];
}

export default INotificationsConfig;
