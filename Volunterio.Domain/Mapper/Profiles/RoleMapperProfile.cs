using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.Role;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class RoleMapperProfile : Profile
{
    public RoleMapperProfile()
    {
        CreateMap<Role, AccessView>().ConvertUsing(new RoleToAccessViewConverter());
    }
}