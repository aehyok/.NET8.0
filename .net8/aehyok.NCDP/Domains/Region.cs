using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    /// <summary>
    /// 行政区域
    /// </summary>
    public class Region : AuditedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        [MaxLength(64)]
        public string ShortName { get; set; } = string.Empty;

        /// <summary>
        /// 别名
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 父级编号，无父级为 0
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        public RegionLevel Level { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// Id 序列
        /// </summary>
        [MaxLength(250)]
        public string IdSequences { get; set; }
    }

    /// <summary>
    /// 区域等级
    /// </summary>
    public enum RegionLevel
    {
        省 = 0,
        市 = 1,
        区县 = 2,
        乡镇 = 3,
        行政村 = 4,
        自然村 = 5,
        国家 = 9
    }
}
