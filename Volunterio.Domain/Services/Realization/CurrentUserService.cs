using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Exceptions;
using Volunterio.Domain.Extensions;
using Volunterio.Domain.Services.Abstraction;

namespace Volunterio.Domain.Services.Realization;

internal class CurrentUserService(
    IRepository<User> userRepository,
    IHttpContextAccessor httpContextAccessor
) : ICurrentUserService
{
    public async Task<User> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    ) => await userRepository
            .NoTrackingQuery()
            .Include(user => user.Role)
            .FirstOrDefaultAsync(
                user => user.Id == httpContextAccessor.GetCurrentUserId(),
                cancellationToken
            )
        ?? throw new ApiException(StatusCode.Unauthorized);
}