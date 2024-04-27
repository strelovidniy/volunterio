using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.HelpRequest;

internal class HelpRequestToHelpRequestViewConverter : ITypeConverter<Data.Entities.HelpRequest, HelpRequestView>
{
    public HelpRequestView Convert(
        Data.Entities.HelpRequest helpRequest,
        HelpRequestView helpRequestView,
        ResolutionContext context
    ) => new(
        helpRequest.Id,
        helpRequest.Title,
        helpRequest.Description,
        helpRequest.Tags ?? [],
        helpRequest.Latitude,
        helpRequest.Longitude,
        helpRequest.ShowContactInfo
            ? context.Mapper.Map<ContactInfoView>(helpRequest.Issuer?.Details?.ContactInfo)
            : null,
        helpRequest.Issuer?.FullName,
        helpRequest.Issuer?.Details?.ImageUrl,
        helpRequest.Issuer?.Details?.ImageThumbnailUrl,
        helpRequest.Deadline,
        context.Mapper.Map<IEnumerable<HelpRequestImageView>>(helpRequest.Images?.OrderBy(image => image.Position))
    );
}