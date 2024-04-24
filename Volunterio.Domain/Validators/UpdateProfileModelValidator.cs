using FluentValidation;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class UpdateProfileModelValidator : AbstractValidator<UpdateProfileModel>
{
    public UpdateProfileModelValidator(IValidationService validationService)
    {
        RuleFor(updateProfileModel => updateProfileModel.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.UserIdRequired)
            .MustAsync(validationService.IsExistAsync<User>)
            .WithStatusCode(StatusCode.UserNotFound);

        RuleFor(updateProfileModel => updateProfileModel.FirstName)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .WithStatusCode(StatusCode.FirstNameTooLong);

        RuleFor(updateProfileModel => updateProfileModel.LastName)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .WithStatusCode(StatusCode.LastNameTooLong);

        When(
            updateProfileModel => updateProfileModel.Email is not null,
            () =>
            {
                RuleFor(updateProfileModel => updateProfileModel.Email)
                    .Cascade(CascadeMode.Stop)
                    .EmailAddress()
                    .WithStatusCode(StatusCode.InvalidEmailFormat)
                    .MustAsync(validationService.IsEmailUniqueAsync)
                    .WithStatusCode(StatusCode.EmailAlreadyExists);
            }
        );
    }
}