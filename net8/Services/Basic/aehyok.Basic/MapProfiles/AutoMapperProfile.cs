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

            CreateMap<Menu, MenuDto>()
    .ForMember(a => a.IsLeaf, a => a.MapFrom(c => c.Children.Count == 0));
            CreateMap<Menu, MenuTreeDto>()
                .ForMember(a => a.Children, a => a.Ignore());

            CreateMap<MenuDto, MenuTreeDto>();

            CreateMap<ApiResource, ApiResourceDto>();
            CreateMap<ApiResourceDto, MenuResourceDto>();

            CreateMap<Role, RoleDto>();
        }
    }
}
