using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.Core.Domains;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace aehyok.Basic.SeedData
{
    /// <summary>
    /// 角色初始化
    /// </summary>
    public class RoleSeedData(IServiceScopeFactory serviceScopeFactory) : ISeedData, ITransientDependency
    {
        public int Order => 1;

        public string ConfigPath { get; set; } = null;

        public async Task ApplyAsync(SeedDataTask model)
        {
            var roles = new Role[]
            {
                new Role{ Name = "运维管理员", Code = SystemRoles.ROOT, Remark ="", Order = 10, IsSystem = true },

            };

            using var scope = serviceScopeFactory.CreateScope();
            var roleService = scope.ServiceProvider.GetService<IRoleService>();

            foreach (var role in roles)
            {
                if (await roleService.ExistsAsync(a => a.Code == role.Code))
                {
                    continue;
                }

                await roleService.InsertAsync(role);
            }
        }
    }
}
