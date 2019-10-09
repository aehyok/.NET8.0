using aehyok.Core.DataAccess;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Repository
{
    public class TestRepository: ITestRepository
    {
        private readonly IDbAccossor _dbAccossor;
        public TestRepository(IDbAccossor dbAccossor)
        {
            this._dbAccossor = dbAccossor;
        }

        public IDbAccossor DbAccossor => _dbAccossor;

        private const string sql_CheckLogin = "SELECT count(*) FROM lagou WHERE 1=1";
        public bool CheckLogin(string userName, string password)
        {
            return _dbAccossor.DbDefaultConnection.Using((dbConnection) =>
            {
                int count = dbConnection.ExecuteScalar<int>(sql_CheckLogin, new { });
                return count > 0;
            });
        }
    }
}
