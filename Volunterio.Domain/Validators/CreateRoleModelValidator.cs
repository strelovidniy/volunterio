using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class CreateRoleModelValidator : AbstractValidator<CreateRoleModel>
{
    public CreateRoleModelValidator(IValidationService validationService)
    {
        RuleFor(createRoleModel => createRoleModel.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.RoleNameRequired)
            .MustAsync(async (name, cancellationToken) =>
                !await validationService.IsRoleExistAsync(name, cancellationToken))
            .WithStatusCode(StatusCode.RoleAlreadyExists);

        RuleFor(createRoleModel => createRoleModel.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithStatusCode(StatusCode.RoleTypeRequired)
            .IsInEnum()
            .WithStatusCode(StatusCode.InvalidRoleType);
    }
}