using MySql.Data.MySqlClient;
using aehyok.Lib;
using aehyok.Lib.MetaData.Common;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using aehyok.Lib.MetaData.RefCode;
using aehyok.Lib.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using aehyok.Core.MySql;

namespace aehyok.Core.MySqlDataAccessor
{
    public class MyDA_MetaDataQuery : IMetaDataQuery
    {
        private MyDA_MetaDataManager magFactroy = new MyDA_MetaDataManager();
        public static Dictionary<string, MDModel_QueryModel> ModelLib = new Dictionary<string, MDModel_QueryModel>();
        public static Dictionary<string, MD_QueryModel> QueryModelCache = new Dictionary<string, MD_QueryModel>();

        readonly IEncryptionService enService;


        public MyDA_MetaDataQuery(IEncryptionService enService)
        {
            this.enService = enService;
        }

        /// <summary>
        /// 保存用户token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="code"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<bool> SaveUserToken(string userName, string code, string token)
        {
            string sql_select = string.Format("select id from xt_usertoken where username='{0}' and dwdm='{1}' ", userName, code);
            string sql_insert = @"insert into xt_usertoken (id,username,dwdm,token,updatetime) values (@id,@username,@dwdm,@token,now())";
            string sql_update = @"update xt_usertoken set dwdm=@dwdm,token=@token,updatetime=now() where id=@id ";

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, sql_select);
            var id = "";
            while (await dr.ReadAsync())
            { id = dr.GetString(0); }
            dr.Close();

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    // 插入
                    if (string.IsNullOrEmpty(id))
                    {
                        MySqlCommand _cmd = new MySqlCommand(sql_insert, cn);
                        _cmd.Parameters.Add(new MySqlParameter("@id", Guid.NewGuid().ToString()));
                        _cmd.Parameters.Add(new MySqlParameter("@username", userName));
                        _cmd.Parameters.Add(new MySqlParameter("@dwdm", code));
                        _cmd.Parameters.Add(new MySqlParameter("@token", token));
                        await _cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                    // 更新
                    else
                    {
                        MySqlCommand _cmd = new MySqlCommand(sql_update, cn);
                        _cmd.Parameters.Add(new MySqlParameter("@dwdm", code));
                        _cmd.Parameters.Add(new MySqlParameter("@token", token));
                        _cmd.Parameters.Add(new MySqlParameter("@id", id));
                        await _cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    string _msg = string.Format("保存用户的[{0}]的TOKEN信息时发生错误，错误信息：{1} ", userName, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }

        public async Task<string> GetCurrentUserName(string Token)
        {
            string _ret = "";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    string sql_select = string.Format("select username from xt_usertoken where token='{0}'", Token);
                    MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, sql_select);
                    while (await dr.ReadAsync())
                    { _ret = dr.GetString(0); }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    string _msg = string.Format("通过TOKEN[{0}]获取UserName发生错误，错误信息：{1} ", Token, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                }
                cn.Close();
            }
            return _ret;
        }

        public async Task<string> GetCodeByToken(string token)
        {
            string _ret = "";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    string sql_select = string.Format("select dwdm from xt_usertoken where token='{0}'", token);
                    MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, sql_select);
                    while (await dr.ReadAsync())
                    { _ret = dr.GetString(0); }
                    dr.Close();
                }
                catch (Exception ex)
                {
                    string _msg = string.Format("通过TOKEN[{0}]获取CODE发生错误，错误信息：{1} ", token, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                }
                cn.Close();
            }
            return _ret;
        }


        public Task<string> AddNewQueryTask(string _taskName, MDQuery_Request _queryRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CancleQueryTask(string _taskID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeDataCheckRule(string _ruleID, string _gzsf)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeQueryTaskRequestTime(string TaskID, DateTime RequestTime)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearQueryTask(string _taskID)
        {
            throw new NotImplementedException();
        }

        public Task<DataSet> CompareData(MDCompare_Request compareRequest, DataTable srcData)
        {
            throw new NotImplementedException();
        }

        public Task<MD_PAnalizeProject> CreateNewPASpace(string PersonAnalizeSapceName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_DelComputeFieldDefine = "delete from md_computecolumn where ID=@ID";
        public async Task<bool> DelComputeFieldDefine(string ColumnName)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {

                    MySqlCommand _cmd = new MySqlCommand(SQL_DelComputeFieldDefine, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", ColumnName));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;

                }
                catch (Exception ex)
                {
                    string _msg = string.Format("删除保存的计算项字段[{0}]时发生错误，错误信息：{1} ", ColumnName, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }

        public Task<bool> DelDataCheckRule(string _ruleID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteDataCheckMsg(string _ggjlid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DelSavedQuery(string _savedID)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetAttachFileBytes(string IndexString, string FieldName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetAttachFileName(string IndexString, string FieldName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetCanUsePanalizeSet()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MDQuery_ChildTableRowCount>> GetChildRowCountList(string _queryModelName, string _keyid)
        {
            List<MDQuery_ChildTableRowCount> _ret = new List<MDQuery_ChildTableRowCount>();
            //  SinoSZStopWatch _watch = new SinoSZStopWatch();
            try
            {
                MDModel_QueryModel _model = await GetMDQueryModelDefine2(_queryModelName);
                // _watch.Tick("取查询模型用时");

                if (_model != null)
                {
                    foreach (MDModel_Table _ctable in _model.ChildTableDict.Values)
                    {
                        int _startTime = Environment.TickCount;
                        string _sqlStr = MySqlQueryBuilder.GetChildTableCount(_model.MainTable, _ctable, _keyid);
                        int _count = Convert.ToInt32(await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _sqlStr, null));
                        int _endTime = Environment.TickCount;
                        MysqlLogWriter.WriteQueryLog(_sqlStr, _endTime - _startTime, "1");
                        MDQuery_ChildTableRowCount _item = new MDQuery_ChildTableRowCount(_ctable.TableName, _count);
                        _ret.Add(_item);
                    }
                }
            }
            catch (Exception ex)
            {
                string _msg = string.Format("获取子表记录列表ID:{0}发生错误，错误信息：{1} ", _keyid, ex.Message);
                MysqlLogWriter.WriteSystemLog(_msg, "ERROR");

            }
            // _watch.Tick("取子表记录用时");
            return _ret;
        }

        public async Task<int> GetChildTableCountByKey(string _queryModelName, string _childTableName, string _keyid)
        {
            MDModel_QueryModel _model = await GetMDQueryModelDefine2(_queryModelName);
            if (_model != null)
            {
                if (!_model.ChildTableDict.ContainsKey(_childTableName)) return 0;
                MDModel_Table _childTable = _model.ChildTableDict[_childTableName];
                string _sqlStr = MySqlQueryBuilder.GetChildTableCount(_model.MainTable, _childTable, _keyid);
                int _count = Convert.ToInt32(await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _sqlStr, null));

                return _count;

            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过主表主键键取子表记录
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_childTableName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        public async Task<DataTable> GetChildTableDataByKey(string _queryModelName, string _childTableName, string _keyid)
        {
            MDModel_QueryModel _model = await GetMDQueryModelDefine2(_queryModelName);
            string _sqlStr = "";

            if (_model != null)
            {
                if (!_model.ChildTableDict.ContainsKey(_childTableName)) return null;
                MDModel_Table _childTable = _model.ChildTableDict[_childTableName];

                DataTable _ret = new DataTable();
                _ret.TableName = _childTableName;
                using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
                {
                    try
                    {
                        List<MD_SecretFieldItem> _secretFields = new List<MD_SecretFieldItem>();
                        _sqlStr = MySqlQueryBuilder.GetChildTableData(_model.MainTable, _childTable, _keyid, _secretFields);
                        _ret = await MysqlDBHelper.FillDataTable(cn, null, CommandType.Text, _sqlStr);
                        cn.Close();
                        //处理加密字段 StrUtils.Decrypt()
                        foreach (MD_SecretFieldItem _sf in _secretFields)
                        {
                            DecryptResultData(_sf, _ret);
                        }
                    }
                    catch (Exception e)
                    {
                        string _errStr = string.Format("通过主表主键键取子表记录时发生错误! QueryModelName={0} ChildTableName={1} Key={2} \n\r ErrorMsg:{4} \n\r SQL: {3} ",
                                _queryModelName, _childTableName, _keyid, _sqlStr, e.Message);
                        MysqlLogWriter.WriteSystemLog(_errStr, "ERROR");
                        throw e;
                    }
                }
                return _ret;

            }
            else
            {
                return null;
            }
        }

        public Task<List<MD_ConceptGroup>> GetConceptList()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetDataCheckBoardList()
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetDataCheckInfo(string _modelName, string _mainKey, ref string SHID)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDataCheckInfoJLID(string QueryModelName, string _mainkeyId, string _level, ref string SHID)
        {
            throw new NotImplementedException();
        }

        public Task<MD_DataCheckMsg> GetDataCheckMsg(string _ggjlid)
        {
            throw new NotImplementedException();
        }

        public Task<List<MD_CheckRule>> GetDataCheckRuleDefine(string QueryModelName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetDataCheckWHXH(string _tableName, string _mainColumn, string _mainKey)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetFLWSFileBytes(string _indexString, string FieldName)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetFLWSFileName(string _indexString, string FieldName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetFunctionList = @"select FUNID,FUNCTIONNAME,DISPLAYNAME,DESCRIPTION,RESULTTYPE,TYPE,PARAMETA
                                                    from  md_function where TYPE=@TYPE";
        public async Task<List<MD_FUNCTION>> GetFunctionList(int _type)
        {
            List<MD_FUNCTION> _ret = new List<MD_FUNCTION>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetFunctionList, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@TYPE", Convert.ToInt32(_type)));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            string _meta = _dr.IsDBNull(6) ? "" : _dr.GetString(6);
                            MD_FUNCTION _item = new MD_FUNCTION(
                                    _dr.IsDBNull(0) ? "" : _dr.GetInt64(0).ToString(),
                                    _dr.IsDBNull(1) ? "" : _dr.GetString(1),
                                    _dr.IsDBNull(2) ? "" : _dr.GetString(2),
                                    _dr.IsDBNull(3) ? "" : _dr.GetString(3),
                                    _dr.IsDBNull(4) ? "" : _dr.GetString(4),
                                    _dr.IsDBNull(5) ? "" : _dr.GetInt32(5).ToString()
                            );
                            _item.ParamList = new List<string>();
                            _item.ParamTypeDict = new Dictionary<string, string>();
                            if (!string.IsNullOrEmpty(_meta))
                            {
                                string[] _strs = _meta.Split(',');
                                foreach (string _s in _strs)
                                {
                                    string _pname = StrUtils.GetMetaByName2("name", _s);
                                    string _ptype = StrUtils.GetMetaByName2("type", _s);
                                    _item.ParamList.Add(_pname);
                                    _item.ParamTypeDict.Add(_pname, _ptype);
                                }
                            }
                            _ret.Add(_item);
                        }
                    }
                    cn.Close();
                }
                catch (Exception e)
                {
                    cn.Close();
                    throw e;
                }
            }
            return _ret;
        }

        public async Task<MD_GuideLine> GetGuideLineByID(string _guideLineID)
        {
            MD_GuideLine _ret = null;

            StringBuilder _sql = new StringBuilder();
            _sql.Append(" select ID,ZBMC,ZBZT,ZBSF, ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH,ZBSM ");
            _sql.Append(" from tj_zdyzbdyb where ID=@ID ");
            MySqlParameter[] _param = {
                  new MySqlParameter("@ID",MySqlDbType.Int64)
            };
            _param[0].Value = Convert.ToInt64(_guideLineID);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql.ToString(), _param);

            while (await dr.ReadAsync())
            {
                _ret = new MD_GuideLine(
                    dr.GetInt64(0).ToString(),
                    dr.IsDBNull(1) ? "" : dr.GetString(1),
                    dr.IsDBNull(2) ? "" : dr.GetString(2),
                    dr.IsDBNull(3) ? "" : dr.GetString(3),
                    dr.IsDBNull(4) ? "" : dr.GetString(4),
                    dr.IsDBNull(5) ? "0" : dr.GetInt64(5).ToString(),
                    dr.IsDBNull(6) ? "" : dr.GetString(6),
                    dr.IsDBNull(7) ? "" : dr.GetString(7),
                    dr.IsDBNull(8) ? 0 : dr.GetInt32(8),
                    dr.IsDBNull(9) ? "" : dr.GetString(9)
               );
            }
            dr.Close();
            return _ret;
        }

        public async Task<List<MD_GuideLine>> GetGuideLineListByFatherID(string _fatherID)
        {
            List<MD_GuideLine> _ret = new List<MD_GuideLine>();
            StringBuilder _sql = new StringBuilder();
            _sql.Append(" select ID,ZBMC,ZBZT,ZBSF, ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH,ZBSM ");
            _sql.Append(" from tj_zdyzbdyb where FID=@FID order by XSXH asc");
            MySqlParameter[] _param = {
                 new MySqlParameter("@FID",MySqlDbType.Int64)
            };
            _param[0].Value = Convert.ToInt64(_fatherID);
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql.ToString(), _param);

            while (await dr.ReadAsync())
            {
                MD_GuideLine _vt = new MD_GuideLine(
                    dr.GetInt64(0).ToString(),
                    dr.IsDBNull(1) ? "" : dr.GetString(1),
                    dr.IsDBNull(2) ? "" : dr.GetString(2),
                    dr.IsDBNull(3) ? "" : dr.GetString(3),
                    dr.IsDBNull(4) ? "" : dr.GetString(4),
                    dr.IsDBNull(5) ? "0" : dr.GetInt64(5).ToString(),
                    dr.IsDBNull(6) ? "" : dr.GetString(6),
                    dr.IsDBNull(7) ? "" : dr.GetString(7),
                    dr.IsDBNull(8) ? 0 : dr.GetInt32(8),
                    dr.IsDBNull(9) ? "" : dr.GetString(9)
                 );
                _ret.Add(_vt);
            }
            dr.Close();
            return _ret;
        }

        public async Task<MD_GuideLine_ParamSetting> GetGuideLineParamSetting(string _guideLineID)
        {
            //MD_GuideLine_ParamSetting _ret = null;
            //using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            //{
            //    try
            //    {
            //        //判断本单位参数设置记录是否存在
            //        _ret = Ora_GuideLineParamSetting.GetCurrentPostRecord(_guideLineID, cn);
            //        if (_ret == null) _ret = Ora_GuideLineParamSetting.GetDefaultRecord(_guideLineID, cn);
            //    }
            //    catch (Exception e)
            //    {
            //        string _errmsg = string.Format("取指定指标[{1}]在本单位[{2}]的参数设置时出错,错误信息为:{0}!\n",
            //           e.Message, _guideLineID, "000");
            //        MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");

            //    }
            //}
            //return _ret;

            throw new NotImplementedException();
        }

        public Task<MD_InputModel_ColumnGroup> GetInputGroupByID(string InputGroupID)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetInputModelBlankData(string _initGuideLineID, string _getBlankGuideLineID, List<MDQuery_GuideLineParameter> _params)
        {
            throw new NotImplementedException();
        }

        public Task<MD_InputModel> GetInputModelByID(string InputModelID)
        {
            throw new NotImplementedException();
        }

        public Task<MD_InputModel> GetInputModelByName(string _modelName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过主键取主表记录
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_mainTableName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        public async Task<DataTable> GetMainTableDataByKey(string _queryModelName, string _mainTableName, string _keyid)
        {
            string _sqlStr = "";
            MDModel_QueryModel _model = await GetMDQueryModelDefine2(_queryModelName);
            if (_model != null)
            {
                MDModel_Table _maintable = _model.MainTable;
                if (_maintable.TableName != _mainTableName) return null;

                DataTable _ret = new DataTable();
                _ret.TableName = _mainTableName;
                using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
                {
                    try
                    {
                        List<MD_SecretFieldItem> _secretFields = new List<MD_SecretFieldItem>();
                        _sqlStr = MySqlQueryBuilder.GetMainTableData(_maintable, _keyid, _secretFields);
                        _ret = await MysqlDBHelper.FillDataTable(cn, null, CommandType.Text, _sqlStr);
                        cn.Close();

                        //处理加密字段 StrUtils.Decrypt()
                        foreach (MD_SecretFieldItem _sf in _secretFields)
                        {
                            DecryptResultData(_sf, _ret);
                        }
                    }
                    catch (Exception e)
                    {
                        string _errStr = string.Format("通过主键取主表记录时发生错误! QueryModelName={0} MainTableName={1} Key={2} \n\r ErrorMsg:{4} \n\r SQL: {3} ",
                                _queryModelName, _mainTableName, _keyid, _sqlStr, e.Message);
                        MysqlLogWriter.WriteSystemLog(_errStr, "ERROR");
                        throw e;
                    }
                }
                return _ret;

            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetMainTableKeyByChildKey(MDSearch_Column _childColumn, string _childKey)
        {
            MDModel_QueryModel _model = await GetMDQueryModelDefine2(_childColumn.QueryModel.FullName);
            if (_model != null)
            {
                if (_model.MainTable.TableName == _childColumn.TableName)
                {
                    return _childKey;
                }
                else
                {
                    if (!_model.ChildTableDict.ContainsKey(_childColumn.TableName)) return null;
                    MDModel_Table _childTable = _model.ChildTableDict[_childColumn.TableName];
                    string _sqlStr = MySqlQueryBuilder.GetMainTableKeyByChildKey(_model.MainTable, _childTable, _childKey);
                    MySqlParameter[] _param = { new MySqlParameter(":CHILDKEY", MySqlDbType.VarChar) };
                    _param[0].Value = _childKey;
                    return MysqlDBHelper.ExecuteScalar(MysqlDBHelper.queryString, CommandType.Text, _sqlStr, _param).ToString();
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<string> GetMainTableKeyByColumnCondition(string _modelName, string _columnName, string _data)
        {
            MDModel_QueryModel _model = await GetMDQueryModelDefine2(_modelName);
            if (_model != null)
            {
                if (_model.MainTable.TableDefine.Table.MainKey == _columnName)
                {
                    return _data;
                }
                else
                {
                    string _sqlStr = MySqlQueryBuilder.GetMainTableKeyByColumnCondition(_model.MainTable, _columnName, _data);

                    return MysqlDBHelper.ExecuteScalar(MysqlDBHelper.queryString, CommandType.Text, _sqlStr).ToString();
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 取查询模型定义
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <returns></returns>
        public async Task<MD_QueryModel> GetMDQueryModelDefine(string QueryModelName)
        {
            if (!QueryModelCache.ContainsKey(QueryModelName))
            {
                MD_QueryModel _qv = await magFactroy.GetQueryModelByName(QueryModelName);
                _qv.MainTable = await magFactroy.GetMainTableOfQueryModel(_qv);
                _qv.ChildTables = await magFactroy.GetChildTableOfQueryModel(_qv);

                QueryModelCache.Add(QueryModelName, _qv);
            }
            return QueryModelCache[QueryModelName];
        }

        /// <summary>
        /// 取查询模型定义2
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public async Task<MDModel_QueryModel> GetMDQueryModelDefine2(string _queryModel)
        {

            if (!ModelLib.ContainsKey(_queryModel))
            {
                try
                {
                    MD_QueryModel _qv = await magFactroy.GetQueryModelByName(_queryModel);
                    _qv.MainTable = await magFactroy.GetMainTableOfQueryModel(_qv);
                    _qv.ChildTables = await magFactroy.GetChildTableOfQueryModel(_qv);
                    ModelLib.Add(_queryModel, MC_QueryModel.CreateQuery_ModelDefine(_qv));
                }
                catch (Exception ex)
                {
                    MysqlLogWriter.WriteSystemLog(string.Format("取查询模型定义[{0}]失败!{1}", _queryModel, ex.Message), "ERROR");
                    return null;
                }
            }
            WriteUserActionLog("取查询模型定义", string.Format("取查询模型[{0}]成功！", _queryModel));
            return ModelLib[_queryModel];
        }

        public Task<List<MD_PAnalizeProject>> GetPAProjectOfUser()
        {
            throw new NotImplementedException();
        }
        private const string SQL_GetPersonSavedComputField = @"select ID,COLUMNAME,COLUMNEXP,COLUMNDES,ISPUBLIC,VIEWNAME,TABLENAME,COLUMNMETA,USERID,CREATEDATE
                                                                 from  md_computecolumn where VIEWNAME= @VIEWNAME and TABLENAME=@TABLENAME and ISPUBLIC =0";
        public async Task<List<MDQuery_ComputeColumnDefine>> GetPersonSavedComputField(string ModelName, string TableName)
        {
            List<MDQuery_ComputeColumnDefine> _ret = new List<MDQuery_ComputeColumnDefine>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetPersonSavedComputField, cn);
                    _cmd.Parameters.AddWithValue("@VIEWNAME", ModelName);
                    _cmd.Parameters.AddWithValue("@TABLENAME", TableName);
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            string _meta = _dr.IsDBNull(7) ? "" : _dr.GetString(7);
                            MDQuery_ComputeColumnDefine _item = new MDQuery_ComputeColumnDefine(
                                    _dr.IsDBNull(0) ? "" : _dr.GetString(0),
                                    _dr.IsDBNull(1) ? "" : _dr.GetString(1),
                                    _dr.IsDBNull(2) ? "" : _dr.GetString(2),
                                    _dr.IsDBNull(3) ? "" : _dr.GetString(3),
                                    _dr.IsDBNull(4) ? false : (_dr.GetDecimal(4) > 0),
                                    _dr.IsDBNull(5) ? "" : _dr.GetString(5),
                                    _dr.IsDBNull(6) ? "" : _dr.GetString(6),
                                    StrUtils.GetMetaByName2("ResultDataType", _meta),
                                    StrUtils.GetMetaByName2("Expression", _meta),
                                    _dr.IsDBNull(9) ? DateTime.MinValue : _dr.GetDateTime(9)
                            );
                            _ret.Add(_item);
                        }
                    }
                    cn.Close();
                }
                catch (Exception e)
                {
                    cn.Close();
                    throw e;
                }
            }
            return _ret;
        }

        public async Task<string> GetQueryModelDescription(string _modelName)
        {
            string _ret = "";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    string[] _strs = _modelName.Split('.');
                    string _sql = "select description from md_view where namespace=@ns and viewname = @nv";
                    MySqlCommand _cmd = new MySqlCommand(_sql, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ns", _strs[0]));
                    _cmd.Parameters.Add(new MySqlParameter("@nv", _strs[1]));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            _ret = _dr.IsDBNull(0) ? "" : _dr.GetString(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                cn.Close();
            }
            return _ret;
        }

        public async Task<List<MD_QueryModel>> GetQueryModels(string _queryModelNames)
        {
            MyDA_MetaDataManager _of = new MyDA_MetaDataManager();
            List<MD_QueryModel> _ret = new List<MD_QueryModel>();
            foreach (string _qmName in _queryModelNames.Split(','))
            {
                MD_QueryModel _qv = await _of.GetQueryModelByName(_qmName);
                _ret.Add(_qv);
            }
            return _ret;
        }

        public Task<List<MDQuery_Task>> GetQueryTaskList()
        {
            throw new NotImplementedException();
        }

        public Task<MDQuery_Request> GetQueryTaskRequestContext(string _taskID)
        {
            throw new NotImplementedException();
        }

        public Task<MDQuery_Task> GetQueryTaskStateByID(string _taskID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MD_GuideLine>> GetRootGuideLineList(string rootGuideLines)
        {
            List<MD_GuideLine> _ret = new List<MD_GuideLine>();

            StringBuilder _sql = new StringBuilder();
            _sql.Append(" select ID,ZBMC,ZBZT,ZBSF, ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH, ZBSM ");
            _sql.Append(" from tj_zdyzbdyb where ID in ( ");
            _sql.Append(rootGuideLines);
            _sql.Append(" ) ");
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql.ToString());
            while (await dr.ReadAsync())
            {
                MD_GuideLine _vt = new MD_GuideLine(
                        dr.GetInt64(0).ToString(),
                        dr.IsDBNull(1) ? "" : dr.GetString(1),
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? "" : dr.GetString(3),
                        dr.IsDBNull(4) ? "" : dr.GetString(4),
                        dr.IsDBNull(5) ? "0" : dr.GetInt64(5).ToString(),
                        dr.IsDBNull(6) ? "" : dr.GetString(6),
                        dr.IsDBNull(7) ? "" : dr.GetString(7),
                        dr.IsDBNull(8) ? 0 : dr.GetInt32(8),
                        dr.IsDBNull(9) ? "" : dr.GetString(9)
                        );
                _ret.Add(_vt);
            }
            dr.Close();

            return _ret;
        }

        public Task<DataTable> GetRuleList(string QueryModelName)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetSaveQueryList(string QueryModelName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MDSearch_ResultDataIndex>> GetSearchResult(string _searchText, int _searchType, MDSearch_Column _sc)
        {
            string _sqlConcept = "";
            string _stext = _searchText;
            string _conditionStr = "";
            var dwid = await enService.GetCodeByToken();

            MDModel_QueryModel _qv = await GetMDQueryModelDefine2(_sc.QueryModel.FullName);
            List<MDSearch_ResultDataIndex> _ret = new List<MDSearch_ResultDataIndex>();
            string _filterStr = MySqlQueryBuilder.CreateDataFilterStr(_qv, dwid);
            if (_searchType == 1)
            {
                _conditionStr = string.Format(" {2}.{0} = '{1}' ", _sc.ColumnName, _stext, _sc.TableName);
            }
            else
            {
                //如果是模糊
                _stext = _stext.Replace(' ', '%');
                _stext = string.Format("%{0}%", _stext);
                _conditionStr = string.Format(" {2}.{0} like '{1}' ", _sc.ColumnName, _stext, _sc.TableName);
            }

            if (_qv.MainTable.TableName == _sc.TableName)
            {
                _sqlConcept = string.Format("select {0} RESDATA,{1} MAINKEY from {2} where {3} {4}  ", _sc.ColumnName, _sc.TableKeyColumn, _sc.TableName,
                        _filterStr, _conditionStr);
            }
            else
            {
                List<string> _tableList = new List<string>();
                _tableList.Add(_qv.MainTable.TableName);
                _tableList.Add(_sc.TableName);
                string _tableRelationStr = MySqlQueryBuilder.CreateTableRelationString(_qv, _tableList);
                _sqlConcept = string.Format("select {2}.{0} RESDATA,{2}.{1} MAINKEY from {2},{3} where {4}  ( {5} ({6})) ", _sc.ColumnName, _sc.TableKeyColumn, _sc.TableName, _qv.MainTable.TableName, _filterStr, _tableRelationStr, _conditionStr);
            }


            //筛选权限范围记录
            //if (_sfun != "")
            //{
            //        _sqlConcept += string.Format(" and {2}({0}.ZHCX_DW,'{1}') = '1' ", _sc.TableName, SinoUserCtx.CurUser.CurrentPost.PostDWDM, _sfun);
            //}

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sqlConcept);
            string _sourceStr = _sc.QueryModel.DisplayTitle;
            while (await dr.ReadAsync())
            {
                MDSearch_ResultDataIndex _item = new MDSearch_ResultDataIndex(
                        dr.IsDBNull(0) ? "" : dr.GetString(0),
                        0, _sc, _sourceStr,
                        dr.IsDBNull(1) ? "" : dr.GetValue(1).ToString());
                _ret.Add(_item);
            }
            dr.Close();
            return _ret;
        }

        public async Task<List<MDSearch_Column>> GetSearchResultColumn(string _searchText, int _searchType, string _searchConceptGroup, string _queryModelName)
        {
            string[] _modelNames = _queryModelName.Split('.');
            List<MDSearch_Column> _ret = new List<MDSearch_Column>();
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" select a.namespace,a.tid,a.tablename,a.displayname tdname,a.mainkey,");
            _sb.Append(" b.tcid,b.columnname,b.displaytitle cdname,b.type columntype,");
            _sb.Append(" b.refdmb refdmb,b.ctag,a.secretfun,v.displayname vdname");
            _sb.Append(" from md_table a");
            _sb.Append(" join md_tablecolumn b on a.tid = b.tid");
            _sb.Append(" join md_viewtable vt on vt.tid = a.tid");
            _sb.Append(" join md_view v on v.viewid = vt.viewid");
            _sb.Append(" where v.namespace = @NAMESPACE ");
            _sb.Append(" and v.viewname=@VIEWNAME ");

            // 注释概念标签
            //List<MD_ConceptItem> _tags = GetSubConceptTag(_searchConceptGroup);
            //if (_tags.Count < 1)
            //{
            //    return new List<MDSearch_Column>();
            //}

            //string _fgf = "AND (";
            //foreach (MD_ConceptItem _tag in _tags)
            //{
            //    _sb.Append(string.Format(" {0} b.CTAG LIKE '%{1}%' ", _fgf, _tag));
            //    _fgf = "OR";
            //}
            //if (_fgf == "OR")
            //{
            //    _sb.Append(" )");
            //}

            MySqlParameter[] _param = { new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar),
                                        new MySqlParameter("@VIEWNAME",MySqlDbType.VarChar)};
            _param[0].Value = _modelNames[0];
            _param[1].Value = _modelNames[1];
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            while (await dr.ReadAsync())
            {
                MDSearch_Column _item = new MDSearch_Column(_queryModelName,
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? "" : dr.GetString(3),
                        dr.IsDBNull(6) ? "" : dr.GetString(6),
                        dr.IsDBNull(7) ? "" : dr.GetString(7),
                        dr.IsDBNull(4) ? "" : dr.GetString(4));
                _ret.Add(_item);
            }
            dr.Close();
            return _ret;
        }

        public Task<string> GetSjshInfo_DWID(string _shjlid)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSjshInfo_HGJS(MDQuery_Request _queryRequest, string MainKeyID)
        {
            throw new NotImplementedException();
        }

        public Task<DataTable> GetTaskQueryLog(string TaskID)
        {
            throw new NotImplementedException();
        }

        public Task<DataSet> GetTaskQueryResult_DataSet(string _taskID)
        {
            throw new NotImplementedException();
        }

        public Task<DataSet> GetTaskQueryResult_ORA(string _taskID)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserLevel()
        {
            throw new NotImplementedException();
        }

        public void ImportRule(string SrcRuleID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> InsertNewDataCheckMsg(string _shjlid, string _title, string _context, string _cddw, string _tel, string _email, decimal _sfkj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsPASpaceExist(string PersonAnalizeSapceName)
        {
            throw new NotImplementedException();
        }

        public Task<MDQuery_Request> LoadQuery(string SaveQueryID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LockQueryTaskResult(string _taskID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 个人分析空间查询 (未使用)
        /// </summary>
        /// <param name="_queryRequest"></param>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public async Task<DataSet> PAQueryData(MDQuery_Request _queryRequest, MDModel_QueryModel _queryModel)
        {
            DataSet _ds = null;
            int _startTime = Environment.TickCount;
            MDModel_QueryModel _qv = _queryModel;
            int _GetQueryFinishedTime = Environment.TickCount;

            string _mainQueryStr = MySqlQueryBuilder.GetPAQueryStr(_qv, _queryRequest);
            int _BuildQueryStringFinishedTime = Environment.TickCount;


            //单查询语句
            _ds = await MysqlDBHelper.FillDataSet(_mainQueryStr, _qv.MainTable.TableName);
            MysqlLogWriter.WriteQueryLog(_mainQueryStr, Environment.TickCount - _GetQueryFinishedTime, "1");

            int _GetDataByQueryFinishedTime = Environment.TickCount;

            return _ds;
        }



        private const string SQL_GetOrgName = "select mdfun_getdwbyid(@dwid) ";
        public async Task<string> GetOrgName()
        {
            var dwid = await enService.GetCodeByToken();

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetOrgName, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@dwid", dwid));
                    object _ret = await _cmd.ExecuteScalarAsync();
                    cn.Close();
                    return (_ret == null) ? "" : _ret.ToString();
                }
                catch (Exception e)
                {
                    cn.Close();
                    throw e;
                }
            }

        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="_queryRequest"></param>
        /// <returns></returns>
        public async Task<DataSet> QueryData(MDQuery_Request _queryRequest)
        {
            var dwid = await enService.GetCodeByToken();

            MDModel_QueryModel _qv;
            int _GetQueryFinishedTime;
            Dictionary<string, string> _resultQueryStrings;
            DataSet _ds = null;
            int _startTime = Environment.TickCount;
            try
            {
                _qv = await GetMDQueryModelDefine2(_queryRequest.QueryModelName);
                _GetQueryFinishedTime = Environment.TickCount;
            }
            catch (Exception ex)
            {
                string _error = string.Format("取查询模型定义时出错！{0}", ex.Message);
                throw new Exception(_error);
            }

            List<MD_SecretFieldItem> _secretFields = new List<MD_SecretFieldItem>();
            string _mainQueryStr = "";
            string _tempTableName = string.Format("QUERY_TEMP_{0}", DateTime.Now.Ticks.ToString());
            //构建查询字符串
            try
            {
                _resultQueryStrings = MySqlQueryBuilder.GetQueryStr(_qv, _queryRequest, ref _mainQueryStr, dwid, _secretFields, _tempTableName);
            }
            catch (Exception ex)
            {
                string _error = string.Format("构建查询字符串时出错！{0}", ex.Message);
                throw new Exception(_error);
            }

            int _BuildQueryStringFinishedTime = Environment.TickCount;

            //用查询字符串取数据
            if (_resultQueryStrings.Count > 0)
            {
                //多查询语句
                try
                {
                    _ds = await MultiQuery(_mainQueryStr, _resultQueryStrings, _tempTableName);
                }
                catch (Exception ex)
                {
                    string _error = string.Format("多查询语句取数据时出错！{0}", ex.Message);
                    MysqlLogWriter.WriteSystemLog(_error, "ERROR");
                    throw new Exception(_error);
                }
            }
            else
            {
                //单查询语句
                try
                {
                    _ds = await MysqlDBHelper.FillDataSet(MysqlDBHelper.queryString, _mainQueryStr, _qv.MainTable.TableName);
                    MysqlLogWriter.WriteQueryLog(_mainQueryStr, Environment.TickCount - _GetQueryFinishedTime, "1");
                }
                catch (Exception ex)
                {
                    string _error = string.Format("单查询语句取数据时出错！{0}", ex.Message);
                    MysqlLogWriter.WriteQueryLog(_mainQueryStr, Environment.TickCount - _GetQueryFinishedTime, "13");
                    MysqlLogWriter.WriteSystemLog(_error, "ERROR");
                    throw new Exception(_error);
                }
            }
            int _GetDataByQueryFinishedTime = Environment.TickCount;

            //处理加密字段 StrUtils.Decrypt()
            foreach (MD_SecretFieldItem _sf in _secretFields)
            {
                DecryptResultData(_sf, _ds);
            }
            return _ds;

        }

        //对加密字段进行解密
        private void DecryptResultData(MD_SecretFieldItem sf, DataSet _ds)
        {
            if (_ds.Tables.Contains(sf.TableName))
            {
                DataTable _dt = _ds.Tables[sf.TableName];
                if (_dt.Columns.Contains(sf.FieldName))
                {
                    foreach (DataRow _dr in _dt.Rows)
                    {
                        string _sdata = _dr.IsNull(sf.FieldName) ? "" : _dr[sf.FieldName].ToString();
                        if (_sdata != null)
                        {
                            string _out = StrUtils.Decrypt(_sdata);
                            string t = _out.Contains("解密") ? "*" + _out : _out;
                            _dr[sf.FieldName] = t;
                        }
                    }
                }
            }
        }
        //对加密字段进行解密
        private void DecryptResultData(MD_SecretFieldItem sf, DataTable _dt)
        {
            if (_dt.Columns.Contains(sf.FieldName))
            {
                foreach (DataRow _dr in _dt.Rows)
                {
                    string _sdata = _dr.IsNull(sf.FieldName) ? "" : _dr[sf.FieldName].ToString();
                    if (_sdata != null)
                    {
                        string _out = StrUtils.Decrypt(_sdata);
                        string t = _out.Contains("解密") ? "*" + _out : _out;
                        _dr[sf.FieldName] = t;
                    }
                }
            }
        }

        /// <summary>
        /// 通过查询模型取带维护序号的数据(数据审核用)  未使用
        /// </summary>
        /// <param name="_queryRequest"></param>
        /// <param name="_dwdm"></param>
        /// <returns></returns>
        public async Task<DataTable> QueryDataWithWHXH(MDQuery_Request _queryRequest, string _dwdm)
        {
            int _startTime = Environment.TickCount;
            MDModel_QueryModel _qv = await GetMDQueryModelDefine2(_queryRequest.QueryModelName);
            int _GetQueryFinishedTime = Environment.TickCount;

            //构建查询字符串
            string _QueryStrings = MySqlQueryBuilder.GetQueryStrWithWHXH(_qv, _queryRequest, _dwdm);
            int _BuildQueryStringFinishedTime = Environment.TickCount;

            //单查询语句
            DataTable _dt = await MysqlDBHelper.Get_Data(_QueryStrings, _qv.MainTable.TableName);
            MysqlLogWriter.WriteQueryLog(_QueryStrings, Environment.TickCount - _GetQueryFinishedTime, "1");

            int _GetDataByQueryFinishedTime = Environment.TickCount;

            return _dt;
        }

        public async Task<DataTable> QueryGuideLine(string _guideLineID, List<MDQuery_GuideLineParameter> _param)
        {
            DataTable _ret;
            List<MD_GuideLine> _gls = await GetRootGuideLineList(_guideLineID);
            if (_gls.Count < 1) return null;
            int _GetQueryFinishedTime = Environment.TickCount;
            MD_GuideLine _glDefine = _gls[0];
            string _queryStr = _glDefine.GuideLineMethod;
            if (_param != null)
            {
                foreach (MDQuery_GuideLineParameter _gp in _param)
                {
                    _queryStr = MySqlQueryBuilder.RebuildGuideLineQueryString(_queryStr, _gp);
                }
            }
            _queryStr = MySqlQueryBuilder.ReplaceExtSecret(_queryStr, "");
            try
            {
                _ret = await MysqlDBHelper.FillDataTable(MysqlDBHelper.conf, CommandType.Text, _queryStr);
                MysqlLogWriter.WriteQueryLog(_queryStr, Environment.TickCount - _GetQueryFinishedTime, "2");
            }
            catch (Exception ex)
            {
                MysqlLogWriter.WriteQueryLog(_queryStr, 0, "23");
                throw ex;
            }
            return _ret;
        }

        public async Task<DataTable> QueryGuideLineByDefault(string _guideLineID, List<MDQuery_GuideLineParameter> _param)
        {
            DataTable _ret;
            List<MD_GuideLine> _gls = await GetRootGuideLineList(_guideLineID);
            if (_gls.Count < 1) return null;
            int _GetQueryFinishedTime = Environment.TickCount;
            MD_GuideLine _glDefine = _gls[0];
            string _queryStr = _glDefine.GuideLineMethod;
            foreach (MDQuery_GuideLineParameter _gp in _param)
            {
                _queryStr = MySqlQueryBuilder.RebuildGuideLineQueryStringByDefault(_queryStr, _gp);
            }
            _queryStr = MySqlQueryBuilder.ReplaceExtSecret(_queryStr, "");
            try
            {
                _ret = await MysqlDBHelper.FillDataTable(MysqlDBHelper.queryString, CommandType.Text, _queryStr);
                MysqlLogWriter.WriteQueryLog(_queryStr, Environment.TickCount - _GetQueryFinishedTime, "2");
            }
            catch (Exception ex)
            {
                MysqlLogWriter.WriteQueryLog(_queryStr, 0, "23");
                throw ex;
            }
            return _ret;
        }

        public Task<int> QueryGuideLineResultCount(string _guideLineID, List<MDQuery_GuideLineParameter> _params)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RebuildGuideLineParamSetting(string GuideLineID)
        {
            List<MD_GuideLine> _rootGls = await GetRootGuideLineList(GuideLineID);
            foreach (MD_GuideLine _rgl in _rootGls)
            {
                //生成本指标的算法
                RebuildCurrentGuideLineParamSetting(_rgl);
                //生成下载指标算法
                ReBuildChildGuideLineParamSetting(_rgl.ID);
            }
            return true;
        }

        private async void ReBuildChildGuideLineParamSetting(string GuideLineID)
        {
            List<MD_GuideLine> _gls = await GetGuideLineListByFatherID(GuideLineID);
            foreach (MD_GuideLine _rgl in _gls)
            {
                //生成本指标的算法
                RebuildCurrentGuideLineParamSetting(_rgl);
                //递归生成下载指标算法
                ReBuildChildGuideLineParamSetting(_rgl.ID);
            }
        }

        private async void RebuildCurrentGuideLineParamSetting(MD_GuideLine _rgl)
        {
            MDQuery_GuideLineParameter _ret;
            if (_rgl.GuideLineMethod.Trim() != "")
            {

                MD_GuideLine_ParamSetting _gp = await GetGuideLineParamSetting(_rgl.ID);
                List<MDQuery_GuideLineParameter> _params = new List<MDQuery_GuideLineParameter>();
                foreach (MD_GuideLineParameter _p in MC_GuideLine.GetParametersFromMeta(_rgl.GuideLineMeta))
                {
                    _ret = null;
                    string _valueStr = StrUtils.GetMetaByName(_p.ParameterName, _gp.ParamMeta);
                    switch (_p.ParameterType)
                    {
                        case "代码表":
                            string _s2 = StrUtils.GetMetaByName("REF_CODE", _valueStr);
                            _ret = new MDQuery_GuideLineParameter(_p, _s2);
                            break;
                        default:
                            _ret = new MDQuery_GuideLineParameter(_p, _valueStr);
                            break;
                    }
                    if (_ret != null) _params.Add(_ret);
                }


                await SaveGuideLineParamSetting(_gp, _params);
            }
        }

        public void RecoverRuleDefine(string TargetRuleID, string SrcRuleID)
        {
            throw new NotImplementedException();
        }

        public void SaveCheckRuleState(string QueryModelName, List<MD_CheckRule> _ruleList)
        {
            throw new NotImplementedException();
        }

        private const string SQL_SaveComputeFieldDefine = @"
            insert into md_computecolumn (ID,COLUMNAME,COLUMNEXP,TABLENAME,VIEWNAME,COLUMNMETA,COLUMNDES,ISPUBLIC,USERID,CREATEDATE) 
            values (@ID,@COLUMNAME,@COLUMNEXP,@TABLENAME,@VIEWNAME,@COLUMNMETA,@COLUMNDES,0,@USERID,now()) ";
        public async Task<bool> SaveComputeFieldDefine(string DisplayName, string Description, string Expression, string QueryString, string ResultDataType, string TableName, string ModelName)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_SaveComputeFieldDefine, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                    _cmd.Parameters.Add(new MySqlParameter("@COLUMNAME", DisplayName));
                    _cmd.Parameters.Add(new MySqlParameter("@COLUMNEXP", QueryString));
                    _cmd.Parameters.Add(new MySqlParameter("@TABLENAME", TableName));
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWNAME", ModelName));
                    _cmd.Parameters.Add(new MySqlParameter("@COLUMNMETA", string.Format("<Expression>{0}</Expression><ResultDataType>{1}</ResultDataType>", QueryString, ResultDataType)));
                    _cmd.Parameters.Add(new MySqlParameter("@COLUMNDES", Description));
                    _cmd.Parameters.Add(new MySqlParameter("@USERID", Convert.ToInt64(000)));
                    await _cmd.ExecuteScalarAsync();
                    cn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    cn.Close();
                    throw e;
                }
            }
        }

        public Task<bool> SaveDataByInputModel(string _inputModelName, DataTable _changedData)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveDataCheckResult(string CurrentJLID, string CurrentLevel, string CurrentID, string _shjg, string _shr, string _xgyj, string WHXH)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveGuideLineParamSetting(MD_GuideLine_ParamSetting _paramSetting, List<MDQuery_GuideLineParameter> _paramters)
        {
            MD_GuideLine_ParamSetting SavedSetting = null;
            string _guideLineID = _paramSetting.GuideLineID;
            string _queryStr = "";
            //建查询语句
            try
            {
                List<MD_GuideLine> _gls = await GetRootGuideLineList(_guideLineID);
                if (_gls.Count < 1) return false;
                int _GetQueryFinishedTime = Environment.TickCount;
                MD_GuideLine _glDefine = _gls[0];
                _queryStr = _glDefine.GuideLineMethod;
                foreach (MDQuery_GuideLineParameter _gp in _paramters)
                {
                    _queryStr = MySqlQueryBuilder.RebuildGuideLineQueryString(_queryStr, _gp);
                }
                _queryStr = MySqlQueryBuilder.ReplaceExtSecret(_queryStr, "");
            }
            catch (Exception e2)
            {
                string _errmsg = string.Format("在创建指标[{1}]在单位[{2}]的参数设置下的查询语句时出错,错误信息为:{0}!\n",
                         e2.Message, _guideLineID, "000");
                MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                return false;
            }

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    //判断本单位参数设置记录是否存在
                    SavedSetting = GuideLineParamSetting.GetCurrentPostRecord(_guideLineID, cn);
                    if (SavedSetting == null)
                    {
                        SavedSetting = _paramSetting;
                        GuideLineParamSetting.InsertCurrentPostRecord(SavedSetting, _queryStr, cn);
                    }
                    else
                    {
                        SavedSetting.ParamMeta = _paramSetting.ParamMeta;
                        GuideLineParamSetting.SaveCurrentPostRecord(SavedSetting, _queryStr, cn);
                    }
                    _txn.Commit();
                }
                catch (Exception e)
                {
                    string _errmsg = string.Format("保存指标[{1}]在本单位[{2}]的参数设置时出错,错误信息为:{0}!\n",
                       e.Message, _guideLineID, "000");
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    _txn.Rollback();
                    return false;
                }
            }
            return true;
        }

        public Task<bool> SaveNewDataCheckRule(string _ruleName, string _queryModelName, string _gzsf)
        {
            throw new NotImplementedException();
        }

        public void SaveQuery(string SaveName, bool IsPublic, MDQuery_Request QueryRequest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendDataCheckMsgFK(string _ggjlid, string _fkjg)
        {
            throw new NotImplementedException();
        }

        public async Task<MD_ComputeField> TestComputeExpress(string ExpressionString, MDModel_Table TableDefine)
        {
            MD_ComputeField _ret = new MD_ComputeField();
            string resultDataType;
            string QueryString = MySqlQueryBuilder.BuildComupteField(ExpressionString, TableDefine);
            string _sql = string.Format("select {0} from {1} limit 1 ", QueryString, TableDefine.TableName);
            try
            {

                DataTable _dt = await MysqlDBHelper.FillDataTable(MysqlDBHelper.queryString, CommandType.Text, _sql, null);
                Type _rtype = _dt.Columns[0].DataType;
                if (_rtype == typeof(DateTime))
                {
                    resultDataType = "DATE";
                }
                else if (_rtype == typeof(decimal))
                {
                    resultDataType = "NUMBER";
                }
                else if (_rtype == typeof(Int64) || _rtype == typeof(Int32) || _rtype == typeof(Int16))
                {
                    resultDataType = "NUMBER";
                }
                else
                {
                    resultDataType = "VARCHAR2";
                }
                _ret.ResultDataType = resultDataType;
                _ret.QueryString = QueryString;
                return _ret;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public async Task<MD_ComputeField> TestStatisticsExpress(string FunctionName, string TableName, MDModel_Table_Column TableColumn)
        {
            MD_ComputeField _ret = new MD_ComputeField();
            string resultDataType;
            string QueryString = MySqlQueryBuilder.BuildStatisticsField(FunctionName, TableName, TableColumn);
            try
            {
                DataTable _dt = await MysqlDBHelper.FillDataTable(MysqlDBHelper.queryString, CommandType.Text, QueryString, null);
                Type _rtype = _dt.Columns[0].DataType;
                if (_rtype == typeof(DateTime))
                {
                    resultDataType = "DATE";
                }
                else if (_rtype == typeof(decimal))
                {
                    resultDataType = "NUMBER";
                }
                else if (_rtype == typeof(Int64) || _rtype == typeof(Int32) || _rtype == typeof(Int16))
                {
                    resultDataType = "NUMBER";
                }
                else
                {
                    resultDataType = "VARCHAR2";
                }
                _ret.ResultDataType = resultDataType;
                _ret.QueryString = QueryString;
                return _ret;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public Task<bool> UpdateDataCheckMsg(string _ggjlid, string _title, string _context, string _cddw, string _tel, string _email, decimal _sfkj)
        {
            throw new NotImplementedException();
        }


        private const string SQL_Create_Query_Temp = "create temporary table if not exists {0} ( PK_C varchar(400) primary key)";
        private const string SQL_Clear_Query_Temp = "TRUNCATE {0}";
        private async Task<DataSet> MultiQuery(string _mainQueryStr, Dictionary<string, string> _resultQueryStrings, string _tempTableName)
        {
            string _childSQL = "";
            DataSet _ds = new DataSet();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    #region 创建临时表
                    try
                    {
                        MySqlCommand cmd = new MySqlCommand(string.Format(SQL_Create_Query_Temp, _tempTableName), cn, txn);
                        int val = await cmd.ExecuteNonQueryAsync();

                    }
                    catch (Exception ex)
                    {
                        var t = string.Format(SQL_Create_Query_Temp, _tempTableName);
                        string _error = string.Format("创建多查询用的临时表出错:{0}  执行的SQL:{1}", ex.Message, t);
                        MysqlLogWriter.WriteSystemLog(_error, "ERROR");
                        throw new Exception(_error);
                    }
                    #endregion

                    #region 为保证数据正确而清空临时表
                    try
                    {
                        MySqlCommand cmdClear = new MySqlCommand(string.Format(SQL_Clear_Query_Temp, _tempTableName), cn, txn);
                        int val = await cmdClear.ExecuteNonQueryAsync();

                    }
                    catch (Exception ex)
                    {
                        string _error = string.Format("清空查询用的临时表出错:{0}", ex.Message);
                        throw new Exception(_error);
                    }
                    #endregion

                    int _startTime = Environment.TickCount;
                    string _sqlStr = string.Format("insert into {1} (PK_C) {0}", _mainQueryStr, _tempTableName);
                    try
                    {
                        MySqlCommand cmd2 = new MySqlCommand(_sqlStr, cn, txn);
                        int val = await cmd2.ExecuteNonQueryAsync();
                    }
                    catch (Exception ex)
                    {
                        string _error = string.Format("插入{1}出错:{0}", ex.Message, _tempTableName);
                        throw new Exception(_error);
                    }

                    MysqlLogWriter.WriteQueryLog(_sqlStr, Environment.TickCount - _startTime, "1");

                    foreach (string _key in _resultQueryStrings.Keys)
                    {
                        _startTime = Environment.TickCount;
                        try
                        {
                            MySqlDataAdapter _adapter = new MySqlDataAdapter();
                            //Set the select command to fetch product details
                            _childSQL = (string)_resultQueryStrings[_key];
                            DataTable _dt = await MysqlDBHelper.FillDataTable(cn, txn, CommandType.Text, _childSQL);
                            _dt.TableName = _key;
                            _ds.Tables.Add(_dt);
                            MysqlLogWriter.WriteQueryLog(_childSQL, Environment.TickCount - _startTime, "1");
                        }
                        catch (Exception ex)
                        {
                            MysqlLogWriter.WriteQueryLog(_childSQL, Environment.TickCount - _startTime, "13");
                            string _error = string.Format("取{0}表的结果数据出错!{1}", _key, ex.Message);
                            throw new Exception(_error);
                        }
                    }

                    #region 为减少数据空间占用而清空临时表
                    try
                    {
                        MySqlCommand cmdClear = new MySqlCommand(string.Format(SQL_Clear_Query_Temp, _tempTableName), cn, txn);
                        int val = await cmdClear.ExecuteNonQueryAsync();

                    }
                    catch (Exception ex)
                    {
                        string _error = string.Format("清空查询用的临时表出错:{0}", ex.Message);
                        throw new Exception(_error);
                    }
                    #endregion

                    txn.Commit();
                    cn.Close();
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    cn.Close();
                    throw ex;
                }
            }
            return _ds;
        }




        /// <summary>
        /// 写用户操作日志
        /// </summary>
        /// <param name="_type"></param>
        /// <param name="_msg"></param>
        /// <returns></returns>
        public bool WriteUserActionLog(string _type, string _msg)
        {
            try
            {
                decimal yhid = 0000;
                MysqlLogWriter.WriteUserLog(yhid, _type, _msg, 1, "127.0.0.1", "", "");
                return true;
            }
            catch (Exception ex)
            {
                MysqlLogWriter.WriteSystemLog(string.Format("在写用户日志时出错:{2},type={0},msg={1}", _type, _msg, ex.Message), "ERROR");
                return false;
            }
        }


        public async Task<RefCodeTablePropertie> GetRefCodePropertie(string RefTableName)
        {
            MySqlParameter[] _param;
            string[] _ctNames = RefTableName.Split('.');

            RefCodeTablePropertie _ret = new RefCodeTablePropertie();
            StringBuilder _sb = new StringBuilder();
            _sb.Append("select REFTABLENAME,DESCRIPTION,REFTABLEMODE,DOWNLOADMODE,REFTABLELEVELFORMAT,HIDECODE ");
            _sb.Append(" from metadata.md_reftablelist where REFTABLENAME = @TNAME");

            if (_ctNames.Length > 1)
            {
                _sb.Append(" and NAMESPACE=@NAMESPACE ");

                _param = new MySqlParameter[] {
                                                new MySqlParameter("@TNAME",MySqlDbType.VarChar,50),
                                                new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50) };
                _param[0].Value = _ctNames[1];
                _param[1].Value = _ctNames[0];
            }
            else
            {
                _param = new MySqlParameter[] {
                                                new MySqlParameter("@TNAME",MySqlDbType.VarChar,50)};
                _param[0].Value = _ctNames[0];
            }


            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            while (await dr.ReadAsync())
            {
                _ret = new RefCodeTablePropertie(dr.GetString(0), dr.IsDBNull(1) ? dr.GetString(0) : dr.GetString(1),
                        RefCodeType.Alpha, true, dr.IsDBNull(4) ? false : true,
                        dr.IsDBNull(3) ? 1 : Convert.ToInt32(dr.GetDecimal(3)),
                        dr.IsDBNull(2) ? 1 : Convert.ToInt32(dr.GetDecimal(2)),
                        dr.IsDBNull(4) ? "" : dr.GetString(4),
                        dr.IsDBNull(5) ? false : (dr.GetDecimal(5) > 0));
            }

            dr.Close();

            return _ret;
        }


        public async Task<IList<RefCodeData>> GetFullRefCodeData(string _refCodeName)
        {
            IList<RefCodeData> _ret = new List<RefCodeData>();

            string _sql = string.Format("select DM,MC,PYZT,PX,SFYX,BZ,FATHERCODE,SFXS,SFLR,SFYJD from dm_reftabledata where type='{0}' order by PX,DM", _refCodeName);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.queryString, CommandType.Text, _sql);

            while (await dr.ReadAsync())
            {
                RefCodeData _rdata = new RefCodeData(
                        dr.GetString(0),
                        dr.IsDBNull(1) ? "" : dr.GetString(1),
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? 0 : dr.GetInt32(3),
                        dr.IsDBNull(4) ? true : (dr.GetString(4) == "1"),
                        dr.IsDBNull(5) ? "" : dr.GetString(5),
                        dr.IsDBNull(6) ? "" : dr.GetString(6),
                        dr.IsDBNull(7) ? true : (dr.GetString(7) == "1"),
                        dr.IsDBNull(8) ? true : (dr.GetString(8) == "1"),
                        dr.IsDBNull(9) ? true : (dr.GetString(9) == "1"));
                _ret.Add(_rdata);
            }

            dr.Close();
            return _ret;
        }

        public async Task<IList<RefCodeData>> GetChildRefCodeData(string _refCodeName, string _fatherCode)
        {
            MySqlDataReader dr;
            IList<RefCodeData> _ret = new List<RefCodeData>();

            string _sql = string.Format("select DM,MC,PYZT,PX,SFYX,BZ,FATHERCODE,SFXS,SFLR,SFYJD  from dm_reftabledata where type='{0}' ", _refCodeName);

            if (_fatherCode == string.Empty)
            {
                _sql += "and FATHERCODE IS NULL order by PX,DM";
                dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql);
            }
            else
            {
                _sql += " and FATHERCODE = @FCODE order by PX,DM";
                MySqlParameter[] _param = new MySqlParameter[] {
                                                new MySqlParameter("@FCODE",MySqlDbType.VarChar,50)};
                _param[0].Value = _fatherCode;
                dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql, _param);
            }

            while (await dr.ReadAsync())
            {
                RefCodeData _rdata = new RefCodeData(
                        dr.GetString(0),
                        dr.IsDBNull(1) ? "" : dr.GetString(1),
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? 0 : dr.GetInt32(3),
                        dr.IsDBNull(4) ? true : (dr.GetString(4) == "1"),
                        dr.IsDBNull(5) ? "" : dr.GetString(5),
                        dr.IsDBNull(6) ? "" : dr.GetString(6),
                        dr.IsDBNull(7) ? true : (dr.GetString(7) == "1"),
                        dr.IsDBNull(8) ? true : (dr.GetString(8) == "1"),
                        dr.IsDBNull(9) ? true : (dr.GetString(9) == "1"));
                _ret.Add(_rdata);
            }

            dr.Close();
            return _ret;
        }

        public async Task<RefCodeData> GetRefCodeByCode(string RefTableName, string CodeValue)
        {
            RefCodeData _ret = null;

            string _sql = string.Format("select DM,MC,PYZT,PX,SFYX,BZ,FATHERCODE,SFXS,SFLR,SFYJD  from dm_reftabledata where type='{0}' ", RefTableName);
            _sql += " and DM = @CODE order by PX,DM";


            MySqlParameter[] _param = new MySqlParameter[] {
                                                new MySqlParameter("@CODE",MySqlDbType.VarChar,50)};
            _param[0].Value = CodeValue;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sql, _param);
            while (await dr.ReadAsync())
            {
                _ret = new RefCodeData(
                        dr.GetString(0),
                        dr.IsDBNull(1) ? "" : dr.GetString(1),
                        dr.IsDBNull(2) ? "" : dr.GetString(2),
                        dr.IsDBNull(3) ? 0 : Convert.ToInt32(dr.GetDecimal(3)),
                        dr.IsDBNull(4) ? true : (dr.GetString(4) == "1"),
                        dr.IsDBNull(5) ? "" : dr.GetString(5),
                        dr.IsDBNull(6) ? "" : dr.GetString(6),
                        dr.IsDBNull(7) ? true : (dr.GetString(7) == "1"),
                        dr.IsDBNull(8) ? true : (dr.GetString(8) == "1"),
                        dr.IsDBNull(9) ? true : (dr.GetString(9) == "1"));

            }

            dr.Close();
            return _ret;
        }



    }
}
