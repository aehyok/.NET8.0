using aehyok.Core.Config;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace aehyok.Core.MySqlDataAccessor
{
    /// <summary>
    /// Oracle数据库访问通用类
    /// </summary>
    public abstract class SqlHelper
    {
        /// <summary>
        /// 判断数据库是否可以正确连接
        /// </summary>
        /// <returns></returns>
        public static bool IsReady()
        {
            return IsReady(GetConnectionStr());
        }

        /// <summary>
        /// 判断数据库是否可以正确连接
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public static bool IsReady(string connectString)
        {
            using MySqlConnection cn = new MySqlConnection(connectString);
            try
            {
                cn.Open();
                cn.Close();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 数据库连接字符串缓存
        /// </summary>
        public static Dictionary<string, string> connStrCache = new Dictionary<string, string>();
        /// <summary>
        /// 获取ORACLE链接字符串
        /// </summary>
        /// <returns></returns>
        public static string GetConnectionStr(string connStrName = null)
        {
            if (connStrCache.ContainsKey(connStrName))
            {
                return connStrCache[connStrName];
            }
            else
            {
                //string ConnectionStringProfile = ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
                string ConnectionStringProfile = ConfigInitialize.ConnectionString; // ConfigurationManager.ConnectionStrings[connStrName].ConnectionString;
                connStrCache.Add(connStrName, ConnectionStringProfile);
                return ConnectionStringProfile;
            }
        }
        /// <summary>
        /// 打开一个ORACLE连接
        /// </summary>
        /// <returns></returns>
        public static MySqlConnection OpenConnection(string connectionStringName = null)
        {
            MySqlConnection conn = new MySqlConnection(GetConnectionStr(connectionStringName));
            conn.Open();
            return conn;
        }

        //Create a hashtable for the parameter cached
        private static readonly Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a database query which does not include a select
        /// </summary>
        /// <param name="connectionString">Connection string to database</param>
        /// <param name="cmdType">Command type either stored procedure or SQL</param>
        /// <param name="cmdText">Acutall SQL Command</param>
        /// <param name="commandParameters">Parameters to bind to the command</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            // Create a new Oracle command
            MySqlCommand cmd = new MySqlCommand();

            //Create a connection
            using MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                //Prepare the command
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);

                //Execute the command
                int val = cmd.ExecuteNonQuery();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        /// <summary>
        /// Execute an MySqlCommand (that returns no resultset) against an existing database transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(trans, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter(":prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing database transaction</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlTransaction trans, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }

        }

        /// <summary>
        /// Execute an MySqlCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connection">an existing database connection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(MySqlConnection connection, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        /// <summary>
        /// Execute a select query that will return a result set
        /// 此处要修改，存在的缺陷是打开的MySqlConnection可能未被关闭。
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                conn.Close();
                throw e;
            }
        }

        /// <summary>
        /// Execute a select query that will return a result set
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns></returns>
        public static MySqlDataReader ExecuteReader(MySqlConnection cn, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {

            //Create the command and connection
            MySqlCommand cmd = new MySqlCommand();

            try
            {
                //Prepare the command to execute
                PrepareCommand(cmd, cn, null, cmdType, cmdText, commandParameters);

                //Execute the query, stating that the connection should close when the resulting datareader has been read
                MySqlDataReader rdr = cmd.ExecuteReader();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                //If an error occurs close the connection as the reader will not be used and we expect it to close the connection
                throw e;
            }
        }

        /// <summary>
        /// Execute an MySqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a SqlConnection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlCommand cmd = new MySqlCommand();

            using MySqlConnection conn = new MySqlConnection(connectionString);
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        ///	<summary>
        ///	Execute	a MySqlCommand (that returns a 1x1 resultset)	against	the	specified SqlTransaction
        ///	using the provided parameters.
        ///	</summary>
        ///	<param name="transaction">A	valid SqlTransaction</param>
        ///	<param name="commandType">The CommandType (stored procedure, text, etc.)</param>
        ///	<param name="commandText">The stored procedure name	or PL/SQL command</param>
        ///	<param name="commandParameters">An array of	OracleParamters used to execute the command</param>
        ///	<returns>An	object containing the value	in the 1x1 resultset generated by the command</returns>
        public static object ExecuteScalar(MySqlTransaction transaction, CommandType commandType, string commandText, params MySqlParameter[] commandParameters)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");
            if (transaction != null && transaction.Connection == null)
                throw new ArgumentException("The transaction was rollbacked	or commited, please	provide	an open	transaction.", "transaction");

            // Create a	command	and	prepare	it for execution
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, transaction.Connection, transaction, commandType, commandText, commandParameters);

                // Execute the command & return	the	results
                object retval = cmd.ExecuteScalar();

                // Detach the SqlParameters	from the command object, so	they can be	used again
                cmd.Parameters.Clear();
                return retval;
            }
            catch (Exception e)
            {
                string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n类型:{1}\n查询语句为:{2}\n参数:",
                                e.Message, commandType, commandText);
                if (commandParameters != null)
                {
                    foreach (MySqlParameter _p in commandParameters)
                    {
                        _errmsg += string.Format("{0}={1}\n", _p.ParameterName, _p.Value.ToString());
                    }
                }
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        /// <summary>
        /// Execute an MySqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(conn, CommandType.StoredProcedure, "PublishOrders", new MySqlParameter(":prodid", 24));
        /// </remarks>
        /// <param name="connectionString">an existing database connection</param>
        /// <param name="cmdType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="cmdText">the stored procedure name or PL/SQL command</param>
        /// <param name="commandParameters">an array of OracleParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(MySqlConnection connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();

                PrepareCommand(cmd, connectionString, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw e;
            }
        }

        /// <summary>
        /// Add a set of parameters to the cached
        /// </summary>
        /// <param name="cacheKey">Key value to look up the parameters</param>
        /// <param name="commandParameters">Actual parameters to cached</param>
        public static void CacheParameters(string cacheKey, params MySqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Fetch parameters from the cache
        /// </summary>
        /// <param name="cacheKey">Key to look up the parameters</param>
        /// <returns></returns>
        public static MySqlParameter[] GetCachedParameters(string cacheKey)
        {
            MySqlParameter[] cachedParms = (MySqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            // If the parameters are in the cache
            MySqlParameter[] clonedParms = new MySqlParameter[cachedParms.Length];

            // return a copy of the parameters
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (MySqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(MySqlCommand cmd, MySqlConnection conn, MySqlTransaction trans, CommandType cmdType, string cmdText, MySqlParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            //if (trans != null)
            //        cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (MySqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static string OraBit(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static bool OraBool(string value)
        {
            if (value.Equals("Y"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 通过查询语句取数据到DataSet
        /// </summary>
        /// <param name="_selectStr"></param>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public static DataSet FillDataSet(string _selectStr, string _tableName)
        {
            MySqlDataReader rdr;
            DataSet _ds = new DataSet();
            using (MySqlConnection cn = OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_selectStr, cn);
                    DataTable _dt = new DataTable(_tableName);

                    using (rdr = _cmd.ExecuteReader())
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

                    SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
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
        /// 通过查询语句填充表
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="commandParameters"></param>
        /// <returns></returns>
        public static DataTable FillDataTable(string connectionString, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader rdr;
            // Create a new Oracle command
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable("ResultTable");
            //Create a connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {
                    //Prepare the command
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    //Execute the command
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
                    SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
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
        public static DataTable FillDataTable(MySqlConnection cn, CommandType cmdType, string cmdText, params MySqlParameter[] commandParameters)
        {
            MySqlDataReader rdr;
            // Create a new Oracle command
            MySqlCommand cmd = new MySqlCommand();
            DataTable _dt = new DataTable("ResultTable");
            //Create a connection

            try
            {
                //Prepare the command
                PrepareCommand(cmd, cn, null, cmdType, cmdText, commandParameters);
                //Execute the command
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
                SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                throw;
            }

            return _dt;
        }

        /// <summary>
        /// 通过查询语句取数据到表
        /// </summary>
        /// <param name="_selectStr"></param>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public static DataTable Get_Data(string _selectStr, string _tableName)
        {
            MySqlDataReader rdr;
            DataTable _dt = new DataTable(_tableName);
            using (MySqlConnection cn = OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_selectStr, cn);
                    using (rdr = _cmd.ExecuteReader())
                    {
                        FillTableByReader(_dt, rdr);
                    }
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
                                    e.Message, _selectStr);

                    SqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    throw e;
                }
                finally
                {
                    cn.Close();
                }
            }
            return _dt;
        }

        static public void FillTableByReader(DataTable _dt, MySqlDataReader rdr)
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

            while (rdr.Read())
            {
                _newrow = _dt.NewRow();
                for (i = 0; i < _fcount; i++)
                {
                    if (NumberColumnList.Contains(i) && !rdr.IsDBNull(i))
                    {
                        _newrow[i] = Decimal.Round(rdr.GetDecimal(i), 20);
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
            //MySqlCommandBuilder builder = new MySqlCommandBuilder(_da);
            _da.Update(_refData);
            return true;
        }
    }

}
