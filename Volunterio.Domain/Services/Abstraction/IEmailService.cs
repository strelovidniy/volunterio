using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Models.ViewModels;

namespace Volunterio.Domain.Services.Abstraction;

public interface IEmailService
{
    public Task SendEmailAsync<TModel>(
        string toAddresses,
        EmailSubject subject,
        TModel viewModel,
        IEnumerable<(string Path, string FileName)>? attachments = null,
        CancellationToken cancellationToken = default
    ) where TModel : class, IEmailViewModel;
}