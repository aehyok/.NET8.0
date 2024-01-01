using aehyok.Basic.Domains;
using aehyok.Basic.Dtos;
using aehyok.EntityFramework.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    public interface IPermissionService: IServiceBase<Permission>
    {
        /// <summary>
        /// 获取角色下拥有的菜单权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<List<PermissionDto>> GetRolePermissionAsync(long roleId);
    }
}
