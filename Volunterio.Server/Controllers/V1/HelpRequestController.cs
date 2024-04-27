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
        CancellationToken cancellationToken = default
    )
    {
        var model = ParseFormDataToCreateHelpRequestModel(Request.Form);

        await userAccessService.CheckIfUserCanCreateHelpRequestsAsync(cancellationToken);

        await ValidateAsync(model, cancellationToken);

        await helpRequestService.CreateHelpRequestAsync(model, cancellationToken);

        return Ok();
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateHelpRequestAsync(
        CancellationToken cancellationToken = default
    )
    {
        var model = ParseFormDataToUpdateHelpRequestModel(Request.Form);

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

    private static CreateHelpRequestModel ParseFormDataToCreateHelpRequestModel(IFormCollection form)
    {
        // Parse basic fields
        var title = form["title"].ToString();
        var description = form["description"].ToString();

        var tags = form["tags"]
            .ToString()
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(tag => tag.Trim())
            .Where(tag => !string.IsNullOrWhiteSpace(tag));

        DateTime? deadline = DateTime.TryParse(form["deadline"].ToString(), out var deadlineDate)
            ? deadlineDate.ToUniversalTime()
            : null;

        double? latitude = double.TryParse(form["latitude"].ToString(), out var lat)
            ? lat
            : null;

        double? longitude = double.TryParse(form["latitude"].ToString(), out var lon)
            ? lon
            : null;

        var showInfo = bool.TryParse(form["showContactInfo"].ToString(), out var show) && show;

        var images = form.Files.GetFiles("images");

        return new CreateHelpRequestModel(
            title,
            description,
            tags,
            latitude,
            longitude,
            showInfo,
            deadline,
            images
        );
    }

    private static UpdateHelpRequestModel ParseFormDataToUpdateHelpRequestModel(IFormCollection form)
    {
        // Parse basic fields
        var id = Guid.Parse(form["id"].ToString());
        var title = form["title"].ToString();
        var description = form["description"].ToString();

        var tags = form["tags"]
            .ToString()
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(tag => tag.Trim())
            .Where(tag => !string.IsNullOrWhiteSpace(tag));

        DateTime? deadline = DateTime.TryParse(form["deadline"].ToString(), out var deadlineDate)
            ? deadlineDate.ToUniversalTime()
            : null;

        double? latitude = double.TryParse(form["latitude"].ToString(), out var lat)
            ? lat
            : null;

        double? longitude = double.TryParse(form["latitude"].ToString(), out var lon)
            ? lon
            : null;

        var showInfo = bool.TryParse(form["showContactInfo"].ToString(), out var show) && show;

        var images = form.Files.GetFiles("images");

        var imageToDeleteIds = new List<Guid>();

        for (var i = 0;; i++)
        {
            var key = $"imagesToDelete[{i}]";

            if (!form.ContainsKey(key))
            {
                break;
            }

            var imageId = form[key];

            if (Guid.TryParse(imageId, out var imageToDeleteId))
            {
                imageToDeleteIds.Add(imageToDeleteId);
            }
        }

        return new UpdateHelpRequestModel(
            id,
            title,
            description,
            tags,
            latitude,
            longitude,
            showInfo,
            deadline,
            images,
            imageToDeleteIds
        );
    }
}