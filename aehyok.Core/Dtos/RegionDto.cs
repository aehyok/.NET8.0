using aehyok.Core.Domains;
using aehyok.Infrastructure.Dtos;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Dtos
{
    public class RegionDto : AuditedDtoBase
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
        /// 备注
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }
    }
}
