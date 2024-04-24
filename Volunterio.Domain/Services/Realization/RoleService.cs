﻿using EntityFrameworkCore.RepositoryInfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Exceptions;
using Volunterio.Domain.Extensions;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Models.Views;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Runtime;

namespace Volunterio.Domain.Services.Realization;

internal class RoleService(
    IRepository<Role> roleRepository,
    ICurrentUserService currentUserService,
    ILogger<RoleService> logger
) : IRoleService
{
    public Task<Role?> GetRoleAsync(
        Guid id,
        CancellationToken cancellationToken = default
    ) => roleRepository
        .Query()
        .FirstOrDefaultAsync(
            role => role.Id == id,
            cancellationToken
        );

    public Task<Role?> GetUserRoleAsync(
        CancellationToken cancellationToken = default
    )
    {
        return roleRepository
            .Query()
            .FirstOrDefaultAsync(
                role => role.Type == RoleType.User,
                cancellationToken
            );
    }

    public async Task<Role> UpdateRoleAsync(
        UpdateRoleModel updateRoleModel,
        CancellationToken cancellationToken = default
    )
    {
        var role = await GetRoleAsync(updateRoleModel.Id, cancellationToken)
            ?? throw new ApiException(StatusCode.RoleNotFound);

        if (role.IsHidden)
        {
            throw new ApiException(StatusCode.RoleCannotBeUpdated);
        }

        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        if (currentUser.Role?.Type is not RoleType.Admin)
        {
            RuntimeValidator.Assert(role.Type is RoleType.Admin, StatusCode.RoleCannotBeUpdated);
        }

        if (role.Name != updateRoleModel.Name)
        {
            role.Name = updateRoleModel.Name.Trim();
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.Type != updateRoleModel.Type && updateRoleModel.Type is not null)
        {
            role.Type = updateRoleModel.Type.Value;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanDeleteUsers != updateRoleModel.CanDeleteUsers)
        {
            role.CanDeleteUsers = updateRoleModel.CanDeleteUsers;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanEditUsers != updateRoleModel.CanEditUsers)
        {
            role.CanEditUsers = updateRoleModel.CanEditUsers;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanRestoreUsers != updateRoleModel.CanRestoreUsers)
        {
            role.CanRestoreUsers = updateRoleModel.CanRestoreUsers;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanCreateRoles != updateRoleModel.CanCreateRoles)
        {
            role.CanCreateRoles = updateRoleModel.CanCreateRoles;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanEditRoles != updateRoleModel.CanEditRoles)
        {
            role.CanEditRoles = updateRoleModel.CanEditRoles;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanDeleteRoles != updateRoleModel.CanDeleteRoles)
        {
            role.CanDeleteRoles = updateRoleModel.CanDeleteRoles;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanSeeAllUsers != updateRoleModel.CanSeeAllUsers)
        {
            role.CanSeeAllUsers = updateRoleModel.CanSeeAllUsers && currentUser.Role?.Type is RoleType.Admin;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanSeeUsers != updateRoleModel.CanSeeUsers)
        {
            role.CanSeeUsers = updateRoleModel.CanSeeUsers;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanInviteUsers != updateRoleModel.CanInviteUsers)
        {
            role.CanInviteUsers = updateRoleModel.CanInviteUsers;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanSeeAllRoles != updateRoleModel.CanSeeAllRoles)
        {
            role.CanSeeAllRoles = updateRoleModel.CanSeeAllRoles && currentUser.Role?.Type is RoleType.Admin;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanSeeRoles != updateRoleModel.CanSeeRoles)
        {
            role.CanSeeRoles = updateRoleModel.CanSeeRoles;
            role.UpdatedAt = DateTime.UtcNow;
        }

        if (role.CanMaintainSystem != updateRoleModel.CanMaintainSystem)
        {
            role.CanMaintainSystem = updateRoleModel.CanMaintainSystem && currentUser.Role?.Type is RoleType.Admin;
            role.UpdatedAt = DateTime.UtcNow;
        }

        await roleRepository.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role> CreateRoleAsync(
        CreateRoleModel createRoleModel,
        CancellationToken cancellationToken = default
    )
    {
        var addedRole = await roleRepository.AddAsync(
            new Role
            {
                Name = createRoleModel.Name.Trim(),
                Type = createRoleModel.Type,
                CanDeleteUsers = createRoleModel.CanDeleteUsers,
                CanRestoreUsers = createRoleModel.CanRestoreUsers,
                CanEditUsers = createRoleModel.CanEditUsers,
                CanCreateRoles = createRoleModel.CanCreateRoles,
                CanEditRoles = createRoleModel.CanEditRoles,
                CanDeleteRoles = createRoleModel.CanDeleteRoles,
                CanSeeAllUsers = createRoleModel.CanSeeAllUsers,
                CanSeeUsers = createRoleModel.CanSeeUsers,
                CanInviteUsers = createRoleModel.CanInviteUsers,
                CanSeeAllRoles = createRoleModel.CanSeeAllRoles,
                CanSeeRoles = createRoleModel.CanSeeRoles,
                CanMaintainSystem = createRoleModel.CanMaintainSystem
            },
            cancellationToken
        );

        await roleRepository.SaveChangesAsync(cancellationToken);

        return addedRole;
    }

    public async Task DeleteRoleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var role = await GetRoleAsync(id, cancellationToken);

        RuntimeValidator.Assert(role is not null, StatusCode.RoleNotFound);

        if (role!.IsHidden)
        {
            throw new ApiException(StatusCode.RoleCannotBeDeleted);
        }

        roleRepository.Delete(role);

        await roleRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<PagedCollectionView<Role>> GetRolesAsync(
        QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    )
    {
        var roles = roleRepository
            .Query()
            .AsNoTracking()
            .Where(role => !role.IsHidden);

        var currentUser = await currentUserService.GetCurrentUserAsync(cancellationToken);

        RuntimeValidator.Assert(currentUser.Role?.CanSeeRoles is true, StatusCode.Forbidden);

        if (!string.IsNullOrWhiteSpace(queryParametersModel.SearchQuery))
        {
            roles = roles.Where(role =>
                role.Name.Contains(queryParametersModel.SearchQuery)
            );
        }

        roles = roles.ExpandAndSort(queryParametersModel, logger);

        try
        {
            return new PagedCollectionView<Role>(
                await roles
                    .Skip(queryParametersModel.PageSize * (queryParametersModel.PageNumber - 1))
                    .Take(queryParametersModel.PageSize)
                    .ToListAsync(cancellationToken),
                await roles.CountAsync(cancellationToken)
            );
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while getting Roles");

            throw new ApiException(StatusCode.QueryResultError);
        }
    }

    public Task<Role?> GetHelperRoleAsync(
        CancellationToken cancellationToken = default
    )
    {
        return roleRepository
            .Query()
            .FirstOrDefaultAsync(
                role => role.Type == RoleType.Helper,
                cancellationToken
            );
    }
}