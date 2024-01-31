using aehyok.Core.Domains;
using aehyok.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos.Query
{
    public class TemplateQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public TemplateContentType? ContentType { get; set; }
    }
}
