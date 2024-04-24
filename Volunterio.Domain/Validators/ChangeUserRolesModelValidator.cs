using FluentValidation;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Change;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class ChangeUserRolesModelValidator : AbstractValidator<ChangeUserRoleModel>
{
    public ChangeUserRolesModelValidator(IValidationService validationService)
    {
        RuleFor(changeUserRolesModel => changeUserRolesModel.UserId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.UserIdRequired)
            .MustAsync(validationService.IsExistAsync<User>)
            .WithStatusCode(StatusCode.UserNotFound)
            .MustAsync(validationService.CanUserRoleBeChanged)
            .WithStatusCode(StatusCode.UserRoleCannotBeChanged);

        RuleFor(changeUserRolesModel => changeUserRolesModel.RoleId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.RoleIdRequired)
            .MustAsync(validationService.IsExistAsync<Role>)
            .WithStatusCode(StatusCode.RoleNotFound);
    }
}