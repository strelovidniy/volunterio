namespace Volunterio.Domain.Models.Create;

public record CreateUserModel(
    Guid RegistrationToken,
    bool IsHelper
) : IValidatableModel;