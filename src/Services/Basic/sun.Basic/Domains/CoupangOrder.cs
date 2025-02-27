using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 电商平台订单
    /// </summary>
    public class CoupangOrder: AuditedEntity
    {
        /// <summary>
        /// 订单号码
        /// </summary>
        public long orderId { get; set; }

        /// <summary>
        /// 订单日期/时间 yyyy-MM-dd'T'HH:mm:ss
        /// </summary>
        public DateTime OrderedAt { get; set; }
        
        /// <summary>
        /// 订购人
        /// </summary>
        public string OrdererName { get; set; }

        /// <summary>
        /// 订购人邮箱
        /// </summary>
        public string OrdererEmail { get; set; }

        public string OrdererSafeNumber { get; set; }

        public string OrdererOrdererNumber { get; set; }

        /// <summary>
        /// 付款完成时的日期/时间
        /// </summary>
        public DateTime PaidAt { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 配送费
        /// </summary>
        public decimal ShippingPrice { get; set; }
    }
}
