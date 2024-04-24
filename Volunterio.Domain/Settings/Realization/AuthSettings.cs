using Volunterio.Domain.Settings.Abstraction;

namespace Volunterio.Domain.Settings.Realization;

public class AuthSettings : IAuthSettings
{
    public IEnumerable<string> AllowedOrigins { get; set; } = null!;
}