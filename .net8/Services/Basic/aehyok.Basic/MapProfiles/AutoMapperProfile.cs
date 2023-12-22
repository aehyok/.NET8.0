using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
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

            CreateMap<CreateDictionaryGroupModel, DictionaryGroup>();
        }
    }
}
