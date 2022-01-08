using aehyok.Core.Data.Model;
using aehyok.Core.MySql;
using aehyok.Core.MySqlDataAccessor;
using aehyok.Core.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Repository.Accessor.Builder
{
    public class OraQueryBuilder
    {
        private const string QueryMaxReturnRows = "10000";
        public static Dictionary<string, string> GetQueryStr(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, ref string _mainQueryStr, SinoRequestUser RequestUser)
        {
            _mainQueryStr = "";

            string _filterStr = CreateDataFilterStr(_qv, RequestUser);
            bool canQueryOnce = IsOneResultTable(_qv, _queryRequest);
            string _conditionStr = CreateConditionString(_qv, _queryRequest);

            if (canQueryOnce)
            {
                //单查询语句
                List<string> _usedTableList = GetSingleQueryUsedTable(_qv, _queryRequest);
                string _displayStr = CreateSingleDisplayString(_qv, _queryRequest, RequestUser);
                string _tableStr = CreateTableString(_qv, _usedTableList);
                string _tableRelationStr = CreateTableRelationString(_qv, _usedTableList, RequestUser);
                _mainQueryStr = string.Format("select * from (select DISTINCT {0} from {1} where {4} ( {2} ({3})) ) where rownum<{5}",
                    _displayStr, _tableStr, _tableRelationStr, _conditionStr, _filterStr, QueryMaxReturnRows);
                return new Dictionary<string, string>();
            }
            else
            {
                //多查询语句
                List<string> _QueryUsedTableList = GetQueryUsedTable(_qv, _queryRequest);
                string _tableStr = CreateTableString(_qv, _QueryUsedTableList);
                string _tableRelationStr = CreateTableRelationString(_qv, _QueryUsedTableList, RequestUser);
                string _displayStr = string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.MainKey);
                _mainQueryStr = string.Format("select * from (select {0} from {1} where {4} ( {2} ({3})) ) where rownum<{5} ",
                    _displayStr, _tableStr, _tableRelationStr, _conditionStr, _filterStr, QueryMaxReturnRows);
                Dictionary<string, string> _ret = new Dictionary<string, string>();
                _ret.Add(_qv.MainTable.TableName, CreateMainTableResult(_qv, _queryRequest.MainResultTable, RequestUser));
                foreach (MDQuery_ResultTable _rt in _queryRequest.ChildResultTables)
                {
                    _ret.Add(_rt.TableName, CreateChildTableResult(_qv, _rt, RequestUser));
                }
                return _ret;
            }
        }

        public static string CreateDataFilterStr(MDModel_QueryModel _qv, SinoRequestUser RequestUser)
        {
            string _res = "";
            if (_qv.MainTable.SecretFun.Trim() != "")
            {
                _res = string.Format(" {0}({1}.ZHCX_DW,'{2}') = '1' and ",
                    _qv.MainTable.SecretFun,
                    _qv.MainTable.TableName,
                    RequestUser.SinoPost.PostDWDM);
            }
            else
            {
                _res = "";
            }

            if (_qv.MainTable.ExtSecret.Trim() != "")
            {
                _res = string.Format("{0} and {1}", ReplaceExtSecret(_qv, _qv.MainTable.ExtSecret, RequestUser), _res);
            }
            return _res;
        }

        private static bool IsOneResultTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            if (_queryRequest.ChildResultTables.Count < 1) return true;

            foreach (MDQuery_ResultTable _tb in _queryRequest.ChildResultTables)
            {
                string _tname = _tb.TableName;
                var findResult =
                    from ctb in _queryRequest.ChildResultTables
                    where ctb.TableName == _tname
                    select ctb;

                if (!findResult.Any())
                {
                    throw new Exception(string.Format("构建查询字符串时发出错误，不存在子表{0}", _tname));
                }
            }
            return false;
        }

        private static string CreateConditionString(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            if (string.IsNullOrEmpty(_queryRequest.ConditionExpressions)) return "1=1";

            string conditionStr = _queryRequest.ConditionExpressions;
            StringBuilder res = new StringBuilder();
            string _itemIndex = "";

            for (int i = 0; i < conditionStr.Length; i++)
            {
                char c = conditionStr[i];

                switch (c)
                {
                    case '+':
                        res.Append(" or  ");
                        break;
                    case '*':
                        res.Append(" and ");
                        break;
                    case '!':
                        res.Append(" not ");
                        break;
                    case '(':
                        res.Append(" (");
                        break;
                    case ')':
                        res.Append(" ) ");
                        break;
                    default:
                        //数字
                        if (char.IsDigit(c))
                        {
                            _itemIndex = ProcDigit(conditionStr, ref i);
                        }
                        res.Append(GetConditionStr(_qv, _queryRequest, _itemIndex));
                        break;
                }
            }

            return res.ToString();
        }

        private static string ProcDigit(string conditionStr, ref int index)
        {
            StringBuilder str = new StringBuilder();
            for (int i = index; i < conditionStr.Length; i++)
            {
                char c = conditionStr[i];
                if (char.IsDigit(c))
                    str.Append(c);
                else
                {
                    break;
                }
                index = i;
            }
            return str.ToString();
        }

        private static string GetConditionStr(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, string _itemIndex)
        {
            var conditionItem = from item in _queryRequest.ConditionItems
                                where item.ColumnIndex == _itemIndex
                                select item;
            if (conditionItem.Count() < 1)
            {
                throw new Exception(string.Format("缺少编号为{0}的条件项!", _itemIndex));
            }
            else
            {
                foreach (MDQuery_ConditionItem _cItem in conditionItem)
                {
                    return OraConditionItemBuilder.BuildConditionItemString(_cItem, _qv);
                }
            }
            return "";
        }

        private static List<string> GetSingleQueryUsedTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<string> _usedTableList = (from _c in _queryRequest.ChildResultTables
                                           select _c.TableName).Distinct().ToList();
            if (!_usedTableList.Contains(_qv.MainTable.TableName)) _usedTableList.Add(_qv.MainTable.TableName);

            var _conditionTabls = (from _c in _queryRequest.ConditionItems
                                   select _c.Column.TableName).Distinct();
            foreach (string _cItem in _conditionTabls)
            {
                if (!_usedTableList.Contains(_cItem))
                {
                    _usedTableList.Add(_cItem);
                }
            }
            return _usedTableList;
        }


        private static string CreateSingleDisplayString(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, SinoRequestUser RequestUser)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.MainKey));

            foreach (MDQuery_TableColumn _rc in _queryRequest.MainResultTable.Columns)
            {
                _sql.Append(OraResultItemBuilder.BuildItem(_rc, _qv, RequestUser));
            }

            foreach (MDQuery_ResultTable _rResultTable in _queryRequest.ChildResultTables)
            {
                foreach (MDQuery_TableColumn _rc in _rResultTable.Columns)
                {
                    _sql.Append(OraResultItemBuilder.BuildItem(_rc, _qv, RequestUser));
                }
            }
            return _sql.ToString();
        }

        private static string CreateTableString(MDModel_QueryModel _qv, List<string> _usedTableList)
        {
            StringBuilder _sb = new StringBuilder();
            string _fg = "";
            foreach (string _tname in _usedTableList)
            {
                _sb.Append(_fg);
                _sb.Append(_tname);
                _fg = ",";
            }
            return _sb.ToString();
        }

        private static string CreateTableRelationString(MDModel_QueryModel _qv, List<string> _usedTableList, SinoRequestUser RequestUser)
        {
            StringBuilder _ret = new StringBuilder();
            foreach (string _tname in _usedTableList)
            {
                if (_tname != _qv.MainTable.TableName)
                {
                    MDModel_Table _cTable = (from _t in _qv.ChildTables
                                             where _t.TableName == _tname
                                             select _t).First();

                    if (_cTable != null)
                    {
                        if (_cTable.TableRelation != string.Empty)
                        {
                            _ret.Append(string.Format("and {0} ", ReplaceExtSecret(_qv, _cTable.TableRelation, RequestUser)));
                        }
                    }
                }
            }

            if (_ret.Length > 3)
            {
                return "(" + _ret.ToString().Substring(3) + ") and ";
            }
            else
            {
                return "";
            }

        }

        private static List<string> GetQueryUsedTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<string> _usedTableList = (from _c in _queryRequest.ConditionItems
                                           select _c.Column.TableName).Distinct().ToList();
            if (!_usedTableList.Contains(_qv.MainTable.TableName)) _usedTableList.Add(_qv.MainTable.TableName);

            return _usedTableList;
        }

        private static string CreateMainTableResult(MDModel_QueryModel _qv, MDQuery_ResultTable _ResultTable, SinoRequestUser RequestUser)
        {
            List<string> _usedTables = new List<string>();
            _usedTables.Add(_ResultTable.TableName);
            string _displayStr = CreateDisplayString(_qv, _ResultTable, RequestUser);
            string _conditionRes = CreateConditionStringByMainID(_qv);
            string _tableStr = CreateTableString(_qv, _usedTables);
            return string.Format("select {0} from {1} where ( {2}) ",
                _displayStr, _tableStr, _conditionRes);
        }

        private static string CreateConditionStringByMainID(MDModel_QueryModel _qv)
        {
            string _str = string.Format("{0}.{1} in (select  pk_c from query_temp) ", _qv.MainTable.TableName, _qv.MainTable.MainKey);
            return _str;
        }

        private static string CreateDisplayString(MDModel_QueryModel _qv, MDQuery_ResultTable _ResultTable, SinoRequestUser RequestUser)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.MainKey));
            foreach (MDQuery_TableColumn _rc in _ResultTable.Columns)
            {
                _sql.Append(OraResultItemBuilder.BuildItem(_rc, _qv, RequestUser));
            }
            return _sql.ToString();
        }

        private static string CreateChildTableResult(MDModel_QueryModel _qv, MDQuery_ResultTable _rt, SinoRequestUser RequestUser)
        {
            List<string> _usedTables = new List<string>();
            _usedTables.Add(_qv.MainTable.TableName);
            _usedTables.Add(_rt.TableName);
            string _displayStr = CreateDisplayString(_qv, _rt, RequestUser);
            string _conditionRes = CreateConditionStringByMainID(_qv);
            string _tableStr = CreateTableString(_qv, _usedTables);
            string _tableRelationStr = CreateTableRelationString(_qv, _usedTables, RequestUser);
            return string.Format("select {0} from {1} where ( {2} ({3})) ",
                _displayStr, _tableStr, _tableRelationStr, _conditionRes);
        }

        public static string ReplaceExtSecret(MDModel_QueryModel _qv, string _secret, SinoRequestUser RequestUser)
        {
            if (_secret == null) return _secret;
            string _retstr = _secret;
            if (RequestUser.BaseInfo != null)
            {
                _retstr = _retstr.Replace("%USERID%", RequestUser.BaseInfo.UserId);
                _retstr = _retstr.Replace("%USERNAME%", RequestUser.BaseInfo.UserName);
            }
            if (RequestUser.SinoPost != null)
            {
                _retstr = _retstr.Replace("%DWDM%", RequestUser.SinoPost.PostDWDM);
                _retstr = _retstr.Replace("%DWID%", RequestUser.SinoPost.PostDwId);
                _retstr = _retstr.Replace("%DWMC%", RequestUser.SinoPost.PostDWMC);
                _retstr = _retstr.Replace("%POSTID%", RequestUser.SinoPost.PostId);
            }
            _retstr = _retstr.Replace("%SYSTEMID%", RequestUser.SystemId);
            //_retstr = _retstr.Replace("%ROOTDWID%", ServerConfig.SystemOrganzationId);
            if (_qv != null) _retstr = _retstr.Replace("%MODELID%", _qv.ViewID);

            _retstr = ReplaceFunction(_retstr);
            _retstr = ReplaceProcedureResult(_retstr);
            return _retstr;
        }

        public static string ReplaceFunction(string _retstr)
        {
            object _retValue;
            string _res = _retstr;
            int _pos = _retstr.IndexOf('&');
            if (_pos == -1 || _pos == (_retstr.Length - 1)) return _retstr;
            int _pos2 = _retstr.IndexOf('&', _pos + 1);
            if (_pos2 == -1) return _retstr;
            else
            {
                string _fun = _retstr.Substring(_pos, _pos2 - _pos + 1);
                string _sql = string.Format("select {0} RETVALUE from dual", _fun.Replace("&", ""));
                _retValue = MysqlDBHelper.ExecuteScalar(MysqlDBHelper.OpenConnection(), CommandType.Text, _sql, null); ;
                if (_retValue != null)
                {
                    _res = _retstr.Replace(_fun, _retValue.ToString());
                }
                else
                {
                    _res = _retstr.Replace(_fun, "");
                }
                _res = ReplaceFunction(_res);
            }
            return _res;
        }

        public static string ReplaceProcedureResult(string _retstr)
        {
            int _index = 0;
            int _strCount = _retstr.Length;
            Dictionary<string, string> PValues = new Dictionary<string, string>();
            while (_index < _strCount)
            {
                int _findIndex = _retstr.IndexOf("/*GET ", _index);
                if (_findIndex == -1)
                {
                    //如果未找到头标记，则退出
                    _index = _strCount + 1;
                    break;
                }
                else
                {
                    int _findEnd = _retstr.IndexOf("*/", _findIndex);
                    if (_findEnd == -1)
                    {
                        //如果未找到尾标记，则退出
                        _index = _strCount + 1;
                        break;
                    }
                    else
                    {
                        string _pStr = _retstr.Substring(_findIndex, _findEnd - _findIndex + 2);
                        if (!PValues.ContainsKey(_pStr))
                        {
                            string _pValue = ExcuteProcedure(_pStr);
                            PValues.Add(_pStr, _pValue);
                        }
                        _index = _findEnd + 1;
                    }
                }
            }

            foreach (string _key in PValues.Keys)
            {
                _retstr = _retstr.Replace(_key, PValues[_key]);
            }
            return _retstr;
        }

        public static string ExcuteProcedure(string _pStr)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    string _sql = _pStr.Substring(5, _pStr.Length - 7);

                    MySqlParameter _p1 = new MySqlParameter(":1", MySqlDbType.VarChar, 1000);
                    _p1.Direction = ParameterDirection.Output;
                    //OracleCommand _cmd = new OracleCommand(_sql, cn);
                    //
                    //
                    //_cmd.ExecuteNonQuery();
                    MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _sql, _p1);
                    _txn.Commit();
                    return _p1.Value.ToString();
                }
                catch (InvalidCastException ex)
                {
                    string _errmsg = string.Format("执行命令嵌入的命令出错,错误信息为:{0}!SQL语句：{1}", ex.Message, _pStr);
                    //OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    _txn.Rollback();
                    return "";
                }
                catch (MySqlException ex)
                {
                    string _errmsg = string.Format("执行命令嵌入的命令出错,错误信息为:{0}!SQL语句：{1}", ex.Message, _pStr);
                    //OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    _txn.Rollback();
                    return "";
                }
                catch (Exception ex)
                {
                    string _errmsg = string.Format("执行命令嵌入的命令出错,错误信息为:{0}!SQL语句：{1}", ex.Message, _pStr);
                    //OracleLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    _txn.Rollback();
                    return "";
                }
            }
        }

        public static string RebuildGuideLineQueryString(string _queryStr, MDQuery_GuideLineParameter _gp)
        {
            string _ret = _queryStr;

            switch (_gp.ParameterType)
            {
                case "数值型":
                    string _s3 = string.Format(" {0}", _gp.ParameterValue == null ? "" : _gp.ParameterValue);
                    return _queryStr.Replace(_gp.ParameterName, _s3);

                case "代码表":
                    string _refStr = "";
                    string[] _rtNames = _gp.RefTableName.Split('.');
                    if (_gp.ParameterName == "strYN")
                    {
                        _refStr = string.Format("1 = {0}", _gp.ParameterValue);
                    }
                    else
                    {
                        string _tName = (_rtNames.Length > 1) ? _rtNames[1] : _rtNames[0];
                        if (_gp.IncludeChildren)
                        {
                            //对于树型代码表
                            if (_gp.ParameterValue.ToString() == _gp.SelectAllCode)
                            {
                                _refStr = string.Format(" ('{0}') ", _gp.ParameterValue);
                            }
                            else
                            {
                                _refStr = string.Format(" (select TO_CHAR(dm) from jsods.{1} start with dm='{0}' connect by prior DM=FATHERCODE) ",
                                    _gp.ParameterValue, _tName);
                            }
                        }
                        else
                        {
                            //对于非树型代码表
                            _refStr = string.Format(" '{0}' ", _gp.ParameterValue);
                        }
                    }
                    return _queryStr.Replace(_gp.ParameterName, _refStr);

                case "日期型":
                    string _s1 = DateTime.MaxValue.ToString("yyyy-MM-dd");
                    if (_gp.ParameterValue.GetType() == typeof(DateTime))
                        _s1 = ((DateTime)_gp.ParameterValue).ToString("yyyy-MM-dd HH:mm:ss");
                    else if (_gp.ParameterValue.GetType() == typeof(string))
                        _s1 = StrUtils.String2Date(_gp.ParameterValue.ToString()).ToString("yyyy-MM-dd HH:mm:ss");

                    if (_s1.Length == 10)
                    {
                        _s1 = string.Format(" to_date('{0}','YYYY-MM-DD') ", _s1);
                    }
                    if (_s1.Length == 19)
                    {
                        _s1 = string.Format("to_date('{0}','YYYY-MM-DD HH24:MI:SS') ", _s1);
                    }
                    return _queryStr.Replace(_gp.ParameterName, _s1);
                case "逻辑型":
                    return _queryStr = _queryStr + " and " + _gp.ParameterName + " like " + "'%" + _gp.ParameterValue + "%'";
                default:
                    string _s2 = string.Format(" '{0}'", _gp.ParameterValue.ToString());
                    return _queryStr.Replace(_gp.ParameterName, _s2);
            }
        }


        /// <summary>
        /// 构建分页的sql语句
        /// </summary>
        /// <param name="sql">源sql语句</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页的记录数</param>
        /// <param name="sortBy">排序字段</param>
        /// <param name="sortDirection">排序方向(ASC,DESC)</param>
        /// <returns></returns>
        public static string BuildPagingSQL(string sql, decimal pageIndex, decimal pageSize, string sortBy, string sortDirection)
        {
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(sortBy))
            {
                sql = " select * from (\n " + sql + " \n) order by " + sortBy + " " + sortDirection;
            }
            str.Append(" select t_1.* from ");
            str.Append(" ( select rownum r_0,t_0.* from ");
            str.Append(" (\n " + sql + " \n) t_0 ");
            str.Append(" where rownum <= " + (pageIndex * pageSize) + " ) t_1 ");
            str.Append(" where r_0 > " + (pageIndex - 1) * pageSize);
            return str.ToString();
        }

        public static string GetMainTableData(MDModel_Table maintable, string mainKey, SinoRequestUser requestUser)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            sql.Append(maintable.MainKey);
            sql.Append(" MAINKEY");
            foreach (MDModel_Table_Column _tc in maintable.Columns)
            {
                if (_tc.CanDisplay && _tc.CanResultShow)
                {
                    sql.Append(",");
                    sql.Append(OraResultItemBuilder.BuildItem(_tc, requestUser));
                }
            }
            sql.Append(" from ");
            sql.Append(maintable.TableName);
            sql.Append(" where ");
            sql.Append(maintable.MainKey);
            sql.Append(" = '");
            sql.Append(mainKey);
            sql.Append("' ");
            return sql.ToString();
        }

        public static string GetChildTableCountData(MDModel_QueryModel model, MDModel_Table childTable, string mainKey, SinoRequestUser requestUser)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("select count({0}.{1}) ", childTable.TableName, childTable.MainKey));
            _sql.Append(" from ");
            _sql.Append(string.Format(" {0},{1} ", model.MainTable.TableName, childTable.TableName));
            _sql.Append(" where ");
            _sql.Append(string.Format(" {0}.{1} ", model.MainTable.TableName, model.MainTable.MainKey));
            _sql.Append(" = '");
            _sql.Append(mainKey);
            _sql.Append("'  and ");
            _sql.Append(childTable.TableRelation);
            return _sql.ToString();
        }

        public static string GetChildTableData(MDModel_QueryModel model, MDModel_Table childTable, string MainKey, SinoRequestUser RequestUser)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append("select ");
            _sql.Append(childTable.TableName);
            _sql.Append(".");
            _sql.Append(childTable.MainKey);
            _sql.Append(" MAINKEY");
            foreach (MDModel_Table_Column _tc in childTable.Columns)
            {
                if (_tc.CanDisplay && _tc.CanResultShow)
                {
                    _sql.Append(",");
                    _sql.Append(OraResultItemBuilder.BuildItem(_tc, RequestUser));
                }
            }
            _sql.Append(" from ");
            _sql.Append(string.Format(" {0},{1} ", model.MainTable.TableName, childTable.TableName));
            _sql.Append(" where ");
            _sql.Append(string.Format(" {0}.{1} ", model.MainTable.TableName, model.MainTable.MainKey));
            _sql.Append(" = '");
            _sql.Append(MainKey);
            _sql.Append("'  and ");
            _sql.Append(childTable.TableRelation);
            _sql.Append(string.Format("  order by {0}.{1} ", childTable.TableName, childTable.MainKey));
            return _sql.ToString();
        }
    }
}
