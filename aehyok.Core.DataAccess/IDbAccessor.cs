using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.DataAccess
{
    /// <summary>
    /// 数据库访问者
    /// </summary>
    public interface IDbAccossor
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
