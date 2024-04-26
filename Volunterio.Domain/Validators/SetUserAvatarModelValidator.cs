using FluentValidation;
using Volunterio.Data.Enums.RichEnums;
using Volunterio.Domain.Models;
using Volunterio.Domain.Models.Set;

namespace Volunterio.Domain.Validators;

internal class SetUserAvatarModelValidator : AbstractValidator<SetUserAvatarModel>
{
    public SetUserAvatarModelValidator()
    {
        RuleFor(setCompanyAvatarModel => setCompanyAvatarModel.File)
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
    }
}