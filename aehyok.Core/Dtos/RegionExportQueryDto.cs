using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class RegionExportQueryDto
    {
        /// <summary>
        /// 行政区划 Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 选中项
        /// </summary>
        public long[] Ids { get; set; }
    }
}
