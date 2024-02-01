using sun.Basic.Services;
using sun.Core;
using sun.Core.Domains;
using sun.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace sun.Basic.SeedData
{
    public class RegionSeedData(IServiceScopeFactory scopeFactory) : ISeedData, ITransientDependency
    {
        public int Order => 0;

        public string ConfigPath { get; set; } = null;

        public async Task ApplyAsync(SeedDataTask model)
        {
            using var scope = scopeFactory.CreateScope();
            var regionService = scope.ServiceProvider.GetService<IRegionService>();

            if (!await regionService.ExistsAsync(a => a.ParentId == 0))
            {
                var defaultRegion = new Region
                {
                    Name = "默认区域",
                    Code = "0",
                    ParentId = 0
                };

                await regionService.InsertAsync(defaultRegion);
            }
        }
    }
}
