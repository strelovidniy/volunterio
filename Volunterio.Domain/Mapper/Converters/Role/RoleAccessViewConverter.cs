using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.Role;

internal class RoleAccessViewConverter : ITypeConverter<Data.Entities.Role, AccessView>
{
    public AccessView Convert(
        Data.Entities.Role role,
        AccessView accessView,
        ResolutionContext context
    ) => new(
        role.Name,
        role.Type,
        role.CanDeleteUsers,
        role.CanEditUsers,
        role.CanRestoreUsers,
        role.CanCreateRoles,
        role.CanEditRoles,
        role.CanDeleteRoles,
        role.CanSeeAllUsers,
        role.CanSeeUsers,
        role.CanSeeAllRoles,
        role.CanSeeRoles,
        role.CanMaintainSystem
    );
}