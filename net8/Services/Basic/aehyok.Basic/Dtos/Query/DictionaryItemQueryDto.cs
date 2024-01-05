using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Query
{
    /// <summary>
    /// 字典项查询
    /// </summary>
    public class DictionaryItemQueryDto
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 字典分组编号
        /// </summary>
        public long DictionaryGroupId { get; set; }

        /// <summary>
        /// 字典分组 Key
        /// </summary>
        public string GroupCode { get; set; }
    }
}
