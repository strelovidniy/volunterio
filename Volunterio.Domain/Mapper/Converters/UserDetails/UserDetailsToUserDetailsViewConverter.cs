using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.UserDetails;

internal class UserDetailsToUserDetailsViewConverter : ITypeConverter<Data.Entities.UserDetails, UserDetailsView>
{
    public UserDetailsView Convert(
        Data.Entities.UserDetails userDetails,
        UserDetailsView userDetailsView,
        ResolutionContext context
    ) => new(
        userDetails.ImageUrl,
        userDetails.ImageThumbnailUrl,
        context.Mapper.Map<AddressView>(userDetails.Address),
        context.Mapper.Map<ContactInfoView>(userDetails.ContactInfo)
    );
}