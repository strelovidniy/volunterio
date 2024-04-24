using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class CompleteRegistrationModelValidator : AbstractValidator<CompleteRegistrationModel>
{
    public CompleteRegistrationModelValidator()
    {
        RuleFor(registerUserModel => registerUserModel.FirstName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.FirstNameRequired)
            .Must(firstName => !firstName.Contains(' '))
            .WithStatusCode(StatusCode.FirstNameShouldNotContainWhiteSpace);

        RuleFor(registerUserModel => registerUserModel.LastName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.LastNameRequired)
            .Must(lastName => !lastName.Contains(' '))
            .WithStatusCode(StatusCode.LastNameShouldNotContainWhiteSpace);

        RuleFor(registerUserModel => registerUserModel.Password)
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

        RuleFor(registerUserModel => registerUserModel.ConfirmPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(registerUserModel => registerUserModel.Password)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);
    }
}