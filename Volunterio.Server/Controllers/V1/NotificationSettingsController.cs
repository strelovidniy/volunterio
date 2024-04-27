using Volunterio.Domain.Attributes;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Server.Controllers.Base;

namespace Volunterio.Server.Controllers.V1
{
    [RouteV1("notification-settings")]
    public class NotificationSettingsController(IServiceProvider services, IUserAccessService userAccessService) : BaseController(services)
    {
    }
}
