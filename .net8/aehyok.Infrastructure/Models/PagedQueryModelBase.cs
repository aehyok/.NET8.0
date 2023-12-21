using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Models
{
    public class PagedQueryModelBase
    {
        /// <summary>
        /// 分页大小
        /// </summary>
        public int Limit { get; set; } = 15;

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }
    }
}
