using FluentValidation;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class UpdateRoleModelValidator : AbstractValidator<UpdateRoleModel>
{
    public UpdateRoleModelValidator(IValidationService validationService)
    {
        RuleFor(updateRoleModel => updateRoleModel.Id)
            .Cascade(CascadeMode.Stop)
            .MustAsync(validationService.IsExistAsync<Role>)
            .WithStatusCode(StatusCode.RoleNotFound);

        RuleFor(updateRoleModel => updateRoleModel.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.RoleNameRequired)
            .MustAsync(validationService.CanRoleNameBeChangedAsync)
            .WithStatusCode(StatusCode.RoleAlreadyExists);

        When(updateRoleModel => updateRoleModel.Type is not null, () =>
        {
            RuleFor(updateRoleModel => updateRoleModel.Type)
                .Cascade(CascadeMode.Stop)
                .IsInEnum()
                .WithStatusCode(StatusCode.InvalidRoleType);
        });
    }
}