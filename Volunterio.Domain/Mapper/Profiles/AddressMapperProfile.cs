using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.Address;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class AddressMapperProfile : Profile
{
    public AddressMapperProfile()
    {
        CreateMap<Address, AddressView>().ConvertUsing(new AddressToAddressViewConverter());
    }
}