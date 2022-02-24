using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    public  class GeekArticle : EntityBase
    {
        /// <summary>
        /// 文章详情
        /// </summary>
        public string? Json { get; set; }    

        public DateTime? CreateTime { get; set; }

        public string? ProductId { get; set; }

        public string? Title { get; set; }

        public string? AuthorName { get; set; }
    }
}
