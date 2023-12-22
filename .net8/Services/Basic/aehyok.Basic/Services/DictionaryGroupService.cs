using aehyok.Basic.Domains;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class DictionaryGroupService : ServiceBase<DictionaryGroup>, IDictionaryGroupService, IScopedDependency
    {
        public DictionaryGroupService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
