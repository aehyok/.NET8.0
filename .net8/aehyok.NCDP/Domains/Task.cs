using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    public class Task: Entity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
