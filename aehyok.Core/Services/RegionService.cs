using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Basic.Services
{
    public class RegionService(DbContext dbContext, IMapper mapper) : ServiceBase<Region>(dbContext, mapper), IRegionService, IScopedDependency
    {
    }
}
