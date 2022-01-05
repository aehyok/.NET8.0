using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Base.Models
{
    /// <summary>
    /// 指标查询模型
    /// </summary>
    public class QueryGuidelineModel
    {
        /// <summary>
        /// 指标ID
        /// </summary>
        public string GuideLineId { get; set; }

        /// <summary>
        /// 参数键值对
        /// </summary>
        public Dictionary<string, object> Param { get; set; }

        /// <summary>
        /// 查询关键字
        /// </summary>
        public string FilterWord { get; set; }



    }

    /// <summary>
    /// 指标分页查询
    /// </summary>
    public class QueryGuidelinePageModel : QueryGuidelineModel
    {
        /// <summary>
        /// 当前页 默认为1
        /// </summary>
        public int PageIndex { get; set; } = 1;


        /// <summary>
        /// 每页记录数 默认为15
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// 排序方向 ASC  DESC
        /// </summary>
        public string SortDirection { get; set; }



    }
}
