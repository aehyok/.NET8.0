using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.EntityFramework
{
    /// <summary>
    /// 所有的EFCore 的Repository Service 都需要继承此接口,便于通过反射进行注入
    /// </summary>
    public interface IScopedDependency
    {

    }
}
