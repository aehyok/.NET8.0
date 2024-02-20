using sun.Basic.Dtos;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.EntityFrameworkCore.Repository;

namespace sun.Core.Services
{
    public interface IPermissionService: IServiceBase<Permission>
    {
        /// <summary>
        /// 获取角色下拥有的菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<PermissionDto>> GetRolePermissionAsync(long roleId);

        /// <summary>
        /// 修改角色权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task ChangeRolePermissionAsync(ChangeRolePermissionDto model);

        /// <summary>
        /// 获取当前菜单是否拥有接口权限
        /// </summary>
        /// <param name="code">控制器action</param>
        /// <param name="menuCode">当前菜单或者操作的code</param>
        /// <returns></returns>
        Task<bool> JudgeHasPermissionAsync(string code, string menuCode);
    }
}
