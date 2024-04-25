using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.UserDetails;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class UserDetailsMapperProfile : Profile
{
    public UserDetailsMapperProfile()
    {
        CreateMap<UserDetails, UserDetailsView>().ConvertUsing(new UserDetailsToUserDetailsViewConverter());
    }
}