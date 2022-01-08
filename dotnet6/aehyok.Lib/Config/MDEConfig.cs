using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.Config
{
    /// <summary>
    /// 元数据引擎配置项
    /// 
    /// </summary>
    public class MDEConfig
    {

        /// <summary>
        /// 数据库连接
        /// </summary>
        public static string ConnectionString { get; set; } = "";

        /// <summary>
        /// 查询的数据库连接
        /// </summary>
        public static string QueryString { get; set; } = "";

        /// <summary>
        /// Redis连接字串
        /// </summary>
        public static string RedisConnectionString { get; set; } = "";

        /// <summary>
        /// 查询所使用的数据库
        /// </summary>
        public static string DbUser { get; set; } = "";


    }
}
