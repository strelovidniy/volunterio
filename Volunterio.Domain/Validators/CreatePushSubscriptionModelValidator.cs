using FluentValidation;
using Volunterio.Data.Enums;
using Volunterio.Domain.Models.Create;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class CreatePushSubscriptionModelValidator : AbstractValidator<CreatePushSubscriptionModel>
{
    public CreatePushSubscriptionModelValidator()
    {
        RuleFor(createPushSubscriptionModel => createPushSubscriptionModel.Endpoint)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.EndpointRequired)
            .MaximumLength(500)
            .WithStatusCode(StatusCode.EndpointTooLong);

        RuleFor(createPushSubscriptionModel => createPushSubscriptionModel.Keys)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.KeysRequired);

        RuleFor(createPushSubscriptionModel => createPushSubscriptionModel.Keys)
            .SetValidator(new CreatePushSubscriptionKeysModelValidator());
    }
}