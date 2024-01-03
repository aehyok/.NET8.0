using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Dtos.Create
{
    public class CreateUserRoleDto
    {
        /// <summary>
        /// 角色 Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 区域 Id
        /// </summary>
        public long RegionId { get; set; }
    }
}
