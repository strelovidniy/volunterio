using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class ResetPasswordModelValidator : AbstractValidator<ResetPasswordModel>
{
    public ResetPasswordModelValidator(IValidationService validationService)
    {
        RuleFor(resetPasswordModel => resetPasswordModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsUserExistAsync)
            .WithStatusCode(StatusCode.UserNotFound);
    }
}