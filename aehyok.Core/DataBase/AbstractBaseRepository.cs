using Microsoft.Extensions.Logging;
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
        protected internal readonly IDbAccossor _dbAccossor;

        protected internal readonly ILogger<AbstractBaseRepository> _logger;

        public AbstractBaseRepository(IDbAccossor dbAccossor, ILogger<AbstractBaseRepository> logger)
        {
            this._dbAccossor = dbAccossor;
            this._logger = logger;
        }
    }
}
