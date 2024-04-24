namespace Volunterio.Domain.Models;

public record ResetPasswordModel(
    string Email
) : IValidatableModel;