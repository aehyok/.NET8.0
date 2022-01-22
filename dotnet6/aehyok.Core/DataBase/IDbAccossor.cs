using aehyok.Base;
using aehyok.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.DataBase
{
    /// <summary>
    /// 数据库访问中心
    /// </summary>
    public interface IDbAccossor: IDependency
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        IDbConnection DbDefaultConnection
        {
            get;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="configConnectionName">配置文件连接字符名称</param>
        /// <returns></returns>
        IDbConnection GetConnection(string configConnectionName);

    }
}
