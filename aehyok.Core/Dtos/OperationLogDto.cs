using aehyok.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class OperationLogDto : DtoBase
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string OperationContent { get; set; }

        /// <summary>
        /// 操作菜单
        /// </summary>
        public string OperationMenu { get; set; }
    }
}
