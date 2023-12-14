using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.Entities
{
    /// <summary>
    /// 数据表实体（主键设置、软删除标识、CreatedAt CreatedBy、UpdatedAt UpdateBy、以及添加用户的关联实体）
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class FullAuditedEntity<TUser> : AuditedEntity where TUser : AuditedEntity, new()
    {
        /// <summary>
        /// 创建人
        /// </summary>
        [ForeignKey(nameof(CreatedBy))]
        public virtual TUser Creator { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ForeignKey(nameof(UpdatedBy))]
        public virtual TUser Modifier { get; set; }
    }

    /// <summary>
    /// 数据表实体（主键可设置类型、软删除标识、CreatedAt CreatedBy、UpdatedAt UpdateBy、以及添加用户的关联实体）
    /// </summary>
    /// <typeparam name="TUser"></typeparam>
    public abstract class FullAuditedEntity<TKey, TUser> : AuditedEntity<TKey> where TUser : AuditedEntity, new() where TKey : struct
    {
        protected FullAuditedEntity(TKey id)
            : base(id)
        {
        }

        /// <summary>
        /// 创建人
        /// </summary>
        [ForeignKey(nameof(CreatedBy))]
        public virtual TUser Creator { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [ForeignKey(nameof(UpdatedBy))]
        public virtual TUser Modifier { get; set; }
    }
}
