using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.HelpRequestImage;

internal class HelpRequestImageToHelpRequestImageViewConverter
    : ITypeConverter<Data.Entities.HelpRequestImage, HelpRequestImageView>
{
    public HelpRequestImageView Convert(
        Data.Entities.HelpRequestImage helpRequestImage,
        HelpRequestImageView helpRequestImageView,
        ResolutionContext context
    ) => new(
        helpRequestImage.Id,
        helpRequestImage.ImageUrl,
        helpRequestImage.ImageThumbnailUrl,
        helpRequestImage.Position
    );
}