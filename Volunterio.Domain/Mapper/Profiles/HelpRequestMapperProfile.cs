using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.HelpRequest;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class HelpRequestMapperProfile : Profile
{
    public HelpRequestMapperProfile()
    {
        CreateMap<HelpRequest, HelpRequestView>().ConvertUsing(new HelpRequestToHelpRequestViewConverter());
    }
}