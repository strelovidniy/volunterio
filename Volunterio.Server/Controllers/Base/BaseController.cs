using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volunterio.Domain.Exceptions;
using Volunterio.Domain.Models;

namespace Volunterio.Server.Controllers.Base;

[ApiController]
[Authorize]
public class BaseController(
    IServiceProvider services
) : ControllerBase
{
    protected Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new ApiException(Data.Enums.StatusCode.Unauthorized));

    protected Task ValidateAsync<T>(T validatableModel, CancellationToken cancellationToken = default)
        where T : class, IValidatableModel =>
        services.GetRequiredService<IValidator<T>>().ValidateAndThrowAsync(validatableModel, cancellationToken);
}