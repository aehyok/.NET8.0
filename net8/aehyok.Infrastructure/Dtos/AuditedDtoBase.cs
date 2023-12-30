using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Dtos
{
    public abstract class AuditedDtoBase : AuditedDtoBase<long>
    {
    }

    public abstract class AuditedDtoBase<TKey> : DtoBase<TKey>
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建人id
        /// </summary>
        public long CreatedBy { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string CreatedUser { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改人id
        /// </summary>
        public long UpdatedBy { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdatedUser { get; set; }
    }
}
