using aehyok.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.IRepository
{
    /// <summary>
    /// 用户接口仓库
    /// </summary>
    public interface IAccountRepository: IDependency
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
