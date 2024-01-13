using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace aehyok.Basic.Services
{
    public class RegionService(DbContext dbContext, IMapper mapper) : ServiceBase<Region>(dbContext, mapper), IRegionService, IScopedDependency
    {
        public override async Task<Region> InsertAsync(Region entity, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(entity.IdSequences))
            {
                if (entity.ParentId == 0)
                {
                    entity.IdSequences = $".{entity.Id}.";
                }
                else
                {
                    var parent = await this.GetAsync(a => a.Id == entity.ParentId);
                    if (parent != null)
                    {
                        entity.IdSequences = $"{parent.IdSequences}{entity.Id}.";
                        if (parent.Level == RegionLevel.自然村)
                        {
                            throw new ErrorCodeException(-1, "自然村不能创建下级行政区划");
                        }

                        entity.Level = parent.Level + 1;
                    }
                }
            }

            var exists = await this.GetAsync(a => a.Code == entity.Code);
            if (exists != null)
            {
                throw new ErrorCodeException(-1, $"行政区划代码【{entity.Code}】已存在");
            }

            await base.InsertAsync(entity, cancellationToken);

            return entity;
        }
    }
}
