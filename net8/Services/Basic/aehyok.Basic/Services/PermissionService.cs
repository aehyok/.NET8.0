using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.EntityFramework.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Exceptions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public class PermissionService(DbContext dbContext, IMapper mapper, IMenuService menuService, IRoleService roleService) : ServiceBase<Permission>(dbContext, mapper), IPermissionService, IScopedDependency
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

            var permissions = model.Premission.GroupBy(a => a.MenuId).Select(a => a.FirstOrDefault()).Where(a => a.HasPermission).Select(a => new Permission
            {
                //DataRange = a.DataRange,
                HasPermission = a.HasPermission,
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
            var query = from m in menuService.GetQueryable()
                        join p in this.GetQueryable().Where(a => a.RoleId == roleId) on m.Id equals p.MenuId into pt
                        from pm in pt.DefaultIfEmpty()
                        select new PermissionDto
                        {
                            MenuId = m.Id,
                            MenuName = m.Name,
                            MenuParentId = m.ParentId,
                            MenuType = m.Type,
                            MenuOrder = m.Order,
                            DataRange = pm != null ? pm.DataRange : DataRange.全部,
                            HasPermission = pm != null ? pm.HasPermission : false,
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
    }
}
