using sun.Infrastructure.Enums;
using sun.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.Query
{
    public class UserTokenQueryDto: PagedQueryModelBase
    {
        /// <summary>
        /// 所属平台
        /// </summary>
        public PlatformType platformType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
