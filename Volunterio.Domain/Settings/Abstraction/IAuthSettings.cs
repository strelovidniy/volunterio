namespace Volunterio.Domain.Settings.Abstraction;

public interface IAuthSettings
{
    public IEnumerable<string> AllowedOrigins { get; set; }
}