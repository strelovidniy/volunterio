using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.User;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, UserView>().ConvertUsing(new UserToUserViewConverter());
    }
}