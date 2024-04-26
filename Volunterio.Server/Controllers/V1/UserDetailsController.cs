using Microsoft.AspNetCore.Mvc;
using Volunterio.Domain.Attributes;
using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Server.Controllers.Base;

namespace Volunterio.Server.Controllers.V1;

[RouteV1("user-details")]
public class UserDetailsController(
    IServiceProvider services,
    IUserDetailsService userDetailsService
) : BaseController(services)
{
    [HttpPut("update-address")]
    public async Task<IActionResult> UpdateAddressAsync(
        [FromBody] UpdateAddressModel model,
        CancellationToken cancellationToken = default
    )
    {
        await ValidateAsync(model, cancellationToken);

        await userDetailsService.UpdateAddressesAsync(model, cancellationToken);

        return Ok();
    }

    [HttpPut("update-contact-details")]
    public async Task<IActionResult> UpdateContactInfoAsync(
        [FromBody] UpdateContactInfoModel model,
        CancellationToken cancellationToken = default
    )
    {
        await ValidateAsync(model, cancellationToken);

        await userDetailsService.UpdateContactInfoAsync(model, cancellationToken);

        return Ok();
    }

    [HttpPost("upload-avatar")]
    public async Task<IActionResult> SetUserAvatarAsync(
        [FromForm] SetUserAvatarModel model,
        CancellationToken cancellationToken = default
    )
    {
        await ValidateAsync(model, cancellationToken);

        await userDetailsService.SetUserAvatarAsync(model, cancellationToken);

        return Ok();
    }
}