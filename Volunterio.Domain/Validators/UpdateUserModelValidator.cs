using FluentValidation;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class UpdateUserModelValidator : AbstractValidator<UpdateUserModel>
{
    public UpdateUserModelValidator(IValidationService validationService)
    {
        RuleFor(updateUserModel => updateUserModel.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.UserIdRequired)
            .MustAsync(validationService.IsExistAsync<User>)
            .WithStatusCode(StatusCode.UserNotFound);

        When(updateUserModel => updateUserModel.FirstName is not null, () =>
        {
            RuleFor(updateUserModel => updateUserModel.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithStatusCode(StatusCode.FirstNameRequired)
                .Must(firstName => !firstName!.Contains(" "))
                .WithStatusCode(StatusCode.FirstNameShouldNotContainWhiteSpace);
        });

        When(updateUserModel => updateUserModel.LastName is not null, () =>
        {
            RuleFor(updateUserModel => updateUserModel.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithStatusCode(StatusCode.LastNameRequired)
                .Must(lastName => !lastName!.Contains(" "))
                .WithStatusCode(StatusCode.LastNameShouldNotContainWhiteSpace);
        });
    }
}