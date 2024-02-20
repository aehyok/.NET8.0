using sun.Basic.Dtos;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.Services;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace sun.Basic.Services
{
    public class PermissionService(DbContext dbContext, IMapper mapper, IMenuService menuService, IServiceBase<ApiResource> apiResourceService, IServiceBase<MenuResource> menuResourceService, IRoleService roleService) : ServiceBase<Permission>(dbContext, mapper), IPermissionService, IScopedDependency
    {
        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task ChangeRolePermissionAsync(ChangeRolePermissionDto model)
        {
            var role = await roleService.GetAsync(a => a.Id == model.RoleId);
            if (role is null)
            {
                throw new ErrorCodeException(-1, "要授权的角色不存在");
            }

            // 删除原有数据
            await this.BatchDeleteAsync(a => a.RoleId == model.RoleId);

            var permissions = model.Premission.Select(a => new Permission
            {
                MenuId = a.MenuId,
                RoleId = model.RoleId,
                Remark = string.Empty
            });

            await this.InsertAsync(permissions);
        }

        /// <summary>
        /// 获取对象菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<List<PermissionDto>> GetRolePermissionAsync(long roleId)
        {
            var role = await roleService.GetByIdAsync(roleId);
            if (role is null)
            {
                throw new ErrorCodeException(-1, "角色不存在");
            }

            var query = from m in menuService.GetQueryable().Where(a => !a.IsDeleted && a.PlatformType == role.PlatformType)
                        join p in this.GetQueryable().Where(a => a.RoleId == roleId && !a.IsDeleted) on m.Id equals p.MenuId into pt
                        from pm in pt.DefaultIfEmpty()
                        select new PermissionDto
                        {
                            MenuId = m.Id,
                            MenuName = m.Name,
                            MenuParentId = m.ParentId,
                            MenuType = m.Type,
                            MenuOrder = m.Order,
                            HasPermission = pm != null ? true : false,
                            RoleId = pm != null ? pm.RoleId : roleId,
                            Id = pm != null ? pm.Id : 0,
                            MenuUrl = m.Url
                        };

            var permissions = await query.ToListAsync();

            Func<long, bool, List<PermissionDto>> getChildren = null;

            getChildren = (parentId, operate) =>
            {
                if (operate)
                {
                    return permissions.Where(a => a.MenuParentId == parentId && a.MenuType == MenuType.操作).OrderBy(a => a.MenuOrder).ToList();
                }

                return permissions.Where(a => a.MenuParentId == parentId && a.MenuType != MenuType.操作).OrderBy(a => a.MenuOrder).Select(a =>
                {
                    a.Children = getChildren(a.MenuId, false);
                    a.Operations = getChildren(a.MenuId, true);

                    // 因为 Arco.Design Table 数据中包含 Children 时，就会默认显示 Tree , 所以当下级数据为空时返回 Null，
                    if (a.Children.Count == 0)
                    {
                        a.Children = null;
                    }

                    return a;
                }).ToList();
            };

            return getChildren(0, false);
        }

        public async Task<bool> JudgeHasPermissionAsync(string code, string menuCode)
        {
            var menu = await menuService.GetAsync(a => a.Code == menuCode);

            var resource = await apiResourceService.GetAsync(a => a.Code == code);

            return await menuResourceService.ExistsAsync(a => a.MenuId == menu.Id && a.ApiResourceId == resource.Id);
        }
    }
}
