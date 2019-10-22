using aehyok.Core.Data;
using aehyok.Core.DataBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.IRepository
{
    public interface IRepository:IDependency
    {
        IDbAccossor DbAccossor { get; }
    }
}
