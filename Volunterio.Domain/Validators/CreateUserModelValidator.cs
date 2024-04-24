using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class CreateUserModelValidator : AbstractValidator<CreateUserModel>
{
    public CreateUserModelValidator(IValidationService validationService)
    {
        RuleFor(createUserModel => createUserModel.RegistrationToken)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.InvitationTokenRequired)
            .MustAsync(validationService.IsPendingUserExistAsync)
            .WithStatusCode(StatusCode.UserNotFound);
    }
}