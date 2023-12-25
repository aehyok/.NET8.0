using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Domains
{
    public class System : AuditedEntity
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string Name { get; set; }
    }
}
