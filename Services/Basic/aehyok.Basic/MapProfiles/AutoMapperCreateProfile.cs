using aehyok.Basic.Domains;
using aehyok.Basic.Dtos.Create;
using AutoMapper;

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
        }
    }
}
