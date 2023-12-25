using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Domains
{
    /// <summary>
    /// 系统勾选区域Id
    /// </summary>
    public class SystemRegion: FullAuditedEntity<User>
    {
        /// <summary>
        /// 系统Id
        /// </summary>
        public long SystemId { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public long RegionId { get; set; }

        public virtual System System { get; set; }

        public virtual Region Region { get; set; }
    }
}
