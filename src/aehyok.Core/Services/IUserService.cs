using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Services
{
    public interface IUserService: IServiceBase<User>
    {
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task ResetPasswordAsync(long id);

        /// <summary>
        /// 导入用户列表
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userType"></param>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        Task<dynamic> ImportAsync(string url, UserType userType, long operatorId);
    }
}
