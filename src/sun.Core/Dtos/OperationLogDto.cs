using sun.Infrastructure.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
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

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string RequestJson { get; set; }
    }
}
