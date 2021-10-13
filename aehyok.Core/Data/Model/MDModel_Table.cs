using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MDModel_Table
	{
		public MDModel_Table() { }

		public MDModel_Table(string modelName, string modelID, MD_ViewTable table)
		{
			TableDefine = table;
			QueryModelName = modelName;
			this.ViewTableId = table.ViewTableID;
			this.ViewId = modelID;
			this.TableId = table.TableID;
			this.TableType = (table.ViewTableType == MDType_ViewTable.MainTable) ? "M" : "F";
			this.TableRelation = table.RelationString;
			this.IsSingleRelation = (table.ViewTableRelationType == MDType_ViewTableRelation.SingleChildRecord);
			this.DisplayTitle = table.DisplayTitle;
			this.DisplayOrder = table.DisplayOrder;
			this.DWDM = table.DWDM;
			this.FatherID = table.FatherTableID;
			this.Priority = table.Priority;
			this.DisplayType = (table.DisplayType == MDType_DisplayType.FormType) ? 1 : 0;
			this.MainKey = table.Table.MainKey;
			this.SecretFun = table.Table.SecretFun;
			this.ExtSecret = table.Table.ExtSecret;
			this.TableName = table.TableName;
			this.Columns = table.GetMDModelColumns();

		}

		public MDModel_Table(string vtid, string viewid, string tid, string tabletype, string tablerelation, string cancondition, string displayname, int displayorder,
			string dwdm, string fatherid, int priority, int displaytype, string tablename, string querymodelname, string mainkey, string secretfun, string extsecret, string restype, string integratedapp)
		{
			// TODO: Complete member initialization
			this.ViewTableId = vtid;
			this.ViewId = viewid;
			this.TableId = tid;
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
		
		public List<MDModel_Table_Column> Columns { get; set; }

		/// <summary>
		/// 表字段的别名字典,以字段的别名为字典键值
		/// </summary>
		
		public Dictionary<string, MDModel_Table_Column> AliasDict { get; set; }


		/// <summary>
		/// 显示顺序
		/// </summary>
		/// 
		
		public int DisplayOrder { get; set; }

		/// <summary>
		/// 表的基本定义
		/// </summary>
		
		public MD_ViewTable TableDefine { get; set; }

		/// <summary>
		/// 取表名称
		/// </summary>
		/// 
		
		public string TableName { get; set; }

		/// <summary>
		/// 查询模型名称
		/// </summary>
		/// 
		
		public string QueryModelName { get; set; }

		/// <summary>
		/// 显示名称
		/// </summary>
		/// 
		
		public string DisplayTitle { get; set; }

		/// <summary>
		/// 模型表ID
		/// </summary>
		
		public string ViewTableId { get; set; }

		/// <summary>
		/// 模型ID
		/// </summary>
		
		public string ViewId { get; set; }

		/// <summary>
		/// 表ID
		/// </summary>
		
		public string TableId { get; set; }

		/// <summary>
		/// 表类型  M:主表
		/// </summary>
		
		public string TableType { get; set; }


		/// <summary>
		/// 表关系定义
		/// </summary>
		
		public string TableRelation { get; set; }

		/// <summary>
		/// 是否一对多关系
		/// </summary>
		
		public bool IsSingleRelation { get; set; }


		/// <summary>
		/// 对应的单位代码
		/// </summary>
		
		public string DWDM { get; set; }

		/// <summary>
		/// 父表ID
		/// </summary>
		
		public string FatherID { get; set; }

		/// <summary>
		/// 优先级
		/// </summary>
		
		public int Priority { get; set; }


		/// <summary>
		/// 显示方式类型
		/// </summary>
		
		public int DisplayType { get; set; }
		/// <summary>
		/// 表的主键
		/// </summary>
		
		public string MainKey { get; set; }
		/// <summary>
		/// 安全控制函数
		/// </summary>
		
		public string SecretFun { get; set; }
		/// <summary>
		/// 扩展安全函数
		/// </summary>
		
		public string ExtSecret { get; set; }

		//ph添加
		public string ResourceType { set; get; }

		//ph添加
		public string IntegratedApp { set; get; }

	}
}
