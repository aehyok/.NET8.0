using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework.Entities
{
    /// <summary>
    /// 数据实体基础接口（创建时间、创建人、修改时间、修改人）
    /// </summary>
    public interface IAuditedEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// 创建人id
        /// </summary>
        long? CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        DateTime UpdatedAt { get; set; }

        /// <summary>
        /// 修改人id
        /// </summary>
        long? UpdatedBy { get; set; }
    }
}
