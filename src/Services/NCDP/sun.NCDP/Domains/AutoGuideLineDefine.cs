using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    /// <summary>
    /// form表单自动生成sql的指标定义表
    /// </summary>
    public class AutoGuideLineDefine : AuditedEntity
    {
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指标主题
        /// </summary>
        public string zbzt { get; set; }

        /// <summary>
        /// 指标算法
        /// </summary>
        public string Algorithm { get; set; }

        /// <summary>
        /// 指标元数据
        /// </summary>
        public string Metadata { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 指标查询算法 指标查询的SELECT语句
        /// </summary>
        public string SelectAlgorithm { get; set; }

        /// <summary>
        /// 明细_指标元数据
        /// </summary>
        public string jsmx_zbmeta { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 指标说明
        /// </summary>
        public string Descrption { get; set; }

        /// <summary>
        /// MD5值
        /// </summary>
        public string Md5 { get; set; }
    }
}
