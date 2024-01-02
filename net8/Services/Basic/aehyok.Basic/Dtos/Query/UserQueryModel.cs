using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Query
{
    public class UserQueryModel : PagedQueryModelBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long? RoleId { get; set; }

        /// <summary>
        /// 区域id
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        public string RegionCode { get; set; }

        /// <summary>
        /// 包含下级
        /// </summary>
        public bool IncludeChilds { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 用户类型 0=全部，1=公众用户，2=工作人员, 网格员 = 3,网格长 = 4,企业管理员 = 5
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 权限code
        /// </summary>
        public string PermissionCode { get; set; }
    }
}
