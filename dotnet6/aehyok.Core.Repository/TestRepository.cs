using aehyok.Core.Config;
using aehyok.Core.IRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Core.Repository
{
    public class TestRepository : ITestRepository
    {
        private const string SQL_WriteSystemLog = @"insert into xt_systemlog  (ID,CZSJ,LOGTYPE,LOGTEXT) values  (@ID,now(),@LOGTYPE,@LOGTEXT) ";
        public string GetTest()
        {
            string _msg = "tttttttttttttttttttt";
            string _type = "error";
            if (_msg.Length > 3700) _msg = _msg.Substring(0, 3700);
            using (MySqlConnection cn = new MySqlConnection(ConfigInitialize.ConnectionString))
            {
                cn.Open();
                MySqlTransaction _txn = cn.BeginTransaction();
                MySqlCommand comm = new MySqlCommand(SQL_WriteSystemLog, cn);   //注释：立即提交日志，方便查询。
                comm.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                comm.Parameters.Add(new MySqlParameter("@LOGTYPE", _type));
                comm.Parameters.Add(new MySqlParameter("@LOGTEXT", _msg));
                comm.ExecuteScalar();
                _txn.Commit();
                cn.Close();
            }
            return "OK";
        }
    }
}
