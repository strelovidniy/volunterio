using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.NotificationSettings;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class NotificationSettingsMapperProfile : Profile
{
    public NotificationSettingsMapperProfile()
    {
        CreateMap<NotificationSettings, NotificationSettingsView>()
            .ConvertUsing(new NotificationSettingsToNotificationSettingsViewConverter());
    }
}