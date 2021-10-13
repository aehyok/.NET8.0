using aehyok.Core.Data;
using aehyok.Core.Data.Model;
using aehyok.Core.MySqlDataAccessor;
using aehyok.Core.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace aehyok.Core.Repository.Accessor
{
    public class FormAccessor
    {
        private string _inputModelName = "";       ////录入模型名称
        private MD_InputModel InputModel = null;  ////录入模型定义
        public SinoRequestUser RequestUser = new SinoRequestUser();
        private static Dictionary<string, MD_InputModel> ModelLib = new Dictionary<string, MD_InputModel>();   ///录入模型缓存
        public FormAccessor() { }
        public FormAccessor(string inputModelName, SinoRequestUser sinoRequestUser)
        {
            _inputModelName = inputModelName;
            RequestUser = sinoRequestUser;
            InputModel = GetInputModelDefineByModelName(inputModelName);
        }

        /// <summary>
        /// 清空缓存定义
        /// </summary>
        public static void ClearServerInputModel()
        {
            ModelLib.Clear();
        }

        #region 通过录入模型名称获取录入模型定义，并加入缓存中
        /// <summary>
        /// 通过录入模型名称获取录入模型定义，并加入缓存中
        /// </summary>
        /// <param name="inputModelName"></param>
        /// <returns></returns>
        public MD_InputModel GetInputModelDefineByModelName(string inputModelName)
        {
            if (ModelLib.ContainsKey(inputModelName))
            {
                return ModelLib[inputModelName];
            }
            else
            {
                MD_InputModel md_InputModel = new MD_InputModel();
                md_InputModel = InputModelAccessor.GetInputModelByName(inputModelName);
                ModelLib.Add(inputModelName, md_InputModel);
                return md_InputModel;
            }
        }
        #endregion

        #region 通过指标获取新记录算法
        /// <summary>
        /// 通过指标获取新记录算法
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MD_InputEntity GetNewData(Dictionary<string, object> param, bool isReadDefault = false)
        {
            MD_InputEntity inputEntity = new MD_InputEntity();
            inputEntity.InputModelName = this._inputModelName;
            if (!string.IsNullOrEmpty(InputModel.GetNewRecordGuideLine))
            {
                DataTable dataTable = GuideLineAccessor.QueryGuideline(InputModel.GetNewRecordGuideLine, param, RequestUser);
                inputEntity.IsNewData = true;
                if (dataTable != null)
                {
                    inputEntity.InputData = GetInputDataByDataTable(dataTable);
                }
                if (isReadDefault)
                    SetUserDefaultValue(inputEntity);
            }
            return inputEntity;
        }
        #endregion

        #region 通过指标获取新记录算法(文书)
        /// <summary>
        /// 通过指标获取新记录算法(文书)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MD_InputEntity GetDocNewData(Dictionary<string, object> param)
        {
            MD_InputEntity md_InputEntity = new MD_InputEntity();
            md_InputEntity.InputModelName = this._inputModelName;
            if (!string.IsNullOrEmpty(InputModel.GetNewRecordGuideLine))
            {
                DataTable dataTable = GuideLineAccessor.QueryGuideline(InputModel.GetNewRecordGuideLine, param, null);
                md_InputEntity.IsNewData = true;
                if (dataTable != null)
                {
                    md_InputEntity.InputData = GetInputDataByDataTable(dataTable);
                }
            }
            //复合型的录入模型以后再处理
            if (InputModel.IsMixModel)
            {
                md_InputEntity.ChildInputData = new Dictionary<string, string>();
                Dictionary<string, string> _str = new Dictionary<string, string>();
                foreach (MD_InputModel_Child _child in InputModel.ChildInputModel)
                {
                    switch (_child.ChildModel.ModelType)
                    {
                        case "FORM":
                        //_ret.ChildInputData.Add(_child.ChildModel.ModelFullName, GetChildFormData(_child, _ret));
                        //break;
                        case "GRID":
                            DataTable _dt = ChildInputData(_child, md_InputEntity);
                            _str = GetInputDataByDataTable(_dt);
                            foreach (string _t in _str.Keys)
                            {
                                if (!md_InputEntity.ChildInputData.ContainsKey(_t))
                                {
                                    md_InputEntity.ChildInputData.Add(_t, _str[_t]);
                                }
                            }
                            break;
                    }
                }
            }
            return md_InputEntity;
        }
        #endregion

        /// <summary>
        /// 子模型修改数据的时候获取单条记录信息 added by lqm 2014.04.03
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public MD_InputEntity GetInitData(Dictionary<string, object> param, bool isReadDefault = false)
        {
            MD_InputEntity _ret = new MD_InputEntity();
            _ret.InputModelName = _inputModelName;
            _ret.IsNewData = false;
            if (InputModel != null)
            {
                if (InputModel.InitGuideLine != null)
                {
                    DataTable _dt = GuideLineAccessor.QueryGuideline(InputModel.InitGuideLine, param, null);
                    _ret.IsNewData = false;
                    if (_dt != null)
                    {
                        _ret.InputData = GetInputDataByDataTable(_dt);
                    }

                    if (isReadDefault)
                        SetUserDefaultValue(_ret);
                }
                //复合型的录入模型以后再处理
                if (InputModel.IsMixModel)
                {
                    _ret.ChildInputData = new Dictionary<string, string>();
                    Dictionary<string, string> _str = new Dictionary<string, string>();
                    foreach (MD_InputModel_Child _child in InputModel.ChildInputModel)
                    {
                        switch (_child.ChildModel.ModelType)
                        {
                            case "FORM":
                            //_ret.ChildInputData.Add(_child.ChildModel.ModelFullName, GetChildFormData(_child, _ret));
                            //break;
                            case "GRID":
                                DataTable _dt = ChildInputData(_child, _ret);
                                _str = GetInputDataByDataTable(_dt);
                                foreach (string _t in _str.Keys)
                                {
                                    if (!_ret.ChildInputData.ContainsKey(_t))
                                    {
                                        _ret.ChildInputData.Add(_t, _str[_t]);
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            return _ret;
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <returns></returns>
        public OperationResult WriteEntity(MD_InputEntity inputEntity)
        {
            OperationResult operationResult = new OperationResult();
            using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
            {
                MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                try
                {
                    operationResult = WriteEntity(inputEntity, MySqlConnection);
                    CheckWHXH(inputEntity, MySqlConnection, ref operationResult);
                    if (operationResult.Success)
                    {
                        MySqlTransaction.Commit();
                        return operationResult;
                    }
                    else
                    {
                        MySqlTransaction.Rollback();
                        return operationResult;
                    }
                }
                catch (Exception exception)
                {
                    //OracleLogWriter.WriteSystemLog(string.Format("Exception:WriteEntity方法保存录入模型失败，错误信息为{0}！", exception.StackTrace), "ERROR");
                    MySqlTransaction.Rollback();
                    operationResult.Message = exception.Message;
                    return operationResult;
                }
            }
        }

        public void CheckWHXH(MD_InputEntity inputEntity, MySqlConnection cn, ref OperationResult operationResult)
        {
            string whxhStr = this.InputModel.DeleteRule.GetMetaByName2("WHXH");
            if (whxhStr != "" && operationResult.Success)
            {
                try
                {
                    string[] array = whxhStr.Split(',');
                    string whxh = inputEntity.InputData["CHECK_WHXH"];
                    string tableName = array[0];
                    string bzid = inputEntity.InputData[array[1]];
                    List<string> mainKeyList = InputModelAccessor.GetDbPrimayKeyList(tableName);
                    string sql = string.Format("select whxh from {0} where {1}= :BZID", tableName, mainKeyList[0]);
                    MySqlParameter[] param = { new MySqlParameter(":BZID", bzid) };
                    string curWHXH = SqlHelper.ExecuteScalar(SqlHelper.GetConnectionStr(), CommandType.Text, sql, param).ToString();
                    if (whxh != curWHXH)
                    {
                        operationResult.Success = false;
                        operationResult.Message = "该记录的最新数据已发生变化，请刷新列表重新进入！";
                    }
                    else
                    {
                        operationResult.ReturnValue = SqlHelper.ExecuteScalar(cn, CommandType.Text, sql, param).ToString();
                    }
                }
                catch (Exception e)
                {
                    operationResult.Success = false;
                    operationResult.Message = "保存失败！";
                    string msg = string.Format("录入模型保存时，取WHXH出错，whxhStr={0}，错误信息为{1}。", whxhStr, e.Message);
                    //OracleLogWriter.WriteSystemLog(msg, "ERROR");
                }
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <param name="MySqlConnection"></param>
        /// <returns></returns>
        public OperationResult WriteEntity(MD_InputEntity inputEntity, MySqlConnection MySqlConnection)
        {
            OperationResult operationResult = new OperationResult();

            MD_InputModel inputModel = new MD_InputModel();
            inputModel = GetInputModelDefineByModelName(_inputModelName);
            bool ret = true;
            if (!string.IsNullOrEmpty(inputModel.BeforeWrite))  //保存前的写入操作
            {
                ret = Execute_OperationWrite(MySqlConnection, inputEntity, inputModel.BeforeWrite);
            }
            if (inputEntity.IsNewData && ret)
            {
                ret = WriteNewEntity(inputEntity, MySqlConnection);
            }
            else
            {
                ret = UpdateEntity(inputEntity, MySqlConnection);
            }
            if (!string.IsNullOrEmpty(inputModel.AfterWrite) && ret)   //保存后的写入操作
            {
                ret = Execute_OperationWrite(MySqlConnection, inputEntity, inputModel.AfterWrite);
            }
            operationResult.Success = ret;
            return operationResult;
        }

        #region 保存子模型数据
        /// <summary>
        /// 保存子模型数据
        /// </summary>
        /// <param name="childData"></param>
        /// <param name="cn"></param>
        private void SaveChildData(Dictionary<string, string> childData, MySqlConnection MySqlConnection)
        {
            if (childData != null)
            {
                foreach (KeyValuePair<string, string> Obj in childData)
                {
                    string ChildTableData = childData[Obj.Key];
                    XmlDocument XmlChildContent = new XmlDocument();
                    XmlChildContent.LoadXml(ChildTableData);
                    MD_InputModel inputmodel = GetInputModelDefineByModelName(Obj.Key);
                    List<Dictionary<string, string>> ChildDataList = new List<Dictionary<string, string>>();
                    if (XmlChildContent.SelectSingleNode("//DocumentElement") != null)
                    {
                        foreach (XmlNode Table in XmlChildContent.SelectSingleNode("//DocumentElement").ChildNodes)
                        {
                            Dictionary<string, string> xnChildData = new Dictionary<string, string>();
                            foreach (XmlNode child in Table.ChildNodes)
                            {
                                xnChildData.Add(child.Name, child.InnerXml);
                            }
                            if (xnChildData.Count() > 0)
                            {
                                xnChildData.Add("option", (Table as XmlElement).GetAttribute("option"));
                                MD_InputEntity md = new MD_InputEntity();
                                md.InputData = xnChildData;
                                md.InputModelName = Obj.Value;
                                if (xnChildData["option"].ToString() == "1") //添加
                                {
                                    foreach (MD_InputModel_SaveTable _table in inputmodel.WriteTableNames)
                                    {
                                        WriteNewTableData(_table, md, MySqlConnection);
                                    }
                                }
                                else if (xnChildData["option"].ToString() == "2") //修改(可能存在问题)
                                {
                                    foreach (MD_InputModel_SaveTable _table in inputmodel.WriteTableNames)
                                    {
                                        UpdateTableData(_table, md, MySqlConnection);
                                    }
                                }
                                else if (xnChildData["option"].ToString() == "-1") ///删除
                                {
                                    foreach (MD_InputModel_SaveTable _table in inputmodel.WriteTableNames)
                                    {
                                        DelGridChildData(_table, md, MySqlConnection);
                                    }
                                }
                                else
                                {
                                    //option=0不做任何操作
                                }
                                ChildDataList.Add(xnChildData);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 删除子模型数据added by lqm 2013.1.4
        /// </summary>
        /// <param name="saveTable"></param>
        /// <param name="inputEntity"></param>
        /// <param name="MySqlConnection"></param>
        private void DelGridChildData(MD_InputModel_SaveTable saveTable, MD_InputEntity inputEntity, MySqlConnection MySqlConnection)
        {
            //此处暂时只针对一个主键进行处理
            List<string> mainKeyList = InputModelAccessor.GetDbPrimayKeyList(saveTable.TableName);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("delete  from {0}", saveTable.TableName));
            string key = "", value = "";
            foreach (string item in mainKeyList)
            {
                MD_InputModel_SaveTableColumn column = GetColumnByDesName(item, saveTable.Columns);
                if (column != null)
                {
                    object data = inputEntity.InputData.ContainsKey(column.SrcColumn) ? (object)inputEntity.InputData[column.SrcColumn] : DBNull.Value;
                    key = item;
                    value = data.ToString();
                }
            }
            sb.Append(string.Format("  where {0}={1}", key, value));
            SqlHelper.ExecuteNonQuery(MySqlConnection, CommandType.Text, sb.ToString(), null);
        }

        #region 删除子模型数据
        /// <summary>
        /// 删除子模型数据
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="idArray"></param>
        /// <param name="sinoRequestUser"></param>
        /// <returns></returns>
        public bool DelGridChildData(string modelName, string idArray, SinoRequestUser sinoRequestUser)
        {
            MD_InputModel InputModel = this.InputModel;
            string[] array = idArray.Split(',');
            using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
            {
                MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                try
                {
                    foreach (string item in array)
                    {
                        foreach (MD_InputModel_SaveTable Table in InputModel.WriteTableNames)
                        {
                            ///此处暂时只针对一个主键进行处理
                            List<string> _mainKeyList = InputModelAccessor.GetDbPrimayKeyList(Table.TableName);
                            StringBuilder sb = new StringBuilder();
                            sb.Append(string.Format("delete  from {0}", Table.TableName));
                            string columnName = "", columnValue = "";
                            foreach (string key in _mainKeyList)
                            {
                                MD_InputModel_SaveTableColumn Column = GetColumnByDesName(key, Table.Columns);
                                if (Column != null)
                                {
                                    object value = item;
                                    columnName = key;
                                    columnValue = value.ToString();
                                }
                            }
                            sb.Append(string.Format("  where {0}={1}", columnName, columnValue));
                            SqlHelper.ExecuteNonQuery(MySqlConnection, CommandType.Text, sb.ToString(), null);
                        }
                    }
                    MySqlTransaction.Commit();
                    return true;
                }
                catch (MySqlException MySqlException)
                {
                    MySqlTransaction.Rollback();
                    string errorMessage = string.Format("Exception DelGridChildData采用录入模型删除数据出错！错误信息:{0}!", MySqlException.Message);
                    //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                    return false;
                }
                catch (Exception exception)
                {
                    MySqlTransaction.Rollback();
                    string errorMessage = string.Format("Exception DelGridChildData采用录入模型删除数据出错！错误信息:{0}!", exception.Message);
                    //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                    return false;
                }
                finally
                {
                    MySqlConnection.Close();
                }
            }
        }
        #endregion

        private bool Execute_OperationWrite(MySqlConnection connection, MD_InputEntity entity, string proString)
        {
            //取所有的授权
            try
            {
                string proName = proString.Substring(0, proString.IndexOf('(')); ////截取存储过程的名称
                string[] parammeterStrings =
                    proString.Substring(proString.IndexOf('(') + 1, proString.IndexOf(')') - proString.IndexOf('(') - 1)
                        .Split(',');
                List<MySqlParameter> MySqlParameters = new List<MySqlParameter>();
                foreach (string param in parammeterStrings)
                {
                    if (param.ToUpper().StartsWith("STR"))
                    {
                        MySqlParameter MySqlParameter = new MySqlParameter(param, MySqlDbType.VarChar);
                        MySqlParameter.Value = entity.InputData[param.ToUpper()];
                        MySqlParameters.Add(MySqlParameter);
                    }
                    else if (param.ToUpper().StartsWith("N"))
                    {
                        MySqlParameter MySqlParameter = new MySqlParameter(param, MySqlDbType.Decimal);
                        MySqlParameter.Value =
                            decimal.Parse(!string.IsNullOrEmpty(entity.InputData[param.ToUpper()])
                                ? entity.InputData[param.ToUpper()]
                                : "0");
                        MySqlParameters.Add(MySqlParameter);
                    }
                    else if (param.ToUpper().StartsWith("D"))
                    {

                        MySqlParameter MySqlParameter = new MySqlParameter(param, MySqlDbType.Date);
                        DateTime date;
                        if (entity.InputData.ContainsKey(param.ToUpper()) && DateTime.TryParse(entity.InputData[param.ToUpper()], out date))
                        {
                            MySqlParameter.Value = date;
                        }
                        else
                        {
                            MySqlParameter.Value = System.DBNull.Value;
                        }
                        MySqlParameters.Add(MySqlParameter);
                    }
                    else
                    {
                        MySqlParameter MySqlParameter = new MySqlParameter(param, MySqlDbType.VarChar);
                        MySqlParameter.Value = entity.InputData[param.ToUpper()];
                        MySqlParameters.Add(MySqlParameter);
                    }
                }
                SqlHelper.ExecuteNonQuery(connection, CommandType.StoredProcedure, proName, MySqlParameters.ToArray());
                return true;
            }

            catch (MySqlException MySqlException)
            {
                string errorMessage = MySqlException.Message.GetMetaByName2("MSG");
                if (!string.IsNullOrEmpty(errorMessage))
                    throw new Exception(errorMessage);
                else
                {
                    throw new Exception("保存失败！");
                }
            }
            catch (Exception exception)
            {
                string errorMessage = string.Format("录入模型保存时,存储过程处理失败，错误信息为{0}", exception.Message);
                //OracleLogWriter.WriteSystemLog("Exception:" + errorMessage, "ERROR");
                throw new Exception("保存失败！");
            }
        }

        #region 写入新的记录
        /// <summary>
        /// 写入新的记录
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <param name="MySqlConnection"></param>
        /// <returns></returns>
        private bool WriteNewEntity(MD_InputEntity inputEntity, MySqlConnection MySqlConnection)
        {
            foreach (MD_InputModel_SaveTable table in InputModel.WriteTableNames)
            {
                switch (table.SaveMode)
                {
                    case "OnlyUpdate":     ////只更新
                        UpdateTableData(table, inputEntity, MySqlConnection);
                        break;
                    default:
                        WriteNewTableData(table, inputEntity, MySqlConnection);
                        break;
                }
            }
            return true;
        }

        private void WriteNewTableData(MD_InputModel_SaveTable saveTable, MD_InputEntity _entity, MySqlConnection cn)
        {
            StringBuilder _vStr = new StringBuilder();
            _vStr.Append(" values ( ");
            StringBuilder _sb = new StringBuilder();
            _sb.Append("insert into ");
            _sb.Append(saveTable.TableName);
            _sb.Append("  ( ");
            string _fg = "";
            foreach (MD_InputModel_SaveTableColumn _col in saveTable.Columns)
            {
                if (_col.SrcColumn == "" && _col.Method == "")
                {
                    //如果写入源字段和算法都为空，则此字段不写入
                    continue;
                }
                _sb.Append(string.Format("{0}{1}", _fg, _col.DesColumn));
                if (_col.SrcColumn == "")
                {
                    // 如果写入源字段为空，则仅使用算法写入本字段内容
                    _vStr.Append(_fg);
                    _vStr.Append(ConvertVar(_col.Method, _entity));
                }
                else
                {
                    //如果写入源字段不为空
                    if (_col.Method == "")
                    {
                        //如果算法为空，则写入源字段内容
                        _vStr.Append(string.Format("{0}:{1}", _fg, _col.DesColumn));
                    }
                    else
                    {
                        //算法不为空，将算法做为写入算法，参数照样代入
                        _vStr.Append(_fg);
                        _vStr.Append(string.Format("{0}", ConvertVar(_col.Method, _entity)));
                    }
                }
                _fg = ",";
            }

            _sb.Append(" ) ");
            _sb.Append(_vStr.ToString());
            _sb.Append(" ) ");
            try
            {
                List<MySqlParameter> _param = new List<MySqlParameter>();
                foreach (MD_InputModel_SaveTableColumn _col in saveTable.Columns)
                {
                    string _cname = _col.SrcColumn;
                    if (_cname != "")
                    {
                        object _data = _entity.InputData.ContainsKey(_cname) ? (_entity.InputData[_cname] == null ? "" : (object)_entity.InputData[_cname]) : DBNull.Value;
                        DateTime _date;
                        if (DateTime.TryParse(_data.ToString(), out _date))
                        {
                            //判断小数点数值（1.89） 和判断代码表多选（01,04,09） added by lqm 2015-10-22
                            if (StrUtils.IndexOfComma(_data.ToString()) != 1 && _data.ToString().IndexOf(',') < 0)
                            {
                                _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), _date));
                            }
                            else
                            {
                                _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), _data));
                            }
                        }
                        else
                        {
                            _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), _data));
                        }
                    }
                }
                SqlHelper.ExecuteNonQuery(cn, CommandType.Text, _sb.ToString(), _param.ToArray());
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("录入模型写新数据失败！录入模型名={1}，错误信息为:{0}!", e.Message, _entity.InputModelName);
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region 变量替换
        /// <summary>
        /// 变量替换
        /// </summary>
        /// <param name="method"></param>
        /// <param name="md_InputEntity"></param>
        /// <returns></returns>
        private string ConvertVar(string method, MD_InputEntity md_InputEntity)
        {
            string ret = method;
            ret = "";//  OraQueryBuilder.ReplaceExtSecret(null, ret, RequestUser);
            foreach (string key in md_InputEntity.InputData.Keys)
            {
                string name = string.Format("${0}$", key);
                string value = string.Format("'{0}'", (md_InputEntity.InputData[key] == null) ? "" : md_InputEntity.InputData[key].ToString());
                ret = ret.Replace(name, value);
            }
            return ret;
        }
        #endregion

        private void WriteOnlyInsertTable(MD_InputModel_SaveTable saveTable, MD_InputEntity md_InputEntity, MySqlConnection MySqlConnection)
        {
            string ajid = string.Empty;
            if (md_InputEntity.InputData.ContainsKey("AJID"))
            {
                ajid = md_InputEntity.InputData["AJID"].ToString();
                bool isEmpty = true; //SinoFlowCommon.IsNew_Update(AJID, _table.TableName);
                if (isEmpty)
                    WriteNewTableData(saveTable, md_InputEntity, MySqlConnection);
                else
                    UpdateTableData(saveTable, md_InputEntity, MySqlConnection);
            }
            else
            {
                WriteNewTableData(saveTable, md_InputEntity, MySqlConnection);
            }

        }



        #region 更新记录

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <param name="MySqlConnection"></param>
        /// <returns></returns>
        private bool UpdateEntity(MD_InputEntity inputEntity, MySqlConnection MySqlConnection)
        {
            foreach (MD_InputModel_SaveTable saveTable in InputModel.WriteTableNames)
            {
                switch (saveTable.SaveMode)
                {
                    case "OnlyInsert":  //配置只写入数据表数据
                        WriteNewTableData(saveTable, inputEntity, MySqlConnection);
                        break;
                    default:
                        UpdateTableData(saveTable, inputEntity, MySqlConnection);
                        break;
                }

            }
            return true;
        }

        private void UpdateTableData(MD_InputModel_SaveTable saveTable, MD_InputEntity inputEntity, MySqlConnection MySqlConnection)
        {
            List<string> mainKeyList = InputModelAccessor.GetDbPrimayKeyList(saveTable.TableName);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("update  ");
            stringBuilder.Append(saveTable.TableName);
            stringBuilder.Append(" set  ");
            string separator = "";
            //制作更新字段SQL语句
            int updateFiledCount = 0;
            foreach (MD_InputModel_SaveTableColumn _col in saveTable.Columns)
            {
                if (_col.SrcColumn == "" && _col.Method == "")
                {
                    //如果写入源字段和算法都为空，则此字段不写入
                    continue;
                }
                if (!mainKeyList.Contains(_col.DesColumn))
                {
                    updateFiledCount++;
                    stringBuilder.Append(separator);
                    separator = " , ";
                    stringBuilder.Append(_col.DesColumn);
                    if (_col.SrcColumn == "")
                    {
                        stringBuilder.Append("=");
                        stringBuilder.Append(ConvertVar(_col.Method, inputEntity));
                    }
                    else
                    {
                        if (_col.Method == "")
                        {
                            stringBuilder.Append("=:");
                            stringBuilder.Append(_col.DesColumn);
                        }
                        else
                        {
                            stringBuilder.Append("=");
                            stringBuilder.Append(string.Format(_col.Method, ConvertVar(_col.Method, inputEntity)));
                        }
                    }
                }
            }
            if (updateFiledCount < 1)
            {
                //无更新字段，直接返回true;
                return;
            }
            stringBuilder.Append(" where ");
            separator = "";
            //制作主键字段条件语句
            foreach (string _key in mainKeyList)
            {
                stringBuilder.Append(separator);
                stringBuilder.Append(_key);
                stringBuilder.Append(" =:");
                stringBuilder.Append(_key);
                separator = " and ";
            }
            try
            {
                List<MySqlParameter> _param = new List<MySqlParameter>();
                foreach (MD_InputModel_SaveTableColumn _col in saveTable.Columns)
                {
                    string _cname = _col.SrcColumn;
                    if (_cname != "")
                    {
                        object _data = inputEntity.InputData.ContainsKey(_cname) ? (inputEntity.InputData[_cname] == null ? "" : (object)inputEntity.InputData[_cname]) : DBNull.Value;
                        if (!mainKeyList.Contains(_col.DesColumn))
                        {
                            DateTime date;
                            if (DateTime.TryParse(_data.ToString(), out date))
                            {
                                //判断小数点数值（1.89） 和判断代码表多选（01,04,09） added by lqm 2015-10-22
                                if (StrUtils.IndexOfComma(_data.ToString()) != 1 && _data.ToString().IndexOf(',') < 0)
                                {
                                    _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), date));
                                }
                                else
                                {
                                    _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), _data));
                                }

                            }
                            else
                            {
                                _param.Add(new MySqlParameter(string.Format(":{0}", _col.DesColumn), _data));
                            }
                        }
                    }
                }
                //添加主键字段参数
                foreach (string key in mainKeyList)
                {
                    MD_InputModel_SaveTableColumn _kcol = GetColumnByDesName(key, saveTable.Columns);
                    if (_kcol != null)
                    {
                        object _data = inputEntity.InputData.ContainsKey(_kcol.SrcColumn) ? (object)inputEntity.InputData[_kcol.SrcColumn] : DBNull.Value;
                        _param.Add(new MySqlParameter(string.Format(":{0}", key), _data));

                    }
                }
                SqlHelper.ExecuteNonQuery(MySqlConnection, CommandType.Text, stringBuilder.ToString(), _param.ToArray());
            }
            catch (Exception e)
            {
                string errorMessage = string.Format("录入模型更新数据失败！录入模型名={1}，错误信息为:{0}!", e.Message, inputEntity.InputModelName);
                throw new Exception(errorMessage);
            }
        }
        #endregion

        #region 通过主键名称来获取目标列Column
        /// <summary>
        /// 通过主键名称来获取目标列Column
        /// </summary>
        /// <param name="key"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private MD_InputModel_SaveTableColumn GetColumnByDesName(string key, List<MD_InputModel_SaveTableColumn> columns)
        {
            return columns.Where(col => col.DesColumn == key).FirstOrDefault();
        }
        #endregion

        #region 将DataTable转换为字典类型
        /// <summary>
        /// 将DataTable转换为字典类型
        /// </summary> 
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetInputDataByDataTable(DataTable dataTable)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            if (dataTable.Rows.Count > 0)
            {
                DataRow dataRow = dataTable.Rows[0];
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
                }
            }
            return dictionary;
        }
        #endregion

        #region 获取已用数据

        /// <summary>
        /// 获取已用数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public MD_InputEntity GetDocData(Dictionary<string, object> parameter)
        {
            MD_InputEntity md_InputEntity = new MD_InputEntity();
            using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
            {
                md_InputEntity = GetDocData(parameter, MySqlConnection);
            }
            return md_InputEntity;
        }

        public MD_InputEntity GetDocData(Dictionary<string, object> parameter, MySqlConnection MySqlConnection)
        {
            MD_InputEntity md_InputEntity = new MD_InputEntity();
            md_InputEntity.InputModelName = _inputModelName;
            md_InputEntity.IsNewData = false;

            if (InputModel != null)
            {
                if (!string.IsNullOrEmpty(InputModel.GetDataGuideLine))
                {
                    DataTable dataTable = GuideLineAccessor.QueryGuideline(InputModel.GetDataGuideLine, parameter, RequestUser, MySqlConnection);
                    md_InputEntity.IsNewData = false;
                    if (dataTable != null)
                    {
                        if (InputModel.ModelType == "GRID")
                        {
                            md_InputEntity.InputData = GetGridInputDataByDatable(dataTable);
                        }
                        else
                        {
                            md_InputEntity.InputData = GetInputDataByDataTable(dataTable);
                            if (md_InputEntity.InputData.Count == 0)
                            {
                                return md_InputEntity;
                            }
                        }
                    }
                    //复合型的录入模型以后再处理
                    if (InputModel.IsMixModel)
                    {
                        md_InputEntity.ChildInputData = new Dictionary<string, string>();
                        Dictionary<string, string> _str = new Dictionary<string, string>();
                        foreach (MD_InputModel_Child _child in InputModel.ChildInputModel)
                        {
                            switch (_child.ChildModel.ModelType)
                            {
                                case "FORM":
                                //_ret.ChildInputData.Add(_child.ChildModel.ModelFullName, GetChildFormData(_child, _ret));
                                //break;
                                case "GRID":
                                    DataTable _dt = ChildInputData(_child, md_InputEntity, MySqlConnection);
                                    _str = GetInputDataByDataTable(_dt);
                                    foreach (string _t in _str.Keys)
                                    {
                                        if (!md_InputEntity.ChildInputData.ContainsKey(_t))
                                        {
                                            md_InputEntity.ChildInputData.Add(_t, _str[_t]);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            return md_InputEntity;
        }

        /// <summary>
        /// 报表补录数据获取数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public MD_InputEntity GetReportData(Dictionary<string, object> parameter)
        {
            string modelName = _inputModelName; //录入模型名称
            if (InputModel != null)
            {
                string className = InputModel.Descript.GetMetaByName2("ClassName");
                //IReportProcess reportProcess = (IReportProcess)Assembly.Load("SinoSZJS.BizCaseProcess").CreateInstance("SinoSZJS.BizCaseProcess.V2.InputModel.ReportProcess." + className);
                //if (reportProcess != null) reportProcess.DoAction(parameter);
            }
            return GetData(parameter);
        }

        /// <summary>
        /// 获取已用数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public MD_InputEntity GetData(Dictionary<string, object> parameter, bool isReadDefault = false)
        {
            MD_InputEntity md_InputEntity = new MD_InputEntity();
            md_InputEntity.InputModelName = _inputModelName;
            md_InputEntity.IsNewData = false;

            if (InputModel != null)
            {
                if (!string.IsNullOrEmpty(InputModel.GetDataGuideLine))
                {
                    DataTable dataTable = GuideLineAccessor.QueryGuideline(InputModel.GetDataGuideLine, parameter, RequestUser);
                    md_InputEntity.IsNewData = false;
                    if (dataTable != null)
                    {
                        if (InputModel.ModelType == "GRID")
                        {
                            md_InputEntity.InputData = GetGridInputDataByDatable(dataTable);
                        }
                        else
                        {
                            md_InputEntity.InputData = GetInputDataByDataTable(dataTable);
                            if (md_InputEntity.InputData.Count == 0)
                            {
                                return md_InputEntity;
                            }
                        }
                    }

                    if (isReadDefault)
                        SetUserDefaultValue(md_InputEntity);
                }
            }
            return md_InputEntity;
        }

        private DataTable ChildInputData(MD_InputModel_Child _child, MD_InputEntity entity, MySqlConnection MySqlConnection)
        {
            List<MD_InputEntity> list = new List<MD_InputEntity>();

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (MD_InputModel_ChildParam param in _child.Parameters)
            {
                string parameterValue = ConvetDataByMainEntityData(param.Value, entity);
                dictionary.Add(param.Name, parameterValue);
            }
            DataTable dataTable = null;
            dataTable = string.IsNullOrEmpty(_child.ChildModel.GetDataGuideLine) ? new DataTable("ResultTable") : GuideLineAccessor.QueryGuideline(_child.ChildModel.GetDataGuideLine, dictionary, RequestUser, MySqlConnection);
            return dataTable;
        }

        public DataTable ChildInputData(MD_InputModel_Child _child, MD_InputEntity entity)
        {
            DataTable dataTable = null;
            using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
            {
                dataTable = ChildInputData(_child, entity, MySqlConnection);
            }
            return dataTable;
        }

        private string ConvetDataByMainEntityData(string valuestr, MD_InputEntity entity)
        {
            string _ret = valuestr;
            foreach (string _key in entity.InputData.Keys)
            {
                string _field = string.Format("%{0}%", _key);
                object _valueObj = entity.InputData[_key];
                if (_valueObj != null)
                {
                    string _obj = _valueObj.ToString();
                    _ret = _ret.Replace(_field, _obj);
                }
                else
                {
                    _ret = _ret.Replace(_field, "");
                }
            }
            return _ret;
        }

        /// <summary>
        /// 获取已用数据
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="MySqlConnection"></param>
        /// <returns></returns>
        public MD_InputEntity GetData(Dictionary<string, object> parameter, MySqlConnection MySqlConnection)
        {
            MD_InputEntity mdInputEntity = new MD_InputEntity();
            mdInputEntity.InputModelName = _inputModelName;
            mdInputEntity.IsNewData = false;
            mdInputEntity.InputData = new Dictionary<string, string>();

            if (InputModel != null)
            {
                if (!string.IsNullOrEmpty(InputModel.GetDataGuideLine))
                {
                    DataTable dataTable = GuideLineAccessor.QueryGuideline(InputModel.GetDataGuideLine, parameter, RequestUser, MySqlConnection);
                    mdInputEntity.IsNewData = false;
                    if (dataTable != null)
                    {
                        if (InputModel.ModelType == "GRID")
                        {
                            mdInputEntity.InputData = GetGridInputDataByDatable(dataTable);
                        }
                        else
                        {
                            mdInputEntity.InputData = GetInputDataByDataTable(dataTable);
                            if (mdInputEntity.InputData.Count == 0)
                            {
                                return mdInputEntity;
                            }
                        }
                    }
                }
            }
            return mdInputEntity;
        }
        #endregion

        #region 将DataTable转换为XML列表，并添加到字典中
        /// <summary>
        /// 将DataTable转换为XML列表，并添加到字典中
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetGridInputDataByDatable(DataTable dataTable)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string xml = dataTable.DataTableToXml();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var xmlElement = xmlDoc.SelectSingleNode("DataTable") as XmlElement;
            if (xmlElement != null)
                xmlElement.SetAttribute("ModelName", _inputModelName);
            //XmlNodeList TableNode = XmlDoc.SelectNodes("//ResultTable");
            dictionary.Add(_inputModelName, xmlDoc.OuterXml);
            return dictionary;
        }
        #endregion


        #region Excel数据导入到Oracle数据库

        /// <summary>
        /// Excel数据导入到Oracle数据库
        /// </summary>
        /// <param name="inputEntity"></param>
        /// <param name="dataTable"></param>
        /// <param name="saveModelName"></param>
        /// <param name="sinoRequestUser"></param>
        /// <returns></returns>
        public static OperationResult SaveDataTable(MD_InputEntity inputEntity, DataSet dataSet, string saveModelName, SinoRequestUser sinoRequestUser)
        {
            DataTable dataTable = dataSet.Tables[0];
            var inputModelName = inputEntity.InputModelName;
            OperationResult operationResult = new OperationResult();
            switch (inputModelName)
            {
                case "XZZFKP.BL_EXCELIMPORT":
                    //BLBLR
                    try
                    {
                        #region 行政考评补录表数据
                        var year = inputEntity.InputData["Year"];
                        int intYear = int.Parse(year);

                        var kssj = new DateTime(intYear, 1, 1);
                        var jzsj = new DateTime(intYear, 12, 31);

                        const string SqlDeleteData = "delete from JSYW_XZKP_SJBLB  where ksrq=:KSRQ and jzrq=:JZRQ ";
                        using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
                        {
                            MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                            MySqlParameter[] MySqlParameter = {
                                new MySqlParameter(":KSRQ",MySqlDbType.Date),
                                new MySqlParameter(":JZRQ",MySqlDbType.Date)
                            };

                            MySqlParameter[0].Value = kssj;
                            MySqlParameter[1].Value = jzsj;
                            SqlHelper.ExecuteNonQuery(MySqlConnection, CommandType.Text, SqlDeleteData, MySqlParameter);

                            for (var index = 0; index < dataTable.Rows.Count; index++)
                            {
                                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                                dictionary.Add("DWID", dataTable.Rows[index][0].ToString());
                                dictionary.Add("RS", dataTable.Rows[index][2].ToString());
                                dictionary.Add("ZDAJBJS", dataTable.Rows[index][3].ToString());
                                dictionary.Add("ZDAJ1NNBJS", dataTable.Rows[index][4].ToString());
                                dictionary.Add("ZDAJ2NNBJS", dataTable.Rows[index][5].ToString());
                                dictionary.Add("BSAJSPS", dataTable.Rows[index][6].ToString());
                                dictionary.Add("BSAJBGS", dataTable.Rows[index][7].ToString());
                                dictionary.Add("FYSSS", dataTable.Rows[index][8].ToString());
                                dictionary.Add("FYSSBGS", dataTable.Rows[index][9].ToString());
                                dictionary.Add("FMRKJE", dataTable.Rows[index][10].ToString());
                                dictionary.Add("JIAFZ", dataTable.Rows[index][11].ToString());
                                dictionary.Add("JIANFZ", dataTable.Rows[index][12].ToString());
                                dictionary.Add("KSRQ", kssj.ToString());
                                dictionary.Add("JZRQ", jzsj.ToString());
                                dictionary.Add("CZSJ", DateTime.Now.ToString());
                                dictionary.Add("BLDWID", sinoRequestUser.SinoPost.PostDwId);
                                dictionary.Add("BLRYID", sinoRequestUser.BaseInfo.UserId);
                                dictionary.Add("BLID", Guid.NewGuid().ToString());
                                saveModelName = "XZZFKP.BLBLR";
                                //OraInputModelBuilder builder = new OraInputModelBuilder(saveModelName, sinoRequestUser);
                                //MD_InputEntity entity = new MD_InputEntity();
                                //entity.IsNewData = true;
                                //entity.InputData = new Dictionary<string, string>();
                                //entity.InputData = dictionary;
                                //entity.InputModelName = saveModelName;
                                //builder.WriteEntity(entity, MySqlConnection);
                            }
                            MySqlTransaction.Commit();
                            operationResult.Success = true;
                        }
                        #endregion
                    }
                    catch (Exception exception)
                    {
                        var errorMessage = string.Format("Exception 行政考评补录表数据写入数据库异常，错误信息为{0}", exception.StackTrace);
                        //OracleLogWriter.WriteSystemLog(errorMessage, "ERROR");
                        operationResult.Success = false;
                    }
                    break;
                default:
                    using (MySqlConnection MySqlConnection = SqlHelper.OpenConnection())
                    {
                        MySqlTransaction MySqlTransaction = MySqlConnection.BeginTransaction();
                        for (var index = 0; index < dataTable.Rows.Count; index++)
                        {
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();
                            dictionary.Add("DJLX_DJ", dataTable.Rows[index][0].ToString());
                            dictionary.Add("DJBH_DJ", dataTable.Rows[index][1].ToString());
                            dictionary.Add("DJMC_DJ", dataTable.Rows[index][2].ToString());
                            dictionary.Add("BZ_DJ", dataTable.Rows[index][3].ToString());
                            dictionary.Add("AJID_DJ", inputEntity.InputData["AJID_DJ"]);
                            dictionary.Add("AJFL_DJ", inputEntity.InputData["AJFL_DJ"]);
                            //dictionary.Add("ID_DJ", CommonAccessor.GetSequence_JSYW());

                            //OraInputModelBuilder builder = new OraInputModelBuilder(saveModelName, sinoRequestUser);
                            MD_InputEntity entity = new MD_InputEntity();
                            entity.IsNewData = true;
                            entity.InputData = new Dictionary<string, string>();
                            entity.InputData = dictionary;
                            entity.InputModelName = saveModelName;
                            //builder.WriteEntity(entity, MySqlConnection);
                        }
                        MySqlTransaction.Commit();
                    }
                    break;
            }
            return operationResult;
        }
        #endregion

        #region 设置默认值
        public void SetUserDefaultValue(MD_InputEntity entityData)
        {
            Dictionary<string, string> dictionary = InputModelAccessor.GetUserDefaultValue(this._inputModelName, this.RequestUser, "");
            if (dictionary != null && entityData != null)
            {
                foreach (string key in dictionary.Keys)
                {
                    object value = dictionary[key];
                    if (value != null && value.ToString().Trim() != "")
                    {
                        if (entityData.InputData.ContainsKey(key))
                        {
                            if (string.IsNullOrEmpty(entityData.InputData[key]))
                            {
                                entityData.InputData[key] = dictionary[key];
                            }
                        }
                        else
                        {
                            entityData.InputData.Add(key, dictionary[key]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取比较后的结果
        /// </summary>
        /// <param name="entityData">录入模型数据</param>
        /// <param name="userDefaultValueDic">用户默认值</param>
        public void GetComparedValue(MD_InputEntity entityData, Dictionary<string, string> userDefaultValueDic)
        {
            if (userDefaultValueDic != null && entityData != null)
            {
                foreach (string key in userDefaultValueDic.Keys)
                {
                    object value = userDefaultValueDic[key];
                    if (value != null && value.ToString().Trim() != "")
                    {
                        if (entityData.InputData.ContainsKey(key))
                        {
                            if (string.IsNullOrEmpty(entityData.InputData[key]))
                            {
                                entityData.InputData[key] = userDefaultValueDic[key];
                            }
                        }
                        else
                        {
                            entityData.InputData.Add(key, userDefaultValueDic[key]);
                        }
                    }
                }
            }
        }
        #endregion
    }
}
