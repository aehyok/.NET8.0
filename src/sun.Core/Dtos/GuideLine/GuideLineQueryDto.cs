using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.GuideLine
{
    public class GuideLineQueryDto
    {
        /// <summary>
        /// 指标ID
        /// </summary>
        public string GuideLineId { get; set; }

        /// <summary>
        /// 参数键值对
        /// </summary>
        [DataMember]
        public Dictionary<string, object> Parameters { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 是否包含配置
        /// </summary>
        public bool IsContainConfig { get; set; } = false;

        /// <summary>
        /// 区域Id
        /// </summary>
        public long RegionId { get; set; }
    }

    public class GuideLinePageQueryDto: GuideLineQueryDto
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 分页大小
        /// </summary>
        public int Limit { get; set; } = 10;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 排序方向 ASC  DESC
        /// </summary>
        public string SortDirection { get; set; }
    }

    public class GuideLineExportQueryDto: GuideLineQueryDto
    {
        /// <summary>
        /// 导出文件名称
        /// </summary>
        public string ExportFileName { get; set; }
    }
}
