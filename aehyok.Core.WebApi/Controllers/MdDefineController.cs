using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using aehyok.Base.Filters;
using aehyok.Base.Models;
using aehyok.Lib;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using aehyok.Lib.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using aehyok.Core.MySqlDataAccessor;

namespace aehyok.Controllers
{
    /// <summary>
    /// 查询模型定义
    /// </summary>
    [Route("/api/mddefine")]
    public class MdDefineController //: MDControllerBase
    {
        private readonly IEncryptionService encryptionService;
        private readonly IMetaDataManager mdService;

        public MdDefineController(IEncryptionService encryptionService,
             IMetaDataManager mdService)
        {
            this.encryptionService = encryptionService;
            this.mdService = mdService;
        }

        /// <summary>
        /// 取用户token
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="secret">secret</param>
        /// <returns></returns>
        [HttpPost("GetUserToken")]
        [AllowAnonymous]
        public async Task<string> GetUserToken(string name, string secret)
        {
            if (name != "sunlight" || secret != "secret")
            {
                return null;
                //throw new ValidException("用户名或secret不正确！");
            }

            var token = this.encryptionService.CreateSaltKey(64);
            //Redis保存两小时
            var tokenHash = this.encryptionService.CreateHash(token, "SHA1");

            //await RedisHelper.SetAsync($"token_{tokenHash}", token, expireSeconds: 60 * 60 * 24);

            //// 先写一个固定的CODE
            //await RedisHelper.SetAsync($"code_{tokenHash}", 1, expireSeconds: 60 * 60 * 24);

            return token;
        }      

        #region 通用操作

        /// <summary>
        /// 从序列产生器中产生新的ID号
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNewID")]
        public string GetNewID()
        {
            var ret = this.mdService.GetNewID();
            return ret;
        }
        #endregion

        #region   节点 node

        /// <summary>
        ///  获取节点列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetNodeList")]
        public async Task<IList<MD_Nodes>> GetNodeList()
        {

            var ret = await this.mdService.GetNodeList();

            return ret;
        }

        /// <summary>
        /// 保存新的节点信息
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        [HttpPost("SaveNewNodes")]
        public async Task<bool> SaveNewNodes(MD_Nodes nodes)
        {
            var ret = await this.mdService.SaveNewNodes(nodes);
            return ret;

        }

        /// <summary>
        /// 修改节点信息
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        [HttpPost("SaveNodes")]
        public async Task<bool> SaveNodes(MD_Nodes nodes)
        {
            var ret = await this.mdService.SaveNodes(nodes);
            return ret;
        }

        /// <summary>
        /// 删除节点信息
        /// </summary>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        [HttpPost("DelNodes")]
        public async Task<bool> DelNodes(string nodeID)
        {
            var ret = await this.mdService.DelNodes(nodeID);
            return ret;
        }

        #endregion

        #region  命名空间

        /// <summary>
        /// 保存新的命名空间
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        /// 
        [HttpPost("SaveNewNameSapce")]
        public async Task<bool> SaveNewNameSapce(MD_Namespace nspace)
        {
            var ret = await this.mdService.SaveNewNameSapce(nspace);

            return ret;

        }

        /// <summary>
        /// 修改命名空间信息
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        [HttpPost("SaveNameSapce")]
        public async Task<bool> SaveNameSapce(MD_Namespace nspace)
        {
            var ret = await this.mdService.SaveNameSapce(nspace);
            return ret;

        }
        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        [HttpPost("DelNamespace")]
        public async Task<bool> DelNamespace(MD_Namespace nspace)
        {
            var ret = await this.mdService.DelNamespace(nspace);
            return ret;

        }

        /// <summary>
        /// 获取节点下的命名空间
        /// </summary>
        /// <param name="NodeDWDM"></param>
        /// <returns></returns>
        [HttpPost("GetNameSpaceAtNode")]
        public async Task<IList<MD_Namespace>> GetNameSpaceAtNode(string NodeDWDM)
        {
            var ret = await this.mdService.GetNameSpaceAtNode(NodeDWDM);

            return ret;
        }


        #endregion


        #region  md_table操作


        /// <summary>
        /// 从数据库中获取所有的表定义
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetDBTableList")]
        public async Task<IList<DB_TableMeta>> GetDBTableList()
        {
            var ret = await this.mdService.GetDBTableList();
            return ret;
        }
        /// <summary>
        /// 从数据库获取表的所有字段
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        [HttpGet("GetDBColumnsOfTable")]
        public async Task<IList<DB_ColumnMeta>> GetDBColumnsOfTable(string TableName)
        {
            var ret = await this.mdService.GetDBColumnsOfTable(TableName);
            return ret;
        }

        /// <summary>
        /// 删除表定义
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [HttpPost("DelTableMeta")]
        public async Task<bool> DelTableMeta(string tableId)
        {
            var ret = await this.mdService.DelTableMeta(tableId);
            return ret;
        }

        /// <summary>
        /// 取指定表ID的表定义
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        [HttpPost("GetTableByTableID")]
        public async Task<MD_Table> GetTableByTableID(string tid)
        {
            var ret = await this.mdService.GetTableByTableID(tid);
            return ret;
        }

        /// <summary>
        /// 取命名空间下的表的列表
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        [HttpPost("GetTablesAtNamespace")]
        public async Task<IList<MD_Table>> GetTablesAtNamespace(string nspace)
        {
            var ret = await this.mdService.GetTablesAtNamespace(nspace);
            return ret;
        }

        /// <summary>
        /// 导入表定义
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost("ImportTableDefine")]
        public async Task<bool> ImportTableDefine(MD_Table table)
        {
            var ret = await this.mdService.ImportTableDefine(table);
            return ret;
        }

        /// <summary>
        /// 将新的表存入元数据
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost("SaveNewTable")]
        public async Task<bool> SaveNewTable(SaveTableModel model)
        {
            var ret = await this.mdService.SaveNewTable(model.TableMeta, model.NameSpace);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 修改表定义
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        [HttpPost("SaveTableDefine")]
        public async Task<bool> SaveTableDefine(MD_Table table)
        {
            var ret = await this.mdService.SaveTableDefine(table);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }


        #endregion


        #region   md_tablecolumn 操作

        /// <summary>
        /// 取表的列定义
        /// </summary>
        /// <param name="TableID"></param>
        /// <returns></returns>
        [HttpPost("GetColumnsOfTable")]
        public async Task<IList<MD_TableColumn>> GetColumnsOfTable(string TableID)
        {
            var ret = await this.mdService.GetColumnsOfTable(TableID);
            return ret;

        }

        #endregion


        #region  md_view MD_VIEWTABLE 查询模型操作 

        /// <summary>
        /// 修改查询模型定义
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost("SaveQueryModel")]
        public async Task<bool> SaveQueryModel(MD_QueryModel queryModel)
        {
            var ret = await this.mdService.SaveQueryModel(queryModel);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 删除查询模型
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("DelViewMeta")]
        public async Task<bool> DelViewMeta(string queryModelID)
        {
            var ret = await this.mdService.DelViewMeta(queryModelID);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 取所有查询模型名称
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllQueryModelNames")]
        public async Task<IList<string>> GetAllQueryModelNames()
        {
            var ret = await this.mdService.GetAllQueryModelNames();
            return ret;
        }


        /// <summary>
        /// 取查询模型的主表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("GetMainTableOfQueryModel")]
        public async Task<MD_ViewTable> GetMainTableOfQueryModel(string queryModelID)
        {
            var ret = await this.mdService.GetMainTableOfQueryModel(queryModelID);
            return ret;
        }

        /// <summary>
        ///  取命名空间下的查询模型
        /// </summary>
        /// <param name="nspace"></param>
        /// <returns></returns>
        [HttpPost("GetQueryModelAtNamespace")]
        public async Task<IList<MD_QueryModel>> GetQueryModelAtNamespace(string nspace)
        {
            var ret = await this.mdService.GetQueryModelAtNamespace(nspace);
            return ret;
        }

        /// <summary>
        /// 通过名称获取查询模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        [HttpPost("GetQueryModelByName")]
        public async Task<MD_QueryModel> GetQueryModelByName(string modelName)
        {
            var ret = await this.mdService.GetQueryModelByName(modelName);
            return ret;
        }
        /// <summary>
        ///  通过命名空间.名称获取查询模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        [HttpPost("GetQueryModelByName2")]
        public async Task<MD_QueryModel> GetQueryModelByName2(string modelName, string nameSpace)
        {
            var ret = await this.mdService.GetQueryModelByName(modelName, nameSpace);
            return ret;

        }

        /// <summary>
        /// 导入查询模型
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost("ImportQueryModelDefine")]
        public async Task<bool> ImportQueryModelDefine(MD_QueryModel queryModel)
        {
            var ret = await this.mdService.ImportQueryModelDefine(queryModel);
            return ret;
        }

        /// <summary>
        /// 保存新的查询模型
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        [HttpPost("SaveNewQueryModel")]
        public async Task<bool> SaveNewQueryModel(MD_QueryModel queryModel)
        {

            var ret = await this.mdService.SaveNewQueryModel(queryModel);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        ///  指定查询模型中是否有表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("IsExistChildOfView")]
        public async Task<bool> IsExistChildOfView(string queryModelID)
        {
            var ret = await this.mdService.IsExistChildOfView(queryModelID);
            return ret;
        }

        #endregion


        #region  md_viewtable相关操作

        /// <summary> 保存主表定义信息
        /// </summary>
        /// <param name="viewTable"></param>
        /// <returns></returns>
        [HttpPost("SaveViewMainTable")]
        public async Task<bool> SaveViewMainTable(MD_ViewTable viewTable)
        {
            var ret = await this.mdService.SaveViewMainTable(viewTable);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }


        /// <summary>
        ///  保存查询模型副表定义
        /// </summary>
        /// <param name="viewTable"></param>
        /// <returns></returns>
        [HttpPost("SaveViewChildTable")]
        public async Task<bool> SaveViewChildTable(MD_ViewTable viewTable)
        {
            var ret = await this.mdService.SaveViewChildTable(viewTable);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 向查询模型里添加子表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <param name="mainTableID"></param>
        /// <param name="selectedTable"></param>
        /// <returns></returns>
        [HttpPost("AddChildTableToQueryModel")]
        public async Task<bool> AddChildTableToQueryModel(string queryModelID, string mainTableID, MD_Table selectedTable)
        {
            var ret = await this.mdService.AddChildTableToQueryModel(queryModelID, mainTableID, selectedTable);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;

        }

        /// <summary>
        /// 向查询模型里添加主表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <param name="selectedTable"></param>
        /// <returns></returns>
        [HttpPost("AddMainTableToQueryModel")]
        public async Task<bool> AddMainTableToQueryModel(string queryModelID, MD_Table selectedTable)
        {
            var ret = await this.mdService.AddMainTableToQueryModel(queryModelID, selectedTable);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 删除查询模型中的表
        /// </summary>
        /// <param name="viewTableID"></param>
        /// <returns></returns>
        [HttpPost("DelViewTable")]
        public async Task<bool> DelViewTable(string viewTableID)
        {
            var ret = await this.mdService.DelViewTable(viewTableID);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 取查询模型主表的子表
        /// </summary>
        /// <param name="QueryModelID"></param>
        /// <returns></returns>
        [HttpPost("GetChildTableOfQueryModel")]
        public async Task<IList<MD_ViewTable>> GetChildTableOfQueryModel(string QueryModelID)
        {
            var ret = await this.mdService.GetChildTableOfQueryModel(QueryModelID);
            return ret;
        }

        /// <summary>
        /// 指定的表是否有子表
        /// </summary>
        /// <param name="viewTableID"></param>
        /// <returns></returns>
        [HttpPost("IsExistChildTable")]
        public async Task<bool> IsExistChildTable(string viewTableID)
        {
            var ret = await this.mdService.IsExistChildTable(viewTableID);
            return ret;
        }

        /// <summary>
        /// 是否存在使用此表的查询模型
        /// </summary>
        /// <param name="tableID"></param>
        /// <returns></returns>
        [HttpPost("IsExistViewUsedTable")]
        public async Task<bool> IsExistViewUsedTable(string tableID)
        {
            var ret = await this.mdService.IsExistViewUsedTable(tableID);
            return ret;
        }

        /// <summary>
        /// 保存查询模型子表的顺序
        /// </summary>
        /// <param name="childTableOrder"></param>
        [HttpPost("SaveViewTableOrder_UserDefine")]
        public async Task<bool> SaveViewTableOrder_UserDefine(Dictionary<string, int> childTableOrder)
        {
            var ret = await this.mdService.SaveViewTableOrder_UserDefine(childTableOrder);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 保存用户定义的查询模型
        /// </summary>
        /// <param name="viewTableID"></param>
        /// <param name="displayString"></param>
        /// <param name="displayType"></param>
        /// <param name="tableColumnDefine"></param>
        /// <returns></returns>
        [HttpPost("SaveViewTable_UserDefine")]
        public async Task<bool> SaveViewTable_UserDefine(string viewTableID, string displayString, MDType_DisplayType displayType, List<MD_ViewTableColumn> tableColumnDefine)
        {
            var ret = await this.mdService.SaveViewTable_UserDefine(viewTableID, displayString, displayType, tableColumnDefine);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }


        #endregion


        #region  MD_TABLE2VIEW操作

        /// <summary>
        /// 添加表到查询模型的关联定义
        /// </summary>
        /// <param name="tableID"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        [HttpPost("AddTableRelationView")]
        public async Task<string> AddTableRelationView(string tableID, string modelName)
        {
            var ret = await this.mdService.AddTableRelationView(tableID, modelName);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 取表关联的查询模型的列表
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        [HttpPost("GetTable2ViewList")]
        public async Task<IList<MD_Table2View>> GetTable2ViewList(string tid)
        {
            var ret = await this.mdService.GetTable2ViewList(tid);
            return ret;
        }

        /// <summary>
        /// 删除查询模型及其子对象定义__new
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("DelViewAndChildren")]
        public async Task<bool> DelViewAndChildren(string queryModelID)
        {
            var ret = await this.mdService.DelViewAndChildren(queryModelID);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        #endregion


        #region  MD_VIEW2VIEW操作
        /// <summary>
        /// 取相关联模型分组信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="viewID"></param>
        /// <returns></returns>
        [HttpPost("GetView2ViewList")]
        public async Task<IList<MD_View2View>> GetView2ViewList(string groupID, string viewID)
        {
            var ret = await this.mdService.GetView2ViewList(groupID, viewID);
            return ret;
        }

        /// <summary>
        /// 更新关联的模型信息
        /// </summary>
        /// <param name="view2View"></param>
        /// <returns></returns>
        [HttpPost("SaveView2View")]
        public async Task<bool> SaveView2View(MD_View2View view2View)
        {
            var ret = await this.mdService.SaveView2View(view2View);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 添加相关联的模型信息
        /// </summary>
        /// <param name="viewID"></param>
        /// <param name="groupID"></param>
        /// <returns></returns>
        [HttpPost("AddView2View")]
        public async Task<string> AddView2View(string viewID, string groupID)
        {
            var ret = await this.mdService.AddView2View(viewID, groupID);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }

        /// <summary>
        /// 删除查询模型相关联的模型信息
        /// </summary>
        /// <param name="v2vid"></param>
        /// <returns></returns>
        [HttpPost("CMD_DelView2View")]
        public async Task<string> CMD_DelView2View(string v2vid)
        {
            var ret = await this.mdService.CMD_DelView2View(v2vid);
            MyDA_MetaDataQuery.QueryModelCache.Clear();
            return ret;
        }


        #endregion


        #region  md_view2viewgroup操作

        /// <summary>
        /// 新建相关联模型分组信息
        /// </summary>
        /// <param name="viewID"></param>
        /// <returns></returns>
        [HttpPost("AddView2ViewGroup")]
        public async Task<string> AddView2ViewGroup(string viewID)
        {
            var ret = await this.mdService.AddView2ViewGroup(viewID);
            return ret;
        }

        /// <summary>
        /// 删除关联模型分组信息
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        [HttpPost("DelView2ViewGroup")]
        public async Task<string> DelView2ViewGroup(string groupID)
        {
            var ret = await this.mdService.AddView2ViewGroup(groupID);
            return ret;
        }

        /// <summary>
        /// 取查询模型相关联模型分组信息
        /// </summary>
        /// <param name="viewID"></param>
        /// <returns></returns>
        [HttpPost("GetView2ViewGroupOfQueryModel")]
        public async Task<IList<MD_View2ViewGroup>> GetView2ViewGroupOfQueryModel(string viewID)
        {
            var ret = await this.mdService.GetView2ViewGroupOfQueryModel(viewID);
            return ret;
        }

        #endregion


        #region  md_view2gl相关操作

        /// <summary>
        /// 保存查询模型的关联指标定义
        /// </summary>
        /// <param name="v2gid"></param>
        /// <param name="viewId"></param>
        /// <param name="guideLineID"></param>
        /// <param name="param"></param>
        /// <param name="displayOrder"></param>
        /// <param name="displayTitle"></param>
        /// <returns></returns>
        [HttpPost("SaveView2GL")]
        public async Task<bool> SaveView2GL(string v2gid, string viewId, string guideLineID, string param, int displayOrder, string displayTitle)
        {
            var ret = await this.mdService.SaveView2GL(v2gid, viewId, guideLineID, param, displayOrder, displayTitle);
            return ret;

        }
        /// <summary>
        /// 删除查询模型的关联指标扩展信息
        /// </summary>
        /// <param name="view2GL"></param>
        /// <returns></returns>
        [HttpPost("CMD_DelView2GL")]
        public async Task<string> CMD_DelView2GL(MD_View_GuideLine view2GL)
        {
            var ret = await this.mdService.CMD_DelView2GL(view2GL);
            return ret;
        }

        /// <summary>
        /// 取查询模型的关联指标列表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("GetView2GuideLineList")]
        public async Task<IList<MD_View_GuideLine>> GetView2GuideLineList(string queryModelID)
        {
            var ret = await this.mdService.GetView2GuideLineList(queryModelID);
            return ret;
        }

        #endregion

        #region  md_reftablelist代码表相关操作

        /// <summary>
        ///  保存代码表内容
        /// </summary>
        /// <param name="model"></param>        
        /// <returns></returns>
        [HttpPost("SaveRefTable")]
        public async Task<bool> SaveRefTable(SaveRefTableModel model)
        {
            var ret = await this.mdService.SaveRefTable(model.RefTable, model.RefData);
            return ret;
        }

        /// <summary>
        /// 删除代码表
        /// </summary>
        /// <param name="refTableID"></param>
        /// <returns></returns>
        [HttpPost("DelRefTable")]
        public async Task<bool> DelRefTable(string refTableID)
        {
            var ret = await this.mdService.DelRefTable(refTableID);
            return ret;
        }

        /// <summary>
        /// 通过名称获取代码表
        /// </summary>
        /// <param name="refTableName"></param>
        /// <returns></returns>
        [HttpPost("GetRefTable")]
        public async Task<MD_RefTable> GetRefTable(string refTableName)
        {
            var ret = await this.mdService.GetRefTable(refTableName);
            return ret;
        }

        /// <summary>
        /// 取命名空间下的代码表
        /// </summary>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        [HttpPost("GetRefTableAtNamespace")]
        public async Task<IList<MD_RefTable>> GetRefTableAtNamespace(string nameSpace)
        {
            var ret = await this.mdService.GetRefTableAtNamespace(nameSpace);
            return ret;

        }
        /// <summary>
        /// 导入代码表表定义
        /// </summary>
        /// <param name="refTable"></param>
        /// <returns></returns>
        [HttpPost("ImportRefTableDefine")]
        public async Task<bool> ImportRefTableDefine(MD_RefTable refTable)
        {
            var ret = await this.mdService.ImportRefTableDefine(refTable);
            return ret;
        }

        /// <summary>
        /// 保存新的代码表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("SaveNewRefTable")]
        public async Task<bool> SaveNewRefTable(SaveTableModel model)
        {
            var ret = await this.mdService.SaveNewRefTable(model.TableMeta, model.NameSpace);
            return ret;

        }

        /// <summary>
        /// 创建新的代码表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Namespace"></param>
        /// <param name="DWDM"></param>
        /// <returns></returns>
        [HttpPost("CreateNewRefTable")]
        public async Task<bool> CreateNewRefTable(string TableName, string Namespace,string DWDM)
        {
            var ret = await this.mdService.CreateNewRefTable(TableName, Namespace,DWDM);
            return ret;
        }

        /// <summary>
        /// 取代码表列__NEW
        /// </summary>
        /// <param name="refTableName"></param>
        /// <returns></returns>
        [HttpPost("Get_RefTableColumn")]
        public async Task<DataTable> Get_RefTableColumn(string refTableName)
        {
            var ret = await this.mdService.Get_RefTableColumn(refTableName);
            return ret;
        }

        /// <summary>
        ///  取数据库中的代码表
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDBTableListOfDMB")]
        public async Task<IList<DB_TableMeta>> GetDBTableListOfDMB()
        {
            var ret = await this.mdService.GetDBTableListOfDMB();
            return ret;
        }

        /// <summary>
        /// 刷新代码表中的AllFatherCode
        /// </summary>
        /// <returns></returns>
        [HttpPost("RefreshAllFatherCode")]
        public async Task<bool> RefreshAllFatherCode()
        {
            var ret = await this.mdService.RefreshAllFatherCode();
            return ret;
        }

        /// <summary>
        /// 刷新AreaMap表
        /// </summary>
        /// <returns></returns>
        [HttpPost("RefreshAreaMap")]
        public async Task<bool> RefreshAreaMap()
        {
            var ret = await this.mdService.RefreshAreaMap();
            return ret;
        }
        #endregion


        #region   md_view_exright操作

        /// <summary>
        /// 修改查询模型相关联的模型扩展权限信息
        /// </summary>
        /// <param name="exRight"></param>
        /// <returns></returns>
        [HttpPost("SaveQueryModelExRight")]
        public async Task<bool> SaveQueryModelExRight(MD_QueryModel_ExRight exRight)
        {
            var ret = await this.mdService.SaveQueryModelExRight(exRight);
            return ret;
        }

        /// <summary>
        /// 新建查询模型相关联的模型扩展权限信息
        /// </summary>
        /// <param name="rightValue"></param>
        /// <param name="rightTitle"></param>
        /// <param name="viewID"></param>
        /// <param name="fatherRight"></param>
        /// <returns></returns>
        [HttpPost("AddNewViewExRight")]
        public async Task<bool> AddNewViewExRight(string rightValue, string rightTitle, string viewID, MD_QueryModel_ExRight fatherRight)
        {
            var ret = await this.mdService.AddNewViewExRight(rightValue, rightTitle, viewID, fatherRight);
            return ret;
        }

        /// <summary>
        /// 删除查询模型相关联的模型扩展权限信息
        /// </summary>
        /// <param name="exRight"></param>
        /// <returns></returns>
        [HttpPost("CMD_DelViewExRight")]
        public async Task<string> CMD_DelViewExRight(MD_QueryModel_ExRight exRight)
        {
            var ret = await this.mdService.CMD_DelViewExRight(exRight);
            return ret;

        }

        /// <summary>
        /// 在取查询模型相关联的模型扩展权限信息
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <param name="fatherID"></param>
        /// <returns></returns>
        [HttpPost("GetQueryModelExRights")]
        public async Task<IList<MD_QueryModel_ExRight>> GetQueryModelExRights(string queryModelID, string fatherID)
        {
            var ret = await this.mdService.GetQueryModelExRights(queryModelID, fatherID);
            return ret;
        }

        #endregion


        #region  guideline相关操作
        /// <summary>
        /// 取指标分组列表
        /// </summary>
        /// <param name="nodeCode"></param>
        /// <param name="guideLineGroupType"></param>
        /// <returns></returns>
        [HttpPost("GetGuideLineGroup")]
        public async Task<IList<MD_GuideLineGroup>> GetGuideLineGroup(string nodeCode, string guideLineGroupType)
        {
            var ret = await this.mdService.GetGuideLineGroup(nodeCode, guideLineGroupType);
            return ret;
        }

        /// <summary>
        /// 取指标分组下的指标定义
        /// </summary>
        /// <param name="GroupName">分组名称</param>
        /// <returns></returns>
        [HttpPost("GetGuideLineOfGroup")]
        public async Task<IList<MD_GuideLine>> GetGuideLineOfGroup(string GroupName)
        {
            var ret = await this.mdService.GetGuideLineOfGroup(GroupName);
            return ret;
        }

        /// <summary>
        /// 取父指标下的所有子指标
        /// </summary>
        /// <param name="FatherGuildLineID"></param>
        /// <returns></returns>
        [HttpPost("GetChildGuideLines")]
        public async Task<IList<MD_GuideLine>> GetChildGuideLines(string FatherGuildLineID)
        {
            var ret = await this.mdService.GetChildGuideLines(FatherGuildLineID);
            return ret;
        }

        /// <summary>
        /// 判断是否已经有此指标主题
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        [HttpPost("IsExistGuideLineGroupName")]
        public async Task<bool> IsExistGuideLineGroupName(string GuideLineGroupName)
        {
            var ret = await this.mdService.IsExistGuideLineGroupName(GuideLineGroupName);
            return ret;
        }

        /// <summary>
        ///  保存新的指标主题定义
        /// </summary>
        /// <param name="GuideLineGroup"></param>
        /// <returns></returns>
        [HttpPost("SaveNewGuideLineGroupDefine")]
        public async Task<bool> SaveNewGuideLineGroupDefine(MD_GuideLineGroup GuideLineGroup)
        {
            var ret = await this.mdService.SaveNewGuideLineGroupDefine(GuideLineGroup);
            return ret;
        }



        /// <summary>
        /// 保存指标主题 定义
        /// </summary>
        /// <param name="GuideLineGroup"></param>
        /// <returns></returns>
        [HttpPost("SaveGuideLineGroupDefine")]
        public async Task<bool> SaveGuideLineGroupDefine(MD_GuideLineGroup GuideLineGroup)
        {
            var ret = await this.mdService.SaveGuideLineGroupDefine(GuideLineGroup);
            return ret;
        }

        /// <summary>
        /// 插入新的指标
        /// </summary>
        /// <param name="GuideLineName"></param>
        /// <param name="FatherID"></param>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        [HttpPost("InsertNewGuideLine")]
        public async Task<bool> InsertNewGuideLine(string GuideLineName, decimal FatherID, string GuideLineGroupName)
        {
            var ret = await this.mdService.SaveNewGuideLine(GuideLineName, FatherID, GuideLineGroupName);
            return ret;
        }

        /// <summary>
        /// 保存新指标
        /// </summary>
        /// <param name="GuideLine"></param>
        /// <returns></returns>
        [HttpPost("SaveNewGuideLine")]
        public async Task<bool> SaveNewGuideLine(MD_GuideLine GuideLine)
        {
            var ret = await this.mdService.SaveNewGuideLine(GuideLine);
            return ret;
        }

        /// <summary>
        /// 保存指标定义
        /// </summary>
        /// <param name="GuideLine"></param>
        /// <returns></returns>
        [HttpPost("SaveGuideLine")]
        public async Task<bool> SaveGuideLine(MD_GuideLine GuideLine)
        {
            var ret = await this.mdService.SaveGuideLine(GuideLine);
            return ret;
        }

        /// <summary>
        /// 是否存在指定ID的指标
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        [HttpPost("IsExistGuideLineID")]
        public async Task<bool> IsExistGuideLineID(string GuideLineID)
        {
            var ret = await this.mdService.IsExistGuideLineID(GuideLineID);
            return ret;
        }
        /// <summary>
        /// 查询指标下是否有子指标
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        [HttpPost("IsExistChildOfGuideLine")]
        public async Task<bool> IsExistChildOfGuideLine(string GuideLineID)
        {
            var ret = await this.mdService.IsExistChildOfGuideLine(GuideLineID);
            return ret;
        }
        /// <summary>
        /// 删除指标主题
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        [HttpPost("DelGuideLineGroup")]
        public async Task<bool> DelGuideLineGroup(string GuideLineGroupName)
        {
            var ret = await this.mdService.DelGuideLineGroup(GuideLineGroupName);
            return ret;
        }

        /// <summary>
        /// 删除指定指标
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        [HttpPost("DelGuideLine")]
        public async Task<bool> DelGuideLine(string GuideLineID)
        {
            var ret = await this.mdService.DelGuideLine(GuideLineID);
            return ret;
        }
        /// <summary>
        /// 判断一个指标主题是否有子指标
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        [HttpPost("IsExistChildOfGuideLineGroup")]
        public async Task<bool> IsExistChildOfGuideLineGroup(string GuideLineGroupName)
        {
            var ret = await this.mdService.IsExistChildOfGuideLineGroup(GuideLineGroupName);
            return ret;
        }
        #endregion


        #region  MD_View2App 相关操作

        /// <summary>
        /// 获取查询模型的集成应用展示定义
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("GetView2ApplicationList")]
        public async Task<List<MD_View2App>> GetView2ApplicationList(string queryModelID)
        {
            var ret = await this.mdService.GetView2ApplicationList(queryModelID);
            return ret;
        }

        /// <summary>
        /// 删除查询模型的集成应用展示定义
        /// </summary>
        /// <param name="v2AID"></param>
        /// <returns></returns>
        [HttpPost("CMD_DelView2App")]
        public async Task<string> CMD_DelView2App(string v2AID)
        {
            var ret = await this.mdService.CMD_DelView2App(v2AID);
            return ret;
        }

        /// <summary>
        /// 保存查询模型的集成应用展示定义
        /// </summary>
        /// <param name="v2AID"></param>
        /// <param name="view2AppData"></param>
        /// <returns></returns>
        [HttpPost("SaveView2App")]
        public async Task<bool> SaveView2App(string v2AID, MD_View2App view2AppData)
        {
            var ret = await this.mdService.SaveView2App(v2AID, view2AppData);
            return ret;

        }

        /// <summary>
        /// 清空查询模型的集成应用展示定义
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        [HttpPost("CMD_ClearView2App")]
        public async Task<string> CMD_ClearView2App(string queryModelID)
        {
            var ret = await this.mdService.CMD_ClearView2App(queryModelID);
            return ret;
        }



        #endregion



        /// <summary>
        /// 判断某个表中是否存在ID值
        /// </summary>
        /// <param name="oldid"></param>
        /// <param name="tname"></param>
        /// <param name="colname"></param>
        /// <returns></returns>
        [HttpPost("IsExistID")]
        public async Task<bool> IsExistID(string oldid, string tname, string colname)
        {
            var ret = await this.mdService.IsExistID(oldid, tname, colname);
            return ret;

        }





    }
}
