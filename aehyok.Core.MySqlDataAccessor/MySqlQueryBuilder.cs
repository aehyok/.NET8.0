using MySql.Data.MySqlClient;
using aehyok.Lib.MetaData.Common;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using aehyok.Core.MySql;

namespace aehyok.MysqlDataAccess
{
    /// <summary>
    /// 查询语句生成器 (mysql语法)
    /// </summary>
    public class MySqlQueryBuilder
    {
        public static string GetPAQueryStr(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<MD_SecretFieldItem> _secretFields = new List<MD_SecretFieldItem>();
            string _conditionStr = CreateConditionString(_qv, _queryRequest);
            string _displayStr = CreateSingleDisplayString(_qv, _queryRequest, _secretFields);
            string _mainQueryStr = string.Format("select DISTINCT {0} from PANALIZE. {1} where {2} ",
                         _displayStr, _qv.MainTable.TableName, _conditionStr);
            return _mainQueryStr;

        }

        public static string GetMainTableData(MDModel_Table _maintable, string _keyid, List<MD_SecretFieldItem> _secretFields)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append("select ");
            string _fg = "";
            foreach (MDModel_Table_Column _tc in _maintable.Columns)
            {
                if (_tc.ColumnDefine.TableColumn.CanDisplay)
                {
                    _sql.Append(_fg);
                    _sql.Append(MySqlResultItemBuilder.BuildItem(_tc));
                    _fg = ",";
                }
                if (_tc.SecretLevel > 0)
                {
                    // 字符串类型才进行解密
                    switch (_tc.ColumnDataType.ToUpper())
                    {
                        case "CHAR":
                        case "VARCHAR":
                        case "NVARCHAR":
                        case "NVARCHAR2":
                        case "VARCHAR2":
                            _secretFields.Add(new MD_SecretFieldItem() { TableName = _tc.TableName, FieldName = _tc.ColumnAlias });
                            break;
                    }
                }
            }

            _sql.Append(" from ");
            _sql.Append(_maintable.TableName);
            _sql.Append(" where ");
            _sql.Append(_maintable.TableDefine.Table.MainKey);
            _sql.Append(" = '");
            _sql.Append(_keyid);
            _sql.Append("' ");
            return _sql.ToString();
        }

        public static string GetChildTableData(MDModel_Table _maintable, MDModel_Table _childTable, string _keyid, List<MD_SecretFieldItem> _secretFields)
        {

            StringBuilder _sql = new StringBuilder();
            _sql.Append("select ");
            string _fg = "";
            foreach (MDModel_Table_Column _tc in _childTable.Columns)
            {
                if (_tc.ColumnDefine.TableColumn.CanDisplay && _tc.ColumnDefine.CanShowAsResult)
                {
                    _sql.Append(_fg);
                    _sql.Append(MySqlResultItemBuilder.BuildItem(_tc));
                    _fg = ",";
                }
                if (_tc.SecretLevel > 0)
                {
                    // 字符串类型才进行解密
                    switch (_tc.ColumnDataType.ToUpper())
                    {
                        case "CHAR":
                        case "VARCHAR":
                        case "NVARCHAR":
                        case "NVARCHAR2":
                        case "VARCHAR2":
                            _secretFields.Add(new MD_SecretFieldItem() { TableName = _tc.TableName, FieldName = _tc.ColumnAlias });
                            break;
                    }
                }
            }
            _sql.Append(" from ");
            _sql.Append(string.Format(" {0},{1} ", _maintable.TableName, _childTable.TableName));
            _sql.Append(" where ");
            _sql.Append(string.Format(" {0}.{1} ", _maintable.TableName, _maintable.TableDefine.Table.MainKey));
            _sql.Append(" = '");
            _sql.Append(_keyid);
            _sql.Append("'  and ");
            _sql.Append(_childTable.TableDefine.RelationString);
            return _sql.ToString();
        }

        public static string GetMainTableKeyByChildKey(MDModel_Table _mainTable, MDModel_Table _childTable, string _childKey)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append("select ");
            _sql.Append(string.Format(" {0}.{1} MAINKEY ", _mainTable.TableName, _mainTable.TableDefine.Table.MainKey));
            _sql.Append(" from ");
            _sql.Append(string.Format(" {0},{1} ", _mainTable.TableName, _childTable.TableName));
            _sql.Append(" where ");
            _sql.Append(string.Format(" {0}.{1} ", _childTable.TableName, _childTable.TableDefine.Table.MainKey));
            _sql.Append(" = '");
            _sql.Append(_childKey);
            _sql.Append("'  and ");
            _sql.Append(_childTable.TableDefine.RelationString);
            return _sql.ToString();
        }

        public static string GetMainTableKeyByColumnCondition(MDModel_Table _mainTable, string _columnName, string _data)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append("select ");
            _sql.Append(string.Format(" {0}.{1} MAINKEY ", _mainTable.TableName, _mainTable.TableDefine.Table.MainKey));
            _sql.Append(" from ");
            _sql.Append(_mainTable.TableName);
            _sql.Append(" where ");
            _sql.Append(_columnName);
            _sql.Append(" = ");

            MDModel_Table_Column _tc = _mainTable.GetColumnByName(_columnName);
            if (_tc == null)
            {
                _sql.Append(string.Format(" '{0}' ", _data));
            }
            else
            {
                switch (_tc.ColumnDataType)
                {
                    case "NUMBER":
                        _sql.Append(string.Format(" {0} ", _data));
                        break;
                    case "DATE":
                        _sql.Append(string.Format(" STR_TO_DATE('{0}','%Y%m%d') ", _data));
                        break;
                    default:
                        _sql.Append(string.Format(" '{0}' ", _data));
                        break;
                }
            }
            return _sql.ToString();
        }

        public static string GetChildTableCount(MDModel_Table _maintable, MDModel_Table _childTable, string _keyid)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("select count({0}.{1}) ", _childTable.TableName, _childTable.TableDefine.Table.MainKey));

            _sql.Append(" from ");
            _sql.Append(string.Format(" {0},{1} ", _maintable.TableName, _childTable.TableName));
            _sql.Append(" where ");
            _sql.Append(string.Format(" {0}.{1} ", _maintable.TableName, _maintable.TableDefine.Table.MainKey));
            _sql.Append(" = '");
            _sql.Append(_keyid);
            _sql.Append("'  and ");
            _sql.Append(_childTable.TableDefine.RelationString);
            return _sql.ToString();
        }


        public static Dictionary<string, string> GetQueryStr(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, ref string _mainQueryStr, string _dwid, List<MD_SecretFieldItem> secretFields, string _tempTableName)
        {

            _mainQueryStr = "";

            string _filterStr = CreateDataFilterStr(_qv, _dwid);
            bool canQueryOnce = IsOneResultTable(_qv, _queryRequest);
            string _conditionStr = CreateConditionString(_qv, _queryRequest);

            if (canQueryOnce)
            {
                //单查询语句
                List<string> _usedTableList = GetSingleQueryUsedTable(_qv, _queryRequest);
                string _displayStr = CreateSingleDisplayString(_qv, _queryRequest, secretFields);
                string _tableStr = CreateTableString(_qv, _usedTableList);
                string _tableRelationStr = CreateTableRelationString(_qv, _usedTableList);
                _mainQueryStr = string.Format("select DISTINCT {0} from {1} where {4} ( {2} ({3})) ",
                        _displayStr, _tableStr, _tableRelationStr, _conditionStr, _filterStr);
                return new Dictionary<string, string>();
            }
            else
            {
                //多查询语句
                List<string> _QueryUsedTableList = GetQueryUsedTable(_qv, _queryRequest);
                string _tableStr = CreateTableString(_qv, _QueryUsedTableList);
                string _tableRelationStr = CreateTableRelationString(_qv, _QueryUsedTableList);
                string _displayStr = string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.TableDefine.Table.MainKey);
                _mainQueryStr = string.Format("select DISTINCT {0} from {1} where {4} ( {2} ({3})) ",
                        _displayStr, _tableStr, _tableRelationStr, _conditionStr, _filterStr);
                Dictionary<string, string> _ret = new Dictionary<string, string>();
                _ret.Add(_qv.MainTable.TableName, CreateMainTableResult(_qv, _queryRequest.MainResultTable, secretFields, _tempTableName));
                if (_queryRequest.ChildResultTables != null)
                {
                    foreach (MDQuery_ResultTable _rt in _queryRequest.ChildResultTables)
                    {
                        _ret.Add(_rt.TableName, CreateChildTableResult(_qv, _rt, secretFields, _tempTableName));
                    }
                }
                return _ret;

            }
        }

        public static string GetQueryStrWithWHXH(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, string _rangeDWDM)
        {
            string _res = "";
            bool canQueryOnce = IsOneResultTable(_qv, _queryRequest);
            List<MD_SecretFieldItem> _secretFields = new List<MD_SecretFieldItem>();
            List<string> _usedTableList = GetQueryUsedTableWithWHXH(_qv, _queryRequest);
            string _filterStr = CreateDataFilterStrWithRange(_qv, _rangeDWDM);

            string _displayStr = CreateSingleDisplayString(_qv, _queryRequest, _secretFields);
            string _conditionStr = CreateConditionString(_qv, _queryRequest);
            string _tableStr = CreateTableString(_qv, _usedTableList);
            string _tableRelationStr = CreateTableRelationString(_qv, _usedTableList);


            string _vn = _queryRequest.QueryModelName.Split('.')[1];
            string _ns = _queryRequest.QueryModelName.Split('.')[0];
            string _mainKeyTableName = _qv.MainTable.TableName;
            string _mainKeyColumnName = _qv.MainTable.TableDefine.Table.MainKey;
            string _shStr = string.Format(" (SELECT SH.SH_FJ  FROM SJSH_B SH WHERE SH.VIEWNAME = '{0}' AND SH.NAMESPACE = '{1}' AND TRIM(TO_CHAR({2}.{3}))= SH.SHDXGJZ ) SH_FJ", _vn, _ns, _mainKeyTableName, _mainKeyColumnName);
            _shStr += string.Format(" ,(SELECT SH.SH_ZHISJ  FROM SJSH_B SH WHERE SH.VIEWNAME = '{0}' AND SH.NAMESPACE = '{1}' AND TRIM(TO_CHAR({2}.{3}))= SH.SHDXGJZ ) SH_ZHISJ", _vn, _ns, _mainKeyTableName, _mainKeyColumnName);
            _shStr += string.Format(" ,(SELECT SH.SH_FSJ  FROM SJSH_B SH WHERE SH.VIEWNAME = '{0}' AND SH.NAMESPACE = '{1}' AND TRIM(TO_CHAR({2}.{3}))= SH.SHDXGJZ ) SH_FSJ", _vn, _ns, _mainKeyTableName, _mainKeyColumnName);
            _shStr += string.Format(" ,(SELECT SH.SH_ZONGSJ  FROM SJSH_B SH WHERE SH.VIEWNAME = '{0}' AND SH.NAMESPACE = '{1}' AND TRIM(TO_CHAR({2}.{3}))= SH.SHDXGJZ ) SH_ZONGSJ", _vn, _ns, _mainKeyTableName, _mainKeyColumnName);
            _shStr += string.Format(",ZHCX_HGJS.Get_ODS_CHANGED('{0}','{1}',{0}.{1}) RKQK", _mainKeyTableName, _mainKeyColumnName);

            string _whStr = string.Format("(SELECT SHJG.WHXH  FROM SJSH_B SH,SJSH_JGJLB SHJG WHERE SH.VIEWNAME = '{0}' AND SH.NAMESPACE = '{1}' AND TRIM(TO_CHAR({2}.{3}))= SH.SHDXGJZ AND SH.ID = SHJG.ID AND SHJG.DWDM = '{4}') OLD_WHXH  ",
                    _vn, _ns, _mainKeyTableName, _mainKeyColumnName, _rangeDWDM);
            string _CurrentWhStr = string.Format("(SELECT WHXHB.WHXH FROM {0}_WHXH WHXHB WHERE {0}.{1} = WHXHB.{1}) CURRENT_WHXH ",
                    _mainKeyTableName, _mainKeyColumnName);

            if (!canQueryOnce)
            {
                _res = string.Format("select DISTINCT {0},{5},{6},{7} from {1} where {4} ( {2} ({3})) ",
                        _displayStr, _tableStr, _tableRelationStr, _conditionStr, _filterStr, _shStr, _whStr, _CurrentWhStr);
            }
            else
            {
                _res = string.Format("select {0},{5},{6},{7} from {1} where {4} ( {2} ({3})) ", _displayStr, _tableStr, _tableRelationStr,
                        _conditionStr, _filterStr, _shStr, _whStr, _CurrentWhStr);
            }

            return _res;

        }

        private static List<string> GetQueryUsedTableWithWHXH(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<string> _usedTableList = new List<string>();
            _usedTableList.Add(_qv.MainTable.TableName);

            foreach (MDQuery_ConditionItem _cItem in _queryRequest.ConditionItems)
            {
                if (!_usedTableList.Contains(_cItem.Column.TableName))
                {
                    _usedTableList.Add(_cItem.Column.TableName);
                }
            }

            foreach (MDQuery_ResultTable _resItem in _queryRequest.ChildResultTables)
            {
                if (!_usedTableList.Contains(_resItem.TableName))
                {
                    _usedTableList.Add(_resItem.TableName);
                }
            }
            return _usedTableList;
        }

        /// <summary>
        /// 以指定范围数据
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_rangeDWID"></param>
        /// <returns></returns>
        private static string CreateDataFilterStrWithRange(MDModel_QueryModel _qv, string _rangeDWDM)
        {
            string _res = "";
            string _rangeStr = "";
            if (_qv.MainTable.TableDefine.Table.SecretFun != "")
            {
                //if (SinoUserCtx.CurUser.QxszDWDM == _rangeDWDM)
                //{
                //    _res = string.Format(" {0}({1}.ZHCX_DW,'{2}') = '1' and  ",
                //          _qv.MainTable.TableDefine.Table.SecretFun,
                //          _qv.MainTable.TableDefine.TableName,
                //          SinoUserCtx.CurUser.QxszDWDM);
                //}
                //else
                //{
                //    _res = string.Format(" ({0}({1}.ZHCX_DW,'{2}') = '1' and {0}({1}.ZHCX_DW,'{3}') = '1' ) and ",
                //            _qv.MainTable.TableDefine.Table.SecretFun,
                //            _qv.MainTable.TableDefine.TableName,
                //            SinoUserCtx.CurUser.QxszDWDM,
                //            _rangeDWDM);
                //}

            }
            else
            {
                _res = "";
            }


            if (_qv.MainTable.TableDefine.Table.ExtSecret != "")
            {
                _res = string.Format("{0} and {1}", ReplaceExtSecret(_qv.MainTable.TableDefine.Table.ExtSecret, _qv.FullQueryModelName), _res);
            }

            return _res;
        }


        /// <summary>
        /// 构建子表的查询结果语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_rt"></param>
        /// <returns></returns>
        private static string CreateChildTableResult(MDModel_QueryModel _qv, MDQuery_ResultTable _rt, List<MD_SecretFieldItem> _secretFields, string _tempTableName)
        {
            List<string> _usedTables = new List<string>();
            _usedTables.Add(_qv.MainTable.TableName);
            _usedTables.Add(_rt.TableName);
            string _displayStr = CreateDisplayString(_qv, _rt, _secretFields);
            string _conditionRes = CreateConditionStringByMainID(_qv, _tempTableName);
            string _tableStr = CreateTableString(_qv, _usedTables);
            string _tableRelationStr = CreateTableRelationString(_qv, _usedTables);
            return string.Format("select {0} from {1} where ( {2} ({3})) ",
                    _displayStr, _tableStr, _tableRelationStr, _conditionRes);

        }

        /// <summary>
        /// 建立主表的查询结果语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="mDQuery_ResultTable"></param>
        /// <returns></returns>
        private static string CreateMainTableResult(MDModel_QueryModel _qv, MDQuery_ResultTable _ResultTable, List<MD_SecretFieldItem> _secretFields, string _tempTableName)
        {
            List<string> _usedTables = new List<string>();
            _usedTables.Add(_ResultTable.TableName);
            string _displayStr = CreateDisplayString(_qv, _ResultTable, _secretFields);
            string _conditionRes = CreateConditionStringByMainID(_qv, _tempTableName);
            string _tableStr = CreateTableString(_qv, _usedTables);
            return string.Format("select {0} from {1} where ( {2}) ",
                    _displayStr, _tableStr, _conditionRes);
        }

        /// <summary>
        /// 构建主表的查询结果字段语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_ResultTable"></param>
        /// <returns></returns>
        public static string CreateDisplayString(MDModel_QueryModel _qv, MDQuery_ResultTable _ResultTable, List<MD_SecretFieldItem> _secretFields)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.TableDefine.Table.MainKey));
            foreach (MDQuery_TableColumn _rc in _ResultTable.Columns)
            {
                _sql.Append(MySqlResultItemBuilder.BuildItem(_rc, _qv));
                if (_rc.SecretLevel > 0)
                {
                    // 字符串类型才进行解密
                    switch (_rc.ColumnDataType.ToUpper())
                    {
                        case "CHAR":
                        case "VARCHAR":
                        case "NVARCHAR":
                        case "NVARCHAR2":
                        case "VARCHAR2":
                            _secretFields.Add(new MD_SecretFieldItem() { TableName = _rc.TableName, FieldName = _rc.ColumnAlias });
                            break;
                    }
                }
            }
            return _sql.ToString();
        }

        /// <summary>
        /// 构建通过MAINID做条件查询表结果的条件语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <returns></returns>
        public static string CreateConditionStringByMainID(MDModel_QueryModel _qv, string _tempTableName)
        {
            string _str = string.Format("{0}.{1} in (select  pk_c from {2}) ", _qv.MainTable.TableName, _qv.MainTable.TableDefine.Table.MainKey, _tempTableName);
            return _str;
        }

        /// <summary>
        /// 查询结果是否仅有一个表
        /// </summary>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        private static bool IsOneResultTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            if (_queryRequest.ChildResultTables == null || _queryRequest.ChildResultTables.Count < 1) return true;

            foreach (MDQuery_ResultTable _table in _queryRequest.ChildResultTables)
            {
                if (!_qv.ChildTableDict.ContainsKey(_table.TableName))
                {
                    throw new Exception(string.Format("构建查询字符串时发出错误，不存在子表{0}", _table.TableName));
                }
            }
            return false;
        }

        /// <summary>
        /// 取单查询中所有使用到的表
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        public static List<string> GetSingleQueryUsedTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<string> _usedTableList = new List<string>();
            _usedTableList.Add(_qv.MainTable.TableName);
            if (_queryRequest.ChildResultTables != null)
            {
                foreach (MDQuery_ResultTable _table in _queryRequest.ChildResultTables)
                {
                    if (!_usedTableList.Contains(_table.TableName))
                    {
                        _usedTableList.Add(_table.TableName);
                    }
                }
            }

            foreach (MDQuery_ConditionItem _cItem in _queryRequest.ConditionItems)
            {
                if (!_usedTableList.Contains(_cItem.Column.TableName))
                {
                    _usedTableList.Add(_cItem.Column.TableName);
                }
            }
            return _usedTableList;
        }

        /// <summary>
        /// 取查询条件中用到的所有表
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        public static List<string> GetQueryUsedTable(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {
            List<string> _usedTableList = new List<string>();
            _usedTableList.Add(_qv.MainTable.TableName);

            foreach (MDQuery_ConditionItem _cItem in _queryRequest.ConditionItems)
            {
                if (!_usedTableList.Contains(_cItem.Column.TableName))
                {
                    _usedTableList.Add(_cItem.Column.TableName);
                }
            }
            return _usedTableList;
        }

        /// <summary>
        /// 构建单查询语句的表列表语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        public static string CreateTableString(MDModel_QueryModel _qv, List<string> _usedTableList)
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

        /// <summary>
        /// 构建单查询语句的表间关系语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_usedTables"></param>
        /// <returns></returns>
        public static string CreateTableRelationString(MDModel_QueryModel _qv, List<string> _usedTableList)
        {
            StringBuilder _ret = new StringBuilder();
            foreach (string _tname in _usedTableList)
            {
                if (_tname != _qv.MainTable.TableName)
                {
                    if (_qv.ChildTableDict.ContainsKey(_tname))
                    {
                        MDModel_Table _cTable = _qv.ChildTableDict[_tname];
                        if (_cTable != null)
                        {
                            if (_cTable.TableDefine.RelationString != string.Empty)
                            {
                                _ret.Append(string.Format("and {0} ", ReplaceExtSecret(_cTable.TableDefine.RelationString, _qv.FullQueryModelName)));
                            }
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


        /// <summary>
        /// 构建通过用户岗位权限筛选数据的语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <returns></returns>
        public static string CreateDataFilterStr(MDModel_QueryModel _qv, string _dwid)
        {
            string _res = "";
            switch (_qv.MainTable.TableDefine.Table.SecretFun.ToUpper())
            {
                case "AREAID":  //通过表的areaid进行权限处理
                    _res = string.Format(" ({0}.areaid in (select containsid from md_areamap where areaid={1})) and ",
                            _qv.MainTable.TableDefine.TableName,
                            _dwid);
                    break;
            }


            //权限扩展函数先不实现 
            //if (_qv.MainTable.TableDefine.Table.ExtSecret != "")
            //{
            //    _res = string.Format("{0} and {1}", ReplaceExtSecret(_qv.MainTable.TableDefine.Table.ExtSecret, _qv.FullQueryModelName), _res);
            //}
            return _res;
        }

        //常量替换
        public static string ReplaceExtSecret(string _secret, string _viewName)
        {
            if (_secret == null) return _secret;
            string _retstr = _secret;
            //if (SinoUserCtx.CurUser == null) return _secret;
            //_retstr = _retstr.Replace("%USERID%", SinoUserCtx.CurUser.UserID);
            //_retstr = _retstr.Replace("%USERNAME%", SinoUserCtx.CurUser.UserName);
            //_retstr = _retstr.Replace("%DWDM%", SinoUserCtx.CurUser.QxszDWDM);
            //_retstr = _retstr.Replace("%DWID%", SinoUserCtx.CurUser.QxszDWID);
            //_retstr = _retstr.Replace("%DWMC%", SinoUserCtx.CurUser.QxszDWMC);
            //_retstr = _retstr.Replace("%ROOTDWID%", ConfigFile.SytemDWRootID);
            if (_retstr.Contains("%MODELID%") && _viewName.Trim() != "")
            {
                _retstr = _retstr = _retstr.Replace("%MODELID%", GetViewIDByName(_viewName));
            }
            _retstr = ReplaceFunction(_retstr);
            _retstr = ReplaceProcedureResult(_retstr);
            return _retstr;
        }

        private const string SQL_GetViewIDByName = @"select viewid from md_view where namespace=@NS and viewname=@VN";
        private static string GetViewIDByName(string _viewName)
        {

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    string[] _fs = _viewName.Split('.');
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetViewIDByName, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@NS", _fs[0]));
                    _cmd.Parameters.Add(new MySqlParameter("@VN", _fs[1]));
                    string _s = _cmd.ExecuteScalar().ToString();
                    return _s;
                }
                catch (Exception ex)
                {
                    string _errmsg = string.Format("执行不错查询模型ID的命令出错,错误信息为:{0}!SQL语句：{1}", ex.Message, SQL_GetViewIDByName);
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    return "";
                }
            }
        }

        //函数替换
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
                _retValue = MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _sql, null); ;
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

        //存贮过程替换
        private static string ReplaceProcedureResult(string _retstr)
        {
            object _retValue;
            string _procedure;
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

        private static string ExcuteProcedure(string _pStr)
        {

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    MySqlParameter _p1 = new MySqlParameter("@1", MySqlDbType.VarChar, 1000);
                    _p1.Direction = ParameterDirection.Output;
                    string _sql = _pStr.Substring(5, _pStr.Length - 7);
                    MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _sql, _p1);

                    _txn.Commit();
                    return _p1.Value.ToString();
                }
                catch (Exception ex)
                {
                    string _errmsg = string.Format("执行命令嵌入的命令出错,错误信息为:{0}!SQL语句：{1}", ex.Message, _pStr);
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    _txn.Rollback();
                    return "";
                }
            }
            return "";
        }

        /// <summary>
        /// 建立单结果查询语句
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        private static string CreateSingleDisplayString(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, List<MD_SecretFieldItem> _secretFields)
        {
            StringBuilder _sql = new StringBuilder();
            _sql.Append(string.Format("{0}.{1} MAINID", _qv.MainTable.TableName, _qv.MainTable.TableDefine.Table.MainKey));

            foreach (MDQuery_TableColumn _rc in _queryRequest.MainResultTable.Columns)
            {
                _sql.Append(MySqlResultItemBuilder.BuildItem(_rc, _qv));
                if (_rc.SecretLevel > 0)
                {
                    // 字符串类型才进行解密
                    switch (_rc.ColumnDataType.ToUpper())
                    {
                        case "CHAR":
                        case "VARCHAR":
                        case "NVARCHAR":
                        case "NVARCHAR2":
                        case "VARCHAR2":
                            _secretFields.Add(new MD_SecretFieldItem() { TableName = _rc.TableName, FieldName = _rc.ColumnAlias });
                            break;
                    }
                }
            }
            if (_queryRequest.ChildResultTables != null)
            {
                foreach (MDQuery_ResultTable _rResultTable in _queryRequest.ChildResultTables)
                {
                    foreach (MDQuery_TableColumn _rc in _rResultTable.Columns)
                    {
                        _sql.Append(MySqlResultItemBuilder.BuildItem(_rc, _qv));
                        if (_rc.SecretLevel > 0)
                        {
                            // 字符串类型才进行解密
                            switch (_rc.ColumnDataType.ToUpper())
                            {
                                case "CHAR":
                                case "VARCHAR":
                                case "NVARCHAR":
                                case "NVARCHAR2":
                                case "VARCHAR2":
                                    _secretFields.Add(new MD_SecretFieldItem() { TableName = _rc.TableName, FieldName = _rc.ColumnAlias });
                                    break;
                            }
                        }
                    }
                }
            }
            return _sql.ToString();
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        public static string CreateConditionString(MDModel_QueryModel _qv, MDQuery_Request _queryRequest)
        {

            if (_queryRequest.ConditionExpressions == "") return "1=1";

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



        /// <summary>
        /// 处理数值
        /// </summary>
        /// <param name="i"></param>
        /// <param name="_itemIndex"></param>
        public static string ProcDigit(string conditionStr, ref int index)
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

        /// <summary>
        /// 取条件项字符串
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_queryRequest"></param>
        /// <param name="_itemIndex"></param>
        /// <returns></returns>
        private static string GetConditionStr(MDModel_QueryModel _qv, MDQuery_Request _queryRequest, string _itemIndex)
        {
            var _find = from _c in _queryRequest.ConditionItems
                        where _c.ColumnIndex == _itemIndex
                        select _c;
            if (_find == null || _find.Count() < 1)
            {
                throw new Exception(string.Format("缺少编号为{0}的条件项!", _itemIndex));
            }
            else
            {
                MDQuery_ConditionItem _cItem = _find.First();
                return MySqlConditionItemBuilder.BuildConditionItemString(_cItem, _qv);
            }
        }

        public static string RebuildGuideLineQueryString(string _queryStr, MDQuery_GuideLineParameter _gp)
        {
            string _ret = _queryStr;

            switch (_gp.Paramter.ParameterType)
            {
                case "数值型":
                    string _s3 = string.Format(" {0}", _gp.Data.ToString());
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s3);

                case "代码表":
                    string _refStr = "";
                    string[] _rtNames = _gp.Paramter.RefTableName.Split('.');
                    if (_gp.Paramter.ParameterName == "strYN")
                    {
                        _refStr = string.Format("1 = {0}", _gp.Data);
                    }
                    else
                    {
                        string _tName = (_rtNames.Length > 1) ? _rtNames[1] : _rtNames[0];
                        if (_gp.Paramter.IncludeChildren)
                        {
                            //对于树型代码表
                            if (_gp.Data.ToString() == _gp.Paramter.SelectAllCode)
                            {
                                _refStr = string.Format(" ('{0}') ", _gp.Data);
                            }
                            else
                            {
                                return string.Format("(select dm from dm_reftabledata where `type`='{0}' and FIND_IN_SET('{1}',allfathercode) )",
                                                       _tName, _gp.Data);
                            }
                        }
                        else
                        {
                            //对于非树型代码表
                            _refStr = string.Format(" '{0}' ", _gp.Data);
                        }

                    }
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _refStr);

                case "日期型":
                    string _s1 = _gp.Data.ToString();
                    if (_s1.Length == 10)
                    {
                        _s1 = string.Format(" STR_TO_DATE('{0}','%Y-%m-%d') ", _s1);
                    }
                    if (_s1.Length == 19)
                    {
                        _s1 = string.Format("STR_TO_DATE('{0}','%Y-%m-%d %H:%i:%s') ", _s1);
                    }
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s1);


                default:
                    string _s2 = string.Format(" '{0}'", _gp.Data.ToString());
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s2);
            }

            return _ret;
        }


        public static string RebuildGuideLineQueryStringByDefault(string _queryStr, MDQuery_GuideLineParameter _gp)
        {
            string _ret = _queryStr;

            switch (_gp.Paramter.ParameterType)
            {
                case "数值型":
                    string _s3 = string.Format(" {0}", _gp.Data.ToString());
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s3);

                case "代码表":
                    if (_gp.Paramter.IncludeChildren)
                    {
                        string[] _rtNames = _gp.Paramter.RefTableName.Split('.');
                        string _tName = (_rtNames.Length > 1) ? _rtNames[1] : _rtNames[0];
                        string _refStr = "";
                        if (_gp.Data.ToString() == _gp.Paramter.SelectAllCode)
                        {
                            _refStr = string.Format(" ('{0}') ", _gp.Data);
                        }
                        else
                        {
                            return string.Format("(select dm from dm_reftabledata where `type`='{0}' and FIND_IN_SET('{1}',allfathercode) )",
                                                       _tName, _gp.Data);
                        }
                        return _queryStr.Replace(_gp.Paramter.ParameterName, _refStr);
                    }
                    else
                    {
                        string _refStr = string.Format(" ('{0}') ", _gp.Data);
                        return _queryStr.Replace(_gp.Paramter.ParameterName, _refStr);
                    }

                case "日期型":
                    string _s1 = _gp.Data.ToString();
                    _s1 = ReplaceDefaultDataStr(_s1);
                    if (_s1.Length == 8)
                    {
                        _s1 = string.Format(" STR_TO_DATE('{0}','%Y%m%d') ", _s1);
                    }
                    else if (_s1.Length == 10)
                    {
                        _s1 = string.Format(" STR_TO_DATE('{0}','%Y-%m-%d') ", _s1);
                    }
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s1);


                default:
                    string _s2 = string.Format(" '{0}'", _gp.Data.ToString());
                    return _queryStr.Replace(_gp.Paramter.ParameterName, _s2);
            }

            return _ret;
        }


        public static string ReplaceDefaultDataStr(string _s1)
        {
            string _ret = _s1;
            DateTime _dt = DateTime.Now;
            _ret = _ret.Replace("#当年#", _dt.Year.ToString());
            _ret = _ret.Replace("#当月#", _dt.Month.ToString("D2"));
            _ret = _ret.Replace("#当日#", _dt.Day.ToString("D2"));

            return _ret;
        }

        public static string BuildComupteField(string ExpressionString, MDModel_Table TableDefine)
        {
            StringBuilder _ret = new StringBuilder();
            int _endIndex = 0;
            string _cacheStr = "";
            for (int _index = 0; _index < ExpressionString.Length; _index++)
            {
                Char _c = ExpressionString[_index];
                if (Char.IsLetter(_c))
                {
                    string _aliasName = GetFieldAliasName(ExpressionString, _index, out _endIndex);
                    _index = _endIndex;
                    if (TableDefine.AliasDict.ContainsKey(_aliasName))
                    {
                        MDModel_Table_Column _tc = TableDefine.AliasDict[_aliasName];
                        switch (_tc.ColumnType)
                        {
                            case QueryColumnType.TableColumn:
                                _ret.Append(string.Format("{0}.{1}", TableDefine.TableName, _tc.ColumnName));
                                break;
                            case QueryColumnType.CalculationColumn:
                            case QueryColumnType.StatisticsColumn:
                                _ret.Append(string.Format(" ({0}) ", _tc.ColumnAlgorithm));
                                break;
                        }

                    }
                    else
                    {
                        _ret.Append(_aliasName);
                    }

                }
                else
                {
                    _ret.Append(_c);
                }
            }

            return _ret.ToString();

        }

        private static string GetFieldAliasName(string ExpressionString, int _startIndex, out int _endIndex)
        {
            StringBuilder _ret = new StringBuilder();
            for (int _index = _startIndex; _index < ExpressionString.Length; _index++)
            {
                Char _c = ExpressionString[_index];
                if (Char.IsLetter(_c) || Char.IsDigit(_c))
                {
                    _ret.Append(_c);
                }
                else
                {
                    _endIndex = _index - 1;
                    return _ret.ToString();
                }

            }
            _endIndex = ExpressionString.Length;
            return _ret.ToString();
        }

        public static string BuildStatisticsField(string functionName, string tableName, MDModel_Table_Column tableColumn)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("select ");
            _sb.Append(functionName);
            _sb.Append("(");
            switch (tableColumn.ColumnType)
            {
                case QueryColumnType.TableColumn:
                    _sb.Append(string.Format("{0}.{1}", tableName, tableColumn.ColumnName));
                    break;
                case QueryColumnType.CalculationColumn:
                case QueryColumnType.StatisticsColumn:
                    _sb.Append(string.Format("{0}", tableColumn.ColumnAlgorithm));
                    break;
            }
            _sb.Append(string.Format(") from {0}  ", tableName));
            return _sb.ToString();
        }
    }
}
