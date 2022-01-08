using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib
{
    /// <summary>
    /// 雪花生成序列ID
    /// </summary>
    public static class Snowflake
    {
        private static readonly Lazy<IdWorker> _instance = new Lazy<IdWorker>(() =>
        {
            long WorkerId = 1;
            long DatacenterId = 4;
            return new IdWorker(WorkerId, DatacenterId);
        });

        public static IdWorker Instance = _instance.Value;

    }
}
