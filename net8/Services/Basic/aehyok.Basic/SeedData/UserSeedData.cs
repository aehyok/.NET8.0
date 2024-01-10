using aehyok.Basic.Domains;
using aehyok.Basic.Services;
using aehyok.Core;
using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.SeedData
{
    public class UserSeedData(IServiceScopeFactory scopeFactory) : ISeedData, ITransientDependency
    {
        public int Order => 3;

        public string ConfigPath { get; set; } = null;

        public async Task ApplyAsync(SeedDataTask model)
        {
            using var scope = scopeFactory.CreateScope();
            var userService = scope.ServiceProvider.GetService<IUserService>();

            if (!await userService.ExistsAsync(a => a.UserName == "root"))
            {
                var rootUser = new User
                {
                    UserName = "root",
                    Gender = Gender.未知,
                    Mobile = "18888888888",
                    NickName = "",
                    RealName = "运维管理员",
                    Password = "Long2024!",
                };

                var roleService = scope.ServiceProvider.GetService<IRoleService>();

                var rootRole = await roleService.GetAsync(a => a.Code == SystemRoles.ROOT);

                if (rootRole is null)
                {
                    throw new Exception("运维管理员角色不存在，请先创建角色信息");
                }

                var regionService = scope.ServiceProvider.GetService<IRegionService>();

                var region = regionService.GetAsync(a => a.ParentId == 0);
                if (region is null)
                {
                    throw new Exception("请先创建行政区域数据");
                }

                rootUser.UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        RegionId = region.Id,
                        UserId = rootUser.Id,
                        RoleId = rootRole.Id,
                        IsDefault = true
                    }
                };

                await userService.InsertAsync(rootUser);
            }
        }
    }
}
