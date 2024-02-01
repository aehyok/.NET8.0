using sun.Basic.Domains;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace sun.Basic.Services
{
    public class OptionsService(DbContext dbContext, IMapper mapper) : ServiceBase<Options>(dbContext, mapper), IOptionsSerivce, IScopedDependency
    {

    }
}
