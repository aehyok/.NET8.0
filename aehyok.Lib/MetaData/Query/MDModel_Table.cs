using aehyok.Lib.MetaData.Common;
using aehyok.Lib.MetaData.Define;
using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    public class MDModel_Table
    {
        public MDModel_Table() { }

        public MDModel_Table(string _modelName, string _modelID, MD_ViewTable _table)
        {
            TableDefine = _table;
            QueryModelName = _modelName;
            this.ViewTableID = _table.ViewTableID;
            this.ViewID = _modelID;
            this.TableID = _table.TableID;
            this.TableType = (_table.ViewTableType == MDType_ViewTable.MainTable) ? "M" : "F";
            this.TableRelation = _table.RelationString;
            this.IsSingleRelation = (_table.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord);
            this.DisplayTitle = _table.DisplayTitle;
            this.DisplayOrder = _table.DisplayOrder;
            this.DWDM = _table.DWDM;
            this.FatherID = _table.FatherTableID;
            this.Priority = _table.Priority;
            this.DisplayType = (_table.DisplayType == MDType_DisplayType.FormType) ? 1 : 0;
            this.MainKey = _table.Table.MainKey;
            this.SecretFun = _table.Table.SecretFun;
            this.ExtSecret = _table.Table.ExtSecret;
            this.TableName = _table.TableName;
            this.Columns = _table.GetMDModelColumns();

        }

        public MDModel_Table(string vtid, string viewid, string tid, string tabletype, string tablerelation, string cancondition, string displayname, int displayorder,
            string dwdm, string fatherid, int priority, int displaytype, string tablename, string querymodelname, string mainkey, string secretfun, string extsecret, string restype, string integratedapp)
        {
            // TODO: Complete member initialization
            this.ViewTableID = vtid;
            this.ViewID = viewid;
            this.TableID = tid;
            this.TableType = tabletype;
            this.TableRelation = tablerelation;
            this.IsSingleRelation = (cancondition == "1");
            this.DisplayTitle = displayname;
            this.DisplayOrder = displayorder;
            this.DWDM = dwdm;
            this.FatherID = fatherid;
            this.Priority = priority;
            this.DisplayType = displaytype;
            this.MainKey = mainkey;
            this.SecretFun = secretfun;
            this.ExtSecret = extsecret;
            this.TableName = tablename;
            this.QueryModelName = querymodelname;
            this.ResourceType = restype;
            this.IntegratedApp = integratedapp;
        }

        /// <summary>
        /// 表中的字段
        /// </summary>
        /// 
        [DataMember]
        public List<MDModel_Table_Column> Columns { get; set; }

        /// <summary>
        /// 表字段的别名字典,以字段的别名为字典键值
        /// </summary>
        [DataMember]
        public Dictionary<string, MDModel_Table_Column> AliasDict { get; set; }


        /// <summary>
        /// 显示顺序
        /// </summary>
        /// 
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 表的基本定义
        /// </summary>
        [DataMember]
        public MD_ViewTable TableDefine { get; set; }

        /// <summary>
        /// 取表名称
        /// </summary>
        /// 
        [DataMember]
        public string TableName { get; set; }

        /// <summary>
        /// 查询模型名称
        /// </summary>
        /// 
        [DataMember]
        public string QueryModelName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// 
        [DataMember]
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 模型表ID
        /// </summary>
        [DataMember]
        public string ViewTableID { get; set; }

        /// <summary>
        /// 模型ID
        /// </summary>
        [DataMember]
        public string ViewID { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        [DataMember]
        public string TableID { get; set; }

        /// <summary>
        /// 表类型  M:主表
        /// </summary>
        [DataMember]
        public string TableType { get; set; }


        /// <summary>
        /// 表关系定义
        /// </summary>
        [DataMember]
        public string TableRelation { get; set; }

        /// <summary>
        /// 是否一对多关系
        /// </summary>
        [DataMember]
        public bool IsSingleRelation { get; set; }


        /// <summary>
        /// 对应的单位代码
        /// </summary>
        [DataMember]
        public string DWDM { get; set; }

        /// <summary>
        /// 父表ID
        /// </summary>
        [DataMember]
        public string FatherID { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [DataMember]
        public int Priority { get; set; }


        /// <summary>
        /// 显示方式类型
        /// </summary>
        [DataMember]
        public int DisplayType { get; set; }
        /// <summary>
        /// 表的主键
        /// </summary>
        [DataMember]
        public string MainKey { get; set; }
        /// <summary>
        /// 安全控制函数
        /// </summary>
        [DataMember]
        public string SecretFun { get; set; }
        /// <summary>
        /// 扩展安全函数
        /// </summary>
        [DataMember]
        public string ExtSecret { get; set; }

        [DataMember]//ph添加
        public string ResourceType { set; get; }

        [DataMember]//ph添加
        public string IntegratedApp { set; get; }

    }
}
