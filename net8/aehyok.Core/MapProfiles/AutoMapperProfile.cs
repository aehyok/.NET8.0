using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using AutoMapper;

namespace aehyok.Core.MapProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
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

            CreateMap<Region, RegionDto>();
        }
    }
}
