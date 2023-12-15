using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    /// <summary>
    /// 配置项
    /// </summary>
    public class Options : Entity
    {
        /// <summary>
        /// 键
        /// </summary>
        [MaxLength(1024)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column(TypeName = "text")]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }
    }
}
