﻿using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Set;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class SetNewPasswordModelValidator : AbstractValidator<SetNewPasswordModel>
{
    public SetNewPasswordModelValidator(IValidationService validationService)
    {
        RuleFor(setNewPasswordModel => setNewPasswordModel.NewPassword)
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

        RuleFor(setNewPasswordModel => setNewPasswordModel.ConfirmNewPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.ConfirmPasswordRequired)
            .Equal(setNewPasswordModel => setNewPasswordModel.NewPassword)
            .WithStatusCode(StatusCode.PasswordsDoNotMatch);

        RuleFor(setNewPasswordModel => setNewPasswordModel.VerificationCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.VerificationCodeRequired)
            .MustAsync(validationService.IsUserWithVerificationCodeExistAsync)
            .WithStatusCode(StatusCode.InvalidVerificationCode);
    }
}