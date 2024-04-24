using Volunterio.Data.Entities;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Services.Abstraction;

public interface IRoleService
{
    public Task<PagedCollectionView<Role>> GetRolesAsync(
        QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    );

    public Task<Role?> GetRoleAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    public Task<Role?> GetUserRoleAsync(
        CancellationToken cancellationToken = default
    );

    public Task<Role?> GetHelperRoleAsync(
        CancellationToken cancellationToken = default
    );

    public Task<Role> UpdateRoleAsync(
        UpdateRoleModel updateRoleModel,
        CancellationToken cancellationToken = default
    );

    public Task<Role> CreateRoleAsync(
        CreateRoleModel createRoleModel,
        CancellationToken cancellationToken = default
    );

    public Task DeleteRoleAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}