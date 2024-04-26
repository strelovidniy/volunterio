using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Services.Abstraction;

public interface IHelpRequestService
{
    public Task<HelpRequestView> GetHelpRequestAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    public Task CreateHelpRequestAsync(
        CreateHelpRequestModel createHelpRequestModel,
        CancellationToken cancellationToken = default
    );

    public Task UpdateHelpRequestAsync(
        UpdateHelpRequestModel updateHelpRequestModel,
        CancellationToken cancellationToken = default
    );

    public Task<PagedCollectionView<HelpRequestView>> GetHelpRequestsAsync(
        QueryParametersModel queryParametersModel,
        CancellationToken cancellationToken = default
    );

    public Task DeleteHelpRequestAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}