using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos.Query
{
    public class FileQueryDto: PagedQueryModelBase
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
    }
}
