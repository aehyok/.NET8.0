using aehyok.Basic.Dtos;
using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.EntityFrameworkCore.Repository;

namespace aehyok.Core.Services
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
    }
}
