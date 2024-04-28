﻿using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Runtime;

namespace Volunterio.Domain.Services.Realization;

internal class UserAccessService(
    ICurrentUserService currentUserService
) : IUserAccessService
{
    public async Task CheckIfUserCanSeeUsersAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanSeeAllUsers || user.Role!.CanSeeUsers, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanDeleteUsersAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanDeleteUsers, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanRestoreUsersAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanRestoreUsers, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanEditUsersAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanEditUsers, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanCreateRolesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanCreateRoles, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanEditRolesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanEditRoles, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanDeleteRolesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role!.CanDeleteRoles, StatusCode.Forbidden);
    }

    public async Task CheckIfUserCanSeeRolesAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(
            user.Role!.CanSeeAllRoles || user.Role!.CanSeeRoles,
            StatusCode.Forbidden
        );
    }

    public async Task CheckIfUserCanCreateHelpRequestsAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(
            user.Role!.CanCreateHelpRequest,
            StatusCode.Forbidden
        );
    }

    public async Task CheckIfUserCanSeeHelpRequests(
        CancellationToken cancellationToken = default
    )
    {
        var user = await GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(
            user.Role!.CanSeeHelpRequests,
            StatusCode.Forbidden
        );
    }

    private async Task<User> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    )
    {
        var user = await currentUserService.GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(user.Role is not null, StatusCode.UserHasNoRole);

        return user;
    }
}