using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Models.Update;

namespace Volunterio.Domain.Services.Abstraction;

public interface IUserDetailsService
{
    public Task UpdateAddressesAsync(
        UpdateAddressModel updateAddressModel,
        CancellationToken cancellationToken = default
    );

    public Task SetUserAvatarAsync(
        SetUserAvatarModel model,
        CancellationToken cancellationToken = default
    );

    public Task UpdateContactInfoAsync(
        UpdateContactInfoModel updateContactInfoModel,
        CancellationToken cancellationToken = default
    );
}