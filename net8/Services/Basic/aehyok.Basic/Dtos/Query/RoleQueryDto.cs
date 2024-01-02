using aehyok.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Query
{
    public class RoleQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 角色状态，True 启用 False 禁用
        /// </summary>
        public bool? IsEnable { get; set; }
    }
}
