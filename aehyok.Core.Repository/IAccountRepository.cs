using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Repository
{
    /// <summary>
    /// 用户接口仓库
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool CheckLogin(string userName, string password);
    }
}
