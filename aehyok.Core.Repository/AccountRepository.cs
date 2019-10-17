using aehyok.Core.DataAccess;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Repository
{
    /// <summary>
    /// 账户中心实现
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbAccossor _dbAccossor;
        private readonly ILogger<AccountRepository> _logger;
        public AccountRepository(IDbAccossor dbAccossor, ILogger<AccountRepository> logger)
        {
            this._dbAccossor = dbAccossor;
            this._logger = logger;
        }

        public IDbAccossor DbAccossor => _dbAccossor;

        private const string sql_CheckLogin = "SELECT count(*) FROM lagou WHERE 1=1";
        public bool CheckLogin(string userName, string password)
        {
            _logger.LogError("调用到后台数据库方法");
            return _dbAccossor.DbDefaultConnection.Using((dbConnection) =>
            {
                int count = dbConnection.ExecuteScalar<int>(sql_CheckLogin, new { });
                return count > 0;
            });
        }
    }
}
