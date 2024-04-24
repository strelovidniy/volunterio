using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator(IValidationService validationService)
    {
        RuleFor(loginModel => loginModel.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EmailRequired)
            .EmailAddress()
            .WithStatusCode(StatusCode.InvalidEmailFormat)
            .MustAsync(validationService.IsUserExistAsync)
            .WithStatusCode(StatusCode.UserNotFound);

        RuleFor(loginModel => loginModel.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.PasswordRequired)
            .MinimumLength(8)
            .WithStatusCode(StatusCode.PasswordLengthExceeded);
    }
}