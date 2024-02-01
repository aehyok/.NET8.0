using sun.Core.Domains;
using sun.Core.Dtos.Create;
using AutoMapper;

namespace sun.Core.MapProfiles
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

            CreateMap<CreateUserDto, User>();

            CreateMap<CreateUserRoleDto, UserRole>();
        }
    }
}
