using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.ContactInfo;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class ContactInfoMapperProfile : Profile
{
    public ContactInfoMapperProfile()
    {
        CreateMap<ContactInfo, ContactInfoView>().ConvertUsing(new ContactInfoToContactInfoViewConverter());
    }
}