using Volunterio.Data.Enums;

namespace Volunterio.Domain.Models.Create;

public record CreateRoleModel(
    string Name,
    RoleType Type,
    bool CanDeleteUsers = false,
    bool CanRestoreUsers = false,
    bool CanEditUsers = false,
    bool CanCreateRoles = false,
    bool CanEditRoles = false,
    bool CanDeleteRoles = false,
    bool CanSeeAllUsers = false,
    bool CanSeeUsers = false,
    bool CanInviteUsers = false,
    bool CanSeeAllRoles = false,
    bool CanSeeRoles = false,
    bool CanMaintainSystem = false
) : IValidatableModel;