using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
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
            CreateMap<CreateDictionaryGroupModel, DictionaryGroup>();

            CreateMap<CreateMenuDto, Menu>();

            CreateMap<CreateRoleDto, Role>();
        }
    }
}
