using aehyok.Core.Data.Model;
using aehyok.Core.MySqlDataAccessor;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Repository.Accessor.Builder
{
	public class OraQueryModelHelper
	{
		public static DataTable FillResultData(string _selectStr, string _tableName, ref int _count)
		{
			_count = 0;
			DataTable _t = new DataTable(_tableName);
			using (MySqlConnection cn = SqlHelper.OpenConnection())
			{
				try
				{
					_t = FillResultData(_selectStr, _tableName, cn);
					_count = _t.Rows.Count;
				}
				catch (InvalidCastException ex)
				{
					string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
									  ex.Message, _selectStr);
					//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
				}
				catch (MySqlException ex)
				{
					string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
									ex.Message, _selectStr);
					//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
				}
				catch (Exception e)
				{
					string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
									e.Message, _selectStr);
					//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
				}
				finally
				{
					cn.Close();
				}
			}

			return _t;
		}

		public static DataTable FillResultData(string _selectStr, string _tableName, ref int _count, MySqlConnection MySqlConnection)
		{
			_count = 0;
			DataTable _t = new DataTable(_tableName);
			try
			{
				_t = FillResultData(_selectStr, _tableName, MySqlConnection);
				_count = _t.Rows.Count;
			}
			catch (InvalidCastException ex)
			{
				string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
									ex.Message, _selectStr);
				//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
			}
			catch (MySqlException ex)
			{
				string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
								ex.Message, _selectStr);
				//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
			}
			catch (Exception e)
			{
				string _errmsg = string.Format("执行SQL语句出错,错误信息为:{0}!\n查询语句为:{1}\n:",
								e.Message, _selectStr);
				//OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
			}
			return _t;
		}

		public static DataTable FillResultData(string _selectStr, string _tableName, MySqlConnection cn)
		{
			DataTable _dt = SqlHelper.FillDataTable(cn, CommandType.Text, _selectStr);
			_dt.TableName = _tableName;
			return _dt;
		}

		public static MDQueryResult_Table FillMDResultData(string selectStr, string tableName, MySqlConnection cn)
		{
			MySqlDataReader rdr;
			MDQueryResult_Table _dt = new MDQueryResult_Table(tableName);
			rdr = SqlHelper.ExecuteReader(cn, CommandType.Text, selectStr);
			FillTableByReader(_dt, rdr);
			rdr.Close();
			return _dt;
		}

		public static void FillTableByReader(MDQueryResult_Table dt, MySqlDataReader rdr)
		{
			int i;
			int fcount = rdr.FieldCount;
			DataTable schemaTable = rdr.GetSchemaTable();
			List<int> NumberColumnList = new List<int>();
			dt.Columns = new List<MDQueryResult_TableColumn>();
			int index = 0;
			foreach (DataRow myRow in schemaTable.Rows)
			{
				dt.Columns.Add(new MDQueryResult_TableColumn(myRow["ColumnName"].ToString(), myRow["DataType"].ToString()));
				if ((Type)myRow["DataType"] == typeof(decimal))
				{
					NumberColumnList.Add(index);
				}
				index++;
			}
			dt.Rows = new List<MDQueryResult_TableRow>();
			while (rdr.Read())
			{
				MDQueryResult_TableRow _newrow = new MDQueryResult_TableRow();
				for (i = 0; i < fcount; i++)
				{
					MDQueryResult_TableColumn _col = dt.Columns[i];
					if (NumberColumnList.Contains(i) && !rdr.IsDBNull(i))
					{
						//decimal _v = decimal.Parse((rdr.GetMySqlDecimal(i), 20));
						//_newrow.Values.Add(_col.ColumnName, _v);
					}
					else
					{
						if (rdr.IsDBNull(i))
						{
							_newrow.Values.Add(_col.ColumnName, null);
						}
						else
						{
							if (_col.ColumnType == "System.DateTime")
							{
								_newrow.Values.Add(_col.ColumnName, (DateTime)rdr.GetValue(i));
							}
							else
							{
								_newrow.Values.Add(_col.ColumnName, rdr.GetValue(i).ToString());
							}
						}
					}
				}
				dt.Rows.Add(_newrow);
			}
		}
	}
}
