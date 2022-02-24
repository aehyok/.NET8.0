using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public  class GeekProduct : EntityBase
    {
        /// <summary>
        /// 文章详情
        /// </summary>
        public string? Json { get; set; }

        public string? Title { get; set; }
    }
}
