using aehyok.Basic.Domains;
using aehyok.Basic.Dtos.Create;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.MapProfiles
{
    /// <summary>
    /// 新增和修改数据的映射文件
    /// </summary>
    public class AutoMapperCreateProfile: Profile
    {
        public AutoMapperCreateProfile()
        {
            CreateMap<CreateDictionaryGroupDto, DictionaryGroup>();

            CreateMap<CreateDictionaryItemDto, DictionaryItem>();

            CreateMap<CreateMenuDto, Menu>();

            CreateMap<CreateRoleDto, Role>();

            CreateMap<CreateRegionDto,  Region>();
            
            CreateMap<CreateUserDto, User>()
                .ForMember(a => a.Roles, a => a.Ignore());

            CreateMap<CreateUserRoleDto, UserRole>();
        }
    }
}
