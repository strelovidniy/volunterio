using Volunterio.Domain.Constants;
using Volunterio.Domain.Settings.Abstraction;

namespace Volunterio.Domain.Settings.Realization;

internal class ImageSettings : IImageSettings
{
    public int MaxImageSize { get; set; } = Defaults.MaxImageSize;

    public int MaxThumbnailSize { get; set; } = Defaults.MaxThumbnailSize;
}