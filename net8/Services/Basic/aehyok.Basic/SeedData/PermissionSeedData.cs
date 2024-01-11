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
    public class PermissionSeedData(IServiceScopeFactory scopeFactory) : ISeedData, ITransientDependency
    {
        public int Order => 1;

        public string ConfigPath { get; set; } = null;

        public async Task ApplyAsync(SeedDataTask model)
        {
            using var scope = scopeFactory.CreateScope();

            var roleService = scope.ServiceProvider.GetService<IRoleService>();
            var menuService = scope.ServiceProvider.GetService<IMenuService>();
            var permissionService = scope.ServiceProvider.GetService<IPermissionService>();

            // 获取运维角色id
            var role = await roleService.GetAsync(item => item.Code == SystemRoles.ROOT);

            //获取所有菜单
            var menus = await menuService.GetListAsync();
            
            // 获取角色已勾选菜单
            var permissions = await permissionService.GetListAsync(item => item.RoleId == role.Id);

            //遍历所有菜单
            foreach (var menu in menus)
            {
                var isExist = await permissionService.ExistsAsync(item => item.MenuId == menu.Id && item.RoleId == role.Id);

                //如果不存在则添加
                if(!isExist)
                {
                    await permissionService.InsertAsync(new Permission
                    {
                        MenuId = menu.Id,
                        RoleId = role.Id,
                        Remark = string.Empty
                    });
                }
            }

            // 还需要处理删除多余菜单权限（不删的话有脏数据而已）
        }
    }
}
