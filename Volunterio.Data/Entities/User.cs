using EntityFrameworkCore.RepositoryInfrastructure;
using Volunterio.Data.Enums;

namespace Volunterio.Data.Entities;

public class User : EntityBase, IEntity
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName => FirstName is not null && LastName is not null
        ? $"{FirstName} {LastName}"
        : null;

    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    public Guid? RegistrationToken { get; set; }

    public Guid? VerificationCode { get; set; }

    public string? RefreshToken { get; set; }

    public UserStatus Status { get; set; }

    public DateTime? RefreshTokenExpiresAt { get; set; }

    public Role? Role { get; set; }

    public Guid? RoleId { get; set; }
}