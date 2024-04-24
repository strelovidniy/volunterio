namespace Volunterio.Domain.Models;

public record LoginModel(
    string Email,
    string Password,
    bool RememberMe
) : IValidatableModel;