using Volunterio.Data.Entities;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Change;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Services.Abstraction;

public interface IUserService
{
    public Task<AuthToken> RefreshTokenAsync(
        Guid refreshToken,
        CancellationToken cancellationToken = default
    );

    public Task<LoginView> LoginAsync(
        LoginModel loginModel,
        CancellationToken cancellationToken = default
    );

    public Task ChangePasswordAsync(
        ChangePasswordModel changePasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task ResetPasswordAsync(
        ResetPasswordModel resetPasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task SetNewPasswordAsync(
        SetNewPasswordModel setNewPasswordModel,
        CancellationToken cancellationToken = default
    );

    public Task CreateUserAsync(
        CreateUserModel createUserModel,
        CancellationToken cancellationToken = default
    );

    public Task RegisterUserAsync(
        RegisterUserModel registerUserModel,
        CancellationToken cancellationToken = default
    );

    public Task UpdateUserAsync(
        UpdateUserModel model,
        CancellationToken cancellationToken
    );

    public Task UpdateProfileAsync(
        UpdateProfileModel model,
        CancellationToken cancellationToken
    );

    public Task<User?> GetUserAsync(
        CancellationToken cancellationToken = default
    );

    public Task<UserView?> GetUserViewAsync(
        CancellationToken cancellationToken = default
    );

    public Task<User?> GetUserByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    public Task<PagedCollectionView<User>> GetAllUsersAsync(
        QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    );

    public Task<IEnumerable<User>> GetAllUsersAsync(
        CancellationToken cancellationToken = default
    );

    public Task DeleteUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    public Task RestoreUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    public Task CompleteRegistrationAsync(
        CompleteRegistrationModel completeRegistrationModel,
        CancellationToken cancellationToken = default
    );
}