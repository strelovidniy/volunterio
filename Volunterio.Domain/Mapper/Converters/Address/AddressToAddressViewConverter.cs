using AutoMapper;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Converters.Address;

internal class AddressToAddressViewConverter : ITypeConverter<Data.Entities.Address, AddressView>
{
    public AddressView Convert(
        Data.Entities.Address address,
        AddressView addressView,
        ResolutionContext context
    ) => new(
        address.AddressLine1,
        address.AddressLine2,
        address.City,
        address.State,
        address.PostalCode,
        address.Country
    );
}