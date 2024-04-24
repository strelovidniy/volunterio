using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Models.ViewModels;

namespace Volunterio.Domain.Services.Abstraction;

public interface IRazorViewToStringRenderer
{
    public Task<string> RenderViewToStringAsync<TModel>(
        EmailViewLocation viewName,
        TModel model
    ) where TModel : class, IEmailViewModel;
}