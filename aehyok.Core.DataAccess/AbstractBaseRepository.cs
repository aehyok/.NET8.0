using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.DataAccess
{
    public abstract class AbstractBaseRepository
    {
        protected internal readonly IDbAccossor dbAccossor;
        protected AbstractBaseRepository(IDbAccossor dbAccossor)
        {
            this.dbAccossor = dbAccossor;
        }
    }
}
