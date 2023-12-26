using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Dtos
{
    /// <summary>
    /// DTO 基础类
    /// </summary>
    public class Dto
    {
    }

    public abstract class Dto<TKey>: Dto
    {
        public virtual TKey Id { get; set; }
    }
}
