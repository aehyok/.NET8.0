using sun.Basic.Domains;
using sun.Basic.Dtos;
using AutoMapper;

namespace sun.Basic.MapProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DictionaryGroup, DictionaryGroupDto>();

            CreateMap<DictionaryItem, DictionaryItemDto>();

            CreateMap<Options, OptionsDto>();
        }
    }
}
