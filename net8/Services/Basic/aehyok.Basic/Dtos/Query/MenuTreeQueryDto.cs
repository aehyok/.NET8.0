using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Query
{
    public class MenuTreeQueryDto
    {
        /// <summary>
        /// 父级 Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public string ParentCode { get; set; }

        /// <summary>
        /// 包含下级
        /// </summary>
        public bool IncludeChilds { get; set; }
    }
}
