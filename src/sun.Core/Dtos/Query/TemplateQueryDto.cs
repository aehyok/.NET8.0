using sun.Core.Domains;
using sun.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.Query
{
    public class TemplateQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 内容类型
        /// </summary>
        public TemplateContentType? ContentType { get; set; }
    }
}
