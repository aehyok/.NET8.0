using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.Dtos
{
    public abstract class DtoBase : DtoBase<long>
    {
        public override long Id { get; set; }
    }

    public abstract class DtoBase<TKey> : Dto<TKey>
    {
    }
}
