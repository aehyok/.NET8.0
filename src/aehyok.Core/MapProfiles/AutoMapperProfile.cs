using aehyok.Basic.Domains;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using AutoMapper;
using File = aehyok.Core.Domains.File;

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

            CreateMap<SeedDataTask, SeedDataTaskDto>();

            CreateMap<ApiResource, ApiResourceDto>();

            CreateMap<ScheduleTask, ScheduleTaskDto>();

            CreateMap<Template, TemplateDto>();

            CreateMap<ApiResourceDto, MenuResourceDto>();

            CreateMap<Role, RoleDto>();

            CreateMap<File, FileDto>();

            CreateMap<User, UserDto>()
                //.ForMember(a => a.Roles, a => a.MapFrom(c => c.UserRoles))
                .ForMember(a => a.HasPassword, a => a.MapFrom(c => !string.IsNullOrWhiteSpace(c.Password)));

            CreateMap<UserRole, UserRoleDto>()
                .ForMember(a => a.RegionName, a => a.MapFrom(c => c.Region.Name))
                .ForMember(a => a.RoleName, a => a.MapFrom(c => c.Role.Name))
                .ForMember(a => a.PlatformType, a => a.MapFrom(c => c.Role.PlatformType));

            CreateMap<Region, RegionDto>();

            CreateMap<ScheduleTask, ScheduleTaskExecuteDto>();

            CreateMap<ScheduleTaskExecuteDto, ScheduleTask>();

            CreateMap<UserToken, UserTokenCacheDto>();
    //.ForMember(a => a.Roles, a => a.MapFrom(c => c.User.UserRoles.Select(r => r.Role.Code).ToList()));
            //.ForMember(a => a.PopulationId, a => a.MapFrom(c => c.User.PopulationId));

            CreateMap<UserToken, UserTokenDto>();

            CreateMap<UserTokenCacheDto, UserTokenDto>();

            CreateMap<Permission, RolePermissionDto>();

            CreateMap<User, CurrentUserDto>()
                //.ForMember(a => a.Roles, a => a.MapFrom(c => c.UserRoles))
                .ForMember(a => a.HasPassword, a => a.MapFrom(c => !string.IsNullOrWhiteSpace(c.Password)));

            CreateMap<AsyncTask, AsyncTaskDto>();
        }
    }
}
