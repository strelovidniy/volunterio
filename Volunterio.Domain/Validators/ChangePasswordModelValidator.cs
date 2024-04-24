using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Change;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidator(IValidationService validationService)
    {
        RuleFor(changePasswordModel => changePasswordModel.NewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.PasswordRequired)
            .MinimumLength(8)
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeast8Characters)
            .MaximumLength(32)
            .WithStatusCode(StatusCode.PasswordMustHaveNotMoreThan32Characters)
            .Matches("[A-Z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneUppercaseLetter)
            .Matches("[a-z]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneLowercaseLetter)
            .Matches("[0-9]")
            .WithStatusCode(StatusCode.PasswordMustHaveAtLeastOneDigit);

        RuleFor(changePasswordModel => changePasswordModel.ConfirmNewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(changePasswordModel => changePasswordModel.NewPassword)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);

        RuleFor(changePasswordModel => changePasswordModel.OldPassword)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsCurrentUserPasswordCorrectAsync)
            .WithStatusCode(StatusCode.OldPasswordIncorrect);
    }
}