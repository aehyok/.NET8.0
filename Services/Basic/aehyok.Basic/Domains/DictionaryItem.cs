using aehyok.Core.Domains;
using aehyok.EntityFrameworkCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Basic.Domains
{
    /// <summary>
    /// 字典项
    /// </summary>
    public class DictionaryItem : FullAuditedEntity<User>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        [MaxLength(64)]
        public string Code { get; set; }

        /// <summary>
        /// 字典分组
        /// </summary>
        public long DictionaryGroupId { get; set; }

        /// <summary>
        /// 父级字典编号
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 是否可见
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// 系统字典，应无法编辑修改
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 字典分组
        /// </summary>
        public virtual DictionaryGroup DictionaryGroup { get; set; }

        /// <summary>
        /// 父级字典
        /// </summary>
        public virtual DictionaryItem Parent { get; set; }

        /// <summary>
        /// 子级字典
        /// </summary>
        public virtual List<DictionaryItem> Children { get; set; }
    }
}
