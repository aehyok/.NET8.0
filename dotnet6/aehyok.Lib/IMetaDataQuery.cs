using aehyok.Base;
using aehyok.Core.Data;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.Query;
using aehyok.Lib.MetaData.RefCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Lib
{
    /// <summary>
    /// 元数据查询接口
    /// </summary>
    public interface IMetaDataQuery: IDependency
    {
        /// <summary>
        /// 取查询模型定义
        /// </summary>
        /// <param name="queryModelName"></param>
        /// <returns></returns>
        Task<MD_QueryModel> GetMDQueryModelDefine(string queryModelName);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="mDQuery_Request"></param>
        /// <returns></returns>
        Task<DataSet> QueryData(MDQuery_Request mDQuery_Request);
        /// <summary>
        /// 个人分析空间查询
        /// </summary>
        /// <param name="mDQuery_Request"></param>
        /// <param name="_queryModel"></param>
        /// <returns></returns>
        Task<DataSet> PAQueryData(MDQuery_Request mDQuery_Request, MDModel_QueryModel _queryModel);
        /// <summary>
        /// 通过MAINID取查询模型中的主表记录
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_mainTableName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        Task<DataTable> GetMainTableDataByKey(string _queryModelName, string _mainTableName, string _keyid);
        /// <summary>
        /// 通过MAINID取查询模型中的子表记录
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_childTableName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        Task<DataTable> GetChildTableDataByKey(string _queryModelName, string _childTableName, string _keyid);
        /// <summary>
        /// 通过MAINID取查询模型中的子表记录总数
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_childTableName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        Task<int> GetChildTableCountByKey(string _queryModelName, string _childTableName, string _keyid);

        /// <summary>
        /// 取所有子表的记录数列表
        /// </summary>
        /// <param name="_queryModelName"></param>
        /// <param name="_keyid"></param>
        /// <returns></returns>
        Task<List<MDQuery_ChildTableRowCount>> GetChildRowCountList(string _queryModelName, string _keyid);

        /// <summary>
        /// 取所有的概念定义
        /// </summary>
        /// <returns></returns>
        Task<List<MD_ConceptGroup>> GetConceptList();

        /// <summary>
        /// 取指定名称列表的所有查询模型定义
        /// </summary>
        /// <param name="_queryModelNames"></param>
        /// <returns></returns>
        Task<List<MD_QueryModel>> GetQueryModels(string _queryModelNames);
        /// <summary>
        /// 取搜索字段列表
        /// </summary>
        /// <param name="_searchText"></param>
        /// <param name="_searchType"></param>
        /// <param name="_searchConceptGroup"></param>
        /// <param name="_queryModelName"></param>
        /// <returns></returns>
        Task<List<MDSearch_Column>> GetSearchResultColumn(string _searchText, int _searchType, string _searchConceptGroup, string _queryModelName);


        /// <summary>
        /// 取搜索到的数据记录索引
        /// </summary>
        /// <param name="_searchText"></param>
        /// <param name="_searchType"></param>
        /// <param name="_sc"></param>
        /// <returns></returns>
        Task<List<MDSearch_ResultDataIndex>> GetSearchResult(string _searchText, int _searchType, MDSearch_Column _sc);

        /// <summary>
        /// 通过子表的主键取主表的主键
        /// </summary>
        /// <param name="_childColumn"></param>
        /// <param name="_childKey"></param>
        /// <returns></returns>
        Task<string> GetMainTableKeyByChildKey(MDSearch_Column _childColumn, string _childKey);

        /// <summary>
        /// 通过查询模型的主表的一个字段的值取主键值
        /// </summary>
        /// <param name="_modelName"></param>
        /// <param name="_columnName"></param>
        /// <param name="_data"></param>
        /// <returns></returns>
        Task<string> GetMainTableKeyByColumnCondition(string _modelName, string _columnName, string _data);

        /// <summary>
        /// 取指定列表rootGuideLines中的根指标定义
        /// </summary>
        /// <param name="rootGuideLines"></param>
        /// <returns></returns>
        Task<List<MD_GuideLine>> GetRootGuideLineList(string rootGuideLines);
        /// <summary>
        /// 取指定的指标下的子指标列表
        /// </summary>
        /// <param name="FatherID"></param>
        /// <returns></returns>
        Task<List<MD_GuideLine>> GetGuideLineListByFatherID(string FatherID);

        /// <summary>
        /// 指标查询
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <param name="_param"></param>
        /// <returns></returns>
        Task<DataTable> QueryGuideLine(string _guideLineID, List<MDQuery_GuideLineParameter> _param);
        /// <summary>
        /// 用默认值查询指标
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <param name="_param"></param>
        /// <returns></returns>
        Task<DataTable> QueryGuideLineByDefault(string _guideLineID, List<MDQuery_GuideLineParameter> _param);

        /// <summary>
        /// 取指标查询的结果记录数
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <param name="_params"></param>
        /// <returns></returns>
        Task<int> QueryGuideLineResultCount(string _guideLineID, List<MDQuery_GuideLineParameter> _params);


        /// <summary>
        /// 通过GuideLineID取指标定义
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <returns></returns>
        Task<MD_GuideLine> GetGuideLineByID(string _guideLineID);

        /// <summary>
        /// 添加新的查询任务
        /// </summary>
        /// <param name="_taskName"></param>
        /// <param name="_queryRequest"></param>
        /// <returns>返回查询的ID号</returns>
        Task<string> AddNewQueryTask(string _taskName, MDQuery_Request _queryRequest);

        /// <summary>
        /// 取指标ID的查询任务请求内容
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<MDQuery_Request> GetQueryTaskRequestContext(string _taskID);

        /// <summary>
        /// 取当前用户的任务列表
        /// </summary>
        /// <returns></returns>
        Task<List<MDQuery_Task>> GetQueryTaskList();


        /// <summary>
        /// 取指定查询任务ID的当前状态
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<MDQuery_Task> GetQueryTaskStateByID(string _taskID);

        /// <summary>
        /// 清除任务ID
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<bool> ClearQueryTask(string _taskID);

        /// <summary>
        /// 锁定任务结果
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<bool> LockQueryTaskResult(string _taskID);

        /// <summary>
        /// 取消查询任务
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<bool> CancleQueryTask(string _taskID);

        /// <summary>
        /// 取任务查询结果(以DataTable序列化成XML)
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<DataSet> GetTaskQueryResult_DataSet(string _taskID);

        /// <summary>
        /// 取任务查询结果(以ORACLE序列化成XML)
        /// </summary>
        /// <param name="_taskID"></param>
        /// <returns></returns>
        Task<DataSet> GetTaskQueryResult_ORA(string _taskID);

        /// <summary>
        /// 取指定查询模型的数据审核规则
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <returns></returns>
        Task<List<MD_CheckRule>> GetDataCheckRuleDefine(string QueryModelName);

        /// <summary>
        /// 保存规则选定状态
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <param name="_ruleList"></param>
        void SaveCheckRuleState(string QueryModelName, List<MD_CheckRule> _ruleList);

        /// <summary>
        /// 保存查询
        /// </summary>
        /// <param name="SaveName">保存名称</param>
        /// <param name="IsPublic">是否公开</param>
        /// <param name="QueryRequest">查询请求</param>
        void SaveQuery(string SaveName, bool IsPublic, MDQuery_Request QueryRequest);

        /// <summary>
        /// 取保存的查询列表
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <returns></returns>
        Task<DataTable> GetSaveQueryList(string QueryModelName);

        /// <summary>
        /// 取保存的查询请求定义
        /// </summary>
        /// <param name="SaveQueryID"></param>
        /// <returns></returns>
        Task<MDQuery_Request> LoadQuery(string SaveQueryID);

        /// <summary>
        /// 通过查询模型取带维护序号的数据(数据审核用)
        /// </summary>
        /// <param name="_queryRequest"></param>
        /// <param name="_dwdm"></param>
        /// <returns></returns>
        Task<DataTable> QueryDataWithWHXH(MDQuery_Request _queryRequest, string _dwdm);

        /// <summary>
        /// 取录入模型定义
        /// </summary>
        /// <param name="_modelName"></param>
        /// <returns></returns>
        Task<MD_InputModel> GetInputModelByName(string _modelName);

        /// <summary>
        /// 删除保存的查询模型
        /// </summary>
        /// <param name="_savedID"></param>
        /// <returns></returns>
        Task<bool> DelSavedQuery(string _savedID);

        /// <summary>
        /// 取用户的单位级别 :   总署缉私局、直属缉私局、广东分署缉私局、缉私分局
        /// </summary>
        /// <returns></returns>
        Task<string> GetUserLevel();

        /// <summary>
        /// 取审核信息
        /// </summary>
        /// <param name="_modelName"></param>
        /// <param name="_mainKey"></param>
        /// <param name="SHID"></param>
        /// <param name="WHXH"></param>
        /// <returns></returns>
        Task<DataTable> GetDataCheckInfo(string _modelName, string _mainKey, ref string SHID);

        /// <summary>
        /// 取数据审核的记录ID
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <param name="_mainkeyId"></param>
        /// <param name="_level"></param>
        /// <param name="SHID"></param>
        /// <returns></returns>
        Task<string> GetDataCheckInfoJLID(string QueryModelName, string _mainkeyId, string _level, ref string SHID);

        /// <summary>
        /// 保存审核信息
        /// </summary>
        /// <param name="CurrentJLID"></param>
        /// <param name="CurrentLevel"></param>
        /// <param name="CurrentID"></param>
        /// <param name="_shjg"></param>
        /// <param name="_shr"></param>
        /// <param name="_xgyj"></param>
        /// <param name="WHXH"></param>
        /// <returns></returns>
        Task<string> SaveDataCheckResult(string CurrentJLID, string CurrentLevel, string CurrentID, string _shjg, string _shr, string _xgyj, string WHXH);


        /// <summary>
        /// 取维护序号
        /// </summary>
        /// <param name="_tableName"></param>
        /// <param name="_mainColumn"></param>
        /// <param name="_mainKey"></param>
        /// <returns></returns>
        Task<string> GetDataCheckWHXH(string _tableName, string _mainColumn, string _mainKey);


        /// <summary>
        /// 保存数据审核规则
        /// </summary>
        /// <param name="_ruleName"></param>
        /// <param name="_queryModelName"></param>
        /// <param name="_gzsf"></param>
        /// <returns></returns>
        Task<bool> SaveNewDataCheckRule(string _ruleName, string _queryModelName, string _gzsf);

        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="_ruleID"></param>
        /// <returns></returns>
        Task<bool> DelDataCheckRule(string _ruleID);

        /// <summary>
        /// 修改规则算法
        /// </summary>
        /// <param name="_ruleID"></param>
        /// <param name="_gzsf"></param>
        /// <returns></returns>
        Task<bool> ChangeDataCheckRule(string _ruleID, string _gzsf);

        /// <summary>
        /// 指定名称的个人分析空间是否存在
        /// </summary>
        /// <param name="PersonAnalizeSapceName"></param>
        /// <returns></returns>
        Task<bool> IsPASpaceExist(string PersonAnalizeSapceName);

        /// <summary>
        /// 建立指定名称的个人分析空间
        /// </summary>
        /// <param name="PersonAnalizeSapceName"></param>
        /// <returns></returns>
        Task<MD_PAnalizeProject> CreateNewPASpace(string PersonAnalizeSapceName);

        /// <summary>
        /// 取当前用户的个人分析空间列表
        /// </summary>
        /// <returns></returns>
        Task<List<MD_PAnalizeProject>> GetPAProjectOfUser();

        /// <summary>
        /// 将表保存入个人分析空间
        /// </summary>
        /// <param name="_PAProject"></param>
        /// <param name="_tableName"></param>                
        /// <param name="columnDefine"></param>
        /// <param name="_dt"></param>
        /// <returns></returns>
       // bool SaveDataToPAnalize(MD_PAnalizeProject _PAProject, string _tableName, List<MD_PATable_Column> columnDefine, DataTable _dt);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_id"></param>       
        /// <param name="_params"></param>
        /// <returns></returns>
        Task<DataTable> GetInputModelBlankData(string _initGuideLineID, string _getBlankGuideLineID, List<MDQuery_GuideLineParameter> _params);

        /// <summary>
        /// 保存录入模型的数据
        /// </summary>
        /// <param name="_inputModelName"></param>
        /// <param name="_changedData"></param>
        /// <returns></returns>
        Task<bool> SaveDataByInputModel(string _inputModelName, DataTable _changedData);

        /// <summary>
        /// 测试表达式是否正确
        /// </summary>
        /// <param name="ExpressionString"></param>
        /// <param name="TableDefine"></param>
        Task<MD_ComputeField> TestComputeExpress(string ExpressionString, MDModel_Table TableDefine);
        /// <summary>
        /// 保存计算字段定义
        /// </summary>
        /// <param name="DisplayName"></param>
        /// <param name="Description"></param>
        /// <param name="Expression"></param>
        /// <param name="QueryString"></param>
        /// <param name="TableName"></param>
        /// <param name="ModelName"></param>
        Task<bool> SaveComputeFieldDefine(string DisplayName, string Description, string Expression, string QueryString, string ResultDataType, string TableName, string ModelName);

        /// <summary>
        /// 取用户个人收藏的计算字段
        /// </summary>
        /// <param name="ModelName"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        Task<List<MDQuery_ComputeColumnDefine>> GetPersonSavedComputField(string ModelName, string TableName);

        /// <summary>
        /// 取函数定义列表
        /// </summary>
        /// <param name="_type">类型  0：计算函数  1.统计函数</param>
        /// <returns></returns>
        Task<List<MD_FUNCTION>> GetFunctionList(int _type);

        Task<MD_ComputeField> TestStatisticsExpress(string FunctionName,string TableName, MDModel_Table_Column TableColumn);

        /// <summary>
        /// 取是否可使用分析空间功能
        /// </summary>
        /// <returns>0：不可使用   1：可以使用</returns>
        Task<string> GetCanUsePanalizeSet();

        /// <summary>
        /// 取查询模型的说明
        /// </summary>
        /// <param name="_modelName"></param>
        /// <returns></returns>
        Task<string> GetQueryModelDescription(string _modelName);

        /// <summary>
        /// 取规则列表
        /// </summary>
        /// <param name="QueryModelName"></param>
        /// <returns></returns>
        Task<DataTable> GetRuleList(string QueryModelName);

        /// <summary>
        /// 导入总局的规则
        /// </summary>
        /// <param name="SrcRuleID"></param>
        void ImportRule(string SrcRuleID);

        /// <summary>
        /// 用总局的规则覆盖当前规则
        /// </summary>
        /// <param name="TargetRuleID"></param>
        /// <param name="SrcRuleID"></param>
        void RecoverRuleDefine(string TargetRuleID, string SrcRuleID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MainKeyID"></param>
        /// <returns></returns>
        Task<string> GetSjshInfo_HGJS(MDQuery_Request _queryRequest, string MainKeyID);
        /// <summary>
        /// 取审核 公告信息
        /// </summary>
        /// <returns></returns>
        Task<DataTable> GetDataCheckBoardList();

        /// <summary>
        /// 添加审核公告
        /// </summary>
        /// <param name="_shjlid"></param>
        /// <param name="_title"></param>
        /// <param name="_context"></param>
        /// <param name="_cddw"></param>
        /// <param name="_tel"></param>
        /// <param name="_email"></param>
        /// <param name="_sfkj"></param>
        /// <returns></returns>
        Task<bool> InsertNewDataCheckMsg(string _shjlid, string _title, string _context, string _cddw, string _tel, string _email, decimal _sfkj);

        /// <summary>
        /// 通过审核记录取审核单位的ID
        /// </summary>
        /// <param name="_shjlid"></param>
        /// <returns></returns>
        Task<string> GetSjshInfo_DWID(string _shjlid);


        /// <summary>
        /// 通过记录ID取审核公告信息
        /// </summary>
        /// <param name="_ggjlid"></param>
        /// <returns></returns>
        Task<MD_DataCheckMsg> GetDataCheckMsg(string _ggjlid);

        /// <summary>
        /// 反馈审核公告
        /// </summary>
        /// <param name="_ggjlid"></param>
        /// <param name="_fkjg"></param>
        /// <returns></returns>
        Task<bool> SendDataCheckMsgFK(string _ggjlid, string _fkjg);

        /// <summary>
        /// 保存公告修改信息
        /// </summary>
        /// <param name="_ggjlid"></param>
        /// <param name="_title"></param>
        /// <param name="_context"></param>
        /// <param name="_cddw"></param>
        /// <param name="_tel"></param>
        /// <param name="_email"></param>
        /// <param name="_sfkj"></param>
        /// <returns></returns>
        Task<bool> UpdateDataCheckMsg(string _ggjlid, string _title, string _context, string _cddw, string _tel, string _email, decimal _sfkj);

        /// <summary>
        /// 删除审核公告记录
        /// </summary>
        /// <param name="_ggjlid"></param>
        /// <returns></returns>
        Task<bool> DeleteDataCheckMsg(string _ggjlid);

        /// <summary>
        /// 取指定指标在本单位的参数设置
        /// </summary>
        /// <param name="_guideLineID"></param>
        /// <returns></returns>
        Task<MD_GuideLine_ParamSetting> GetGuideLineParamSetting(string _guideLineID);

        /// <summary>
        /// 保存指标参数定义
        /// </summary>
        /// <param name="_paramSetting"></param>
        /// <param name="_paramters"></param>
        /// <returns></returns>
        Task<bool> SaveGuideLineParamSetting(MD_GuideLine_ParamSetting _paramSetting, List<MDQuery_GuideLineParameter> _paramters);

        Task<MD_InputModel_ColumnGroup> GetInputGroupByID(string InputGroupID);

        Task<MD_InputModel> GetInputModelByID(string InputModelID);

        Task<bool> DelComputeFieldDefine(string ColumnName);

        Task<DataSet> CompareData(MDCompare_Request compareRequest, DataTable srcData);

        Task<string> GetAttachFileName(string IndexString, string FieldName);

        Task<byte[]> GetAttachFileBytes(string IndexString, string FieldName);

        Task<string> GetFLWSFileName(string _indexString, string FieldName);

        Task<byte[]> GetFLWSFileBytes(string _indexString, string FieldName);

        Task<DataTable> GetTaskQueryLog(string TaskID);

        Task<bool> ChangeQueryTaskRequestTime(string TaskID, DateTime RequestTime);

        Task<bool> RebuildGuideLineParamSetting(string GuideLineID);

        Task<RefCodeTablePropertie> GetRefCodePropertie(string RefTableName);

        Task<IList<RefCodeData>> GetFullRefCodeData(string refTableName);

        Task<IList<RefCodeData>> GetChildRefCodeData(string RefTableName, string FatherCode);

        Task<RefCodeData> GetRefCodeByCode(string RefTableName, string CodeValue);


        Task<bool> SaveUserToken(string userName, string code, string token);

        Task<string> GetCodeByToken(string token);

        Task<string> GetOrgName();

        Task<string> GetCurrentUserName(string Token);
    }
}
