using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Query
{
    public class RegionQueryDto
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 父级 Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 是否只查询当前区域下
        /// </summary>
        public bool IsCurrent { get; set; } = false;
    }
}
