using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.DataBase
{
    /// <summary>
    /// 抽象库
    /// </summary>
    public abstract class AbstractBaseRepository
    {
        protected internal readonly IDbAccossor dbAccossor;
        protected AbstractBaseRepository(IDbAccossor dbAccossor)
        {
            this.dbAccossor = dbAccossor;
        }
    }
}
