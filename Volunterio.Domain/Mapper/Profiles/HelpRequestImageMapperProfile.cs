using AutoMapper;
using Volunterio.Data.Entities;
using Volunterio.Domain.Mapper.Converters.HelpRequestImage;
using Volunterio.Domain.Models.Views;

namespace Volunterio.Domain.Mapper.Profiles;

internal class HelpRequestImageMapperProfile : Profile
{
    public HelpRequestImageMapperProfile()
    {
        CreateMap<HelpRequestImage, HelpRequestImageView>()
            .ConvertUsing(new HelpRequestImageToHelpRequestImageViewConverter());
    }
}