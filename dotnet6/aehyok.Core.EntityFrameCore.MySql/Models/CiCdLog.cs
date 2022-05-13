using aehyok.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// CiCdLog日志记录
    /// </summary>
    public  class CiCdLog: EntityBase
    {
        /// <summary>
        /// 所属项目
        /// </summary>
        public string? Project { get; set; }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// 写入时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        public string? Type { get; set; }
    }
}
