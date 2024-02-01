using sun.Core.Domains;
using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure.Enums;

namespace sun.Core.Services
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

        /// <summary>
        /// 获取用户下所有的角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="platformType"></param>
        /// <returns></returns>
        Task<List<UserRole>> GetUserRoles(long userId, PlatformType platformType);

        /// <summary>
        /// 修改用户默认角色
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <param name="userId"></param>
        /// <param name="platformType"></param>
        /// <returns></returns>
        Task ChangeDefaultRoleAsync(long userRoleId, long userId, PlatformType platformType);
    }
}
