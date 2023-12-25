using aehyok.Basic.Domains;
using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    /// <summary>
    /// 自动化任务表
    /// </summary>
    public class AutoTask: FullAuditedEntity<User>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
