using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.Core.Domains;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.MapProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DictionaryGroup, DictionaryGroupDto>();

            CreateMap<DictionaryItem, DictionaryItemDto>();

            CreateMap<Menu, MenuDto>()
                .ForMember(a => a.IsLeaf, a => a.MapFrom(c => c.Children.Count == 0));

            CreateMap<Menu, MenuTreeDto>()
                .ForMember(a => a.Children, a => a.Ignore());

            CreateMap<MenuDto, MenuTreeDto>();

            CreateMap<ApiResource, ApiResourceDto>();
            CreateMap<ApiResourceDto, MenuResourceDto>();

            CreateMap<Role, RoleDto>();

            CreateMap<User, UserDto>()
                .ForMember(a => a.Roles, a => a.MapFrom(c => c.UserRoles))
                .ForMember(a => a.HasPassword, a => a.MapFrom(c => !string.IsNullOrWhiteSpace(c.Password)));

            CreateMap<UserRole, UserRoleDto>();

            CreateMap<UserToken, UserTokenCacheDto>()
                .ForMember(a => a.Roles, a => a.MapFrom(c => c.User.UserRoles.Select(r => r.Role.Code).ToList()));
                //.ForMember(a => a.PopulationId, a => a.MapFrom(c => c.User.PopulationId));

            CreateMap<UserToken, UserTokenDto>();

            CreateMap<UserTokenCacheDto, UserTokenDto>();

            CreateMap<Region, RegionDto>();
        }
    }
}
