namespace Volunterio.Domain.Models.ViewModels;

public record ResetPasswordEmailViewModel(
    string Url
) : IEmailViewModel;