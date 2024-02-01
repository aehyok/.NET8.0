using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Infrastructure.Options
{
    /// <summary>
    /// AppSettings.json 中所有根节点配置信息
    /// </summary>
    public class CommonOptions : IOptions
    {
        public string SectionName => "";

        /// <summary>
        /// 外网访问地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 主机编号（用于雪花 Id 生成）
        /// </summary>
        public long WorkerId { get; set; }

        /// <summary>
        /// 数据中心编号（用于雪花 Id 生成）
        /// </summary>
        public long DatacenterId { get; set; }

        /// <summary>
        /// FFmpeg 可执行文件位置
        /// </summary>
        public string FFmpeg { get; set; }

        /// <summary>
        /// 显示错误详情
        /// </summary>
        public bool ShowStackTrace { get; set; } = false;
    }
}
