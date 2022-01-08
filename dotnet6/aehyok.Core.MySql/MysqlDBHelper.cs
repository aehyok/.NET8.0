using aehyok.Lib.Config;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.MySql
{
    public class MysqlDBHelper
    {

        // public static readonly string ConnectionStringProfile = ConfigurationManager.ConnectionStrings["OraProfileConnString"].ConnectionString;
        /// <summary>
        /// 连接字符串
        /// </summary>
       // public static string conf = "server=172.18.1.167;Port=3306;uid=root;pwd=root;database=metadata;AllowZeroDateTime=True;ConvertZeroDateTime=True";

        public static string conf = MDEConfig.ConnectionString;

        // 查询连接串
        public static string queryString = MDEConfig.QueryString;
        /// <summary>
        /// 判断数据库是否可以正确连接
        /// </summary>
        /// <returns></returns>
        public static bool IsReady()
        {
            return IsReady(conf);
        }

        /// <summary>
        /// 判断数据库是否可以正确连接
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static bool IsReady(string connectString)
        {
            using (MySqlConnection cn = new MySqlConnection(connectString))
            {
                try
                {
                    cn.Open();
                    cn.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 打开一个mysql连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection OpenConnection()
        {
            MySqlConnection conn = new MySqlConnection(MysqlDBHelper.conf);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// 打开一个mysql连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection OpenConnection(string strcn)
        {
            MySqlConnection conn = new MySqlConnection(strcn);
            conn.Open();
            return conn;
        }
        /// <summary>
        /// 返回首行首列
        /// </summary>
        /// <param name="sqltext"></param>
        /// <returns></returns>
        public static async Task<object> ExecuteScalar(string sqltext)
        {
            using (MySqlConnection conn = new MySqlConnection(conf))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sqltext, conn);
                return await comm.ExecuteScalarAsync();
            }
        }
        /// <summary>
        /// 返回记录数
        /// </summary>
        /// <param name="sqltext"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNoQuery(string sqltext)
        {
            using (MySqlConnection conn = new MySqlConnection(conf))
            {
                conn.Open();
                MySqlCommand comm = new MySqlCommand(sqltext, conn);
                return await comm.ExecuteNonQueryAsync();
            }
        }
        /// <summary>
        /// 返回DataSet
        /// </summary>
        /// <param name="sqltext"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataset(string sqltext)
        {
            using (MySqlConnection conn = new MySqlConnection(conf))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqltext, conf);
                DataSet ds = new DataSet();
                await adapter.FillAsync(ds);
                return ds;
            }
        }
        /// <summary>
        /// 返回dataset 传入sqlparameter
        /// </summary>
        /// <param name="sqltext"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static async Task<DataSet> ExecuteDataset(string sqltext, MySqlParameter[] param)
        {
            using (MySqlConnection conn = new MySqlConnection(conf))
            {
                conn.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(sqltext, conn);
                //adapter.SelectCommand.Connection = conn;
                adapter.SelectCommand.CommandType = CommandType.Text;
                //  adapter.SelectCommand.CommandText = sqltext;
                adapter.SelectCommand.Parameters.AddRange(param);
                DataSet ds = new DataSet();
                await adapter.FillAsync(ds);
                return ds;
            }

        }

        public static async Task<int> ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                    int val = await cmd.ExecuteNonQueryAsync();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                            e.Message, cmdType, cmdText);
                    if (commandParameters != null)
                    {
                        foreach (MySqlParameter _p in commandParameters)
                        {
                            _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                        }
                    }
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
            }
        }


        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] commandParameters)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.Transaction = trans;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (commandParameters != null)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        public static MySqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //Create the command and connection
            MySqlCommand cmd = new MySqlCommand();
            MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                //Execute the query, stating that the connection should close when the resulting datareader has been read
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                               e.Message, cmdType, cmdText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                conn.Close();
                throw e;
            }
        }

        public static MySqlDataReader ExecuteReader(MySqlConnection mySqlConnection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            //Create the command and connection
            MySqlCommand cmd = new MySqlCommand();
            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, mySqlConnection, null, cmdType, cmdText, commandParameters);
                //Execute the query, stating that the connection should close when the resulting datareader has been read
                MySqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                               e.Message, cmdType, cmdText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                //mySqlConnection.Close();
                throw e;
            }
        }

        public static async Task<int> ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = await cmd.ExecuteNonQueryAsync();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                                e.Message, cmdType, cmdText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        public static async Task<object> ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    object val = await cmd.ExecuteScalarAsync();
                    cmd.Parameters.Clear();
                    return val;
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                                    e.Message, cmdType, cmdText);
                    if (commandParameters != null)
                    {
                        foreach (MySqlParameter _p in commandParameters)
                        {
                            _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                        }
                    }
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
            }
        }

        public static async Task<object> ExecuteScalar(MySqlConnection connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
                object val = await cmd.ExecuteScalarAsync();
                cmd.Parameters.Clear();
                return val;
            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                                e.Message, cmdType, cmdText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        /// <summary>
        /// 通过查询语句填充表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static async Task<DataTable> FillDataTable(string connectionString,CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DbDataReader rdr;
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable("ResultTable");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    rdr = await cmd.ExecuteReaderAsync();
                    FillTableByReader(_dt, rdr);
                    rdr.Close();
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                           e.Message, cmdType, cmdText);
                    if (commandParameters != null)
                    {
                        foreach (MySqlParameter _p in commandParameters)
                        {
                            _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                        }
                    }
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    connection.Close();
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return _dt;
        }

        /// <summary>
        /// 通过查询语句填充表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable FillDataTable(MySqlConnection mySqlConnection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            DbDataReader rdr;
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable("ResultTable");
            
            {
                try
                {
                    PrepareCommand(cmd, mySqlConnection, null, cmdType, cmdText, commandParameters);
                    rdr = cmd.ExecuteReader();
                    FillTableByReader(_dt, rdr);
                    rdr.Close();
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                           e.Message, cmdType, cmdText);
                    if (commandParameters != null)
                    {
                        foreach (MySqlParameter _p in commandParameters)
                        {
                            _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                        }
                    }
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw;
                }
                finally
                {
                }
            }
            return _dt;
        }



        /// <summary>
        /// 通过查询语句填充表
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static async Task<DataTable> FillDataTable(MySqlConnection cn, MySqlTransaction txn, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable("ResultTable");

            try
            {

                PrepareCommand(cmd, cn, txn, cmdType, cmdText, commandParameters);
                DbDataReader rdr = await cmd.ExecuteReaderAsync();
                FillTableByReader(_dt, rdr);
                rdr.Close();
            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                       e.Message, cmdType, cmdText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw;
            }

            return _dt;
        }

        /// <summary>
        /// 通过查询语句取数据到DataSet
        /// </summary>
        /// <param name="_selectStr"></param>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public static async Task<DataSet> FillDataSet(string _selectStr, string _tableName)
        {
            DbDataReader rdr;
            DataSet _ds = new DataSet();
            using (MySqlConnection cn = OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_selectStr, cn);
                    DataTable _dt = new DataTable(_tableName);

                    using (rdr = await _cmd.ExecuteReaderAsync())
                    {
                        FillTableByReader(_dt, rdr);
                        _ds.Tables.Add(_dt);
                        _ds.AcceptChanges();
                    }
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
                                    e.Message, _selectStr);

                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }
            return _ds;
        }

        public static async Task<DataSet> FillDataSet(string conn,string _selectStr, string _tableName)
        {
            DbDataReader rdr;
            DataSet _ds = new DataSet();
            using (MySqlConnection cn = OpenConnection(conn))
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_selectStr, cn);
                    DataTable _dt = new DataTable(_tableName);

                    using (rdr = await _cmd.ExecuteReaderAsync())
                    {
                        FillTableByReader(_dt, rdr);
                        _ds.Tables.Add(_dt);
                        _ds.AcceptChanges();
                    }
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
                                    e.Message, _selectStr);

                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }
            return _ds;
        }

        /// <summary>
        /// 通过查询语句取数据到表
        /// </summary>
        /// <param name="_selectStr"></param>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public static async Task<DataTable> Get_Data(string _selectStr, string _tableName)
        {
            DbDataReader rdr;
            DataTable _dt = new DataTable(_tableName);
            using (MySqlConnection cn = OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_selectStr, cn);
                    using (rdr = await _cmd.ExecuteReaderAsync())
                    {
                        FillTableByReader(_dt, rdr);
                    }
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
                                    e.Message, _selectStr);

                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }
            return _dt;
        }



        static public async void FillTableByReader(DataTable _dt, DbDataReader rdr)
        {
            int i;
            DataRow _newrow;
            int _fcount = rdr.FieldCount;

            DataTable schemaTable = rdr.GetSchemaTable();
            List<int> NumberColumnList = new List<int>();

            int index = 0;
            foreach (DataRow myRow in schemaTable.Rows)
            {
                _dt.Columns.Add(myRow["ColumnName"].ToString(), (Type)myRow["DataType"]);
                if ((Type)myRow["DataType"] == typeof(decimal))
                {
                    NumberColumnList.Add(index);
                }
                index++;
            }

            while (await rdr.ReadAsync())
            {
                _newrow = _dt.NewRow();
                for (i = 0; i < _fcount; i++)
                {
                    if (NumberColumnList.Contains(i) && !rdr.IsDBNull(i))
                    {
                        _newrow[i] = rdr.GetDecimal(i);
                    }
                    else
                    {
                        _newrow[i] = rdr[i];
                    }
                }
                _dt.Rows.Add(_newrow);
            }
            _dt.AcceptChanges();
        }


        public static bool UpdateData(MySqlConnection cn, string _cmdStr, DataTable _refData)
        {
            MySqlDataAdapter _da = new MySqlDataAdapter(_cmdStr, cn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(_da);
            _da.Update(_refData);
            return true;
        }



        /// <summary>
        /// 执行存储过程，返回SqlDataReader ( 注意：调用该方法后，一定要对SqlDataReader进行Close )
        /// </summary>
        /// <param name="storedProcName">存储过程名</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static int RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(conf))
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(storedProcName, connection);

                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (MySqlParameter parameter in parameters)
                    {
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        cmd.Parameters.Add(parameter);
                    }

                }
                int ret = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();

                //using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                //{
                //    DataSet ds = new DataSet();
                //    try
                //    {
                //        da.Fill(ds, "ds");
                //        cmd.Parameters.Clear();
                //    }
                //    catch (MySql.Data.MySqlClient.MySqlException ex)
                //    {
                //        throw new Exception(ex.Message);
                //    }
                //    return ds;
                //}

                return ret;
            }

        }


    }
}
