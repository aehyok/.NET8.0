using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.MySql
{
    public class MysqlLogWriter
    {
        private const string SQL_WriteSystemLog = @"insert into xt_systemlog  (ID,CZSJ,LOGTYPE,LOGTEXT) values  (@ID,now(),@LOGTYPE,@LOGTEXT) ";
        /// <summary>
        /// 写系统日志
        /// </summary>
        /// <param name="_msg">系统日志信息</param>
        /// <param name="_type">类型　　INFO:信息　ERROR:错误</param>
        /// <returns></returns>
        static public bool WriteSystemLog(string _msg, string _type)
        {
            if (_msg.Length > 3700) _msg = _msg.Substring(0, 3700);
            using (MySqlConnection cn = new MySqlConnection(MysqlDBHelper.conf))
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
            return true;
        }

        private const string SQL_WriteQueryLog = @"insert into metadata.md_querylog (ID,SJ,USETIME,QUERY_STR,LX,YHID)
                                                   values (@ID,now(),@USETIME,@QUERY_STR,@LX,@YHID)";
        public static bool WriteQueryLog(string _sqlStr, int _userTime,string _lx)
        {
            string UserID = "0000";
            using (MySqlConnection cn = new MySqlConnection(MysqlDBHelper.conf))
            {
                cn.Open();
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {                    
                    MySqlCommand _cmd = new MySqlCommand(SQL_WriteQueryLog, cn);
                    _cmd.CommandType = CommandType.Text;
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                    _cmd.Parameters.Add(new MySqlParameter("@USETIME", Convert.ToInt32(_userTime)));
                    _cmd.Parameters.Add(new MySqlParameter("@QUERY_STR", (_sqlStr.Length > 2000) ? _sqlStr.Substring(0, 2000) : _sqlStr));
                    _cmd.Parameters.Add(new MySqlParameter("@LX", _lx));
                    _cmd.Parameters.Add(new MySqlParameter("@YHID", decimal.Parse(UserID)));
                    _cmd.ExecuteNonQuery();
                    _txn.Commit();
                    cn.Close();
                }
                catch(Exception ex)
                {
                    _txn.Rollback();            
                    cn.Close();
                    throw ex;
                }
            }
            return true;
        }


        private const string SQL_WriteUserLog = @"
            insert into xt_userlog (ID,YHID,CZSJ,CZLX,CZXXNR,FROMIP,SYSTEMID,RESULTTYPE,FROMHOST) 
            values(@ID,@YHID,now(),@CZLX,@CZXXNR,@FROMIP,@SYSTEMID,@RESULTTYPE,@FROMHOST) ";

        /// <summary>
        /// 写用户操作日志
        /// </summary>
        /// <param name="_yhid">用户ID</param>
        /// <param name="_czlx">操作类型</param>
        /// <param name="_cxnr">日志内容</param>
        /// <param name="_resulttype">操作结果类型　0.未知　1.成功　　2.失败　</param>
        /// <param name="_ipaddr">客户端IP地址</param>
        /// <param name="_hostName">客户端主机名称</param>
        /// <param name="_systemID">记录日志的系统ID</param>
        /// <returns></returns>
        static public bool WriteUserLog(decimal _yhid, string _czlx, string _cxnr, decimal _resulttype, string _ipaddr, string _hostName, string _systemID)
        {
            if (_cxnr.Length > 3700) _cxnr = _cxnr.Substring(0, 3700);
            if (_czlx.Length > 70) _czlx = _czlx.Substring(0, 70);
            using (MySqlConnection cn = new MySqlConnection(MysqlDBHelper.conf))
            {
                cn.Open();

                MySqlCommand comm = new MySqlCommand(SQL_WriteUserLog, cn);
                comm.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                comm.Parameters.Add(new MySqlParameter("@YHID", _yhid));
                comm.Parameters.Add(new MySqlParameter("@CZLX", _czlx));
                comm.Parameters.Add(new MySqlParameter("@CZXXNR", _cxnr));
                comm.Parameters.Add(new MySqlParameter("@FROMIP", _ipaddr));
                comm.Parameters.Add(new MySqlParameter("@SYSTEMID", _systemID));
                comm.Parameters.Add(new MySqlParameter("@RESULTTYPE", _resulttype));
                comm.Parameters.Add(new MySqlParameter("@FROMHOST", _hostName));
                comm.ExecuteScalar();

                cn.Close();
            }
            return true;
        }


        public static int testpro()
        {

            try
            {
                //MySqlParameter[] parameters = {
                //    new MySqlParameter("@id", MySqlDbType.Decimal),
                //    new MySqlParameter("@cnt", MySqlDbType.Decimal),

                //    };
                //parameters[0].Value = 23423;
                //parameters[0].Direction = ParameterDirection.Input;
                //parameters[1].Value = null;
                //parameters[1].Direction = ParameterDirection.Output;
                //var t = MysqlDBHelper.RunProcedure("testpro", parameters);

                //return 1;



                using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
                {
                    MySqlCommand myComm = new MySqlCommand("testpro", cn);
                    myComm.CommandType = CommandType.StoredProcedure;
                    var t = 0;

                    MySqlParameter myParameter;
                    myParameter = new MySqlParameter("@id", MySqlDbType.Decimal);
                    myParameter.Value = 233;
                    myParameter.Direction = ParameterDirection.Input;
                    myComm.Parameters.Add(myParameter);

                    MySqlParameter par;
                    par = new MySqlParameter("@cnt", MySqlDbType.Decimal);
                    par.Value = null;
                    par.Direction = ParameterDirection.Output;
                    myComm.Parameters.Add(par);
                    myComm.ExecuteNonQuery();

                    var ret = par.Value;


                    return 0;
                }

            }
            catch (Exception ex)
            {
                MysqlLogWriter.WriteSystemLog(ex.Message, "ERROR");
                return 0;
            }

        }



    }
}
