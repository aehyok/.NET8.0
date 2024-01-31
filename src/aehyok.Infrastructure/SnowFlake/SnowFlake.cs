using aehyok.Infrastructure.Options;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.SnowFlake
{
    /// <summary>
    /// 分布式雪花Id生成器
    /// </summary>
    public class SnowFlake
    {
        private static readonly Lazy<IdWorker> _instance = new(() =>
        {
            var commonOptions = App.Options<CommonOptions>();

            long WorkerId = 1;
            long DatacenterId = 0;
            return new IdWorker(WorkerId, DatacenterId);
        });

        public static IdWorker Instance = _instance.Value;
    }
}
