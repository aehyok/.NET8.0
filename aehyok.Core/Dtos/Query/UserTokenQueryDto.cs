using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos.Query
{
    public class UserTokenQueryDto: PagedQueryModelBase
    {
        /// <summary>
        /// 所属平台
        /// </summary>
        public PlatformType platformType { get; set; }
    }
}
