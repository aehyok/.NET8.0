using aehyok.Basic.Domains;
using aehyok.EntityFrameworkCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Services
{
    /// <summary>
    /// 用户角色服务
    /// </summary>
    public interface IUserRoleService: IServiceBase<UserRole>
    {
        /// <summary>
        /// 获取用户默认角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<UserRole> GetUserDefaultRole(long userId);
    }
}
