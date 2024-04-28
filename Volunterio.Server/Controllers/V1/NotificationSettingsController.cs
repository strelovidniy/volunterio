using Microsoft.AspNetCore.Mvc;
using Volunterio.Domain.Attributes;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Server.Controllers.Base;

namespace Volunterio.Server.Controllers.V1;

[RouteV1("notification-settings")]
public class NotificationSettingsController(
    IServiceProvider services,
    IUserAccessService userAccessService,
    INotificationSettingsService notificationSettingsService
) : BaseController(services)
{
    [HttpPut("update")]
    public async Task<IActionResult> UpdateNotificationSettingAsync(
        [FromBody] UpdateNotificationSettingModel model,
        CancellationToken cancellationToken = default
    )
    {
        await userAccessService.CheckIfUserCanSeeHelpRequests(cancellationToken);

        await ValidateAsync(model, cancellationToken);

        await notificationSettingsService.UpdateNotificationSettingAsync(model, cancellationToken);

        return Ok();
    }
}