using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Entities
{
    /// <summary>
    /// 创建数据表实体的基础接口
    /// </summary>
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        /// <summary>
        /// 表实体的主键统一为Id（类型可自定义）
        /// </summary>
        TKey Id { get; set; }
    }
}
