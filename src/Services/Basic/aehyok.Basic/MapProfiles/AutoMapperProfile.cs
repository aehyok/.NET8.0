using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using AutoMapper;

namespace aehyok.Basic.MapProfiles
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
