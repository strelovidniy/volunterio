using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class UpdateNotificationSettingModelValidator : AbstractValidator<UpdateNotificationSettingModel>
{
    public UpdateNotificationSettingModelValidator()
    {
        When(
            updateNotificationSettingModel => !updateNotificationSettingModel.EnableNotifications,
            () =>
            {
                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.EnableTagFiltration)
                    .Cascade(CascadeMode.Stop)
                    .Must(x => !x)
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);

                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.EnableTitleFiltration)
                    .Cascade(CascadeMode.Stop)
                    .Must(x => !x)
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);

                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.EnableUpdateNotifications)
                    .Cascade(CascadeMode.Stop)
                    .Must(x => !x)
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);

                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.FilterTags)
                    .Cascade(CascadeMode.Stop)
                    .Empty()
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);

                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.FilterTitles)
                    .Cascade(CascadeMode.Stop)
                    .Empty()
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);
            }
        );

        When(
            updateNotificationSettingModel => !updateNotificationSettingModel.EnableTitleFiltration,
            () =>
            {
                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.FilterTitles)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithStatusCode(StatusCode.FilterTitlesRequired);
            }
        );

        When(
            updateNotificationSettingModel => !updateNotificationSettingModel.EnableTagFiltration,
            () =>
            {
                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.FilterTags)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .WithStatusCode(StatusCode.FilterTagsRequired);
            }
        );

        When(
            updateNotificationSettingModel => !updateNotificationSettingModel.EnableTagFiltration
                && !updateNotificationSettingModel.EnableTitleFiltration,
            () =>
            {
                RuleFor(updateNotificationSettingModel => updateNotificationSettingModel.EnableUpdateNotifications)
                    .Cascade(CascadeMode.Stop)
                    .Must(x => !x)
                    .WithStatusCode(StatusCode.InvalidNotificationSettings);
            }
        );
    }
}