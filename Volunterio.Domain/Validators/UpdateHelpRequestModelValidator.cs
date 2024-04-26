using FluentValidation;
using Volunterio.Data.Entities;
using Volunterio.Data.Enums;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Update;
using Volunterio.Domain.Services.Abstraction;
using Volunterio.Domain.Validators.Extensions;

namespace Volunterio.Domain.Validators;

internal class UpdateHelpRequestModelValidator : AbstractValidator<UpdateHelpRequestModel>
{
    public UpdateHelpRequestModelValidator(IValidationService validationService)
    {
        RuleFor(updateHelpRequestModel => updateHelpRequestModel.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.HelpRequestNotFound)
            .MustAsync(validationService.IsExistAsync<HelpRequest>)
            .WithStatusCode(StatusCode.HelpRequestNotFound);

        RuleFor(updateHelpRequestModel => updateHelpRequestModel.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.TitleRequired)
            .MaximumLength(200)
            .WithStatusCode(StatusCode.TitleTooLong);

        RuleFor(updateHelpRequestModel => updateHelpRequestModel.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.DescriptionRequired)
            .MaximumLength(2000)
            .WithStatusCode(StatusCode.DescriptionTooLong);

        RuleForEach(updateHelpRequestModel => updateHelpRequestModel.Tags)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithStatusCode(StatusCode.TagCannotBeEmpty)
            .MaximumLength(50)
            .WithStatusCode(StatusCode.TagTooLong);

        RuleFor(updateHelpRequestModel => updateHelpRequestModel)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (model, token) =>
            {
                var imagesCount = await validationService.CountImagesAsync(model.Id, token);

                return imagesCount - model.ImagesToDelete?.Count() + model.Images?.Count() > 0;
            })
            .WithStatusCode(StatusCode.ImagesRequired);

        RuleForEach(updateHelpRequestModel => updateHelpRequestModel.Images)
            .Cascade(CascadeMode.Stop)
            .SetValidator(
                new FileValidator(
                    FileSize.FromMegabytes(100),
                    [
                        ContentType.ImageJpeg,
                        ContentType.ImageJpg,
                        ContentType.ImagePng,
                        ContentType.ImageWebp,
                        ContentType.ImageBmp,
                        ContentType.ImageTiff,
                        ContentType.ImageTif,
                        ContentType.ImageGif,
                        ContentType.ImagePbm
                    ]
                )
            );

        When(
            updateHelpRequestModel => updateHelpRequestModel.ImagesToDelete is not null,
            () =>
            {
                RuleFor(updateHelpRequestModel => updateHelpRequestModel.ImagesToDelete!)
                    .Cascade(CascadeMode.Stop)
                    .MustAsync(validationService.AreAllExistAsync<HelpRequestImage>)
                    .WithStatusCode(StatusCode.ImageNotFound);
            }
        );
    }
}