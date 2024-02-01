using sun.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.EventData
{
    /// <summary>
    /// 操作日志事件传输数据
    /// </summary>
    public class OperationLogEventData: EventBase
    {
        /// <summary>
        /// 菜单Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 传递参数
        /// </summary>
        public string Json { get; set; }

        /// <summary>
        /// ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }
    }
}
