using Volunterio.Data.Entities;

namespace Volunterio.Domain.Services.Abstraction;

public interface ICurrentUserService
{
    public Task<User> GetCurrentUserAsync(
        CancellationToken cancellationToken = default
    );
}