namespace Volunterio.Domain.Models.Change;

public record ChangeUserRoleModel(
    Guid UserId,
    Guid RoleId
) : IValidatableModel;