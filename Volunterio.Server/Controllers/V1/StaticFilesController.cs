using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Attributes;
using Volunterio.Server.Constants;

namespace Volunterio.Server.Controllers.V1;

[RouteV1("static-files")]
[AllowAnonymous]
[ResponseCache(CacheProfileName = ResponseCacheProfile.StaticDataCacheProfile)]
public class StaticFilesController : ControllerBase
{
    [HttpGet("icon")]
    public IActionResult GetIcon() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.Icon)!,
        ContentType.ImagePng
    );

    [HttpGet("twitter-icon")]
    public IActionResult GetTwitterIcon() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.TwitterIcon)!,
        ContentType.ImagePng
    );

    [HttpGet("facebook-icon")]
    public IActionResult GetFacebookIcon() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.FacebookIcon)!,
        ContentType.ImagePng
    );

    [HttpGet("instagram-icon")]
    public IActionResult GetInstagramIcon() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.InstagramIcon)!,
        ContentType.ImagePng
    );

    [HttpGet("white-background")]
    public IActionResult GetWhiteBackground() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.WhiteBackground)!,
        ContentType.ImagePng
    );

    [HttpGet("divider")]
    public IActionResult GetDivider() => File(
        Assembly
            .GetExecutingAssembly()
            .GetManifestResourceStream(ImageLocation.Divider)!,
        ContentType.ImagePng
    );
}