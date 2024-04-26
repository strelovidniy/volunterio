using Microsoft.AspNetCore.Mvc;
using Volunterio.Domain.Attributes;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Server.Controllers.Base;

namespace Volunterio.Server.Controllers.V1;

[RouteV1("help-requests")]
public class HelpRequestController(
    IServiceProvider services,
    IUserAccessService userAccessService,
    IHelpRequestService helpRequestService
) : BaseController(services)
{
    [HttpGet("get")]
    public async Task<IActionResult> GetHelpRequestAsync(
        [FromQuery] Guid id,
        CancellationToken cancellationToken = default
    ) => Ok(
        await helpRequestService.GetHelpRequestAsync(
            id,
            cancellationToken
        )
    );

    [HttpGet]
    public async Task<IActionResult> GetHelpRequestsAsync(
        [FromQuery] QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    ) => Ok(
        await helpRequestService.GetHelpRequestsAsync(
            queryParametersModel,
            cancellationToken
        )
    );

    [HttpPost("create")]
    public async Task<IActionResult> CreateHelpRequestAsync(
        [FromForm] CreateHelpRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        await userAccessService.CheckIfUserCanCreateHelpRequestsAsync(cancellationToken);

        await ValidateAsync(model, cancellationToken);

        await helpRequestService.CreateHelpRequestAsync(model, cancellationToken);

        return Ok();
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateHelpRequestAsync(
        [FromForm] UpdateHelpRequestModel model,
        CancellationToken cancellationToken = default
    )
    {
        await userAccessService.CheckIfUserCanCreateHelpRequestsAsync(cancellationToken);

        await ValidateAsync(model, cancellationToken);

        await helpRequestService.UpdateHelpRequestAsync(model, cancellationToken);

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteHelpRequestAsync(
        [FromQuery] Guid id,
        CancellationToken cancellationToken = default
    )
    {
        await userAccessService.CheckIfUserCanCreateHelpRequestsAsync(cancellationToken);

        await helpRequestService.DeleteHelpRequestAsync(id, cancellationToken);

        return Ok();
    }
}