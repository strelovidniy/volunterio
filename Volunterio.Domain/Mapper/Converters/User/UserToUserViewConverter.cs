using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.User;

internal class UserToUserViewConverter : ITypeConverter<Data.Entities.User, UserView>
{
    public UserView Convert(
        Data.Entities.User user,
        UserView userView,
        ResolutionContext context
    ) => new(
        user.Id,
        user.FirstName,
        user.LastName,
        user.Email,
        context.Mapper.Map<AccessView>(user.Role),
        context.Mapper.Map<UserDetailsView?>(user.Details),
        context.Mapper.Map<NotificationSettingsView>(user.NotificationSettings)
    );
}