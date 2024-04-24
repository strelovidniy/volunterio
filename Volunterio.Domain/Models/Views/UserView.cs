namespace Volunterio.Domain.Models.Views;

public record UserView(
    Guid Id,
    string? FirstName,
    string? LastName,
    string Email,
    AccessView Access
);