using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using aehyok.Lib.MetaData.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace aehyok.Lib.MetaData.Common
{
    /// <summary>
    /// 构建查询模型
    /// </summary>
    public class MC_QueryModel
    {
        /// <summary>
        /// 建立查询模型
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public static MDModel_QueryModel CreateQuery_ModelDefine(MD_QueryModel _queryModel)
        {
            //通过查询模型的基础定义建立查询时模型定义
            MDModel_QueryModel _ret = new MDModel_QueryModel();
            _ret.NameSpaceName = _queryModel.NamespaceName;
            _ret.QueryModelName = _queryModel.QueryModelName;
            _ret.FullQueryModelName = _queryModel.FullName;
            _ret.QueryInterface = _queryModel.QueryInterface;
            _ret.DisplayName = _queryModel.DisplayTitle;
            MDModel_Table _mtable = new MDModel_Table(_queryModel.QueryModelName, _queryModel.QueryModelID, _queryModel.MainTable);
            _ret.MainTable = _mtable;
            _ret.ChildTables = new List<MDModel_Table>();
            _ret.ChildTableDict = new Dictionary<string, MDModel_Table>();
            foreach (MD_ViewTable _ctable in _queryModel.ChildTables)
            {
                MDModel_Table _ct = new MDModel_Table(_queryModel.QueryModelName, _queryModel.QueryModelID, _ctable);
                _ret.ChildTables.Add(_ct);
                _ret.ChildTableDict.Add(_ct.TableName, _ct);
            }

            //重建字段的别名
            int _aliasIndex = 0;
            _ret.MainTable.AliasDict = new Dictionary<string, MDModel_Table_Column>();
            foreach (MDModel_Table_Column _mtc in _ret.MainTable.Columns)
            {
                string _aliasName = "A" + _aliasIndex.ToString();
                _mtc.ColumnAlias = _aliasName;
                _aliasIndex++;
                _ret.MainTable.AliasDict.Add(_aliasName, _mtc);
            }

            foreach (MDModel_Table _ctable in _ret.ChildTableDict.Values)
            {
                int _childIndex = 0;
                int _cIndex = _childIndex;

                if (_ctable.TableDefine.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord)
                {
                    _cIndex = _aliasIndex;
                }
                else
                {
                    _cIndex = _childIndex;
                }
                _ctable.AliasDict = new Dictionary<string, MDModel_Table_Column>();
                foreach (MDModel_Table_Column _mtc in _ctable.Columns)
                {
                    string _aliasName = "A" + _cIndex.ToString();
                    _cIndex++;
                    _mtc.ColumnAlias = _aliasName;
                    _ctable.AliasDict.Add(_aliasName, _mtc);
                }
                if (_ctable.TableDefine.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord)
                {
                    _aliasIndex = _cIndex;
                }
            }

            return _ret;
        }

        /// <summary>
        /// 通过字段名从查询模型中取一个表中的字段定义
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_tName"></param>
        /// <param name="_cName"></param>
        /// <returns></returns>
        public static MDModel_Table_Column GetColumnDefineByName(MDModel_QueryModel _qv, string _tName, string _cName)
        {
            MDModel_Table _mt = GetTableDefine(_qv, _tName);
            if (_mt == null) return null;
            var _find = from _c in _mt.Columns
                        where _c.ColumnName == _cName
                        select _c;
            if (_find != null && _find.Count() > 0)
            {
                return _find.First();
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 通过字段别名从查询模型中取一个表中的字段定义
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_tName"></param>
        /// <param name="_aliasName"></param>
        /// <returns></returns>
        public static MDModel_Table_Column GetColumnDefineByAlias(MDModel_QueryModel _qv, string _tName, string _aliasName)
        {
            MDModel_Table _mt = GetTableDefine(_qv, _tName);
            if (_mt == null) return null;
            if (_mt.AliasDict.ContainsKey(_aliasName))
            {
                return _mt.AliasDict[_aliasName];
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// 通过字段别名从查询请求中取一个表中的字段定义
        /// </summary>
        /// <param name="_qr"></param>
        /// <param name="_tName"></param>
        /// <param name="_aliasName"></param>
        /// <returns></returns>
        public static MDQuery_TableColumn GetColumnDefineByAlias(MDQuery_Request _qr, string _tName, string _aliasName)
        {
            MDQuery_ResultTable _table = GetResultTableDefine(_qr, _tName);
            if (_table == null) return null;
            var _find = from _c in _table.Columns
                        where _c.ColumnAlias == _aliasName
                        select _c;
            if (_find != null && _find.Count() > 0)
            {
                return _find.First();
            }
            else
            {
                return null;
            }

        }


        /// <summary>
        /// 从查询模型中取一个表定义
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_tName"></param>
        /// <returns></returns>
        public static MDModel_Table GetTableDefine(MDModel_QueryModel _qv, string _tName)
        {
            if (_qv == null) return null;
            if (_qv.MainTable.TableName == _tName) return _qv.MainTable;
            if (_qv.ChildTableDict.ContainsKey(_tName))
            {
                return _qv.ChildTableDict[_tName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从查询请求中取一个表名
        /// </summary>
        /// <param name="_qr"></param>
        /// <param name="_tName"></param>
        /// <returns></returns>
        private static MDQuery_ResultTable GetResultTableDefine(MDQuery_Request _qr, string _tName)
        {
            if (_qr.MainResultTable.TableName == _tName) return _qr.MainResultTable;
            var _find = from _t in _qr.ChildResultTables
                        where _t.TableName == _tName
                        select _t;
            if (_find != null && _find.Count() > 0)
            {
                return _find.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将查询结果集中的表建立表关联
        /// </summary>
        /// <param name="_qv"></param>
        /// <param name="_ds"></param>
        public static void CreateDataRelation(MDModel_QueryModel _qv, DataSet _ds)
        {
            DataTable _mtable = _ds.Tables[_qv.MainTable.TableName];
            _mtable.CaseSensitive = true;
            CheckMainKey(_mtable, "mainid");
            _mtable.Columns["mainid"].Unique = true;
            _mtable.PrimaryKey = new DataColumn[] { _mtable.Columns["mainid"] };
            for (int i = 0; i < _ds.Tables.Count; i++)
            {
                DataTable _dt = _ds.Tables[i];
                _dt.CaseSensitive = true;
                if (_dt.TableName != _qv.MainTable.TableName)
                {
                    MDModel_Table _ctable = MC_QueryModel.GetTableDefine(_qv, _dt.TableName);
                    if (_ctable != null)
                    {
                        string _rname = _ctable.TableDefine.DisplayTitle;
                        _ds.Relations.Add(_rname, _mtable.Columns["mainid"], _dt.Columns["mainid"]);
                    }
                }
            }
        }

        private static void CheckMainKey(DataTable _table, string _mainKey)
        {
            DataTable _dt = new DataTable();
            _dt.CaseSensitive = true;
            DataColumn _newdt = _dt.Columns.Add(_mainKey, _table.Columns[_mainKey].DataType);
            _newdt.Unique = true;

            foreach (DataRow _dr in _table.Rows)
            {
                try
                {
                    DataRow _newrow = _dt.NewRow();
                    _newrow[_mainKey] = _dr[_mainKey];
                    _dt.Rows.Add(_newrow);
                }
                catch (Exception e)
                {
                    string _msg = string.Format("{1} 在处理数据时,发现主表的主键存在重复:{0}", e.Message, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    string _fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\ClientErrorLog.log";
                    StreamWriter _fs;
                    if (File.Exists(_fullPath))
                    {
                        _fs = File.AppendText(_fullPath);
                    }
                    else
                    {
                        _fs = File.CreateText(_fullPath);
                    }
                    _fs.WriteLine(_msg);
                    _fs.Close();
                    throw e;
                }
            }
        }

        /// <summary>
        /// 从查询结果中建立全部展示数据
        /// </summary>
        /// <param name="QueryRequest"></param>
        /// <param name="QueryModel"></param>
        /// <param name="QueryResultData"></param>
        /// <returns></returns>
        public static DataTable CreateFullComboData(MDQuery_Request QueryRequest, MDModel_QueryModel QueryModel, DataSet QueryResultData)
        {
            DataTable _ret = new DataTable();
            //建TableColumn 
            CreateFullComboTableColumn(QueryRequest, QueryModel, _ret);

            //填充数据
            DataTable _mtableData = QueryResultData.Tables[QueryRequest.MainResultTable.TableName];
            bool _isOddRow = false;
            foreach (DataRow _mainRow in _mtableData.Rows)
            {
                _isOddRow = !_isOddRow;
                int _intRows = 1;
                int ChildTableCount = _mtableData.ChildRelations.Count;
                for (int i = 0; i < ChildTableCount; i++)
                {
                    string _rname = _mtableData.ChildRelations[i].RelationName;
                    int _ChildCount = _mainRow.GetChildRows(_rname).Length;
                    _intRows = (_ChildCount > _intRows) ? _ChildCount : _intRows;

                }

                for (int _line = 0; _line < _intRows; _line++)
                {
                    DataRow _newRow = _ret.NewRow();
                    _newRow["ROW_MAINID"] = _mainRow["mainid"].ToString();
                    _newRow["IS_ODD_ROW"] = _isOddRow;
                    #region 将主表的记录数据加入新行
                    foreach (MDQuery_TableColumn _rc in QueryRequest.MainResultTable.Columns)
                    {
                        string FieldName = _rc.TableName + "_" + _rc.ColumnName;
                        _newRow[FieldName] = _mainRow[_rc.ColumnAlias];
                    }
                    #endregion

                    #region 依次将子表的数据加入新行
                    for (int i = 0; i < ChildTableCount; i++)
                    {
                        string _rname = _mtableData.ChildRelations[i].RelationName;
                        DataRow[] ChildRows = _mainRow.GetChildRows(_rname);
                        int _ChildCount = ChildRows.Length;
                        if (_ChildCount > 0)
                        {
                            int _CurrentLine = 0;
                            if (_line < _ChildCount)
                            {
                                _CurrentLine = _line;
                            }
                            else
                            {
                                _CurrentLine = _ChildCount - 1;
                            }
                            DataRow _CRow = ChildRows[_CurrentLine];
                            var _find = from _t in QueryRequest.ChildResultTables
                                        where _t.TableName == _mtableData.ChildRelations[i].ChildTable.TableName
                                        select _t;
                            MDQuery_ResultTable _resultChildTable = _find.First();
                            foreach (MDQuery_TableColumn _crc in _resultChildTable.Columns)
                            {
                                string cFieldName = _crc.TableName + "_" + _crc.ColumnName;
                                _newRow[cFieldName] = _CRow[_crc.ColumnAlias];
                            }
                        }

                    }
                    #endregion

                    _ret.Rows.Add(_newRow);
                }

            }
            return _ret;
        }

        public static DataTable CreateFullComboData(MDCompare_Request QueryRequest, MDModel_QueryModel QueryModel, DataSet QueryResultData)
        {
            DataTable _ret = new DataTable();
            //建TableColumn 
            CreateFullComboTableColumn(QueryRequest as MDQuery_Request, QueryModel, _ret);
            foreach (DataColumn _dc in QueryResultData.Tables["EXCELRESULTDATA"].Columns)
            {
                if (_dc.ColumnName != "mainid")
                {
                    string FieldName = "EXCEL_" + _dc.ColumnName;
                    DataColumn _newdc = new DataColumn(FieldName);
                    _newdc.Caption = _dc.ColumnName;
                    _ret.Columns.Add(_newdc);
                }
            }
            //填充数据
            DataTable _mtableData = QueryResultData.Tables[QueryRequest.MainResultTable.TableName];
            bool _isOddRow = false;
            foreach (DataRow _mainRow in _mtableData.Rows)
            {
                _isOddRow = !_isOddRow;
                int _intRows = 1;
                int ChildTableCount = _mtableData.ChildRelations.Count;
                for (int i = 0; i < ChildTableCount; i++)
                {
                    string _rname = _mtableData.ChildRelations[i].RelationName;
                    int _ChildCount = _mainRow.GetChildRows(_rname).Length;
                    _intRows = (_ChildCount > _intRows) ? _ChildCount : _intRows;

                }

                for (int _line = 0; _line < _intRows; _line++)
                {
                    DataRow _newRow = _ret.NewRow();
                    _newRow["ROW_MAINID"] = _mainRow["mainid"].ToString();
                    _newRow["IS_ODD_ROW"] = _isOddRow;
                    #region 将主表的记录数据加入新行
                    foreach (MDQuery_TableColumn _rc in QueryRequest.MainResultTable.Columns)
                    {
                        string FieldName = _rc.TableName + "_" + _rc.ColumnName;
                        _newRow[FieldName] = _mainRow[_rc.ColumnAlias];
                    }
                    #endregion

                    #region 依次将子表的数据加入新行
                    for (int i = 0; i < ChildTableCount; i++)
                    {
                        string _rname = _mtableData.ChildRelations[i].RelationName;

                        DataRow[] ChildRows = _mainRow.GetChildRows(_rname);
                        int _ChildCount = ChildRows.Length;
                        if (_ChildCount > 0)
                        {
                            int _CurrentLine = 0;
                            if (_line < _ChildCount)
                            {
                                _CurrentLine = _line;
                            }
                            else
                            {
                                _CurrentLine = _ChildCount - 1;
                            }
                            DataRow _CRow = ChildRows[_CurrentLine];
                            if (_rname != "比对的EXCEL文件")
                            {
                                var _find = from _t in QueryRequest.ChildResultTables
                                            where _t.TableName == _mtableData.ChildRelations[i].ChildTable.TableName
                                            select _t;
                                MDQuery_ResultTable _resultChildTable = _find.First();
                                foreach (MDQuery_TableColumn _crc in _resultChildTable.Columns)
                                {
                                    string cFieldName = _crc.TableName + "_" + _crc.ColumnName;
                                    _newRow[cFieldName] = _CRow[_crc.ColumnAlias];
                                }
                            }
                            else
                            {
                                foreach (DataColumn _dc in QueryResultData.Tables["EXCELRESULTDATA"].Columns)
                                {
                                    if (_dc.ColumnName != "mainid")
                                    {
                                        string cFieldName = "EXCEL_" + _dc.ColumnName;
                                        _newRow[cFieldName] = _CRow[_dc.ColumnName];
                                    }
                                }
                            }
                        }

                    }
                    #endregion

                    _ret.Rows.Add(_newRow);
                }

            }
            return _ret;
        }

        /// <summary>
        /// 建立平铺数据表字段
        /// </summary>
        /// <param name="QueryRequest"></param>
        /// <param name="QueryModel"></param>
        /// <param name="_ret"></param>
        private static void CreateFullComboTableColumn(MDQuery_Request QueryRequest, MDModel_QueryModel QueryModel, DataTable _ret)
        {
            _ret.Columns.Clear();

            MDQuery_ResultTable _mtable = QueryRequest.MainResultTable;
            //通过表定义建字段
            CreateComboTableColumn(QueryModel, _mtable, _ret);
            _ret.Columns.Add(new DataColumn("ROW_MAINID", typeof(string)));
            _ret.Columns.Add(new DataColumn("IS_ODD_ROW", typeof(bool)));
            foreach (MDQuery_ResultTable _ctable in QueryRequest.ChildResultTables)
            {
                CreateComboTableColumn(QueryModel, _ctable, _ret);
            }
        }



        private static void CreateComboTableColumn(MDModel_QueryModel QueryModel, MDQuery_ResultTable _resultTable, DataTable _ret)
        {

            MDModel_Table _tableDefine = MC_QueryModel.GetTableDefine(QueryModel, _resultTable.TableName);

            foreach (MDQuery_TableColumn _rc in _resultTable.Columns)
            {
                string FieldName = _rc.TableName + "_" + _rc.ColumnName;
                DataColumn _dc = new DataColumn(FieldName);
                _dc.Caption = _rc.ColumnTitle;
                bool _isRefTable = (_rc.RefDMB == null || _rc.RefDMB == "") ? false : true;
                _dc.DataType = CreateTypeByMeta(_rc.ColumnDataType, _isRefTable);
                _ret.Columns.Add(_dc);
            }
        }

        private static Type CreateTypeByMeta(string _typeDefine, bool _isRefTable)
        {
            if (_isRefTable) return typeof(string);
            switch (_typeDefine.ToUpper())
            {
                case "DATE":     //日期型
                case "DATETIME":
                case "TIMESTAMP":
                    return typeof(DateTime);

                case "NUMBER": //数值型
                case "INT":
                case "TINYINT":
                    return typeof(long);

                default:
                    return typeof(string);

            }

        }


    }
}
