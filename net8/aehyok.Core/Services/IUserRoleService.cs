using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;

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
