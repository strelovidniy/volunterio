using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Volunterio.Data.Enums;
using Volunterio.Domain.Exceptions;

namespace Volunterio.Domain.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetCurrentUserId(
        this IHttpContextAccessor httpContextAccessor
    ) => httpContextAccessor.HttpContext.GetCurrentUserId();

    public static Guid GetCurrentUserId(
        this HttpContext? httpContext
    ) => Guid.Parse(httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new ApiException(StatusCode.Unauthorized));
}