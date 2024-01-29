using aehyok.Basic.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Basic.Services
{
    public class OptionsService(DbContext dbContext, IMapper mapper) : ServiceBase<Options>(dbContext, mapper), IOptionsSerivce, IScopedDependency
    {

    }
}
