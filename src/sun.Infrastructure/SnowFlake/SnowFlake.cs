using sun.Infrastructure.Options;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Infrastructure.SnowFlake
{
    /// <summary>
    /// 分布式雪花Id生成器
    /// </summary>
    public class SnowFlake
    {
        /// <summary>
        /// 通过静态类只实例化一次IdWorker 否则生成的Id会有重复
        /// </summary>
        private static readonly Lazy<IdWorker> _instance = new(() =>
        {
            var commonOptions = App.Options<CommonOptions>();

            return new IdWorker(commonOptions.WorkerId, commonOptions.DatacenterId);
        });

        public static IdWorker Instance = _instance.Value;
    }
}
