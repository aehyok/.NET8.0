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


            CreateMap<UserToken, UserTokenCacheDto>()
                .ForMember(a => a.Roles, a => a.MapFrom(c => c.User.UserRoles.Select(r => r.Role.Code).ToList()));
                //.ForMember(a => a.PopulationId, a => a.MapFrom(c => c.User.PopulationId));

            CreateMap<UserToken, UserTokenDto>();

            CreateMap<UserTokenCacheDto, UserTokenDto>();
        }
    }
}
