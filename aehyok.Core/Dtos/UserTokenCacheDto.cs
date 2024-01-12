using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class UserTokenCacheDto: UserTokenDto
    {
        /// <summary>
        /// UserToken Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 人口信息 Id
        /// </summary>
        public long PopulationId { get; set; }

        /// <summary>
        /// 用户所有角色代码
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// 当前角色区域 Id
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 当前角色
        /// </summary>
        public long RoleId { get; set; }
    }
}
