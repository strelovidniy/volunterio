using Volunterio.Domain.Settings.Abstraction;

namespace Volunterio.Domain.Settings.Realization;

internal class WebPushSettings : IWebPushSettings
{
    public string PublicKey { get; set; } = null!;

    public string PrivateKey { get; set; } = null!;
}