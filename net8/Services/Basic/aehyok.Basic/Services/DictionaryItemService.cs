using aehyok.Basic.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class DictionaryItemService : ServiceBase<DictionaryItem>, IDictionaryItemService,IScopedDependency
    {
        public DictionaryItemService(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
