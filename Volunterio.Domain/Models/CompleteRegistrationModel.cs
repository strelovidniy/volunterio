namespace Volunterio.Domain.Models;

public record CompleteRegistrationModel(
    Guid RegistrationToken,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string LastName
) : IValidatableModel;