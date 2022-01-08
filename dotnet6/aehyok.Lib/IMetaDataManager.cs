using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Lib
{
    /// <summary>
    /// 元数据管理接口
    /// 
    /// Create by: Lintx
    /// </summary>
    public interface IMetaDataManager
    {
        /// <summary>
        /// 通过名称获取查询模型
        /// </summary>
        /// <param name="modelName">查询模型名称</param>
        /// <returns></returns>
        Task<MD_QueryModel> GetQueryModelByName(string modelName);

        /// <summary>
        /// 通过命名空间.名称获取查询模型
        /// </summary>
        /// <param name="modelName">查询模型名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <returns></returns>
        Task<MD_QueryModel> GetQueryModelByName(string modelName, string nameSpace);

        //MD_QueryModelGroup GetQueryModelGroup(string queryModelGroupID);

        //MD_QueryModelGroup GetQueryModelGroup(string queryModelGroupID, string nameSpace);

        /// <summary>
        /// 通过名称获取代码表
        /// </summary>
        /// <param name="refTableName">代码表名称</param>
        /// <returns></returns>
        Task<MD_RefTable> GetRefTable(string refTableName);

        /// <summary>
        /// 通过命名空间.名称获取代码表
        /// </summary>
        /// <param name="refTableName">代码表名称</param>
        /// <param name="nameSpace">命名空间</param>
        /// <returns></returns>
        Task<MD_RefTable> GetRefTable(string refTableName, string nameSpace);

        /// <summary>
        /// 通过ID获取查询模型
        /// </summary>
        /// <param name="modelID">查询模型ID</param>
        /// <returns></returns>
        Task<MD_QueryModel> GetQueryModelByID(string modelID);

        /// <summary>
        /// 通过命名空间.ID获取查询模型
        /// </summary>
        /// <param name="modelID">查询模型ID</param>
        /// <param name="nameSpace">命名空间</param>
        /// <returns></returns>
        Task<MD_QueryModel> GetQueryModelByID(string modelID, string nameSpace);

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <returns></returns>
        Task<IList<MD_Nodes>> GetNodeList();

        /// <summary>
        /// 获取节点下的命名空间
        /// </summary>
        /// <param name="_nodeDWDM"></param>
        /// <returns></returns>
        Task<IList<MD_Namespace>> GetNameSpaceAtNode(string _nodeDWDM);

        /// <summary>
        /// 保存新的命名空间
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<bool> SaveNewNameSapce(MD_Namespace _ns);

        /// <summary>
        /// 保存修改的节点信息
        /// </summary>
        /// <param name="_nodes"></param>
        Task<bool> SaveNodes(MD_Nodes _nodes);

        /// <summary>
        /// 保存新的节点信息
        /// </summary>
        /// <param name="_nodes"></param>
        /// <returns></returns>
        Task<bool> SaveNewNodes(MD_Nodes _nodes);

        /// <summary>
        /// 删除节点信息
        /// </summary>
        /// <param name="_nodeID"></param>
        /// <returns></returns>
        Task<bool> DelNodes(string _nodeID);

        /// <summary>
        /// 保存命名空间信息
        /// </summary>
        /// <param name="_Namespace"></param>
        /// <returns></returns>
        Task<bool> SaveNameSapce(MD_Namespace _Namespace);

        /// <summary>
        /// 删除命名空间
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<bool> DelNamespace(MD_Namespace _ns);

        /// <summary>
        /// 取数据库中的表的列表
        /// </summary>
        /// <returns></returns>
        Task<IList<DB_TableMeta>> GetDBTableList();


        /// <summary>
        /// 取数据库中的代码表
        /// </summary>
        /// <returns></returns>
        Task<IList<DB_TableMeta>> GetDBTableListOfDMB();

        /// <summary>
        /// 从数据库中取表的字段定义
        /// </summary>
        /// <param name="_tableName"></param>
        /// <returns></returns>
        Task<IList<DB_ColumnMeta>> GetDBColumnsOfTable(string _tableName);

        /// <summary>
        /// 将新的表存入元数据
        /// </summary>
        /// <param name="_tm"></param>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<bool> SaveNewTable(DB_TableMeta _tm, MD_Namespace _ns);

        /// <summary>
        /// 取命名空间下的表的列表
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<IList<MD_Table>> GetTablesAtNamespace(string _ns);

        /// <summary>
        /// 取指定表ID的表定义
        /// </summary>
        /// <param name="_tid"></param>
        /// <returns></returns>
        Task<MD_Table> GetTableByTableID(string _tid);

        /// <summary>
        /// 取命名空间下的查询模型
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<IList<MD_QueryModel>> GetQueryModelAtNamespace(string _ns);

        /// <summary>
        /// 取命名空间下的代码表
        /// </summary>
        /// <param name="_ns"></param>
        /// <returns></returns>
        Task<IList<MD_RefTable>> GetRefTableAtNamespace(MD_Namespace _ns);
        Task<IList<MD_RefTable>> GetRefTableAtNamespace(string _ns);

        /// <summary>
        /// 取表的列定义
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<IList<MD_TableColumn>> GetColumnsOfTable(string _tid);

        /// <summary>
        /// 取查询模型的主表
        /// </summary>
        /// <param name="queryModelID"></param>
        /// <returns></returns>
        Task<MD_ViewTable> GetMainTableOfQueryModel(string queryModelID);

        /// <summary>
        /// 取最新的序列产生器的值
        /// </summary>
        /// <returns></returns>
        string GetNewID();

        /// <summary>
        /// 保存表定义
        /// </summary>
        /// <param name="_table"></param>
        /// <returns></returns>
        Task<bool> SaveTableDefine(MD_Table _table);

        /// <summary>
        /// 保存新的查询模型
        /// </summary>
        /// <param name="_queryModel"></param>
        Task<bool> SaveNewQueryModel(MD_QueryModel _queryModel);

        /// <summary>
        /// 保存查询模型定义
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        Task<bool> SaveQueryModel(MD_QueryModel _queryModel);

        /// <summary>
        ///　保存查询模型主表定义
        /// </summary>
        /// <param name="_viewtable"></param>
        /// <returns></returns>
        Task<bool> SaveViewMainTable(MD_ViewTable _viewtable);

        /// <summary>
        /// 取查询模型主表的子表
        /// </summary>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        Task<IList<MD_ViewTable>> GetChildTableOfQueryModel(MD_QueryModel _queryModel);
        Task<IList<MD_ViewTable>> GetChildTableOfQueryModel(string QueryModelID);

        /// <summary>
        /// 保存查询模型副表定义
        /// </summary>
        /// <param name="_viewtable"></param>
        /// <returns></returns>
        Task<bool> SaveViewChildTable(MD_ViewTable _viewtable);


        /// <summary>
        /// 向查询模型里添加主表
        /// </summary>
        /// <param name="p"></param>
        /// <param name="_selectedTable"></param>
        /// <returns></returns>
        Task<bool> AddMainTableToQueryModel(string _queryModelID, MD_Table _selectedTable);

        /// <summary>
        /// 向查询模型里添加子表
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <param name="_mainTableID"></param>
        /// <param name="_selectedTable"></param>
        /// <returns></returns>
        Task<bool> AddChildTableToQueryModel(string _queryModelID, string _mainTableID, MD_Table _selectedTable);

        /// <summary>
        /// 指定的表是否有子表
        /// </summary>
        /// <param name="_viewTableID"></param>
        /// <returns></returns>
        Task<bool> IsExistChildTable(string _viewTableID);

        /// <summary>
        /// 删除查询模型中的表
        /// </summary>
        /// <param name="_viewTableID"></param>
        /// <returns></returns>
        Task<bool> DelViewTable(string _viewTableID);

        /// <summary>
        /// 指定查询模型中是否有表
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <returns></returns>
        Task<bool> IsExistChildOfView(string _queryModelID);

        /// <summary>
        /// 删除查询模型
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <returns></returns>
        Task<bool> DelViewMeta(string _queryModelID);

        /// <summary>
        /// 删除查询模型及其主表子表等子定义
        /// </summary>
        /// <param name="QueryModelID"></param>
        /// <returns></returns>
        Task<bool> DelViewAndChildren(string QueryModelID);

        /// <summary>
        /// 是否存在使用此表的查询模型
        /// </summary>
        /// <param name="_tableID"></param>
        /// <returns></returns>
        Task<bool> IsExistViewUsedTable(string _tableID);

        /// <summary>
        /// 删除指定的数据表
        /// </summary>
        /// <param name="_tableID"></param>
        /// <returns></returns>
        Task<bool> DelTableMeta(string _tableID);

        /// <summary>
        /// 保存新的代码表
        /// </summary>
        /// <param name="_tm"></param>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        Task<bool> SaveNewRefTable(DB_TableMeta _tm, MD_Namespace _namespace);

        /// <summary>
        /// 创建新的代码表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Namespace"></param>
        /// <param name="DWDM"></param>
        /// <returns></returns>
        Task<bool> CreateNewRefTable(string TableName, string Namespace,string DWDM);
        
        /// <summary>
        /// 取代码表中的记录
        /// </summary>
        /// <param name="_refTable"></param>
        /// <returns></returns>
        Task<DataTable> Get_RefTableColumn(string _refTable);

        /// <summary>
        /// 保存代码表内容
        /// </summary>
        /// <param name="_refTable"></param>
        /// <param name="_refData"></param>
        /// <returns></returns>
        Task<bool> SaveRefTable(MD_RefTable _refTable, DataTable _refData);

        /// <summary>
        /// 取指定节点下的一级菜单
        /// </summary>
        /// <param name="_node"></param>
        /// <returns></returns>
        IList<MD_Menu> GetMenuDefineOfNode(string _nodeCode);

        /// <summary>
        /// 取指定菜单项下的子菜单
        /// </summary>
        /// <param name="_fmenuID"></param>
        /// <returns></returns>
        IList<MD_Menu> GetSubMenuDefine(string _fmenuID);

        /// <summary>
        /// 保存菜单定义
        /// </summary>
        /// <param name="_menu"></param>
        /// <returns></returns>
        bool SaveMenuDefine(MD_Menu _menu);

        /// <summary>
        /// 添加系统菜单
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool AddSystemMenu(string _nodeCode);


        /// <summary>
        /// 添加子菜单
        /// </summary>
        /// <param name="_fatherMenuID"></param>
        /// <param name="_nodeID"></param>
        /// <returns></returns>
        bool AddSystemSubMenu(string _fatherMenuID, string _nodeID);


        /// <summary>
        /// 删除系统菜单
        /// </summary>
        /// <param name="_menuid"></param>
        /// <returns></returns>
        bool DelSystemMenu(string _menuid);

        /// <summary>
        /// 取指定节点下的指标定义组
        /// </summary>
        /// <param name="_nodeCode">节点名称</param>
        /// <param name="_guideLineGroupType">组类型</param>
        /// <returns></returns>
        Task<IList<MD_GuideLineGroup>> GetGuideLineGroup(string _nodeCode, string _guideLineGroupType);

        /// <summary>
        /// 取指定指标组下的指标列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        Task<IList<MD_GuideLine>> GetGuideLineOfGroup(string _groupName);

        /// <summary>
        /// 保存指标组的定义
        /// </summary>
        /// <param name="mD_GuideLineGroup"></param>
        /// <returns></returns>
        Task<bool> SaveGuideLineGroupDefine(MD_GuideLineGroup GuideLineGroup);
        /// <summary>
        /// 取指定指标下的子指标列表
        /// </summary>
        /// <param name="_fatherGuildLineID"></param>
        /// <returns></returns>
        Task<IList<MD_GuideLine>> GetChildGuideLines(string _fatherGuildLineID);

        /// <summary>
        /// 删除指标组
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        Task<bool> DelGuideLineGroup(string GuideLineGroupName);
        /// <summary>
        /// 取指标组下的指标个数
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        Task<bool> IsExistChildOfGuideLineGroup(string GuideLineGroupName);
        /// <summary>
        /// 保存新的指标组定义
        /// </summary>
        /// <param name="GuideLineGroup"></param>
        /// <returns></returns>
        Task<bool> SaveNewGuideLineGroupDefine(MD_GuideLineGroup GuideLineGroup);
        /// <summary>
        /// 系统中是否存在指定名称的指标组
        /// </summary>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        Task<bool> IsExistGuideLineGroupName(string GuideLineGroupName);

        /// <summary>
        /// 保存新的指标
        /// </summary>
        /// <param name="GuideLineName"></param>
        /// <param name="FatherID"></param>
        /// <param name="GuideLineGroupName"></param>
        /// <returns></returns>
        Task<bool> SaveNewGuideLine(string GuideLineName, decimal FatherID, string GuideLineGroupName);


        /// <summary>
        /// 保存新的指标定义
        /// </summary>
        /// <param name="_guideLine"></param>
        Task<bool> SaveNewGuideLine(MD_GuideLine _guideLine);

        /// <summary>
        /// 指定指标下是否包含子指标
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        Task<bool> IsExistChildOfGuideLine(string GuideLineID);

        /// <summary>
        /// 删除指定指标
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <returns></returns>
        Task<bool> DelGuideLine(string GuideLineID);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="GuideLine"></param>
        /// <returns></returns>
        Task<bool> SaveGuideLine(MD_GuideLine GuideLine);

        /// <summary>
        /// 取概念组
        /// </summary>
        /// <returns></returns>
        IList<MD_ConceptGroup> GetConceptGroups();

        /// <summary>
        /// 取概念组下的所有概念标签
        /// </summary>
        /// <param name="_groupName"></param>
        /// <returns></returns>
        IList<MD_ConceptItem> GetSubConceptTagDefine(string _groupName);
        /// <summary>
        /// 是否存在指定名称概念组
        /// </summary>
        /// <param name="_groupName"></param>
        /// <returns></returns>
        bool IsExistConceptGroup(string _groupName);


        /// <summary>
        /// 保存概念组
        /// </summary>
        /// <param name="_ConceptGroup"></param>
        /// <returns></returns>
        bool SaveConceptGroup(MD_ConceptGroup _ConceptGroup);

        /// <summary>
        /// 添加新的概念组
        /// </summary>
        /// <param name="_groupName"></param>
        /// <returns></returns>
        bool AddNewConceptGroup(string _groupName);

        /// <summary>
        /// 删除概念组
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool DelConcpetGroup(string p);

        /// <summary>
        /// 概念组是否有子内容
        /// </summary>
        /// <param name="_groupName"></param>
        /// <returns></returns>
        bool IsExistChildOfConceptGroup(string _groupName);

        /// <summary>
        /// 是否存在概念标签
        /// </summary>
        /// <param name="_TagName"></param>
        /// <returns></returns>
        bool IsExistConceptTag(string _TagName);

        /// <summary>
        /// 添加新的概念标签
        /// </summary>
        /// <param name="_TagName"></param>
        /// <param name="_description"></param>
        /// <param name="_groupName"></param>
        /// <returns></returns>
        bool AddNewConceptTag(string _TagName, string _description, string _groupName);

        /// <summary>
        /// 保存标签定义
        /// </summary>
        /// <param name="mD_ConceptItem"></param>
        /// <returns></returns>
        bool SaveConceptTag(MD_ConceptItem mD_ConceptItem);
        /// <summary>
        /// 删除概念标签
        /// </summary>
        /// <param name="_CTag"></param>
        /// <returns></returns>
        bool DelConceptTag(string _CTag);

        /// <summary>
        /// 取指定指标ID的指标定义
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        MD_GuideLine GetGuideLineDefine(string _guideLineID);

        

        /// <summary>
        /// 判断指定指标ID的指标是否存在
        /// </summary>
        /// <param name="GuideLineID"></param>
        /// <returns></returns>
        Task<bool> IsExistGuideLineID(string GuideLineID);

        /// <summary>
        /// 取所有的权限定义
        /// </summary>
        /// <returns></returns>
        IList<MD_RightDefine> GetRightData();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SystemID"></param>
        /// <returns></returns>
        IList<MD_RightDefine> GetRightData(string SystemID);
        /// <summary>
        /// 保存权限定义
        /// </summary>
        /// <param name="_ret"></param>
        /// <returns></returns>
        bool SaveRightDefine(IList<MD_RightDefine> _rightList);

        /// <summary>
        /// 导入表定义
        /// </summary>
        /// <param name="_tableDefine"></param>
        Task<bool> ImportTableDefine(MD_Table _tableDefine);


        /// <summary>
        /// 导入代码表定义
        /// </summary>
        /// <param name="_rt"></param>
        /// <returns></returns>
        Task<bool> ImportRefTableDefine(MD_RefTable _rt);

        /// <summary>
        /// 导入查询模型
        /// </summary>
        /// <param name="_qv"></param>
        /// <returns></returns>
        Task<bool> ImportQueryModelDefine(MD_QueryModel _qv);

        /// <summary>
        /// 添加表到查询模型的关联定义
        /// </summary>
        /// <param name="_tableID"></param>
        /// <param name="_modelName"></param>
        /// <returns></returns>
        Task<string> AddTableRelationView(string _tableID, string _modelName);

        /// <summary>
        /// 取所有查询模型名称
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> GetAllQueryModelNames();

        /// <summary>
        /// 取表关联的查询模型的列表
        /// </summary>
        /// <param name="_tid"></param>
        /// <returns></returns>
        Task<IList<MD_Table2View>> GetTable2ViewList(string _tid);

        /// <summary>
        /// 保存查询模型用户定义信息
        /// </summary>
        /// <param name="_queryModelID"></param>
        /// <param name="_display"></param>
        /// <param name="_descript"></param>
        /// <returns></returns>
        Task<bool> SaveQueryModel_UserDefine(string _queryModelID, string _display, string _descript);

        Task<bool> SaveViewTable_UserDefine(string _viewTableID, string _displayString, MDType_DisplayType _displayType, List<MD_ViewTableColumn> TableColumnDefine);

        Task<bool> SaveViewTableOrder_UserDefine(Dictionary<string, int> ChildTableOrder);

        Task<IList<MD_InputModel>> GetInputModelOfNamespace(string _namespace);
        Task<MD_InputModel> GetInputModel(string _namespace, string ModelName);

        bool SaveNewInputModel(string _namespace, MD_InputModel SaveModel);

        bool DelInputModel(string InputModelID);

        bool SaveInputModel(MD_InputModel SaveModel);

        bool AddNewInputModelGroup(MD_InputModel_ColumnGroup Group);

        bool DelInputModelColumnGroup(string InputModelID, string GroupID);

        bool InputModel_MoveColumnToGroup(MD_InputModel_Column _col, MD_InputModel_ColumnGroup mD_InputModel_ColumnGroup);

        bool SaveInputModelColumnGroup(MD_InputModel_ColumnGroup _group);

        bool FindInputModelColumnByName(string InputModelID, string ColumnName);

        bool AddNewInputModelColumn(string InputModelID, string GroupID, string ColumnName);

        bool DelInputModelColumn(string ColumnID);

        bool OracleTableExist(string TableName);

        bool AddNewInputModelSavedTable(string InputModelID, string TableName);

        bool DelInputModelSavedTable(string TableID);

        bool SaveInputModelSaveTable(MD_InputModel_SaveTable _newTable);


        bool AddInputModelTableColumn(string TableName, string AddFieldName, string DataType);

        IList<string> GetDBPrimayKeyList(string TableName);

        bool DelInputModelTableColumn(string TableName, string DelFieldName);

        Task<bool> AddChildInputModel(string MainModelID, string ChildModelID);

        bool SaveInputModelChildDefine(MD_InputModel_Child InputModelChild);

        Task<bool> DelRefTable(string RefTableID);

        bool DelInputModelChild(string ChildModelID);

        Task<bool> IsExistID(string _oldid, string _tname, string _colname);

        Task<IList<MD_View2ViewGroup>> GetView2ViewGroupOfQueryModel(string ViewID);

        Task<string> AddView2ViewGroup(string ViewID);

        Task<bool> SaveView2ViewGroup(MD_View2ViewGroup View2ViewGroup);

        Task<IList<MD_View2View>> GetView2ViewList(string GroupID, string ViewID);

        Task<string> DelView2ViewGroup(string GroupID);

        Task<string> AddView2View(string ViewID, string GroupID);

        Task<bool> SaveView2View(MD_View2View View2View);

        Task<string> CMD_DelView2View(string v2vid);

        Task<IList<MD_QueryModel_ExRight>> GetQueryModelExRights(string QueryModelID, string FatherID);

        Task<bool> AddNewViewExRight(string RightValue, string RightTitle, string ViewID, MD_QueryModel_ExRight FatherRight);


        Task<bool> SaveQueryModelExRight(MD_QueryModel_ExRight mD_QueryModel_ExRight);

        Task<string> CMD_DelViewExRight(MD_QueryModel_ExRight ExRight);

        Task<IList<MD_View_GuideLine>> GetView2GuideLineList(string QueryModelID);

        Task<bool> SaveView2GL(string V2GID, string VIEWID, string GuideLineID, string Params, int DisplayOrder, string DisplayTitle);

        Task<string> CMD_DelView2GL(MD_View_GuideLine View2GL);

        Task<List<MD_View2App>> GetView2ApplicationList(string QueryModelID);

        Task<bool> SaveView2App(string V2AID, MD_View2App View2AppData);

        Task<string> CMD_DelView2App(string V2AID);

        Task<string> CMD_ClearView2App(string QueryModelID);

        Task<bool> RefreshAllFatherCode();

        Task<bool> RefreshAreaMap();
    }
}
