using aehyok.Core.Domains;
using aehyok.Core.Dtos.Create;
using AutoMapper;

namespace aehyok.Core.MapProfiles
{
    /// <summary>
    /// 新增和修改数据的映射文件
    /// </summary>
    public class AutoMapperCreateProfile: Profile
    {
        public AutoMapperCreateProfile()
        {
            CreateMap<CreateMenuDto, Menu>();

            CreateMap<CreateRoleDto, Role>();

            CreateMap<CreateRegionDto,  Region>();
            
            CreateMap<CreateUserDto, User>()
                .ForMember(a => a.Roles, a => a.Ignore());

            CreateMap<CreateUserRoleDto, UserRole>();


        }
    }
}
