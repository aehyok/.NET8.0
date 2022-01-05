using MySql.Data.MySqlClient;
using aehyok.Lib;
using aehyok.Lib.Config;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.MysqlDataAccess;

namespace aehyok.Core.MySql
{
    public class MyDA_MetaDataManager : IMetaDataManager
    {
        public async Task<bool> AddChildInputModel(string MainModelID, string ChildModelID)
        {
            string _sql = "insert into md_inputviewchild (id,iv_id,civ_id,param,displayorder) values (@ID,@IV_ID,@CIV_ID,'',0)";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_sql, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Lib.Snowflake.Instance.NextId()));
                    _cmd.Parameters.Add(new MySqlParameter("@IV_ID", Convert.ToInt64(MainModelID)));
                    _cmd.Parameters.Add(new MySqlParameter("@CIV_ID", Convert.ToInt64(ChildModelID)));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 向查询模型里添加子表
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <param name="_mainTableID"></param>
        /// <param name="_selectedTable"></param>
        /// <returns></returns>
        public async Task<bool> AddChildTableToQueryModel(string _queryModelID, string _mainTableID, MD_Table _selectedTable)
        {
            string InsertStr = "insert into md_viewtable (VTID,VIEWID,TID,TABLETYPE,CANCONDITION,DISPLAYNAME,DWDM,FATHERID) VALUES";
            InsertStr += " ( @VTID,@VIEWID,@TID,'F',1,@DISPLAYNAME,@DWDM,@FATHERID)";
            MySqlParameter[] _param = {
                 new MySqlParameter("@VTID", MySqlDbType.Int64),
                 new MySqlParameter("@VIEWID", MySqlDbType.Int64),
                 new MySqlParameter("@TID", MySqlDbType.Int64),
                 new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar,100),
                 new MySqlParameter("@DWDM", MySqlDbType.VarChar,12),
                 new MySqlParameter("@FATHERID",MySqlDbType.Int64)
                        };
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = Convert.ToInt64(_queryModelID);
            _param[2].Value = Convert.ToInt64(_selectedTable.TID);
            _param[3].Value = _selectedTable.DisplayTitle;
            _param[4].Value = _selectedTable.DWDM;
            _param[5].Value = Convert.ToInt64(_mainTableID);

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, InsertStr, _param);
            return true;


        }

        public bool AddInputModelTableColumn(string TableName, string AddFieldName, string DataType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 向查询模型里添加主表
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <param name="_selectedTable"></param>
        /// <returns></returns>
        public async Task<bool> AddMainTableToQueryModel(string _queryModelID, MD_Table _selectedTable)
        {
            string InsertStr = "insert into md_viewtable (VTID,VIEWID,TID,TABLETYPE,CANCONDITION,DISPLAYNAME,DWDM) VALUES ";
            InsertStr += "( @VTID,@VIEWID,@TID,'M',1,@DISPLAYNAME,@DWDM)";

            MySqlParameter[] _param = {
                 new MySqlParameter("@VTID", MySqlDbType.Int64),
                 new MySqlParameter("@VIEWID", MySqlDbType.Int64),
                 new MySqlParameter("@TID", MySqlDbType.Int64),
                 new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar,100),
                 new MySqlParameter("@DWDM", MySqlDbType.VarChar,12),
            };
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = Convert.ToInt64(_queryModelID);
            _param[2].Value = Convert.ToInt64(_selectedTable.TID);
            _param[3].Value = _selectedTable.DisplayTitle;
            _param[4].Value = _selectedTable.DWDM;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, InsertStr, _param);
            return true;
        }

        public bool AddNewConceptGroup(string _groupName)
        {
            throw new NotImplementedException();
        }

        public bool AddNewConceptTag(string _TagName, string _description, string _groupName)
        {
            throw new NotImplementedException();
        }

        public bool AddNewInputModelColumn(string InputModelID, string GroupID, string ColumnName)
        {
            throw new NotImplementedException();
        }

        public bool AddNewInputModelGroup(MD_InputModel_ColumnGroup Group)
        {
            throw new NotImplementedException();
        }

        public bool AddNewInputModelSavedTable(string InputModelID, string TableName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_AddNewViewExRight = "insert into md_view_exright (id,rvalue,rtitle,viewid,fid,displayorder) values (@ID,@RVALUE,@RTITLE,@VIEWID,@FID,0)";
        public async Task<bool> AddNewViewExRight(string RightValue, string RightTitle, string ViewID, MD_QueryModel_ExRight FatherRight)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_AddNewViewExRight, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                    _cmd.Parameters.Add(new MySqlParameter("@RVALUE", RightValue));
                    _cmd.Parameters.Add(new MySqlParameter("@RTITLE", RightTitle));
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(ViewID)));
                    _cmd.Parameters.Add(new MySqlParameter("@FID", (FatherRight == null) ? "0" : FatherRight.ID));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;

                }
                catch (Exception e)
                {
                    string _msg = string.Format("新建查询模型[{0}]相关联的模型扩展权限信息时发生错误，错误信息：{1} ", ViewID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }

        public bool AddSystemMenu(string _nodeCode)
        {
            throw new NotImplementedException();
        }

        public bool AddSystemSubMenu(string _fatherMenuID, string _nodeID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 添加表到查询模型的关联定义
        /// </summary>
        /// <param name="_tableID"></param>
        /// <param name="_modelName"></param>
        /// <returns></returns>
        public async Task<string> AddTableRelationView(string _tableID, string _modelName)
        {
            //查找是否已经有此关联定义
            string _isExistSql = "select count(id) from md_table2view where TID=@TID and VIEWNAME=@VIEWNAME ";
            MySqlParameter[] _isParam = {
               new MySqlParameter("@TID", MySqlDbType.Int64),
               new MySqlParameter("@VIEWNAME",MySqlDbType.VarChar,1000)
            };
            _isParam[0].Value = Convert.ToInt64(_tableID);
            _isParam[1].Value = _modelName;

            long _isResult = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _isExistSql, _isParam);
            if (_isResult > 0)
            {
                return string.Format("此表对应查询模型[{0}]的关联定义已经存在!", _modelName);
            }

            //添加关联定义
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" insert into md_table2view (ID,TID,VIEWNAME,CONDITIONSTR) ");
            _sb.Append(" values (@ID,@TID,@VIEWNAME,'') ");
            MySqlParameter[] _Param = {
               new MySqlParameter("@ID", Guid.NewGuid().ToString()),
               new MySqlParameter("@TID",Convert.ToInt64(_tableID)),
               new MySqlParameter("@VIEWNAME",_modelName)
            };
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _Param);
            return "";
        }

        private const string SQL_AddView2View = @"
            insert into md_view2view (ID,VIEWID,DISPLAYORDER,DISPLAYTITLE,GROUPID) 
            values (@ID,@VIEWID,0,'未设置的关联模型',@GROUPID)";
        public async Task<string> AddView2View(string ViewID, string GroupID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_AddView2View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(ViewID)));
                    _cmd.Parameters.Add(new MySqlParameter("@GROUPID", GroupID));
                    await _cmd.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _msg = string.Format("在新建查询模型{0}相关联的模型信息时发生错误，错误信息：{1} ", ViewID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        private const string SQL_AddView2ViewGroup = @"
            insert into md_view2viewgroup (ID,VIEWID,DISPLAYORDER,DISPLAYTITLE) values (@ID,@VIEWID,0,'未命名分组')";
        public async Task<string> AddView2ViewGroup(string ViewID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_AddView2ViewGroup, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Guid.NewGuid().ToString()));
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(ViewID)));
                    await _cmd.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _msg = string.Format("在新建查询模型{0}相关联模型分组信息时发生错误，错误信息：{1} ", ViewID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        private const string SQL_CMD_DelView2GL = @"delete from md_view2gl where ID=@ID";
        public async Task<string> CMD_DelView2GL(MD_View_GuideLine View2GL)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmdDel = new MySqlCommand(SQL_CMD_DelView2GL, cn);
                    _cmdDel.Parameters.Add(new MySqlParameter("@ID", View2GL.ID));
                    await _cmdDel.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _msg = string.Format("删除查询模型[{0}]的关联指标扩展信息时发生错误，错误信息：{1} ", View2GL.ViewID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        private const string SQL_DelView2View = @"delete from md_view2view where ID=@ID";
        public async Task<string> CMD_DelView2View(string v2vid)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_DelView2View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", v2vid));
                    await _cmd.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _msg = string.Format("在删除查询模型相关联的模型信息{0}时发生错误，错误信息：{1} ", v2vid, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        private const string SQL_CheckDelViewExRight = @"select count(*) from md_view_exright where FID=@FID ";
        private const string SQL_DelViewExRight = @"delete from md_view_exright where ID=@ID";
        public async Task<string> CMD_DelViewExRight(MD_QueryModel_ExRight ExRight)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmdCheck = new MySqlCommand(SQL_CheckDelViewExRight, cn);
                    _cmdCheck.Parameters.Add(new MySqlParameter("@FID", ExRight.ID));
                    long _ret = (long)await _cmdCheck.ExecuteScalarAsync();
                    if (_ret > 0) return "请先删除子权限！";

                    MySqlCommand _cmdDel = new MySqlCommand(SQL_DelViewExRight, cn);
                    _cmdDel.Parameters.Add(new MySqlParameter("@ID", ExRight.ID));
                    await _cmdDel.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _msg = string.Format("删除查询模型[{0}]的模型扩展权限信息时发生错误，错误信息：{1} ", ExRight.ModelID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        private const string SQL_DelGuideLineGroup = "delete from tj_zbztmcdyb where ZBZTMC=@ZBZTMC";
        public async Task<bool> DelGuideLineGroup(string _guideLineGroupName)
        {

            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmdDel = new MySqlCommand(SQL_DelGuideLineGroup, cn);
                    _cmdDel.Parameters.Add(new MySqlParameter("@ZBZTMC", _guideLineGroupName));
                    await _cmdDel.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception e)
                {
                    string _msg = string.Format("删除指标主题[{0}]的模型扩展权限信息时发生错误，错误信息：{1} ", _guideLineGroupName, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }

        }

        //private const string SQL_DelGuideLine = " delete from tj_zdyzbdyb where id in (select t.id from tj_zdyzbdyb t  START WITH   id =:ID  CONNECT BY PRIOR  id=fid )";
        //private const string SQL_DelGuideLine = " delete from tj_zdyzbdyb where id =@ID";
        private const string SQL_DelGuideLine = "mdproc_delete_zb";
        public async Task<bool> DelGuideLine(string _guideLineID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmdDel = new MySqlCommand(SQL_DelGuideLine, cn);
                    _cmdDel.CommandType = CommandType.StoredProcedure;
                    MySqlParameter _p1 = new MySqlParameter("data", Int64.Parse(_guideLineID));
                    _p1.Direction = ParameterDirection.Input;
                    _cmdDel.Parameters.Add(_p1);
                    await _cmdDel.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception e)
                {
                    string _msg = string.Format("删除指标[{0}]的模型扩展权限信息时发生错误，错误信息：{1} ", _guideLineID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }



        public bool DelConceptTag(string _CTag)
        {
            throw new NotImplementedException();
        }

        public bool DelConcpetGroup(string p)
        {
            throw new NotImplementedException();
        }
        public bool DelInputModel(string InputModelID)
        {
            throw new NotImplementedException();
        }

        public bool DelInputModelChild(string ChildModelID)
        {
            throw new NotImplementedException();
        }

        public bool DelInputModelColumn(string ColumnID)
        {
            throw new NotImplementedException();
        }

        public bool DelInputModelColumnGroup(string InputModelID, string GroupID)
        {
            throw new NotImplementedException();
        }

        public bool DelInputModelSavedTable(string TableID)
        {
            throw new NotImplementedException();
        }

        public bool DelInputModelTableColumn(string TableName, string DelFieldName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_DelNamespace = "delete from md_tbnamespace where NAMESPACE=@NAMESPACE  ";
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<bool> DelNamespace(MD_Namespace _ns)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    MySqlParameter[] _param = {
                           new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 100)
                    };
                    _param[0].Value = _ns.NameSpace;
                    await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, SQL_DelNamespace, _param);
                    txn.Commit();
                    MyDA_MetaDataQuery.ModelLib.Clear();
                    return true;
                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    return false;
                }
            }

        }

        private const string SQL_DelNodes = @"delete from md_nodes where ID=@ID ";
        /// <summary>
        /// 删除节点信息
        /// </summary>
        /// <param name="_nodeID"></param>
        /// <returns></returns>
        public async Task<bool> DelNodes(string _nodeID)
        {
            MySqlParameter[] _param = {
                 new MySqlParameter("@ID", MySqlDbType.VarChar, 100)
            };
            _param[0].Value = _nodeID;
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_DelNodes, _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }

        private const string SQL_DelRefTable = @"delete from md_reftablelist  where RTID=@RTID";
        public async Task<bool> DelRefTable(string RefTableID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_DelRefTable, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@RTID", Convert.ToInt64(RefTableID)));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        private const string SQL_RefreshAllFatherCode = "Generate_dm_reftabledata_allfathercode";
        public async Task<bool> RefreshAllFatherCode()
        {
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.queryString, CommandType.StoredProcedure, SQL_RefreshAllFatherCode);
            return true;
        }

        private const string SQL_RefreshAreaMap = "Generate_AreaMap";
        public async Task<bool> RefreshAreaMap()
        {
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.queryString, CommandType.StoredProcedure, SQL_RefreshAreaMap);
            return true;
        }

        public bool DelSystemMenu(string _menuid)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DelTableMeta(string _tableID)
        {
            string _del2 = "delete from md_tablecolumn where TID=@TID ";
            string _del = "delete from md_table where TID =@TID";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(_del2, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@TID", Convert.ToInt64(_tableID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(_del, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@TID", Convert.ToInt64(_tableID)));
                    await _cmd.ExecuteNonQueryAsync();

                    txn.Commit();
                    MyDA_MetaDataQuery.ModelLib.Clear();
                }
                catch (Exception ex)
                {
                    string _errStr = string.Format("删除表定义时发生错误! TableID={0} \n\r ErrorMsg:{1}  ",
                                    _tableID, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_errStr, "ERROR");
                    txn.Rollback();
                    return false;
                }
            }
            return true;
        }

        private const string SQL_DelView2ViewGroup = @"delete from md_view2viewgroup where ID=@ID";
        private const string SQL_DelView2ViewGroup2 = @"delete from md_view2view where GROUPID=@ID";
        public async Task<string> DelView2ViewGroup(string GroupID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_DelView2ViewGroup2, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", GroupID));
                    await _cmd.ExecuteNonQueryAsync();

                    MySqlCommand _cmd2 = new MySqlCommand(SQL_DelView2ViewGroup, cn);
                    _cmd2.Parameters.Add(new MySqlParameter("@ID", GroupID));
                    await _cmd2.ExecuteNonQueryAsync();
                    txn.Commit();
                    return "";
                }
                catch (Exception e)
                {
                    txn.Rollback();
                    string _msg = string.Format("删除查询相关联模型分组{0}信息时发生错误，错误信息：{1} ", GroupID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return _msg;
                }
            }
        }

        public async Task<bool> DelViewAndChildren(string QueryModelID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {

                    MySqlCommand _cmd = new MySqlCommand(DelView_ViewTableColumn, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_ViewTable, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View2View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View2ViewGroup, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_InGroup, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View2Guideline, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View2Application, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    txn.Commit();
                }
                catch (Exception ex)
                {
                    string _errStr = string.Format("删除查询模型及其子对象定义时发生错误! QueryModelID={0} \n\r ErrorMsg:{1}  ",
                                    QueryModelID, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_errStr, "ERROR");
                    txn.Rollback();
                    return false;
                }
            }
            return true;
        }

        private const string DelView_ViewTableColumn = @"delete from md_viewtablecolumn vtc where vtc.vtid in  (
														select vt.vtid from md_viewtable vt where vt.VIEWID=@VIEWID) ";
        private const string DelView_ViewTable = "delete from md_viewtable vt where vt.VIEWID=@VIEWID ";
        private const string DelView_View = "delete from md_view where VIEWID=@VIEWID";
        private const string DelView_InGroup = "delete from md_viewgroupitem where VIEWID=@VIEWID";
        private const string DelView_View2ViewGroup = "delete from md_view2viewgroup where VIEWID=@VIEWID";
        private const string DelView_View2View = "delete from md_view2view where VIEWID=@VIEWID";
        private const string DelView_View2Guideline = "delete from md_view2gl where VIEWID=@VIEWID";
        private const string DelView_View2Application = "delete from md_view2app where viewid=@VIEWID";

        /// <summary>
        /// 删除查询模型
        /// </summary>
        /// <param name="QueryModelID"></param>
        /// <returns></returns>
        public async Task<bool> DelViewMeta(string QueryModelID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(DelView_View2View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View2ViewGroup, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_InGroup, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();

                    _cmd = new MySqlCommand(DelView_View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    await _cmd.ExecuteNonQueryAsync();


                    txn.Commit();
                }
                catch (Exception ex)
                {
                    string _errStr = string.Format("删除查询模型及其子对象定义时发生错误! QueryModelID={0} \n\r ErrorMsg:{1}  ",
                                    QueryModelID, ex.Message);
                    MysqlLogWriter.WriteSystemLog(_errStr, "ERROR");
                    txn.Rollback();
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 删除查询模型中的表
        /// </summary>
        /// <param name="_viewTableID"></param>
        /// <returns></returns>
        public async Task<bool> DelViewTable(string _viewTableID)
        {
            string _del = "delete from md_viewtablecolumn WHERE VTID= @VTID";
            string _del2 = "delete from md_viewtable WHERE VTID =@VTID ";
            MySqlParameter[] _param = {
                 new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param[0].Value = Convert.ToInt64(_viewTableID);

            MySqlParameter[] _param2 = {
                  new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param2[0].Value = Convert.ToInt64(_viewTableID);

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _del, _param);
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _del2, _param2);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }

        public bool FindInputModelColumnByName(string InputModelID, string ColumnName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetAllQueryModelNames = @"select NAMESPACE,VIEWNAME from md_view ";
        public async Task<IList<string>> GetAllQueryModelNames()
        {
            List<string> _ret = new List<string>();
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetAllQueryModelNames);

            while (await dr.ReadAsync())
            {
                _ret.Add(string.Format("{0}.{1}",
                         dr.IsDBNull(0) ? "" : dr.GetString(0),
                         dr.IsDBNull(1) ? "" : dr.GetString(1)
                ));
            }
            dr.Close();
            return _ret;
        }

        private const string SQL_GetChildGuideLines = "select ID,ZBMC,ZBZT,ZBSF, ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH,ZBSM from tj_zdyzbdyb where FID=@FID ORDER BY XSXH ASC";
        public async Task<IList<MD_GuideLine>> GetChildGuideLines(string _fatherGuildLineID)
        {
            IList<MD_GuideLine> _ret = new List<MD_GuideLine>();

            MySqlParameter[] _param = {
                                new MySqlParameter("@FID",MySqlDbType.Decimal)
                        };
            _param[0].Value = decimal.Parse(_fatherGuildLineID);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetChildGuideLines, _param);
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

        private const string SQL_GetColumnOfTable = @"
                            select TCID,TID,COLUMNNAME,ISNULLABLE,TYPE,`PRECISION`,SCALE,LENGTH,REFDMB,DMBLEVELFORMAT,SECRETLEVEL,
                            DISPLAYTITLE,DISPLAYFORMAT,DISPLAYLENGTH,DISPLAYHEIGHT,DISPLAYORDER,CANDISPLAY,COLWIDTH, DWDM,CTAG,REFWORDTB 
                            from md_tablecolumn where TCID = @TCID order by DISPLAYORDER";
        public async Task<MD_TableColumn> GetColumnOfTable(string _tcid)
        {
            MD_TableColumn nodeInfo = null;
            MySqlParameter[] _param = {
                new MySqlParameter("@TCID", MySqlDbType.Int64),
            };
            _param[0].Value = Convert.ToInt64(_tcid);
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetColumnOfTable, _param);
            while (await dr.ReadAsync())
            {
                nodeInfo = new MD_TableColumn(
                     dr.GetInt64(0).ToString(),
                     dr.GetInt64(1).ToString(),
                     dr.GetString(2),
                     dr.IsDBNull(3) ? true : ((dr.GetString(3) == "Y") ? true : false),
                     dr.GetString(4),
                     dr.IsDBNull(5) ? 1 : dr.GetInt32(5),
                     dr.IsDBNull(6) ? 1 : dr.GetInt32(6),
                     dr.IsDBNull(7) ? 1 : dr.GetInt32(7),
                     dr.IsDBNull(8) ? "" : dr.GetString(8),
                     dr.IsDBNull(9) ? "" : dr.GetString(9),
                     dr.IsDBNull(10) ? 0 : dr.GetInt32(10),
                     dr.IsDBNull(11) ? "" : dr.GetString(11),
                     dr.IsDBNull(12) ? "" : dr.GetString(12),
                     dr.IsDBNull(13) ? 0 : dr.GetInt32(13),
                     dr.IsDBNull(14) ? 0 : dr.GetInt32(14),
                     dr.IsDBNull(15) ? 0 : dr.GetInt32(15),
                     dr.IsDBNull(16) ? false : dr.GetInt32(16) > 0,
                     dr.IsDBNull(17) ? 0 : dr.GetInt32(17),
                     dr.IsDBNull(18) ? "" : dr.GetString(18),
                     dr.IsDBNull(19) ? "" : dr.GetString(19),
                     dr.IsDBNull(20) ? "" : dr.GetString(20)
                     );
            }
            dr.Close();
            return nodeInfo;
        }


        private const string SQL_GetColumnsOfViewTable = @"
                select vtc.VTCID,vtc.VTID,vtc.TCID,vtc.CANCONDITIONSHOW,vtc.CANRESULTSHOW,vtc.DEFAULTSHOW,vtc.FIXQUERYITEM,vtc.CANMODIFY,
                  vtc.dwdm,vtc.PRIORITY,case when vtc.DISPLAYORDER =0 or  vtc.DISPLAYORDER is null then 
                  (select tc.displayorder from md_tablecolumn tc where tc.tcid=vtc.tcid) else vtc.DISPLAYORDER end  VTCORDER                 
                  from md_viewtablecolumn vtc 
                  where vtc.VTID = @VTID order by vtcorder ";
        private async Task<IList<MD_ViewTableColumn>> GetColumnsOfViewTable(MD_ViewTable _vt)
        {
            IList<MD_ViewTableColumn> nodeItems = new List<MD_ViewTableColumn>();
            MD_ViewTableColumn nodeInfo;
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlCommand _cmd = new MySqlCommand(SQL_GetColumnsOfViewTable, cn);
                _cmd.Parameters.Add(new MySqlParameter("@VTID", _vt.ViewTableID));
                MySqlDataReader _dr = _cmd.ExecuteReader();
                while (await _dr.ReadAsync())
                {
                    nodeInfo = new MD_ViewTableColumn(
                       _dr.IsDBNull(0) ? "" : _dr.GetInt64(0).ToString(),
                       _dr.IsDBNull(1) ? "" : _dr.GetInt64(1).ToString(),
                       _dr.IsDBNull(2) ? "" : _dr.GetInt64(2).ToString(),
                       _dr.IsDBNull(3) ? false : _dr.GetInt32(3) > 0,
                       _dr.IsDBNull(4) ? false : _dr.GetInt32(4) > 0,
                       _dr.IsDBNull(5) ? false : _dr.GetInt32(5) > 0,
                       _dr.IsDBNull(6) ? false : _dr.GetInt32(6) > 0,
                       _dr.IsDBNull(7) ? false : _dr.GetInt32(7) > 0,
                       _dr.IsDBNull(8) ? "" : _dr.GetString(8),
                       _dr.IsDBNull(9) ? 0 : _dr.GetInt32(9),
                       _dr.IsDBNull(10) ? 0 : _dr.GetInt32(10)
                       );
                    nodeInfo.TableColumn = await GetColumnOfTable(nodeInfo.ColumnID);
                    nodeInfo.TID = _vt.TableID;
                    nodeInfo.TableName = _vt.TableName;
                    nodeItems.Add(nodeInfo);
                }
                _dr.Close();
            }
            return nodeItems;
        }

        /// <summary>
        /// 取查询模型主表的子表
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public async Task<IList<MD_ViewTable>> GetChildTableOfQueryModel(MD_QueryModel _queryModel)
        {
            return await GetChildTableOfQueryModel(_queryModel.QueryModelID);
        }

        private const string SQL_GetChildTableOfQueryModel = @"
                select VTID,VIEWID,TID,TABLETYPE,TABLERELATION,CANCONDITION,DISPLAYNAME,DISPLAYORDER,DWDM,
                FATHERID,PRIORITY,DISPLAYTYPE,INTEGRATEDAPP from md_viewtable where VIEWID = @VID and TABLETYPE = 'F' order by DISPLAYORDER";

        public async Task<IList<MD_ViewTable>> GetChildTableOfQueryModel(string QueryModelID)
        {
            IList<MD_ViewTable> _ret = new List<MD_ViewTable>();

            MySqlParameter[] _param = {
                 new MySqlParameter("@VID",MySqlDbType.Int64),
            };
            _param[0].Value = Convert.ToInt64(QueryModelID);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetChildTableOfQueryModel, _param);

            while (await dr.ReadAsync())
            {
                MD_ViewTable _vt = new MD_ViewTable(
                    dr.GetInt64(0).ToString(),
                    dr.GetInt64(1).ToString(),
                    dr.GetInt64(2).ToString(),
                    dr.IsDBNull(3) ? "M" : dr.GetString(3),
                    dr.IsDBNull(4) ? "" : dr.GetString(4),
                    dr.IsDBNull(5) ? "" : dr.GetString(5),
                    dr.IsDBNull(6) ? "" : dr.GetString(6),
                    dr.IsDBNull(7) ? 0 : dr.GetInt32(7),
                    dr.IsDBNull(8) ? "" : dr.GetString(8),
                    dr.IsDBNull(9) ? "" : dr.GetInt64(9).ToString(),
                    dr.IsDBNull(10) ? 0 : dr.GetInt32(10),
                    dr.IsDBNull(11) ? 0 : dr.GetInt32(11),
                    dr.IsDBNull(12) ? "" : dr.GetString(12)
                    );
                _vt.Table = await GetTableByTableID(_vt.TableID);
                _vt.TableName = _vt.Table.TableName;
                _vt.Columns = await GetColumnsOfViewTable(_vt);
                _ret.Add(_vt);
            }
            dr.Close();

            return _ret;
        }

        /// <summary>
        /// 取表的列定义
        /// </summary>
        /// <param name="_tid"></param>
        /// <returns></returns>
        public async Task<IList<MD_TableColumn>> GetColumnsOfTable(string _tid)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("select TCID,TID,COLUMNNAME,");
            _sb.Append(" ISNULLABLE,TYPE,`PRECISION`,");
            _sb.Append(" SCALE,LENGTH,REFDMB,");
            _sb.Append(" DMBLEVELFORMAT,SECRETLEVEL,DISPLAYTITLE,");
            _sb.Append(" DISPLAYFORMAT,DISPLAYLENGTH,DISPLAYHEIGHT,");
            _sb.Append(" DISPLAYORDER,CANDISPLAY,COLWIDTH,");
            _sb.Append(" DWDM,CTAG,REFWORDTB");
            _sb.Append(" from md_tablecolumn where TID = @TID order by DISPLAYORDER");

            MySqlParameter[] _param = {
               new MySqlParameter("@TID", MySqlDbType.Int64),
            };
            _param[0].Value = Convert.ToInt64(_tid);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);
            IList<MD_TableColumn> nodeItems = new List<MD_TableColumn>();
            while (await dr.ReadAsync())
            {
                MD_TableColumn nodeInfo = new MD_TableColumn(
                   dr.GetInt64(0).ToString(),
                   dr.GetInt64(1).ToString(),
                   dr.GetString(2),
                   dr.IsDBNull(3) ? true : ((dr.GetString(3) == "Y") ? true : false),
                   dr.GetString(4),
                   dr.IsDBNull(5) ? 1 : dr.GetInt32(5),
                   dr.IsDBNull(6) ? 1 : dr.GetInt32(6),
                   dr.IsDBNull(7) ? 1 : dr.GetInt32(7),
                   dr.IsDBNull(8) ? "" : dr.GetString(8),
                   dr.IsDBNull(9) ? "" : dr.GetString(9),
                   dr.IsDBNull(10) ? 0 : dr.GetInt32(10),
                   dr.IsDBNull(11) ? "" : dr.GetString(11),
                   dr.IsDBNull(12) ? "" : dr.GetString(12),
                   dr.IsDBNull(13) ? 0 : dr.GetInt32(13),
                   dr.IsDBNull(14) ? 0 : dr.GetInt32(14),
                   dr.IsDBNull(15) ? 0 : dr.GetInt32(15),
                   dr.IsDBNull(16) ? false : dr.GetInt32(16) > 0,
                   dr.IsDBNull(17) ? 0 : dr.GetInt32(17),
                   dr.IsDBNull(18) ? "" : dr.GetString(18),
                   dr.IsDBNull(19) ? "" : dr.GetString(19),
                   dr.IsDBNull(20) ? "" : dr.GetString(20)
                 );
                nodeItems.Add(nodeInfo);
            }
            dr.Close();

            return nodeItems;
        }

        public IList<MD_ConceptGroup> GetConceptGroups()
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetDBColumnsOfTable = @"
                SELECT COLUMN_NAME,COLUMN_COMMENT,DATA_TYPE,IS_NULLABLE,CHARACTER_MAXIMUM_LENGTH,NUMERIC_PRECISION FROM information_schema.`COLUMNS` t WHERE t.TABLE_NAME = @TABLENAME and t.TABLE_SCHEMA = @TSCHEMA ";
        /// <summary>
        ///  从数据库中取表的字段定义
        /// </summary>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        public async Task<IList<DB_ColumnMeta>> GetDBColumnsOfTable(string _tableName)
        {
            string[] _names = _tableName.Split('.');

            MySqlParameter[] _param = {
                new MySqlParameter("@TABLENAME",MySqlDbType.VarChar),
                new MySqlParameter("@TSCHEMA",MySqlDbType.VarChar)
            };

            if (_names.Length < 2)
            {
                _param[0].Value = _names[0];
                _param[1].Value = "metadata";
            }
            else
            {
                _param[0].Value = _names[1];
                _param[1].Value = _names[0];
            }
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetDBColumnsOfTable, _param);

            IList<DB_ColumnMeta> nodeItems = new List<DB_ColumnMeta>();

            while (await dr.ReadAsync())
            {
                DB_ColumnMeta nodeInfo = new DB_ColumnMeta();
                nodeInfo.ColumnName = dr.IsDBNull(0) ? "" : dr.GetString(0);
                nodeInfo.Comments = dr.IsDBNull(1) ? "" : dr.GetString(1);
                nodeInfo.DataType = dr.IsDBNull(2) ? "" : dr.GetString(2);
                nodeInfo.Nullable = dr.IsDBNull(3) ? true : ((dr.GetString(3) == "Y") ? true : false);
                nodeInfo.DataLength = dr.IsDBNull(4) ? 0 : dr.GetInt64(4);
                nodeInfo.DataPrecision = dr.IsDBNull(5) ? 0 : dr.GetInt32(5);

                nodeItems.Add(nodeInfo);
            }
            dr.Close();

            return nodeItems;
        }

        public IList<string> GetDBPrimayKeyList(string TableName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetDBTableList = @"SELECT TABLE_NAME TNAME,TABLE_COMMENT COMMENTS,TABLE_TYPE TYPE,TABLE_SCHEMA OWNER  FROM  information_schema.`TABLES` WHERE TABLE_SCHEMA not in ('sys','performance_schema','mysql','metadata','information_schema') ";
        /// <summary>
        /// 取数据库中的表的列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DB_TableMeta>> GetDBTableList()
        {
            // 数据库名

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetDBTableList);

            IList<DB_TableMeta> tableList = new List<DB_TableMeta>();

            while (await dr.ReadAsync())
            {
                string _tanme = dr.GetString(0);
                string _owner = dr.GetString(3);
                string _comment = dr.IsDBNull(1) ? "" : dr.GetString(1);
                string _type = dr.GetString(2);

                DB_TableMeta nodeInfo = new DB_TableMeta();
                nodeInfo.TableName = string.Format("{0}.{1}", _owner, _tanme);
                nodeInfo.TableComment = _comment;
                nodeInfo.TableType = _type;
                tableList.Add(nodeInfo);
            }
            dr.Close();
            return tableList;
        }

        /// <summary>
        ///  取数据库中的代码表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DB_TableMeta>> GetDBTableListOfDMB()
        {
            string cmdStr = "SELECT TABLE_NAME TNAME,TABLE_COMMENT COMMENTS,TABLE_TYPE TYPE,TABLE_SCHEMA OWNER  FROM  information_schema.`TABLES` WHERE TABLE_SCHEMA NOT IN ('sys','performance_schema','mysql','information_schema') AND TABLE_NAME LIKE 'DM%' ";

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, cmdStr);

            IList<DB_TableMeta> tableList = new List<DB_TableMeta>();

            while (await dr.ReadAsync())
            {
                string _tanme = dr.GetString(0);
                string _owner = dr.GetString(3);
                string _comment = dr.IsDBNull(1) ? "" : dr.GetString(1);
                string _type = dr.GetString(2);

                DB_TableMeta nodeInfo = new DB_TableMeta();
                nodeInfo.TableName = string.Format("{0}.{1}", _owner, _tanme);
                nodeInfo.TableComment = _comment;
                nodeInfo.TableType = _type;
                tableList.Add(nodeInfo);
            }
            dr.Close();
            return tableList;
        }

        public MD_GuideLine GetGuideLineDefine(string _guideLineID)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetGuideLineGroup = "select ZBZTMC,ZBZTSM,LX,QXLX,NAMESPACE,SSDW from tj_zbztmcdyb where SSDW=@DWDM and LX=@LX";
        public async Task<IList<MD_GuideLineGroup>> GetGuideLineGroup(string _nodeCode, string _guideLineGroupType)
        {
            MySqlParameter[] _param = {
                new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                new MySqlParameter("@LX",MySqlDbType.Int32)
            };

            _param[0].Value = _nodeCode;
            _param[1].Value = Convert.ToInt32(_guideLineGroupType);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetGuideLineGroup, _param);
            IList<MD_GuideLineGroup> _ret = new List<MD_GuideLineGroup>();

            while (await dr.ReadAsync())
            {
                MD_GuideLineGroup _vt = new MD_GuideLineGroup(
                   dr.GetString(0),
                   dr.IsDBNull(1) ? "" : dr.GetString(1),
                   dr.IsDBNull(4) ? "" : dr.GetString(4),
                   dr.IsDBNull(5) ? "" : dr.GetString(5),
                   dr.IsDBNull(2) ? 0 : dr.GetInt32(2),
                   dr.IsDBNull(3) ? 0 : dr.GetInt32(3)
                   );
                _ret.Add(_vt);
            }
            dr.Close();
            return _ret;
        }

        private const string SQL_GetGuideLineOfGroup = "select ID,ZBMC,ZBZT,ZBSF, ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH,ZBSM from tj_zdyzbdyb where ZBZT=@ZBZT and FID=1 order by XSXH ASC";
        public async Task<IList<MD_GuideLine>> GetGuideLineOfGroup(string _groupName)
        {
            IList<MD_GuideLine> _ret = new List<MD_GuideLine>();
            MySqlParameter[] _param = {
                                new MySqlParameter("@ZBZT",MySqlDbType.VarChar,200)
                        };
            _param[0].Value = _groupName;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetGuideLineOfGroup, _param);

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

        public Task<MD_InputModel> GetInputModel(string _namespace, string ModelName)
        {
            throw new NotImplementedException();
        }

        public Task<IList<MD_InputModel>> GetInputModelOfNamespace(string _namespace)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取查询模型的主表
        /// </summary>
        /// <param name="_qm"></param>
        /// <returns></returns>
        public async Task<MD_ViewTable> GetMainTableOfQueryModel(MD_QueryModel _qm)
        {
            return await GetMainTableOfQueryModel(_qm.QueryModelID);
        }

        private const string SQL_GetMainTableOfQueryModel = @"
                select vt.VTID,vt.VIEWID,vt.TID,
                vt.TABLETYPE,vt.TABLERELATION,vt.CANCONDITION,
                vt.DISPLAYNAME,vt.DISPLAYORDER,vt.DWDM,
                vt.FATHERID,vt.PRIORITY,vt.DISPLAYTYPE,vt.INTEGRATEDAPP,v.namespace
                from md_viewtable vt
                join md_view v on  v.viewid=vt.viewid
                where vt.VIEWID = @VID and vt.TABLETYPE = 'M' order by vt.DISPLAYORDER";
        public async Task<MD_ViewTable> GetMainTableOfQueryModel(string QueryModelID)
        {
            MD_ViewTable _vt = null;

            MySqlParameter[] _param = {
                                new MySqlParameter("@VID", MySqlDbType.Int64),
                        };
            _param[0].Value = Convert.ToInt64(QueryModelID);

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetMainTableOfQueryModel, _param);

            while (await dr.ReadAsync())
            {
                _vt = new MD_ViewTable(dr.GetInt64(0).ToString(),
                   dr.GetInt64(1).ToString(),
                   dr.GetInt64(2).ToString(),
                   dr.IsDBNull(3) ? "M" : dr.GetString(3),
                   dr.IsDBNull(4) ? "" : dr.GetString(4),
                   dr.IsDBNull(5) ? "" : dr.GetString(5),
                   dr.IsDBNull(6) ? "" : dr.GetString(6),
                   dr.IsDBNull(7) ? 0 : dr.GetInt32(7),
                   dr.IsDBNull(8) ? "" : dr.GetString(8),
                   dr.IsDBNull(9) ? "" : dr.GetInt64(9).ToString(),
                   dr.IsDBNull(10) ? 0 : dr.GetInt32(10),
                   dr.IsDBNull(11) ? 0 : dr.GetInt32(11),
                   dr.IsDBNull(12) ? "" : dr.GetString(12));
                _vt.NamespaceName = dr.IsDBNull(13) ? "" : dr.GetString(13);
            }
            dr.Close();
            if (_vt != null)
            {
                _vt.Table = await GetTableByTableID(_vt.TableID);
                _vt.TableName = _vt.Table.TableName;
                _vt.Columns = await GetColumnsOfViewTable(_vt);
            }
            return _vt;
        }

        public IList<MD_Menu> GetMenuDefineOfNode(string _nodeCode)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetNameSpaceAtNode = @"SELECT NAMESPACE,DESCRIPTION,MENUPOSITION,DISPLAYTITLE,OWNER,DISPLAYORDER,DWDM,CONCEPTS FROM md_tbnamespace
                                                        where DWDM = @DWDM order by DISPLAYORDER ASC";
        /// <summary>
        /// 获取节点下的命名空间
        /// </summary>
        /// <param name="_nodeDWDM"></param>
        /// <returns></returns>
        public async Task<IList<MD_Namespace>> GetNameSpaceAtNode(string _nodeDWDM)
        {
            MySqlParameter[] _param = {
                                new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                        };
            _param[0].Value = _nodeDWDM;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetNameSpaceAtNode, _param);

            IList<MD_Namespace> nodeItems = new List<MD_Namespace>();

            while (await dr.ReadAsync())
            {
                MD_Namespace nodeInfo = new MD_Namespace();
                nodeInfo.NameSpace = dr.GetString(0);
                nodeInfo.Description = dr.IsDBNull(1) ? "" : dr.GetString(1);
                nodeInfo.MenuPosition = dr.IsDBNull(2) ? "" : dr.GetString(2);
                nodeInfo.DisplayTitle = dr.IsDBNull(3) ? "" : dr.GetString(3);
                nodeInfo.Owner = dr.IsDBNull(4) ? "" : dr.GetString(4);
                nodeInfo.DisplayOrder = dr.IsDBNull(5) ? 0 : dr.GetInt32(5);
                nodeInfo.DWDM = dr.IsDBNull(6) ? "" : dr.GetString(6);
                nodeInfo.Concepts = dr.IsDBNull(7) ? "" : dr.GetString(7);
                nodeItems.Add(nodeInfo);
            }
            dr.Close();
            return nodeItems;
        }

        public string GetNewID()
        {
            return Lib.Snowflake.Instance.NextId().ToString();
        }

        private const string SQL_GetNodeList = @"SELECT ID,NODENAME,DISPLAYTITLE,DESCRIPT,DWDM FROM md_nodes";
        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MD_Nodes>> GetNodeList()
        {
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetNodeList);
            IList<MD_Nodes> nodeItems = new List<MD_Nodes>();

            while (await dr.ReadAsync())
            {
                MD_Nodes nodeInfo = new MD_Nodes(dr.GetString(0), dr.GetString(1),
                                dr.GetString(2), dr.GetString(3), dr.GetString(4));
                nodeItems.Add(nodeInfo);
            }
            dr.Close();
            return nodeItems;
        }

        /// <summary>
        ///  /// <summary>
        /// 取命名空间下的查询模型
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<IList<MD_QueryModel>> GetQueryModelAtNamespace(MD_Namespace _ns)
        {
            return await GetQueryModelAtNamespace(_ns.NameSpace);
        }

        private const string SQL_GetQueryModelAtNamespace = @"
                select VIEWID,NAMESPACE,VIEWNAME,DESCRIPTION,DISPLAYNAME,DWDM,IS_GDCX,IS_GLCX,IS_SJSH,DISPLAYORDER,ICSTYPE,EXTMETA
                from md_view where NAMESPACE = @NAMESPACE order by DISPLAYORDER";
        public async Task<IList<MD_QueryModel>> GetQueryModelAtNamespace(string _ns)
        {
            MySqlParameter[] _param = {
                 new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
             };
            _param[0].Value = _ns;
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetQueryModelAtNamespace, _param);
            IList<MD_QueryModel> nodeItems = new List<MD_QueryModel>();

            while (await dr.ReadAsync())
            {
                MD_QueryModel nodeInfo = new MD_QueryModel(
                    dr.GetInt64(0).ToString(),
                    dr.GetString(1), dr.GetString(2),
                    dr.IsDBNull(3) ? "" : dr.GetString(3),
                    dr.IsDBNull(4) ? "" : dr.GetString(4),
                    dr.IsDBNull(5) ? "" : dr.GetString(5),
                    dr.IsDBNull(6) ? false : dr.GetInt32(6) > 0,
                    dr.IsDBNull(7) ? false : dr.GetInt32(7) > 0,
                    dr.IsDBNull(8) ? false : dr.GetInt32(8) > 0,
                    dr.IsDBNull(9) ? 0 : dr.GetInt32(9),
                    dr.IsDBNull(10) ? "ORA_JSIS" : dr.GetString(10));
                nodeInfo.EXTMeta = dr.IsDBNull(11) ? "" : dr.GetString(11);
                nodeItems.Add(nodeInfo);
            }
            dr.Close();
            return nodeItems;
        }
        /// <summary>
        /// 通过ID获取查询模型
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        public Task<MD_QueryModel> GetQueryModelByID(string modelID)
        {
            throw new NotImplementedException();
        }

        public Task<MD_QueryModel> GetQueryModelByID(string modelID, string nameSpace)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetQueryModelByName = @"
                select VIEWID,NAMESPACE,VIEWNAME,DESCRIPTION,DISPLAYNAME,DWDM,IS_GDCX,IS_GLCX,IS_SJSH,DISPLAYORDER,ICSTYPE,EXTMETA
                from md_view where VIEWNAME = @VIEWNAME order by DISPLAYORDER";
        /// <summary>
        /// 通过名称获取查询模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public async Task<MD_QueryModel> GetQueryModelByName(string modelName)
        {
            string[] _ms = modelName.Split('.');
            if (_ms.Length > 1)
            {
                return await GetQueryModelByName(_ms[1], _ms[0]);
            }
            else
            {
                MD_QueryModel modelInfo = null;
                StringBuilder _sb = new StringBuilder();
                MySqlParameter[] _param = {
                     new MySqlParameter("@VIEWNAME",MySqlDbType.VarChar,50)
                };

                _param[0].Value = modelName;
                MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetQueryModelByName, _param);
                IList<MD_QueryModel> nodeItems = new List<MD_QueryModel>();
                while (await dr.ReadAsync())
                {
                    modelInfo = new MD_QueryModel(
                        dr.GetInt64(0).ToString(),
                        dr.GetString(1),
                        dr.GetString(2),
                        dr.IsDBNull(3) ? "" : dr.GetString(3),
                        dr.IsDBNull(4) ? "" : dr.GetString(4),
                        dr.IsDBNull(5) ? "" : dr.GetString(5),
                        dr.IsDBNull(6) ? false : dr.GetInt32(6) > 0,
                        dr.IsDBNull(7) ? false : dr.GetInt32(7) > 0,
                        dr.IsDBNull(8) ? false : dr.GetInt32(8) > 0,
                        dr.IsDBNull(9) ? 0 : dr.GetInt32(9),
                        dr.IsDBNull(10) ? "ORA_JSIS" : dr.GetString(10));
                    modelInfo.EXTMeta = dr.IsDBNull(11) ? "" : dr.GetString(11);
                }
                dr.Close();

                return modelInfo;
            }
        }

        private const string SQL_GetQueryModelByName2 = @"
                select VIEWID,NAMESPACE,VIEWNAME,DESCRIPTION,DISPLAYNAME,DWDM,IS_GDCX,IS_GLCX,IS_SJSH,DISPLAYORDER,ICSTYPE,EXTMETA
                from md_view where NAMESPACE = @NAMESPACE and VIEWNAME = @VIEWNAME order by DISPLAYORDER";
        /// <summary>
        ///  通过命名空间.名称获取查询模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public async Task<MD_QueryModel> GetQueryModelByName(string modelName, string nameSpace)
        {
            MD_QueryModel modelInfo = null;
            StringBuilder _sb = new StringBuilder();

            MySqlParameter[] _param = {
                  new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
                  new MySqlParameter("@VIEWNAME",MySqlDbType.VarChar,50)
            };
            _param[0].Value = nameSpace;
            _param[1].Value = modelName;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetQueryModelByName2, _param);

            IList<MD_QueryModel> nodeItems = new List<MD_QueryModel>();

            while (await dr.ReadAsync())
            {
                modelInfo = new MD_QueryModel(
                    dr.GetInt64(0).ToString(),
                     dr.GetString(1),
                     dr.GetString(2),
                     dr.IsDBNull(3) ? "" : dr.GetString(3),
                     dr.IsDBNull(4) ? "" : dr.GetString(4),
                     dr.IsDBNull(5) ? "" : dr.GetString(5),
                     dr.IsDBNull(6) ? false : dr.GetInt32(6) > 0,
                     dr.IsDBNull(7) ? false : dr.GetInt32(7) > 0,
                     dr.IsDBNull(8) ? false : dr.GetInt32(8) > 0,
                     dr.IsDBNull(9) ? 0 : dr.GetInt32(9),
                     dr.IsDBNull(10) ? "ORA_JSIS" : dr.GetString(10));

                modelInfo.EXTMeta = dr.IsDBNull(11) ? "" : dr.GetString(11);
            }
            dr.Close();

            return modelInfo;
        }

        private const string SQL_GetQueryModelExRights = @"select ID,rvalue,rtitle,viewid,fid from md_view_exright where viewid=@VIEWID and fid=@FID ";
        public async Task<IList<MD_QueryModel_ExRight>> GetQueryModelExRights(string QueryModelID, string FatherID)
        {
            List<MD_QueryModel_ExRight> _ret = new List<MD_QueryModel_ExRight>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetQueryModelExRights, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    _cmd.Parameters.Add(new MySqlParameter("@FID", FatherID));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            MD_QueryModel_ExRight _ritem = new MD_QueryModel_ExRight();
                            _ritem.ID = _dr.IsDBNull(0) ? "" : _dr.GetString(0);
                            _ritem.RightName = _dr.IsDBNull(1) ? "" : _dr.GetString(1);
                            _ritem.RightTitle = _dr.IsDBNull(2) ? "" : _dr.GetString(2);
                            _ritem.ModelID = _dr.IsDBNull(3) ? "" : _dr.GetInt64(3).ToString();
                            _ritem.FatherRightID = _dr.IsDBNull(4) ? "" : _dr.GetString(4);
                            _ret.Add(_ritem);
                        }
                    }
                }
                catch (Exception e)
                {
                    string _msg = string.Format("在取查询模型[{0}]相关联的模型扩展权限信息时发生错误，错误信息：{1} ", QueryModelID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                }
            }
            return _ret;
        }

        /// <summary>
        /// 通过名称获取代码表
        /// </summary>
        /// <param name="refTableName"></param>
        /// <returns></returns>
        public async Task<MD_RefTable> GetRefTable(string refTableName)
        {
            MD_RefTable _ret = null;
            MySqlParameter[] _param;
            string[] _ctNames = refTableName.Split('.');

            StringBuilder _sb = new StringBuilder();
            _sb.Append("select RTID,NAMESPACE,REFTABLENAME,REFTABLELEVELFORMAT,DESCRIPTION,DWDM,DOWNLOADMODE,REFTABLEMODE,HIDECODE ");
            _sb.Append(" from md_reftablelist where REFTABLENAME = @TNAME");
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
                _ret = new MD_RefTable(dr.GetInt64(0).ToString(),
                   dr.GetString(1), dr.GetString(2),
                   dr.IsDBNull(3) ? "" : dr.GetString(3),
                   dr.IsDBNull(4) ? "" : dr.GetString(4),
                   dr.IsDBNull(5) ? "" : dr.GetString(5),
                   dr.IsDBNull(6) ? 0 : dr.GetInt32(6),
                   dr.IsDBNull(7) ? 0 : dr.GetInt32(7),
                   dr.IsDBNull(8) ? false : (dr.GetInt32(8) > 0));
            }
            dr.Close();

            return _ret;
        }

        public Task<MD_RefTable> GetRefTable(string refTableName, string nameSpace)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取命名空间下的代码表
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<IList<MD_RefTable>> GetRefTableAtNamespace(MD_Namespace _ns)
        {
            return await GetRefTableAtNamespace(_ns.NameSpace);
        }

        public async Task<IList<MD_RefTable>> GetRefTableAtNamespace(string _ns)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("select RTID,NAMESPACE,REFTABLENAME,REFTABLELEVELFORMAT,DESCRIPTION,DWDM,DOWNLOADMODE,REFTABLEMODE ,HIDECODE");
            _sb.Append(" from md_reftablelist where NAMESPACE = @NAMESPACE order by REFTABLENAME");

            MySqlParameter[] _param = {
                new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
            };
            _param[0].Value = _ns;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            IList<MD_RefTable> nodeItems = new List<MD_RefTable>();

            while (await dr.ReadAsync())
            {
                MD_RefTable nodeInfo = new MD_RefTable(
                   dr.GetInt64(0).ToString(),
                   dr.GetString(1), dr.GetString(2),
                   dr.IsDBNull(3) ? "" : dr.GetString(3),
                   dr.IsDBNull(4) ? "" : dr.GetString(4),
                   dr.IsDBNull(5) ? "" : dr.GetString(5),
                   dr.IsDBNull(6) ? 0 : dr.GetInt32(6),
                   dr.IsDBNull(7) ? 0 : dr.GetInt32(7),
                   dr.IsDBNull(8) ? false : (dr.GetInt32(8) > 0));
                nodeItems.Add(nodeInfo);
            }
            dr.Close();
            return nodeItems;
        }


        public IList<MD_RightDefine> GetRightData()
        {
            throw new NotImplementedException();
        }

        public IList<MD_RightDefine> GetRightData(string SystemID)
        {
            throw new NotImplementedException();
        }

        public IList<MD_ConceptItem> GetSubConceptTagDefine(string _groupName)
        {
            throw new NotImplementedException();
        }

        public IList<MD_Menu> GetSubMenuDefine(string _fmenuID)
        {
            throw new NotImplementedException();
        }

        private const string SQL_GetTable2ViewList = @"select ID,TID,VIEWNAME,CONDITIONSTR,CONFINE from md_table2view where TID=@TID";
        /// <summary>
        /// 取表关联的查询模型的列表
        /// </summary>
        /// <param name="_tid"></param>
        /// <returns></returns>
        public async Task<IList<MD_Table2View>> GetTable2ViewList(string _tid)
        {
            List<MD_Table2View> _ret = new List<MD_Table2View>();
            MySqlParameter[] _Param = { new MySqlParameter("@TID", Convert.ToInt64(_tid)) };
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetTable2ViewList, _Param);
            while (await dr.ReadAsync())
            {
                MD_Table2View _t2v = new MD_Table2View(
                   dr.IsDBNull(0) ? "" : dr.GetString(0),
                   dr.IsDBNull(1) ? "" : dr.GetInt64(1).ToString(),
                   dr.IsDBNull(2) ? "" : dr.GetString(2),
                   dr.IsDBNull(3) ? "" : dr.GetString(3),
                   dr.IsDBNull(4) ? "" : dr.GetString(4));
                _ret.Add(_t2v);
            }
            dr.Close();
            return _ret;
        }

        private const string SQL_GetTableByTableID = @"
            select TID,NAMESPACE,TABLENAME,TABLETYPE,DESCRIPTION,DISPLAYNAME,MAINKEY,DWDM,
            SECRETFUN,EXTSECRET,RESTYPE from md_table where TID=@TID";
        /// <summary>
        ///  取指定表ID的表定义
        /// </summary>
        /// <param name="_tid"></param>
        /// <returns></returns>
        public async Task<MD_Table> GetTableByTableID(string _tid)
        {
            MD_Table _ret = null;
            MySqlParameter[] _param = {
                  new MySqlParameter("@TID", MySqlDbType.Int64)
            };
            _param[0].Value = Convert.ToInt64(_tid);
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetTableByTableID, _param);

            while (await dr.ReadAsync())
            {
                _ret = new MD_Table(dr.GetInt64(0).ToString(),
                    dr.GetString(1),
                    dr.GetString(2),
                    dr.GetString(3),
                    dr.IsDBNull(4) ? "" : dr.GetString(4),
                    dr.IsDBNull(5) ? "" : dr.GetString(5),
                    dr.IsDBNull(6) ? "" : dr.GetString(6),
                    dr.IsDBNull(7) ? "" : dr.GetString(7),
                    dr.IsDBNull(8) ? "" : dr.GetString(8),
                    dr.IsDBNull(9) ? "" : dr.GetString(9),
                    dr.IsDBNull(10) ? "" : dr.GetString(10));
            }
            dr.Close();
            return _ret;
        }

        /// <summary>
        /// 取命名空间下的表的列表
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<IList<MD_Table>> GetTablesAtNamespace(MD_Namespace _ns)
        {
            return await GetTablesAtNamespace(_ns.NameSpace);
        }

        private const string SQL_GetTablesAtNamespace = @"
                select TID,NAMESPACE,TABLENAME,TABLETYPE,DESCRIPTION,DISPLAYNAME,MAINKEY,DWDM, 
                SECRETFUN,EXTSECRET,RESTYPE from md_table where NAMESPACE = @NAMESPACE order by DISPLAYNAME";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<IList<MD_Table>> GetTablesAtNamespace(string _ns)
        {
            MySqlParameter[] _param = {
                new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
            };
            _param[0].Value = _ns;

            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetTablesAtNamespace, _param);

            IList<MD_Table> nodeItems = new List<MD_Table>();

            while (await dr.ReadAsync())
            {
                MD_Table nodeInfo = new MD_Table();
                nodeInfo.TID = dr.GetInt64(0).ToString();
                nodeInfo.NamespaceName = dr.GetString(1);
                nodeInfo.TableName = dr.GetString(2);
                nodeInfo.TableType = dr.GetString(3);
                nodeInfo.Description = dr.IsDBNull(4) ? "" : dr.GetString(4);
                nodeInfo.DisplayTitle = dr.IsDBNull(5) ? "" : dr.GetString(5);
                nodeInfo.MainKey = dr.IsDBNull(6) ? "" : dr.GetString(6);
                nodeInfo.DWDM = dr.IsDBNull(7) ? "" : dr.GetString(7);
                nodeInfo.SecretFun = dr.IsDBNull(8) ? "" : dr.GetString(8);
                nodeInfo.ExtSecret = dr.IsDBNull(9) ? "" : dr.GetString(9);
                string _resType = dr.IsDBNull(10) ? "" : dr.GetString(10);
                if (_resType != null)
                {
                    nodeInfo.ResourceType = _resType.Split(',').ToList<string>();
                }
                else
                {
                    nodeInfo.ResourceType = new List<string>();
                }

                nodeItems.Add(nodeInfo);
            }
            dr.Close();

            return nodeItems;
        }

        private const string SQL_GetView2GuideLineList = @"
                select ID,VIEWID,TARGETGL,TARGETCS,DISPLAYORDER,DISPLAYTITLE 
                from md_view2gl where VIEWID=@VID order by DISPLAYORDER";
        public async Task<IList<MD_View_GuideLine>> GetView2GuideLineList(string QueryModelID)
        {
            List<MD_View_GuideLine> _ret = new List<MD_View_GuideLine>();
            MySqlParameter[] _Param = { new MySqlParameter("@VID", QueryModelID) };
            MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_GetView2GuideLineList, _Param);
            while (await dr.ReadAsync())
            {
                MD_View_GuideLine _mvg = new MD_View_GuideLine();
                _mvg.ID = dr.IsDBNull(0) ? "" : dr.GetString(0);
                _mvg.ViewID = dr.IsDBNull(1) ? "" : dr.GetString(1);
                _mvg.TargetGuideLineID = dr.IsDBNull(2) ? "" : dr.GetString(2);
                _mvg.RelationParam = dr.IsDBNull(3) ? "" : dr.GetString(3);
                _mvg.DisplayOrder = dr.IsDBNull(4) ? 0 : dr.GetInt32(4);
                _mvg.DisplayTitle = dr.IsDBNull(5) ? "" : dr.GetString(5);
                _ret.Add(_mvg);
            }
            dr.Close();
            return _ret;
        }

        private const string SQL_GetView2ViewGroupOfQueryModel = @"
                select ID,VIEWID,DISPLAYTITLE,DISPLAYORDER 
                from md_view2viewgroup where VIEWID=@VIEWID order by DISPLAYORDER";

        public async Task<IList<MD_View2ViewGroup>> GetView2ViewGroupOfQueryModel(string ViewID)
        {
            List<MD_View2ViewGroup> _ret = new List<MD_View2ViewGroup>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetView2ViewGroupOfQueryModel, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(ViewID)));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            MD_View2ViewGroup _g = new MD_View2ViewGroup();
                            _g.ID = _dr.IsDBNull(0) ? "" : _dr.GetString(0);
                            _g.DisplayTitle = _dr.IsDBNull(2) ? "" : _dr.GetString(2);
                            _g.DisplayOrder = _dr.IsDBNull(3) ? 0 : _dr.GetInt32(3);
                            _ret.Add(_g);
                        }
                    }
                }
                catch (Exception e)
                {
                    MysqlLogWriter.WriteSystemLog(string.Format("在取查询模型{0}相关联模型分组信息时发生错误，错误信息：{1} ", ViewID, e.Message), "ERROR");
                    return null;
                }
            }
            return _ret;
        }

        private const string SQL_GetView2ViewList = @"
                select ID,VIEWID,TARGETVIEWNAME,RELATIONSTR,DISPLAYORDER,DISPLAYTITLE,GROUPID 
                from md_view2view WHERE VIEWID=@VIEWID and GROUPID=@GROUPID";
        public async Task<IList<MD_View2View>> GetView2ViewList(string GroupID, string ViewID)
        {
            List<MD_View2View> _ret = new List<MD_View2View>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetView2ViewList, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(ViewID)));
                    _cmd.Parameters.Add(new MySqlParameter("@GROUPID", GroupID));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            MD_View2View _g = new MD_View2View();
                            _g.ID = _dr.IsDBNull(0) ? "" : _dr.GetString(0);
                            _g.TargetViewName = _dr.IsDBNull(2) ? "" : _dr.GetString(2);
                            _g.RelationString = _dr.IsDBNull(3) ? "" : _dr.GetString(3);
                            _g.DisplayOrder = _dr.IsDBNull(4) ? 0 : _dr.GetInt32(4);
                            _g.DisplayTitle = _dr.IsDBNull(5) ? "" : _dr.GetString(5);
                            _ret.Add(_g);
                        }
                    }
                }
                catch (Exception e)
                {
                    MysqlLogWriter.WriteSystemLog(string.Format("在取查询模型{0}相关联模型分组信息时发生错误，错误信息：{1} ", ViewID, e.Message), "ERROR");
                    return null;
                }
            }
            return _ret;
        }

        public async Task<DataTable> Get_RefTableColumn(MD_RefTable _refTable)
        {
            return await Get_RefTableColumn(_refTable.RefTableName);
        }

        private const string SQL_Get_RefTableColumn = "select * from dm_reftabledata where type=@type ";
        public async Task<DataTable> Get_RefTableColumn(string _refTableName)
        {
            DataTable _metadata = new DataTable("RefTable");
            MySqlParameter[] _param = { new MySqlParameter("@type", MySqlDbType.VarChar) };
            _param[0].Value = _refTableName;
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
            {
                _metadata = await MysqlDBHelper.FillDataTable(cn,null, CommandType.Text, SQL_Get_RefTableColumn, _param);
                _metadata.TableName = "RefTable";
                _metadata.CaseSensitive = true;
                cn.Close();
            }
            return _metadata;
        }


        private const string SQL_Insert_MD_View2ViewGroup = @"
                insert into md_view2viewgroup (ID,VIEWID,DISPLAYORDER,DISPLAYTITLE)
                values (@ID,@VIEWID,@DISPLAYORDER,@DISPLAYTITLE)";
        private const string SQL_Insert_MD_View2View = @"
                insert into md_view2view (ID,VIEWID,TARGETVIEWNAME,RELATIONSTR,DISPLAYORDER,DISPLAYTITLE,GROUPID)
                values (@ID,@VIEWID,@TARGETVIEWNAME,@RELATIONSTR,@DISPLAYORDER,@DISPLAYTITLE,@GROUPID)";

        private const string SQL_SaveView2App_Insert = @"
                insert into md_view2app (ID,VIEWID,TITLE,INTEGRATEDAPP,DISPLAYHEIGHT,URL,DISPLAYORDER,META)
                values (@ID,@VIEWID,@TITLE,@INTEGRATEDAPP,@DISPLAYHEIGHT,@URL,@DISPLAYORDER,@META)";
        /// <summary>
        /// 导入查询模型
        /// </summary>
        /// <param name="_qv"></param>
        /// <returns></returns>
        public async Task<bool> ImportQueryModelDefine(MD_QueryModel _qv)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    #region  保存查询模型定义信息
                    StringBuilder _sb = new StringBuilder();
                    _sb.Append(" insert into md_view (");
                    _sb.Append(" VIEWNAME,DESCRIPTION,DISPLAYNAME, ");
                    _sb.Append(" DWDM,IS_GDCX,IS_GLCX,IS_SJSH,");
                    _sb.Append(" DISPLAYORDER,NAMESPACE,VIEWID,EXTMETA )");
                    _sb.Append(" values ( ");
                    _sb.Append(" @VIEWNAME,@DESCRIPTION,@DISPLAYNAME, ");
                    _sb.Append(" @DWDM,@IS_GDCX,@IS_GLCX,@IS_SJSH,");
                    _sb.Append(" @DISPLAYORDER,@NAMESPACE,@VIEWID,@EXTMETA)");

                    MySqlParameter[] _param = {
                        new MySqlParameter("@VIEWNAME", MySqlDbType.VarChar, 50),
                        new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                        new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar, 100),
                        new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                        new MySqlParameter("@IS_GDCX", MySqlDbType.Int32),
                        new MySqlParameter("@IS_GLCX",MySqlDbType.Int32),
                        new MySqlParameter("@IS_SJSH",MySqlDbType.Int32),
                        new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32),
                        new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
                        new MySqlParameter("@VIEWID",MySqlDbType.Int64),
                        new MySqlParameter("@EXTMETA",MySqlDbType.VarChar,4000),
                    };

                    _param[0].Value = _qv.QueryModelName;
                    _param[1].Value = _qv.Description;
                    _param[2].Value = _qv.DisplayTitle;
                    _param[3].Value = _qv.DWDM;
                    _param[4].Value = _qv.IsFixQuery ? 1 : 0;
                    _param[5].Value = _qv.IsRelationQuery ? 1 : 0;
                    _param[6].Value = _qv.IsDataAuditing ? 1 : 0;
                    _param[7].Value = Convert.ToInt32(_qv.DisplayOrder);
                    _param[8].Value = _qv.NamespaceName;
                    _param[9].Value = Convert.ToInt64(_qv.QueryModelID);
                    _param[10].Value = _qv.EXTMeta;
                    await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _sb.ToString(), _param);
                    #endregion

                    #region 导入子表定义
                    foreach (MD_ViewTable _vtable in _qv.ChildTables)
                    {
                        if (_vtable != null)
                        {

                            _sb = new StringBuilder();
                            _sb.Append(" insert into md_viewtable ");
                            _sb.Append(" (VTID,FATHERID,VIEWID,TID,");
                            _sb.Append(" TABLETYPE,TABLERELATION,CANCONDITION,DISPLAYNAME,");
                            _sb.Append(" DISPLAYORDER,DWDM,PRIORITY) ");
                            _sb.Append(" values ");
                            _sb.Append(" (@VTID,@FATHERID,@VIEWID,@TID,");
                            _sb.Append(" @TABLETYPE,@TABLERELATION,@CANCONDITION,@DISPLAYNAME,");
                            _sb.Append(" @DISPLAYORDER,@DWDM,@PRIORITY) ");

                            MySqlParameter[] _param5 = {
                                new MySqlParameter("@VTID",MySqlDbType.Int64),
                                new MySqlParameter("@FATHERID",MySqlDbType.Int64),
                                new MySqlParameter("@VIEWID",MySqlDbType.Int64),
                                new MySqlParameter("@TID",MySqlDbType.Int64),
                                new MySqlParameter("@TABLETYPE",MySqlDbType.VarChar,20),
                                new MySqlParameter("@TABLERELATION",MySqlDbType.VarChar,300),
                                new MySqlParameter("@CANCONDITION",MySqlDbType.VarChar,10),
                                new MySqlParameter("@DISPLAYNAME",MySqlDbType.VarChar,100),
                                new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32),
                                new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                                new MySqlParameter("@PRIORITY",MySqlDbType.Int32)
                            };
                            _param5[0].Value = Convert.ToInt64(_vtable.ViewTableID);
                            if (_vtable.FatherTableID == "")
                            {
                                _param5[1].Value = DBNull.Value;
                            }
                            else
                            {
                                _param5[1].Value = Convert.ToInt64(_vtable.FatherTableID);
                            }
                            _param5[2].Value = Convert.ToInt64(_qv.QueryModelID);
                            _param5[3].Value = Convert.ToInt64(_vtable.TableID);
                            _param5[4].Value = (_vtable.ViewTableType == MDType_ViewTable.MainTable) ? "M" : "F";
                            _param5[5].Value = _vtable.RelationString;
                            _param5[6].Value = (_vtable.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord) ? 1 : 0;
                            _param5[7].Value = _vtable.DisplayTitle;
                            _param5[8].Value = Convert.ToInt32(_vtable.DisplayOrder);
                            _param5[9].Value = _vtable.DWDM;
                            _param5[10].Value = 0;

                            await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _sb.ToString(), _param5);

                            //清除所有字段定义
                            string _del = "delete from md_viewtablecolumn where VTID=@VTID";
                            MySqlParameter[] _param2 = {
                                new MySqlParameter("@VTID",MySqlDbType.Int64)
                            };
                            _param2[0].Value = Convert.ToInt64(_vtable.ViewTableID);
                            await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _del, _param2);

                            _sb = new StringBuilder();
                            _sb.Append(" insert into md_viewtablecolumn (VTCID,VTID,TCID,");
                            _sb.Append(" CANCONDITIONSHOW,CANRESULTSHOW,DEFAULTSHOW,");
                            _sb.Append(" DWDM,FIXQUERYITEM,CANMODIFY,PRIORITY) ");
                            _sb.Append(" VALUES (@VTCID,@VTID,@TCID,");
                            _sb.Append(" @CANCONDITIONSHOW,@CANRESULTSHOW,@DEFAULTSHOW,");
                            _sb.Append(" @DWDM,@FIXQUERYITEM,@CANMODIFY,@PRIORITY) ");
                            //保存字段定义信息
                            foreach (MD_ViewTableColumn _tc in _vtable.Columns)
                            {
                                MySqlParameter[] _param3 = {
                                    new MySqlParameter("@VTCID", MySqlDbType.Int64),
                                    new MySqlParameter("@VTID", MySqlDbType.Int64),
                                    new MySqlParameter("@TCID", MySqlDbType.Int64),
                                    new MySqlParameter("@CANCONDITIONSHOW", MySqlDbType.Int32),
                                    new MySqlParameter("@CANRESULTSHOW", MySqlDbType.Int32),
                                    new MySqlParameter("@DEFAULTSHOW", MySqlDbType.Int32),
                                    new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                                    new MySqlParameter("@FIXQUERYITEM",MySqlDbType.Int32),
                                    new MySqlParameter("@CANMODIFY",MySqlDbType.Int32),
                                    new MySqlParameter("@PRIORITY",MySqlDbType.Int32)
                                };
                                _param3[0].Value = Convert.ToInt64(_tc.ViewTableColumnID);
                                _param3[1].Value = Convert.ToInt64(_vtable.ViewTableID);
                                _param3[2].Value = Convert.ToInt64(_tc.ColumnID);
                                _param3[3].Value = _tc.CanShowAsCondition ? 1 : 0;
                                _param3[4].Value = _tc.CanShowAsResult ? 1 : 0;
                                _param3[5].Value = _tc.DefaultResult ? 1 : 0;
                                _param3[6].Value = _tc.DWDM;
                                _param3[7].Value = _tc.IsFixQueryItem ? 1 : 0;
                                _param3[8].Value = _tc.CanModify ? 1 : 0;
                                _param3[9].Value = Convert.ToInt32(_tc.Priority);
                                await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, _sb.ToString(), _param3);
                            }
                        }

                    }
                    #endregion

                    #region 导入模型关联定义
                    if (_qv.View2ViewGroup != null)
                    {
                        foreach (MD_View2ViewGroup _group in _qv.View2ViewGroup)
                        {
                            MySqlParameter[] _pv2vg = {
                                new MySqlParameter("@ID", MySqlDbType.VarChar, 50),
                                new MySqlParameter("@VIEWID", MySqlDbType.Int64),
                                new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                                new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 200)
                            };
                            _pv2vg[0].Value = _group.ID;
                            _pv2vg[1].Value = Convert.ToInt64(_qv.QueryModelID);
                            _pv2vg[2].Value = Convert.ToInt32(_group.DisplayOrder);
                            _pv2vg[3].Value = _group.DisplayTitle;
                            await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, SQL_Insert_MD_View2ViewGroup, _pv2vg);

                            foreach (MD_View2View _v2v in _group.View2Views)
                            {
                                MySqlParameter[] _pv2v = {
                                    new MySqlParameter("@ID", MySqlDbType.VarChar, 50),
                                    new MySqlParameter("@VIEWID", MySqlDbType.Int64),
                                    new MySqlParameter("@TARGETVIEWNAME", MySqlDbType.VarChar,300),
                                    new MySqlParameter("@RELATIONSTR", MySqlDbType.VarChar,4000),
                                    new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                                    new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 200),
                                    new MySqlParameter("@GROUPID", MySqlDbType.VarChar, 50)
                                };
                                _pv2v[0].Value = _v2v.ID;
                                _pv2v[1].Value = Convert.ToInt64(_qv.QueryModelID);
                                _pv2v[2].Value = _v2v.TargetViewName;
                                _pv2v[3].Value = _v2v.RelationString;
                                _pv2v[4].Value = Convert.ToInt32(_v2v.DisplayOrder);
                                _pv2v[5].Value = _v2v.DisplayTitle;
                                _pv2v[6].Value = _group.ID;
                                await MysqlDBHelper.ExecuteNonQuery(cn, CommandType.Text, SQL_Insert_MD_View2View, _pv2v);
                            }
                        }
                    }
                    #endregion

                    #region 导入关联指标定义
                    if (_qv.View2GuideLines != null)
                    {
                        foreach (MD_View_GuideLine _v2g in _qv.View2GuideLines)
                        {
                            MySqlCommand SaveCmd = new MySqlCommand(SQL_InsertV2G, cn);
                            SaveCmd.Parameters.Add(new MySqlParameter("@ID", _v2g.ID));
                            SaveCmd.Parameters.Add(new MySqlParameter("@VIEWID", _v2g.ViewID));
                            SaveCmd.Parameters.Add(new MySqlParameter("@TARGETGL", _v2g.TargetGuideLineID));
                            SaveCmd.Parameters.Add(new MySqlParameter("@TARGETCS", _v2g.RelationParam));
                            SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(_v2g.DisplayOrder)));
                            SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYTITLE", _v2g.DisplayTitle));
                            await SaveCmd.ExecuteNonQueryAsync();
                        }

                    }
                    #endregion

                    #region 导入关联集成应用定义
                    if (_qv.View2Application != null)
                    {
                        foreach (MD_View2App _v2a in _qv.View2Application)
                        {
                            MySqlCommand _ins = new MySqlCommand(SQL_SaveView2App_Insert, cn);
                            _ins.Parameters.Add(new MySqlParameter("@ID", _v2a.ID));
                            _ins.Parameters.Add(new MySqlParameter("@VIEWID", _v2a.ViewID));
                            _ins.Parameters.Add(new MySqlParameter("@TITLE", _v2a.Title));
                            _ins.Parameters.Add(new MySqlParameter("@INTEGRATEDAPP", _v2a.AppName));
                            _ins.Parameters.Add(new MySqlParameter("@DISPLAYHEIGHT", Convert.ToInt32(_v2a.DisplayHeight)));
                            _ins.Parameters.Add(new MySqlParameter("@URL", _v2a.RegURL));
                            _ins.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(_v2a.DisplayOrder)));
                            _ins.Parameters.Add(new MySqlParameter("@META", _v2a.Meta));
                            await _ins.ExecuteNonQueryAsync();

                        }
                    }
                    #endregion

                    txn.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    txn.Rollback();
                    return false;
                }
            }

        }

        public async Task<bool> ImportRefTableDefine(MD_RefTable _rt)
        {
            StringBuilder _insertStr = new StringBuilder();
            _insertStr.Append(" Insert into md_reftablelist ");
            _insertStr.Append(" (RTID,NAMESPACE,REFTABLENAME,REFTABLELEVELFORMAT,");
            _insertStr.Append(" DESCRIPTION,DOWNLOADMODE,REFTABLEMODE,DWDM,HIDECODE ) ");
            _insertStr.Append(" values ");
            _insertStr.Append(" (@RTID,@NAMESPACE,@REFTABLENAME,@REFTABLELEVELFORMAT,");
            _insertStr.Append(" @DESCRIPTION,@DOWNLOADMODE,@REFTABLEMODE,@DWDM,@HIDECODE ) ");

            MySqlParameter[] _param = {
               new MySqlParameter("@RTID",MySqlDbType.Int64),
               new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
               new MySqlParameter("@REFTABLENAME",MySqlDbType.VarChar,50),
               new MySqlParameter("@REFTABLELEVELFORMAT",MySqlDbType.VarChar,20),
               new MySqlParameter("@DESCRIPTION",MySqlDbType.VarChar,100),
               new MySqlParameter("@DOWNLOADMODE",MySqlDbType.Int32),
               new MySqlParameter("@REFTABLEMODE",MySqlDbType.Int32),
               new MySqlParameter("@DWDM",MySqlDbType.VarChar,12)    ,
               new MySqlParameter("@HIDECODE",MySqlDbType.Int32)
            };
            _param[0].Value = Convert.ToInt64(_rt.RefTableID);
            _param[1].Value = _rt.NamespaceName;
            _param[2].Value = _rt.RefTableName;
            _param[3].Value = _rt.LevelFormat;
            _param[4].Value = _rt.Description;
            _param[5].Value = Convert.ToInt32((int)_rt.RefDownloadMode);
            _param[6].Value = Convert.ToInt32((int)_rt.RefParamMode);
            _param[7].Value = _rt.DWDM;
            _param[8].Value = _rt.HideCode ? 1 : 0;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _insertStr.ToString(), _param);
            return true;
        }

        private const string SQL_ImportTableDefine = @"
                insert into md_table (NAMESPACE,TABLENAME,TABLETYPE,DESCRIPTION,DISPLAYNAME,DWDM,MAINKEY,SECRETFUN,EXTSECRET,TID ) 
                VALUES (@NAMESPACE,@TABLENAME,@TABLETYPE,@DESCRIPTION,@DISPLAYNAME,@DWDM,@MAINKEY,@SECRETFUN,@EXTSECRET,@TID )";
        /// <summary>
        /// 导入表定义
        /// </summary>
        /// <param name="_table"></param>
        /// <returns></returns>
        public async Task<bool> ImportTableDefine(MD_Table _table)
        {
            try
            {

                //保存表定义信息
                using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
                {
                    MySqlTransaction _txn = cn.BeginTransaction();
                    MySqlCommand _cmdInsert = new MySqlCommand(SQL_ImportTableDefine, cn);
                    _cmdInsert.Parameters.Add(new MySqlParameter("@NAMESPACE", _table.NamespaceName));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@TABLENAME", _table.TableName));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@TABLETYPE", _table.TableType));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@DESCRIPTION", _table.Description));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@DISPLAYNAME", _table.DisplayTitle));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@DWDM", _table.DWDM));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@MAINKEY", _table.MainKey));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@SECRETFUN", _table.SecretFun));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@EXTSECRET", _table.ExtSecret));
                    _cmdInsert.Parameters.Add(new MySqlParameter("@TID", Convert.ToInt64(_table.TID)));
                    await _cmdInsert.ExecuteNonQueryAsync();
                    _txn.Commit();



                    // 取md_tablecolumn中已存在的列           
                    string SQL_cl = string.Format("select tcid from md_tablecolumn where tid={0} ", _table.TID);
                    MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_cl);
                    List<long> existTCIDs = new List<long>();
                    while (await dr.ReadAsync())
                    {
                        var id = dr.GetInt64(0);
                        existTCIDs.Add(id);
                    }
                    dr.Close();

                    // 判断已删除的列，md_tablecolumn和md_viewtablecolumn中进行删除
                    var deleted = existTCIDs.Where(a => !_table.Columns.Any(c => c.ColumnID == a.ToString()));
                    if (deleted.Count() > 0)
                    {
                        _txn = cn.BeginTransaction();
                        string del_viewtablecolumn = string.Format("delete from md_viewtablecolumn where tcid in ({0}) ", string.Join(",", deleted));
                        string del_tablecolumn = string.Format("delete from md_tablecolumn where tcid in ({0}) ", string.Join(",", deleted));

                        MySqlCommand _cmd = new MySqlCommand(del_viewtablecolumn, cn);
                        await _cmd.ExecuteNonQueryAsync();
                        _cmd = new MySqlCommand(del_tablecolumn, cn);
                        await _cmd.ExecuteNonQueryAsync();
                        _txn.Commit();
                    }



                    // 新增的列执行插入
                    var news = _table.Columns.Where(a => !existTCIDs.Any(c => c.ToString() == a.ColumnID));
                    foreach (var i in news)
                    {
                        MD_DefineFactory.InsertColumnDefine(_table, i);
                    }

                    // 更新已存在的列
                    var update = _table.Columns.Where(a => !deleted.Contains(Convert.ToInt64(a.ColumnID))).Where(a => !news.Any(c => c.ToString() == a.ColumnID));
                    foreach (var u in update)
                    {
                        MD_DefineFactory.UpdateColumnDefine(_table, u);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MysqlLogWriter.WriteSystemLog(string.Format("导入表{0}的元数据失败!错误信息：{1}", _table.TableName, ex.Message), "ERROR");
                return false;
            }
        }

        public bool InputModel_MoveColumnToGroup(MD_InputModel_Column _col, MD_InputModel_ColumnGroup mD_InputModel_ColumnGroup)
        {
            throw new NotImplementedException();
        }

        public bool IsExistChildOfConceptGroup(string _groupName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_IsExistChildOfGuideLine = "select count(ID)  from tj_zdyzbdyb where FID=@FID";
        public async Task<bool> IsExistChildOfGuideLine(string _guideLineID)
        {
            MySqlParameter[] _param = {
                                new MySqlParameter("@FID",MySqlDbType.Int64)
                        };
            _param[0].Value = Int64.Parse(_guideLineID);

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SQL_IsExistChildOfGuideLine, _param);
            return (_count > 0);
        }

        private const string SQL_IsExistChildOfGuideLineGroup = "select count(id)  from tj_zdyzbdyb where ZBZT=@ZBZT and FID=1";
        public async Task<bool> IsExistChildOfGuideLineGroup(string _guideLineGroupName)
        {
            MySqlParameter[] _param = {
                                new MySqlParameter("@ZBZT",MySqlDbType.VarChar,200)
                        };
            _param[0].Value = _guideLineGroupName;

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SQL_IsExistChildOfGuideLineGroup, _param);
            return (_count > 0);
        }

        /// <summary>
        ///  指定查询模型中是否有表
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <returns></returns>
        public async Task<bool> IsExistChildOfView(string _queryModelID)
        {
            string _querystr = "select count(*) from md_viewtable WHERE VIEWID =@VIEWID ";
            MySqlParameter[] _param = {
                                new MySqlParameter("@VIEWID",MySqlDbType.Int64)
                        };
            _param[0].Value = Convert.ToInt64(_queryModelID);

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _querystr, _param);
            return (_count > 0);
        }

        /// <summary>
        ///  指定的表是否有子表
        /// </summary>
        /// <param name="_viewTableID"></param>
        /// <returns></returns>
        public async Task<bool> IsExistChildTable(string _viewTableID)
        {
            string _sql = "select count(*) from md_viewtable WHERE FATHERID =@FID";
            MySqlParameter[] _param = {
                 new MySqlParameter("@FID",MySqlDbType.Int64)
             };
            _param[0].Value = Convert.ToInt64(_viewTableID);
            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _sql, _param);
            return (_count > 0);
        }

        public bool IsExistConceptGroup(string _groupName)
        {
            throw new NotImplementedException();
        }

        public bool IsExistConceptTag(string _TagName)
        {
            throw new NotImplementedException();
        }

        private const string SQL_IsExistGuideLineGroupName = "select count(ZBZTMC) from tj_zbztmcdyb  where ZBZTMC=@ZBZTMC";
        public async Task<bool> IsExistGuideLineGroupName(string _guideLineGroupName)
        {
            MySqlParameter[] _param = {
                                 new MySqlParameter("@ZBZTMC", MySqlDbType.VarChar, 100)
                        };
            _param[0].Value = _guideLineGroupName;

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SQL_IsExistGuideLineGroupName, _param);
            return (_count > 0);
        }

        private const string SQL_IsExistGuideLineID = " select count(ID) from tj_zdyzbdyb where ID=@ID ";
        public async Task<bool> IsExistGuideLineID(string _guideLineID)
        {
            MySqlParameter[] _param = {
                                 new MySqlParameter("@ID", MySqlDbType.Int64)
                        };
            _param[0].Value = Int64.Parse(_guideLineID);

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, SQL_IsExistGuideLineID, _param);
            return (_count > 0);
        }

        public async Task<bool> IsExistID(string _oldid, string _tname, string _colname)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    string _sql = string.Format("select count({0}) from {1} where {2}='{3}'", _colname, _tname, _colname, _oldid);
                    long _ret = (long)await MysqlDBHelper.ExecuteScalar(cn, CommandType.Text, _sql);

                    return (_ret > 0);
                }
                catch (Exception e)
                {
                    MysqlLogWriter.WriteSystemLog(string.Format("在检查{0}表中是否存在{1}的序列值为{2}时发生错误，错误信息：{3}", _tname, _colname, _oldid, e.Message), "ERROR");
                    return false;
                }
            }
        }

        public async Task<bool> IsExistViewUsedTable(string _tableID)
        {
            string _querystr = "select count(*) from md_viewtable WHERE TID=@TID ";
            MySqlParameter[] _param = {
                 new MySqlParameter("@TID",MySqlDbType.Int64)
            };
            _param[0].Value = Convert.ToInt64(_tableID);

            long _count = (long)await MysqlDBHelper.ExecuteScalar(MysqlDBHelper.conf, CommandType.Text, _querystr, _param);
            return (_count > 0);
        }

        public bool OracleTableExist(string TableName)
        {
            throw new NotImplementedException();
        }

        public bool SaveConceptGroup(MD_ConceptGroup _ConceptGroup)
        {
            throw new NotImplementedException();
        }

        public bool SaveConceptTag(MD_ConceptItem mD_ConceptItem)
        {
            throw new NotImplementedException();
        }





        public bool SaveInputModel(MD_InputModel SaveModel)
        {
            throw new NotImplementedException();
        }

        public bool SaveInputModelChildDefine(MD_InputModel_Child InputModelChild)
        {
            throw new NotImplementedException();
        }

        public bool SaveInputModelColumnGroup(MD_InputModel_ColumnGroup _group)
        {
            throw new NotImplementedException();
        }

        public bool SaveInputModelSaveTable(MD_InputModel_SaveTable _newTable)
        {
            throw new NotImplementedException();
        }

        public bool SaveMenuDefine(MD_Menu _menu)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 保存命名空间信息
        /// </summary>
        /// <param name="_Namespace"></param>
        /// <returns></returns>
        public async Task<bool> SaveNameSapce(MD_Namespace _Namespace)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("update md_tbnamespace set DESCRIPTION = @DESCRIPTION,MENUPOSITION=@MENUPOSTION,");
            _sb.Append(" DISPLAYTITLE = @DISPLAYTITLE,OWNER = @OWNER,DISPLAYORDER = @DISPLAYORDER,DWDM =@DWDM,CONCEPTS = @CONCEPTS ");
            _sb.Append(" where NAMESPACE = @NAMESPACE");
            MySqlParameter[] _param = {
                   new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                   new MySqlParameter("@MENUPOSTION", MySqlDbType.VarChar, 50),
                   new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 50),
                   new MySqlParameter("@OWNER", MySqlDbType.VarChar, 50),
                   new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                   new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                   new MySqlParameter("@CONCEPTS", MySqlDbType.VarChar, 1000),
                   new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50)
            };

            _param[0].Value = _Namespace.Description;
            _param[1].Value = _Namespace.MenuPosition;
            _param[2].Value = _Namespace.DisplayTitle;
            _param[3].Value = _Namespace.Owner;
            _param[4].Value = _Namespace.DisplayOrder;
            _param[5].Value = _Namespace.DWDM;
            _param[6].Value = _Namespace.Concepts;
            _param[7].Value = _Namespace.NameSpace;


            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }

        private const string SQL_SaveNewGuideLine = "INSERT INTO tj_zdyzbdyb (ID,ZBMC,ZBZT,FID,XSXH) VALUES  (@NEWID,@ZBMC,@ZBZT,@FID,0)";
        public async Task<bool> SaveNewGuideLine(string _guideLineName, decimal _fid, string _guideLineGroupName)
        {
            MySqlParameter[] _param = {
                                 new MySqlParameter("@NEWID", MySqlDbType.Int64),
                                 new MySqlParameter("@ZBMC", MySqlDbType.VarChar, 200),
                                 new MySqlParameter("@ZBZT", MySqlDbType.VarChar,200),
                                 new MySqlParameter("@FID", MySqlDbType.Int64)
                        };
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = _guideLineName;
            _param[2].Value = _guideLineGroupName;
            _param[3].Value = Convert.ToInt64(_fid); ;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveNewGuideLine, _param);
            return true;
        }

        private const string SQL_SaveNewGuideLine2 = "INSERT INTO tj_zdyzbdyb (ID,ZBMC,ZBZT,ZBSF,ZBMETA,FID,ZBCXSF,JSMX_ZBMETA,XSXH,ZBSM) VALUES  (@ID,@ZBMC,@ZBZT,@ZBSF,@ZBMETA,@FID,@ZBCXSF,@JSMX_ZBMETA,@XSXH,@ZBSM)";
        public async Task<bool> SaveNewGuideLine(MD_GuideLine _guideLine)
        {
            MySqlParameter[] _param = {
                                 new MySqlParameter("@ID", MySqlDbType.Int64),
                                 new MySqlParameter("@ZBMC", MySqlDbType.VarChar, 200),
                                 new MySqlParameter("@ZBZT", MySqlDbType.VarChar,200),
                                 new MySqlParameter("@ZBSF", MySqlDbType.VarChar,4000),
                                 new MySqlParameter("@ZBMETA", MySqlDbType.Text),
                                 new MySqlParameter("@FID", MySqlDbType.Int64),
                                 new MySqlParameter("@ZBCXSF", MySqlDbType.VarChar,4000),
                                 new MySqlParameter("@JSMX_ZBMETA", MySqlDbType.VarChar,4000),
                                 new MySqlParameter("@XSXH", MySqlDbType.Int32),
                                 new MySqlParameter("@ZBSM", MySqlDbType.VarChar,4000)
                        };
            _param[0].Value = Int64.Parse(_guideLine.ID);
            _param[1].Value = _guideLine.GuideLineName;
            _param[2].Value = _guideLine.GroupName;
            _param[3].Value = _guideLine.GuideLineMethod;
            _param[4].Value = _guideLine.GuideLineMeta;
            _param[5].Value = Int64.Parse(_guideLine.FatherID);
            _param[6].Value = _guideLine.GuideLineQueryMethod;
            _param[7].Value = _guideLine.DetailMeta;
            _param[8].Value = _guideLine.DisplayOrder;
            _param[9].Value = _guideLine.Description;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveNewGuideLine2, _param);
            return true;
        }

        private const string SQL_SaveGuideLine = "update tj_zdyzbdyb set ZBMC=@ZBMC,ZBSF=@ZBSF,ZBMETA=@ZBMETA,XSXH=@XSXH,JSMX_ZBMETA=@JSMXMETA ,ZBSM=@ZBSM where ID=@ID";
        public async Task<bool> SaveGuideLine(MD_GuideLine _guideLine)
        {
            MySqlParameter[] _param = {
                                new MySqlParameter("@ZBMC", MySqlDbType.VarChar,200),
                                new MySqlParameter("@ZBSF", MySqlDbType.VarChar,4000),
                                new MySqlParameter("@ZBMETA", MySqlDbType.VarChar,4000),
                                new MySqlParameter("@XSXH", MySqlDbType.Int32),
                                new MySqlParameter("@JSMXMETA",MySqlDbType.VarChar,4000),
                                new MySqlParameter("@ZBSM",MySqlDbType.VarChar,4000),
                                new MySqlParameter("@ID", MySqlDbType.Int64)
                        };

            //if (_guideLine.GuideLineMeta.Length > 3700)
            //{
            //        _metaStr1 = _guideLine.GuideLineMeta.Substring(0, 3700);
            //        _metaStr2 = _guideLine.GuideLineMeta.Substring(3700);
            //}
            //else
            //{
            //        _metaStr1 = _guideLine.GuideLineMeta;
            //        _metaStr2 = "";
            //}

            _param[0].Value = _guideLine.GuideLineName;
            _param[1].Value = _guideLine.GuideLineMethod;
            _param[2].Value = _guideLine.GuideLineMeta;
            _param[3].Value = Convert.ToDecimal(_guideLine.DisplayOrder);
            _param[4].Value = "";
            _param[5].Value = _guideLine.Description;
            _param[6].Value = decimal.Parse(_guideLine.ID);

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveGuideLine, _param);
            return true;
        }

        private const string SQL_SaveNewGuideLineGroupDefine = "INSERT INTO tj_zbztmcdyb (ZBZTMC,ZBZTSM,LX,QXLX,NAMESPACE,SSDW) VALUES (@ZBZTMC,@ZBZTSM,@LX,@QXLX,@NAMESPACE,@SSDW)";
        public async Task<bool> SaveNewGuideLineGroupDefine(MD_GuideLineGroup _guideLineGroup)
        {
            MySqlParameter[] _param = {
                                 new MySqlParameter("@ZBZTMC", MySqlDbType.VarChar, 200),
                                 new MySqlParameter("@ZBZTSM", MySqlDbType.VarChar, 200),
                                 new MySqlParameter("@LX", MySqlDbType.Decimal),
                                 new MySqlParameter("@QXLX", MySqlDbType.Decimal),
                                 new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
                                 new MySqlParameter("@SSDW", MySqlDbType.VarChar, 12)

                        };
            _param[0].Value = _guideLineGroup.ZBZTMC;
            _param[1].Value = _guideLineGroup.ZBZTSM;
            _param[2].Value = Convert.ToDecimal(_guideLineGroup.LX);
            _param[3].Value = Convert.ToDecimal(_guideLineGroup.QXLX);
            _param[4].Value = _guideLineGroup.NamespaceName;
            _param[5].Value = _guideLineGroup.SSDW;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveNewGuideLineGroupDefine, _param);
            return true;
        }
        private const string SQL_SaveGuideLineGroupDefine = "update tj_zbztmcdyb set ZBZTSM=@ZBZTSM,LX=@LX,QXLX=@QXLX,NAMESPACE=@NAMESPACE where ZBZTMC=@ZBZTMC ";
        public async Task<bool> SaveGuideLineGroupDefine(MD_GuideLineGroup _GuideLineGroup)
        {
            MySqlParameter[] _param = {
                                new MySqlParameter("@ZBZTSM",MySqlDbType.VarChar,200),
                                new MySqlParameter("@LX",MySqlDbType.Decimal),
                                new MySqlParameter("@QXLX",MySqlDbType.Decimal),
                                new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
                                new MySqlParameter("@ZBZTMC",MySqlDbType.VarChar,200)
                        };

            _param[0].Value = _GuideLineGroup.ZBZTSM;
            _param[1].Value = Convert.ToDecimal(_GuideLineGroup.LX);
            _param[2].Value = Convert.ToDecimal(_GuideLineGroup.QXLX);
            _param[3].Value = _GuideLineGroup.NamespaceName;
            _param[4].Value = _GuideLineGroup.ZBZTMC;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveGuideLineGroupDefine, _param);
            return true;
        }

        public bool SaveNewInputModel(string _namespace, MD_InputModel SaveModel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 保存新的命名空间
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<bool> SaveNewNameSapce(MD_Namespace _ns)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("insert into  md_tbnamespace (NAMESPACE,DESCRIPTION,MENUPOSITION,DISPLAYTITLE,OWNER,DISPLAYORDER,DWDM,CONCEPTS) ");
            _sb.Append(" values (@NAMESPACE,@DESCRIPTION,@MENUPOSITION,@DISPLAYTITLE,@OWNER,@DISPLAYORDER,@DWDM,@CONCEPTS) ");
            MySqlParameter[] _param = {
                new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                new MySqlParameter("@MENUPOSITION", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 50),
                new MySqlParameter("@OWNER", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                new MySqlParameter("@CONCEPTS", MySqlDbType.VarChar, 1000)
            };
            _param[0].Value = _ns.NameSpace;
            _param[1].Value = _ns.Description;
            _param[2].Value = _ns.MenuPosition;
            _param[3].Value = _ns.DisplayTitle;
            _param[4].Value = _ns.Owner;
            _param[5].Value = _ns.DisplayOrder;
            _param[6].Value = _ns.DWDM;
            _param[7].Value = _ns.Concepts;


            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            // 待处理
            MyDA_MetaDataQuery.ModelLib.Clear();


            return true;
        }
        /// <summary>
        ///  保存新的节点信息
        /// </summary>
        /// <param name="_nodes"></param>
        /// <returns></returns>
        public async Task<bool> SaveNewNodes(MD_Nodes _nodes)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("insert into  md_nodes (ID,NODENAME,DISPLAYTITLE,DESCRIPT,DWDM) ");
            _sb.Append(" values (@ID,@NODENAME,@DISPLAYTITLE,@DESCRIPT,@DWDM) ");
            MySqlParameter[] _param = {
               new MySqlParameter("@ID", MySqlDbType.VarChar, 100),
               new MySqlParameter("@NODENAME", MySqlDbType.VarChar, 100),
               new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 200),
               new MySqlParameter("@DESCRIPT", MySqlDbType.VarChar, 2000),
               new MySqlParameter("@DWDM", MySqlDbType.VarChar, 100)
            };
            _param[0].Value = _nodes.ID;
            _param[1].Value = _nodes.NodeName;
            _param[2].Value = _nodes.DisplayTitle;
            _param[3].Value = _nodes.Descript;
            _param[4].Value = _nodes.DWDM;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }


        /// <summary>
        ///  保存新的查询模型
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public async Task<bool> SaveNewQueryModel(MD_QueryModel _queryModel)
        {
            //保存查询模型定义信息
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" insert into md_view (");
            _sb.Append(" VIEWID,VIEWNAME,DESCRIPTION,DISPLAYNAME, ");
            _sb.Append(" DWDM,IS_GDCX,IS_GLCX,IS_SJSH,");
            _sb.Append(" DISPLAYORDER,NAMESPACE,EXTMETA )");
            _sb.Append(" values ( ");
            _sb.Append(" @VIEWID,@VIEWNAME,@DESCRIPTION,@DISPLAYNAME, ");
            _sb.Append(" @DWDM,@IS_GDCX,@IS_GLCX,@IS_SJSH,");
            _sb.Append(" @DISPLAYORDER,@NAMESPACE,@EXTMETA)");

            MySqlParameter[] _param = {
                new MySqlParameter("@VIEWID", MySqlDbType.Int64),
                new MySqlParameter("@VIEWNAME", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar, 100),
                new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                new MySqlParameter("@IS_GDCX", MySqlDbType.Int32),
                new MySqlParameter("@IS_GLCX",MySqlDbType.Int32),
                new MySqlParameter("@IS_SJSH",MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32),
                new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
                new MySqlParameter("@EXTMETA",MySqlDbType.VarChar,4000)
            };
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = _queryModel.QueryModelName;
            _param[2].Value = _queryModel.Description;
            _param[3].Value = _queryModel.DisplayTitle;
            _param[4].Value = _queryModel.DWDM;
            _param[5].Value = _queryModel.IsFixQuery ? 1 : 0;
            _param[6].Value = _queryModel.IsRelationQuery ? 1 : 0;
            _param[7].Value = _queryModel.IsDataAuditing ? 1 : 0;
            _param[8].Value = Convert.ToInt32(_queryModel.DisplayOrder);
            _param[9].Value = _queryModel.NamespaceName;
            _param[10].Value = _queryModel.EXTMeta;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            MyDA_MetaDataQuery.ModelLib.Clear();

            return true;
        }

        private const string SQL_SaveNewRefTable = @"insert into md_reftablelist (RTID,NAMESPACE,REFTABLENAME,REFTABLELEVELFORMAT,DESCRIPTION,DOWNLOADMODE,REFTABLEMODE,DWDM ) 
                                                       values  (@RTID,@NAMESPACE,@REFTABLENAME,'',@DESCRIPTION,1,1,@DWDM ) ";
        /// <summary>
        /// 保存新的代码表
        /// </summary>
        /// <param name="_tm"></param>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        public async Task<bool> SaveNewRefTable(DB_TableMeta _tm, MD_Namespace _namespace)
        {

            MySqlParameter[] _param = {
                 new MySqlParameter("@RTID",MySqlDbType.Int64),
                 new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
                 new MySqlParameter("@REFTABLENAME",MySqlDbType.VarChar,50),
                 new MySqlParameter("@DESCRIPTION",MySqlDbType.VarChar,100),
                 new MySqlParameter("@DWDM",MySqlDbType.VarChar,12)
            };
            string[] _tnames = _tm.TableName.Split('.');
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = _namespace.NameSpace;
            _param[2].Value = _tnames[_tnames.Length - 1];
            _param[3].Value = (_tm.TableComment == "") ? _tm.TableName : _tm.TableComment;
            _param[4].Value = _namespace.DWDM;
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveNewRefTable, _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }


        public async Task<bool> CreateNewRefTable(string TableName, string Namespace, string DWDM)
        {
            MySqlParameter[] _param = {
                 new MySqlParameter("@RTID",MySqlDbType.Int64),
                 new MySqlParameter("@NAMESPACE",MySqlDbType.VarChar,50),
                 new MySqlParameter("@REFTABLENAME",MySqlDbType.VarChar,50),
                 new MySqlParameter("@DESCRIPTION",MySqlDbType.VarChar,100),
                 new MySqlParameter("@DWDM",MySqlDbType.VarChar,12)
            };
            _param[0].Value = Lib.Snowflake.Instance.NextId();
            _param[1].Value = Namespace;
            _param[2].Value = TableName;
            _param[3].Value = "";
            _param[4].Value = DWDM;
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveNewRefTable, _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }
        /// <summary>
        /// 将新的表存入元数据
        /// </summary>
        /// <param name="_tm"></param>
        /// <param name="_ns"></param>
        /// <returns></returns>
        public async Task<bool> SaveNewTable(DB_TableMeta _tm, MD_Namespace _ns)
        {
            string InsertStr = "insert into md_table (TID,NAMESPACE,TABLENAME,TABLETYPE,DESCRIPTION,DISPLAYNAME,DWDM) VALUES ";
            InsertStr += "( @TID,@NAMESPACE,@TABLENAME,@TABLETYPE,@DESCRIPTION,@DISPLAYNAME,@DWDM)";

            MySqlParameter[] _param = {
                  new MySqlParameter("@TID", MySqlDbType.Int64),
                  new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
                  new MySqlParameter("@TABLENAME", MySqlDbType.VarChar, 50),
                  new MySqlParameter("@TABLETYPE", MySqlDbType.VarChar, 50),
                  new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                  new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar, 100),
                  new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12)
                        };


            var tid = aehyok.Lib.Snowflake.Instance.NextId();
            _param[0].Value = tid;
            _param[1].Value = _ns.NameSpace;
            _param[2].Value = _tm.TableName;
            _param[3].Value = _tm.TableType;
            _param[4].Value = _tm.TableComment;
            _param[5].Value = _tm.TableName;
            _param[6].Value = _ns.DWDM;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, InsertStr, _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }
        /// <summary>
        /// 保存修改的节点信息
        /// </summary>
        /// <param name="_nodes"></param>
        /// <returns></returns>
        public async Task<bool> SaveNodes(MD_Nodes _nodes)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("update md_nodes set NODENAME=@NODENAME,DISPLAYTITLE=@DISPLAYTITLE,DESCRIPT=@DESCRIPT,DWDM=@DWDM ");
            _sb.Append(" where ID = @ID");
            MySqlParameter[] _param = {
                new MySqlParameter("@NODENAME", MySqlDbType.VarChar, 100),
                new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar, 200),
                new MySqlParameter("@DESCRIPT", MySqlDbType.VarChar, 2000),
                new MySqlParameter("@DWDM", MySqlDbType.VarChar, 100),
                new MySqlParameter("@ID", MySqlDbType.VarChar, 100)
             };
            _param[0].Value = _nodes.NodeName;
            _param[1].Value = _nodes.DisplayTitle;
            _param[2].Value = _nodes.Descript;
            _param[3].Value = _nodes.DWDM;
            _param[4].Value = _nodes.ID;

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);

            // 待处理
            MyDA_MetaDataQuery.ModelLib.Clear();

            return true;
        }

        /// <summary>
        /// 保存查询模型定义
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        public async Task<bool> SaveQueryModel(MD_QueryModel _queryModel)
        {
            //保存查询模型定义信息
            StringBuilder _sb = new StringBuilder();
            _sb.Append(" update md_view SET VIEWNAME=@VIEWNAME,DESCRIPTION=@DESCRIPTION,");
            _sb.Append(" DISPLAYNAME=@DISPLAYNAME,DWDM=@DWDM,IS_GDCX=@IS_GDCX,IS_GLCX=@IS_GLCX, ");
            _sb.Append(" IS_SJSH=@IS_SJSH,DISPLAYORDER=@DISPLAYORDER,ICSTYPE=@ICSTYPE,EXTMETA=@EXTMETA ");
            _sb.Append(" WHERE VIEWID=@VIEWID ");
            MySqlParameter[] _param = {
                new MySqlParameter("@VIEWNAME", MySqlDbType.VarChar, 50),
                new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar, 100),
                new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                new MySqlParameter("@IS_GDCX", MySqlDbType.Int32),
                new MySqlParameter("@IS_GLCX",MySqlDbType.Int32),
                new MySqlParameter("@IS_SJSH",MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32),
                new MySqlParameter("@ICSTYPE", MySqlDbType.VarChar, 20),
                new MySqlParameter("@EXTMETA", MySqlDbType.VarChar, 4000),
                new MySqlParameter("@VIEWID",MySqlDbType.Int64)
            };

            _param[0].Value = _queryModel.QueryModelName;
            _param[1].Value = _queryModel.Description;
            _param[2].Value = _queryModel.DisplayTitle;
            _param[3].Value = _queryModel.DWDM;
            _param[4].Value = _queryModel.IsFixQuery ? 1 : 0;
            _param[5].Value = _queryModel.IsRelationQuery ? 1 : 0;
            _param[6].Value = _queryModel.IsDataAuditing ? 1 : 0;
            _param[7].Value = Convert.ToInt32(_queryModel.DisplayOrder);
            _param[8].Value = _queryModel.QueryInterface;
            _param[9].Value = _queryModel.EXTMeta;
            _param[10].Value = Convert.ToInt64(_queryModel.QueryModelID);

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param);
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }

        private const string SQL_SaveQueryModelExRight = @"
            update md_view_exright  set rvalue=@RVALUE,rtitle=@RTITLE,displayorder=@DISPLAYORDER where ID=@ID";
        public async Task<bool> SaveQueryModelExRight(MD_QueryModel_ExRight ExRight)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_SaveQueryModelExRight, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@RVALUE", ExRight.RightName));
                    _cmd.Parameters.Add(new MySqlParameter("@RTITLE", ExRight.RightTitle));
                    _cmd.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(ExRight.DisplayOrder)));
                    _cmd.Parameters.Add(new MySqlParameter("@ID", ExRight.ID));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;

                }
                catch (Exception e)
                {
                    string _msg = string.Format("保存查询模型[{0}]相关联的模型扩展权限信息时发生错误，错误信息：{1} ", ExRight.ModelID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }

        public async Task<bool> SaveQueryModel_UserDefine(string _queryModelID, string _display, string _descript)
        {
            try
            {
                string _ups = "update md_view set displayname=:DISPLAYNAME, DESCRIPTION=:DESCRIPTION ";
                _ups += " where VIEWID=:VIEWID ";
                MySqlParameter[] _params = {
                     new MySqlParameter(":DISPLAYNAME", MySqlDbType.VarChar,100),
                     new MySqlParameter(":DESCRIPTION",MySqlDbType.VarChar,100),
                     new MySqlParameter(":VIEWID",MySqlDbType.Int64)
                };
                _params[0].Value = _display;
                _params[1].Value = _descript;
                _params[2].Value = Convert.ToInt64(_queryModelID);

                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _ups, _params);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private const string SQL_SaveRefTable_Clear = "delete from dm_reftabledata where type=@type";
        private const string SQL_SaveRefTable_Add = "insert into dm_reftabledata (type,dm,mc,pyzt,px,sfyx,bz,fathercode,sfxs,sflr,sfyjd) values (@type,@dm,@mc,@pyzt,@px,@sfyx,@bz,@fathercode,@sfxs,@sflr,@sfyjd)";

        /// <summary>
        /// 保存代码表内容
        /// </summary>
        /// <param name="_refTable"></param>
        /// <param name="_refData"></param>
        /// <returns></returns>
        public async Task<bool> SaveRefTable(MD_RefTable _refTable, DataTable _refData)
        {
            try
            {
                StringBuilder _updateStr = new StringBuilder();
                _updateStr.Append(" update md_reftablelist set ");
                _updateStr.Append(" REFTABLELEVELFORMAT=@LEVELFORMAT,DESCRIPTION=@DES,DOWNLOADMODE=@DOWNLOAD,");
                _updateStr.Append(" REFTABLEMODE=@REFMODE,HIDECODE=@HIDECODE where RTID=@RTID");
                MySqlParameter[] _param = {
                 new MySqlParameter("@LEVELFORMAT",MySqlDbType.VarChar,20),
                 new MySqlParameter("@DES",MySqlDbType.VarChar,100),
                 new MySqlParameter("@DOWNLOAD",MySqlDbType.Int32),
                 new MySqlParameter("@REFMODE",MySqlDbType.Int32),
                 new MySqlParameter("@HIDECODE",MySqlDbType.Int32),
                 new MySqlParameter("@RTID",MySqlDbType.Int64)
             };
                _param[0].Value = _refTable.LevelFormat;
                _param[1].Value = _refTable.Description;
                _param[2].Value = Convert.ToInt32(_refTable.RefDownloadMode);
                _param[3].Value = Convert.ToInt32(_refTable.RefParamMode);
                _param[4].Value = _refTable.HideCode ? 1 : 0;
                _param[5].Value = Convert.ToInt64(_refTable.RefTableID);
                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _updateStr.ToString(), _param);

                //判断代码表为视图或数据表，视图不执行保存操作

                if (_refData != null && _refData.Rows.Count > 0)
                {

                    // 先把原记录删除        
                    using (MySqlConnection cn = MysqlDBHelper.OpenConnection(MysqlDBHelper.queryString))
                    {
                        MySqlTransaction txn = cn.BeginTransaction();
                        try
                        {
                            MySqlCommand _cmd = new MySqlCommand(SQL_SaveRefTable_Clear, cn);
                            _cmd.Parameters.Add(new MySqlParameter("@type", _refTable.RefTableName));
                            await _cmd.ExecuteNonQueryAsync();
                           
                            // 清除原记录后再进行插入
                            foreach (DataRow _dr in _refData.Rows)
                            {
                                MySqlCommand _cmd2 = new MySqlCommand(SQL_SaveRefTable_Add, cn);
                                _cmd2.Parameters.Add(new MySqlParameter("@type", _refTable.RefTableName));
                                _cmd2.Parameters.Add(new MySqlParameter("@dm", _dr["dm"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@mc", _dr["mc"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@pyzt", _dr["pyzt"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@px", _dr["px"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@sfyx", _dr["sfyx"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@bz", _dr["bz"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@fathercode", _dr["fathercode"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@sfxs", _dr["sfxs"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@sflr", _dr["sflr"]));
                                _cmd2.Parameters.Add(new MySqlParameter("@sfyjd", _dr["sfyjd"]));
                                await _cmd2.ExecuteNonQueryAsync();
                            }
                            txn.Commit();
                        }
                        catch (Exception ex)
                        {
                            txn.Rollback();
                            throw ex;
                        }
                        cn.Close();
                        return true;


                    }

                }
                MyDA_MetaDataQuery.ModelLib.Clear();
                return true;
            }
            catch (Exception ex)
            {
                string _msg = string.Format("代码表{1}内容时发生错误，错误信息：{0} ", ex.Message, _refTable.RefTableName);
                MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                return false;
            }

        }

        public bool SaveRightDefine(IList<MD_RightDefine> _rightList)
        {
            throw new NotImplementedException();
        }

        private const string SQL_SaveTableDefine = @"
               update md_table SET NAMESPACE=@NAMESPACE,TABLENAME =@TABLENAME,TABLETYPE =@TABLETYPE,
               DESCRIPTION =@DESCRIPTION,DISPLAYNAME =@DISPLAYNAME,DWDM =@DWDM,MAINKEY=@MAINKEY, 
               SECRETFUN=@SECRETFUN,EXTSECRET=@EXTSECRET,RESTYPE=@RESTYPE
               WHERE TID =@TID ";
        /// <summary>
        /// 保存表定义
        /// </summary>
        /// <param name="_table"></param>
        /// <returns></returns>
        public async Task<bool> SaveTableDefine(MD_Table _table)
        {
            try
            {
                //保存表定义信息
                MySqlParameter[] _param = {
                 new MySqlParameter("@NAMESPACE", MySqlDbType.VarChar, 50),
                 new MySqlParameter("@TABLENAME", MySqlDbType.VarChar, 50),
                 new MySqlParameter("@TABLETYPE", MySqlDbType.VarChar, 50),
                 new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar, 100),
                 new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar, 100),
                 new MySqlParameter("@DWDM", MySqlDbType.VarChar, 12),
                 new MySqlParameter("@MAINKEY",MySqlDbType.VarChar,50),
                 new MySqlParameter("@SECRETFUN",MySqlDbType.VarChar,50),
                 new MySqlParameter("@EXTSECRET",MySqlDbType.VarChar,1000),
                 new MySqlParameter("@RESTYPE",MySqlDbType.VarChar,100),
                 new MySqlParameter("@TID",MySqlDbType.Int64)
                };
                _param[0].Value = _table.NamespaceName;
                _param[1].Value = _table.TableName;
                _param[2].Value = _table.TableType;
                _param[3].Value = _table.Description;
                _param[4].Value = _table.DisplayTitle;
                _param[5].Value = _table.DWDM;
                _param[6].Value = _table.MainKey;
                _param[7].Value = _table.SecretFun;
                _param[8].Value = _table.ExtSecret;
                _param[9].Value = (_table.ResourceType == null) ? "" : string.Join(",", _table.ResourceType);
                _param[10].Value = Convert.ToInt64(_table.TID);

                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveTableDefine, _param);

                // 取md_tablecolumn中已存在的列           
                string SQL_cl = string.Format("select tcid from md_tablecolumn where tid={0} ", _table.TID);
                MySqlDataReader dr = MysqlDBHelper.ExecuteReader(MysqlDBHelper.conf, CommandType.Text, SQL_cl);
                List<long> existTCIDs = new List<long>();
                while (await dr.ReadAsync())
                {
                    var id = dr.GetInt64(0);
                    existTCIDs.Add(id);
                }
                dr.Close();

                // 判断已删除的列，md_tablecolumn和md_viewtablecolumn中进行删除
                var deleted = existTCIDs.Where(a => !_table.Columns.Any(c => c.ColumnID == a.ToString()));
                if (deleted.Count() > 0)
                {
                    string del_viewtablecolumn = string.Format("delete from md_viewtablecolumn where tcid in ({0}) ", string.Join(",", deleted));
                    string del_tablecolumn = string.Format("delete from md_tablecolumn where tcid in ({0}) ", string.Join(",", deleted));
                    using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
                    {
                        MySqlTransaction txn = cn.BeginTransaction();
                        MySqlCommand _cmd = new MySqlCommand(del_viewtablecolumn, cn);
                        await _cmd.ExecuteNonQueryAsync();

                        _cmd = new MySqlCommand(del_tablecolumn, cn);
                        await _cmd.ExecuteNonQueryAsync();
                        txn.Commit();
                    }
                }

                // 新增的列执行插入
                var news = _table.Columns.Where(a => !existTCIDs.Any(c => c.ToString() == a.ColumnID));
                foreach (var i in news)
                {
                    MD_DefineFactory.InsertColumnDefine(_table, i);
                }

                // 更新已存在的列
                var update = _table.Columns.Where(a => !deleted.Contains(Convert.ToInt64(a.ColumnID))).Where(a => !news.Any(c => c.ToString() == a.ColumnID));
                foreach (var u in update)
                {
                    MD_DefineFactory.UpdateColumnDefine(_table, u);
                }
                MyDA_MetaDataQuery.ModelLib.Clear();
                return true;
            }
            catch (Exception ex)
            {
                string _msg = string.Format("保存表定义信息时发生错误，错误信息：{0} ", ex.Message);
                MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                return false;
            }
        }

        private const string SQL_CheckExistV2G = @"select count(*) from md_view2gl where id=@ID";
        private const string SQL_InsertV2G = @"
                insert into md_view2gl (id,viewid,targetgl,targetcs,displayorder,displaytitle) 
                values (@ID,@VIEWID,@TARGETGL,@TARGETCS,@DISPLAYORDER,@DISPLAYTITLE)";
        private const string SQL_UpdateV2G = @"
                update md_view2gl set viewid=@VIEWID,targetgl=@TARGETGL,targetcs=@TARGETCS,
                displayorder=@DISPLAYORDER,displaytitle=@DISPLAYTITLE where id=@ID";
        public async Task<bool> SaveView2GL(string V2GID, string VIEWID, string GuideLineID, string Params, int DisplayOrder, string DisplayTitle)
        {
            MySqlCommand SaveCmd;
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_CheckExistV2G, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", V2GID));
                    long _count = (long)await _cmd.ExecuteScalarAsync();
                    if (_count > 0)
                    {
                        SaveCmd = new MySqlCommand(SQL_UpdateV2G, cn);
                        SaveCmd.Parameters.Add(new MySqlParameter("@VIEWID", VIEWID));
                        SaveCmd.Parameters.Add(new MySqlParameter("@TARGETGL", GuideLineID));
                        SaveCmd.Parameters.Add(new MySqlParameter("@TARGETCS", Params));
                        SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(DisplayOrder)));
                        SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYTITLE", DisplayTitle));
                        SaveCmd.Parameters.Add(new MySqlParameter("@ID", V2GID));
                        await SaveCmd.ExecuteNonQueryAsync();
                    }
                    else
                    {
                        SaveCmd = new MySqlCommand(SQL_InsertV2G, cn);
                        SaveCmd.Parameters.Add(new MySqlParameter("@ID", V2GID));
                        SaveCmd.Parameters.Add(new MySqlParameter("@VIEWID", VIEWID));
                        SaveCmd.Parameters.Add(new MySqlParameter("@TARGETGL", GuideLineID));
                        SaveCmd.Parameters.Add(new MySqlParameter("@TARGETCS", Params));
                        SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(DisplayOrder)));
                        SaveCmd.Parameters.Add(new MySqlParameter("@DISPLAYTITLE", DisplayTitle));
                        await SaveCmd.ExecuteNonQueryAsync();
                    }
                    _txn.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    string _msg = string.Format("保存查询模型的关联指标定义时发生错误，错误信息：{0} ", ex.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    _txn.Rollback();
                    return false;
                }
            }

        }


        private const string SQL_SaveView2View = @"update md_view2view set TARGETVIEWNAME=@VIEWNAME,RELATIONSTR=@STR,DISPLAYORDER=@DISPORDER,DISPLAYTITLE=@TITLE where ID=@ID";
        public async Task<bool> SaveView2View(MD_View2View View2View)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_SaveView2View, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWNAME", View2View.TargetViewName));
                    _cmd.Parameters.Add(new MySqlParameter("@STR", View2View.RelationString));
                    _cmd.Parameters.Add(new MySqlParameter("@DISPORDER", Convert.ToInt32(View2View.DisplayOrder)));
                    _cmd.Parameters.Add(new MySqlParameter("@TITLE", View2View.DisplayTitle));
                    _cmd.Parameters.Add(new MySqlParameter("@ID", View2View.ID));
                    await _cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch (Exception e)
                {
                    string _msg = string.Format("在保存关联的模型信息{0}时发生错误，错误信息：{1} ", View2View.ID, e.Message);
                    MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                    return false;
                }
            }
        }

        public Task<bool> SaveView2ViewGroup(MD_View2ViewGroup View2ViewGroup)
        {
            throw new NotImplementedException();
        }

        private const string SQL_SaveViewChildTable_Update = @"
                update md_viewtable SET DISPLAYNAME=@DISPLAYNAME,TABLERELATION=@TABLERELATION,CANCONDITION=@CANCONDITION, 
                DISPLAYORDER=@DISPLAYORDER,DISPLAYTYPE=@DISPLAYTYPE,INTEGRATEDAPP=@INTEGRATEDAPP WHERE VTID =@VTID";
        private const string SQL_SaveViewChildTable_Insert = @"
                insert into md_viewtablecolumn (VTCID,VTID,TCID,CANCONDITIONSHOW,CANRESULTSHOW,DEFAULTSHOW,DWDM,FIXQUERYITEM,CANMODIFY,PRIORITY,DISPLAYORDER)
                 VALUES (@VTCID,@VTID,@TCID,@CANCONDITIONSHOW,@CANRESULTSHOW,@DEFAULTSHOW,@DWDM,@FIXQUERYITEM,@CANMODIFY,@PRIORITY,@DISPLAYORDER )";
        private const string SQL_SaveViewChildTable_Delete = @"delete from md_viewtablecolumn where VTID=@VTID";

        /// <summary>
        /// 保存查询模型副表定义
        /// </summary>
        /// <param name="_viewtable"></param>
        /// <returns></returns>
        public async Task<bool> SaveViewChildTable(MD_ViewTable _viewtable)
        {
            try
            {
                MySqlParameter[] _param = {
                new MySqlParameter("@DISPLAYNAME",MySqlDbType.VarChar,100),
                new MySqlParameter("@TABLERELATION",MySqlDbType.VarChar,300),
                new MySqlParameter("@CANCONDITION",MySqlDbType.VarChar,10),
                new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32),
                new MySqlParameter("@DISPLAYTYPE",MySqlDbType.Int32),
                new MySqlParameter("@INTEGRATEDAPP",MySqlDbType.VarChar,1000),
                new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
                _param[0].Value = _viewtable.DisplayTitle;
                _param[1].Value = _viewtable.RelationString;
                _param[2].Value = (_viewtable.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord) ? 1 : 0;
                _param[3].Value = Convert.ToInt32(_viewtable.DisplayOrder);
                _param[4].Value = (_viewtable.DisplayType == MDType_DisplayType.GridType) ? 0 : 1;
                _param[5].Value = _viewtable.IntegratedApp;
                _param[6].Value = Convert.ToInt64(_viewtable.ViewTableID);

                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewChildTable_Update, _param);

                //清除所有字段定义
                MySqlParameter[] _param2 = {
                 new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
                _param2[0].Value = Convert.ToInt64(_viewtable.ViewTableID);
                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewChildTable_Delete, _param2);

                //保存字段定义信息
                foreach (MD_ViewTableColumn _tc in _viewtable.Columns)
                {
                    MySqlParameter[] _param3 = {
                   new MySqlParameter("@VTCID", MySqlDbType.Int64),
                   new MySqlParameter("@VTID", MySqlDbType.Int64),
                   new MySqlParameter("@TCID", MySqlDbType.Int64),
                   new MySqlParameter("@CANCONDITIONSHOW", MySqlDbType.Int32),
                   new MySqlParameter("@CANRESULTSHOW", MySqlDbType.Int32),
                   new MySqlParameter("@DEFAULTSHOW", MySqlDbType.Int32),
                   new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                   new MySqlParameter("@FIXQUERYITEM",MySqlDbType.Int32),
                   new MySqlParameter("@CANMODIFY",MySqlDbType.Int32),
                   new MySqlParameter("@PRIORITY",MySqlDbType.Int32),
                   new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32)
                };
                    _param3[0].Value = Convert.ToInt64(_tc.ViewTableColumnID);
                    _param3[1].Value = Convert.ToInt64(_viewtable.ViewTableID);
                    _param3[2].Value = Convert.ToInt64(_tc.ColumnID);
                    _param3[3].Value = _tc.CanShowAsCondition ? 1 : 0;
                    _param3[4].Value = _tc.CanShowAsResult ? 1 : 0;
                    _param3[5].Value = _tc.DefaultResult ? 1 : 0;
                    _param3[6].Value = _tc.DWDM;
                    _param3[7].Value = _tc.IsFixQueryItem ? 1 : 0;
                    _param3[8].Value = _tc.CanModify ? 1 : 0;
                    _param3[9].Value = Convert.ToInt32(_tc.Priority);
                    _param3[10].Value = Convert.ToInt32(_tc.DisplayOrder);

                    await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewChildTable_Insert, _param3);
                }
                MyDA_MetaDataQuery.ModelLib.Clear();
                return true;
            }
            catch (Exception e)
            {
                string _msg = string.Format("保存查询模型副表定义{0}时发生错误，错误信息：{1} ", _viewtable.ViewTableID, e.Message);
                MysqlLogWriter.WriteSystemLog(_msg, "ERROR");
                return false;
            }
        }

        private const string SQL_SaveViewMainTable_Update = @" 
                update md_viewtable SET DISPLAYNAME=@DISPLAYNAME,INTEGRATEDAPP=@INTEGRATEDAPP WHERE VTID =@VTID";
        private const string SQL_SaveViewMainTable_Delete = @"
                delete from md_viewtablecolumn where VTID=@VTID";
        private const string SQL_SaveViewMainTable_Insert = @"
                insert into md_viewtablecolumn (VTCID,VTID,TCID,CANCONDITIONSHOW,CANRESULTSHOW,DEFAULTSHOW,DWDM,FIXQUERYITEM,CANMODIFY,PRIORITY,DISPLAYORDER)
                VALUES (@VTCID,@VTID,@TCID,@CANCONDITIONSHOW,@CANRESULTSHOW,@DEFAULTSHOW,@DWDM,@FIXQUERYITEM,@CANMODIFY,@PRIORITY,@DISPLAYORDER )";
        /// <summary> 保存主表定义信息
        /// </summary>
        /// <param name="_viewtable"></param>
        /// <returns></returns>
        public async Task<bool> SaveViewMainTable(MD_ViewTable _viewtable)
        {
            MySqlParameter[] _param = {
               new MySqlParameter("@DISPLAYNAME",MySqlDbType.VarChar,100),
               new MySqlParameter("@INTEGRATEDAPP",MySqlDbType.VarChar,1000),
               new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param[0].Value = _viewtable.DisplayTitle;
            _param[1].Value = _viewtable.IntegratedApp;
            _param[2].Value = Convert.ToInt64(_viewtable.ViewTableID);

            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewMainTable_Update, _param);

            //清除所有字段定义
            MySqlParameter[] _param2 = {
                new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param2[0].Value = Convert.ToInt64(_viewtable.ViewTableID);
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewMainTable_Delete, _param2);

            //保存字段定义信息
            foreach (MD_ViewTableColumn _tc in _viewtable.Columns)
            {
                MySqlParameter[] _param3 = {
                    new MySqlParameter("@VTCID", MySqlDbType.Int64),
                    new MySqlParameter("@VTID", MySqlDbType.Int64),
                    new MySqlParameter("@TCID", MySqlDbType.Int64),
                    new MySqlParameter("@CANCONDITIONSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@CANRESULTSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@DEFAULTSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                    new MySqlParameter("@FIXQUERYITEM",MySqlDbType.Int32),
                    new MySqlParameter("@CANMODIFY",MySqlDbType.Int32),
                    new MySqlParameter("@PRIORITY",MySqlDbType.Int32),
                    new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32)

                };
                _param3[0].Value = Convert.ToInt64(_tc.ViewTableColumnID);
                _param3[1].Value = Convert.ToInt64(_viewtable.ViewTableID);
                _param3[2].Value = Convert.ToInt64(_tc.ColumnID);
                _param3[3].Value = _tc.CanShowAsCondition ? 1 : 0;
                _param3[4].Value = _tc.CanShowAsResult ? 1 : 0;
                _param3[5].Value = _tc.DefaultResult ? 1 : 0;
                _param3[6].Value = _tc.DWDM;
                _param3[7].Value = _tc.IsFixQueryItem ? 1 : 0;
                _param3[8].Value = _tc.CanModify ? 1 : 0;
                _param3[9].Value = Convert.ToInt32(_tc.Priority);
                _param3[10].Value = Convert.ToInt32(_tc.DisplayOrder);

                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, SQL_SaveViewMainTable_Insert, _param3);
            }
            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }

        /// <summary>
        /// 保存查询模型子表的顺序
        /// </summary>
        /// <param name="ChildTableOrder"></param>
        /// <returns></returns>
        public async Task<bool> SaveViewTableOrder_UserDefine(Dictionary<string, int> ChildTableOrder)
        {
            string _ups = "update md_viewtable set DISPLAYORDER=@DISPLAYORDER where VTID=@VTID";
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction txn = cn.BeginTransaction();
                try
                {
                    foreach (string _s in ChildTableOrder.Keys)
                    {
                        MySqlCommand _cmd = new MySqlCommand(_ups, cn);
                        _cmd.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(ChildTableOrder[_s])));
                        _cmd.Parameters.Add(new MySqlParameter("@VTID", Convert.ToInt64(_s)));
                        await _cmd.ExecuteNonQueryAsync();
                    }
                    txn.Commit();
                    cn.Close();
                    return true;
                }
                catch (Exception e)
                {
                    txn.Rollback();
                    string _errmsg = string.Format("执行保存查询模型子表的顺序数据时出错,错误信息为:{0}!",
                             e.Message);
                    MysqlLogWriter.WriteSystemLog(_errmsg, "ERROR");
                    return false;
                }
            }
        }

        public async Task<bool> SaveViewTable_UserDefine(string _viewTableID, string _displayString, MDType_DisplayType _displayType, List<MD_ViewTableColumn> TableColumnDefine)
        {
            #region 更新MD_VIEWTABLE表
            string _ups = "update md_viewtable set DISPLAYNAME=@DISPLAYNAME,DISPLAYTYPE=@DISPLAYTYPE where VTID=@VTID";
            MySqlParameter[] _param = {
                new MySqlParameter("@DISPLAYNAME", MySqlDbType.VarChar),
                new MySqlParameter("@DISPLAYTYPE",MySqlDbType.Int32),
                new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param[0].Value = _displayString;
            _param[1].Value = (_displayType == MDType_DisplayType.FormType) ? 1 : 0;
            _param[2].Value = Convert.ToInt64(_viewTableID);
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _ups, _param);

            #endregion

            #region 更新MD_TABLECOLUMN表
            string _up = "update md_tablecolumn set DISPLAYTITLE=@DISPLAYTITLE,DISPLAYLENGTH=@DISPLAYLENGTH,DISPLAYHEIGHT=@DISPLAYHEIGHT,DISPLAYORDER=@DISPLAYORDER,colwidth=@DISPLAYWIDTH ";
            _up += " WHERE TCID=@TCID ";
            foreach (MD_ViewTableColumn _tc in TableColumnDefine)
            {
                MySqlParameter[] _param4 = {
                    new MySqlParameter("@DISPLAYTITLE", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DISPLAYLENGTH", MySqlDbType.Int32),
                    new MySqlParameter("@DISPLAYHEIGHT", MySqlDbType.Int32),
                    new MySqlParameter("@DISPLAYORDER", MySqlDbType.Int32),
                    new MySqlParameter("@DISPLAYWIDTH",MySqlDbType.Int32),
                    new MySqlParameter("@TCID",MySqlDbType.Int64)
                };
                _param4[0].Value = _tc.TableColumn.DisplayTitle;
                _param4[1].Value = Convert.ToInt32(_tc.TableColumn.DisplayLength);
                _param4[2].Value = Convert.ToInt32(_tc.TableColumn.DisplayHeight);
                _param4[3].Value = Convert.ToInt32(_tc.DisplayOrder);
                _param4[4].Value = Convert.ToInt32(_tc.TableColumn.ColWidth);
                _param4[5].Value = Convert.ToInt64(_tc.ColumnID);
                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _up, _param4);
            }
            #endregion


            #region 更新MD_VIEWTABLECOLUMN表
            //清除所有字段定义
            string _del = "delete from md_viewtablecolumn where VTID=@VTID";
            MySqlParameter[] _param2 = {
               new MySqlParameter("@VTID",MySqlDbType.Int64)
            };
            _param2[0].Value = Convert.ToInt64(_viewTableID);
            await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _del, _param2);

            StringBuilder _sb = new StringBuilder();
            _sb.Append(" insert into md_viewtablecolumn (VTCID,VTID,TCID,");
            _sb.Append(" CANCONDITIONSHOW,CANRESULTSHOW,DEFAULTSHOW,");
            _sb.Append(" DWDM,FIXQUERYITEM,CANMODIFY,PRIORITY,DISPLAYORDER) ");
            _sb.Append(" VALUES (@VTCID,@VTID,@TCID,");
            _sb.Append(" @CANCONDITIONSHOW,@CANRESULTSHOW,@DEFAULTSHOW,");
            _sb.Append(" @DWDM,@FIXQUERYITEM,@CANMODIFY,@PRIORITY,@DISPLAYORDER) ");
            //保存字段定义信息
            foreach (MD_ViewTableColumn _tc in TableColumnDefine)
            {
                MySqlParameter[] _param3 = {
                    new MySqlParameter("@VTCID", MySqlDbType.Int64),
                    new MySqlParameter("@VTID", MySqlDbType.Int64),
                    new MySqlParameter("@TCID", MySqlDbType.Int64),
                    new MySqlParameter("@CANCONDITIONSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@CANRESULTSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@DEFAULTSHOW", MySqlDbType.Int32),
                    new MySqlParameter("@DWDM",MySqlDbType.VarChar,12),
                    new MySqlParameter("@FIXQUERYITEM",MySqlDbType.Int32),
                    new MySqlParameter("@CANMODIFY",MySqlDbType.Int32),
                    new MySqlParameter("@PRIORITY",MySqlDbType.Int32),
                    new MySqlParameter("@DISPLAYORDER",MySqlDbType.Int32)
                };
                _param3[0].Value = Convert.ToInt64(_tc.ViewTableColumnID);
                _param3[1].Value = Convert.ToInt64(_viewTableID);
                _param3[2].Value = Convert.ToInt64(_tc.ColumnID);
                _param3[3].Value = _tc.CanShowAsCondition ? 1 : 0;
                _param3[4].Value = _tc.CanShowAsResult ? 1 : 0;
                _param3[5].Value = _tc.DefaultResult ? 1 : 0;
                _param3[6].Value = _tc.DWDM;
                _param3[7].Value = _tc.IsFixQueryItem ? 1 : 0;
                _param3[8].Value = _tc.CanModify ? 1 : 0;
                _param3[9].Value = Convert.ToInt32(_tc.Priority);
                _param3[10].Value = Convert.ToInt32(_tc.DisplayOrder);
                await MysqlDBHelper.ExecuteNonQuery(MysqlDBHelper.conf, CommandType.Text, _sb.ToString(), _param3);
            }
            #endregion

            MyDA_MetaDataQuery.ModelLib.Clear();
            return true;
        }


        private const string SQL_GetView2ApplicationList = @"
                select ID,VIEWID,TITLE,INTEGRATEDAPP,DISPLAYHEIGHT,URL,DISPLAYORDER,META 
                from md_view2app where VIEWID=@VIEWID order by DISPLAYORDER";
        /// <summary>
        /// 获取查询模型的集成应用展示定义
        /// </summary>
        /// <param name="QueryModelID"></param>
        /// <returns></returns>
        public async Task<List<MD_View2App>> GetView2ApplicationList(string QueryModelID)
        {
            List<MD_View2App> _ret = new List<MD_View2App>();
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_GetView2ApplicationList, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(QueryModelID)));
                    using (MySqlDataReader _dr = _cmd.ExecuteReader())
                    {
                        while (await _dr.ReadAsync())
                        {
                            MD_View2App _app = new MD_View2App();
                            _app.ID = _dr.IsDBNull(0) ? "" : _dr.GetString(0);
                            _app.ViewID = _dr.IsDBNull(1) ? "" : _dr.GetString(1);
                            _app.Title = _dr.IsDBNull(2) ? "" : _dr.GetString(2);
                            _app.AppName = _dr.IsDBNull(3) ? "" : _dr.GetString(3);
                            _app.DisplayHeight = _dr.IsDBNull(4) ? 40 : _dr.GetInt32(4);
                            _app.RegURL = _dr.IsDBNull(5) ? "" : _dr.GetString(5);
                            _app.DisplayOrder = _dr.IsDBNull(6) ? 40 : _dr.GetInt32(6);
                            _app.Meta = _dr.IsDBNull(7) ? "" : _dr.GetString(7);

                            _ret.Add(_app);
                        }
                    }

                }
                catch (Exception e)
                {
                    string _err = string.Format("在获取查询模型的集成应用展示定义时发生错误，错误信息：{0}", e.Message);
                    MysqlLogWriter.WriteSystemLog(_err, "ERROR");

                }
            }
            return _ret;
        }

        private const string SQL_CMD_DelView2App = @"delete from md_view2app where id=@ID";
        /// <summary>
        /// 删除查询模型的集成应用展示定义
        /// </summary>
        /// <param name="V2AID"></param>
        /// <returns></returns>
        public async Task<string> CMD_DelView2App(string V2AID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_CMD_DelView2App, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Convert.ToInt64(V2AID)));
                    await _cmd.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _err = string.Format("在删除查询模型的集成应用展示定义时发生错误，错误信息：{0}", e.Message);
                    MysqlLogWriter.WriteSystemLog(_err, "ERROR");
                    return _err;
                }
            }
        }
        /// <summary>
        /// 保存查询模型的集成应用展示定义
        /// </summary>
        /// <param name="V2AID"></param>
        /// <param name="View2AppData"></param>
        /// <returns></returns>
        public async Task<bool> SaveView2App(string V2AID, MD_View2App View2AppData)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                MySqlTransaction _txn = cn.BeginTransaction();
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_CMD_DelView2App, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@ID", Convert.ToInt64(V2AID)));
                    await _cmd.ExecuteNonQueryAsync();

                    MySqlCommand _ins = new MySqlCommand(SQL_SaveView2App_Insert, cn);
                    _ins.Parameters.Add(new MySqlParameter("@ID", Convert.ToInt64(V2AID)));
                    _ins.Parameters.Add(new MySqlParameter("@VIEWID", Convert.ToInt64(View2AppData.ViewID)));
                    _ins.Parameters.Add(new MySqlParameter("@TITLE", View2AppData.Title));
                    _ins.Parameters.Add(new MySqlParameter("@INTEGRATEDAPP", View2AppData.AppName));
                    _ins.Parameters.Add(new MySqlParameter("@DISPLAYHEIGHT", Convert.ToInt32(View2AppData.DisplayHeight)));
                    _ins.Parameters.Add(new MySqlParameter("@URL", View2AppData.RegURL));
                    _ins.Parameters.Add(new MySqlParameter("@DISPLAYORDER", Convert.ToInt32(View2AppData.DisplayOrder)));
                    _ins.Parameters.Add(new MySqlParameter("@META", View2AppData.Meta));
                    await _ins.ExecuteNonQueryAsync();

                    _txn.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    string _err = string.Format("在保存查询模型的集成应用展示定义时发生错误，错误信息：{0}", e.Message);
                    MysqlLogWriter.WriteSystemLog(_err, "ERROR");
                    return false;
                }
            }
        }

        private const string SQL_CMD_ClearView2App = @"delete from md_view2app where VIEWID=@VIEWID";
        /// <summary>
        /// 清空查询模型的集成应用展示定义
        /// </summary>
        /// <param name="QueryModelID"></param>
        /// <returns></returns>
        public async Task<string> CMD_ClearView2App(string QueryModelID)
        {
            using (MySqlConnection cn = MysqlDBHelper.OpenConnection())
            {
                try
                {
                    MySqlCommand _cmd = new MySqlCommand(SQL_CMD_ClearView2App, cn);
                    _cmd.Parameters.Add(new MySqlParameter("@VIEWID", QueryModelID));
                    await _cmd.ExecuteNonQueryAsync();
                    return "";
                }
                catch (Exception e)
                {
                    string _err = string.Format("在清空查询模型的集成应用展示定义时发生错误，错误信息：{0}", e.Message);
                    MysqlLogWriter.WriteSystemLog(_err, "ERROR");
                    return _err;
                }
            }
        }








    }
}
