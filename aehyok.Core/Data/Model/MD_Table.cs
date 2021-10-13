using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_Table
	{
		public MD_Table(string id, string ns, string tname, string ttype, string des, string title, string mkey,
		string dw, string sfun, string extfun)
		{
			TID = id;
			NamespaceName = ns;
			TableName = tname;
			TableType = ttype;
			Description = des;
			DisplayTitle = title;
			MainKey = mkey;
			DWDM = dw;
			SecretFun = sfun;
			ExtSecret = extfun;
		}

		public MD_Table(string id, string ns, string tname, string ttype, string des, string title, string mkey,
		string dw, string sfun, string extfun, string resType)
		{
			TID = id;
			NamespaceName = ns;
			TableName = tname;
			TableType = ttype;
			Description = des;
			DisplayTitle = title;
			MainKey = mkey;
			DWDM = dw;
			SecretFun = sfun;
			ExtSecret = extfun;
			if (resType != null)
			{
				ResourceType = resType.Split(',').ToList<string>();
			}
			else
			{
				ResourceType = new List<string>();
			}
		}

		
		public List<string> ResourceType { get; set; }

		/// <summary>
		/// 命名空间
		/// </summary>
		
		public string NamespaceName { get; set; }

		/// <summary>
		/// 表的ID
		/// </summary>
		
		public string TID { get; set; }

		/// <summary>
		/// 表名称
		/// </summary>
		
		public virtual string TableName { get; set; }

		/// <summary>
		/// 表类型
		/// </summary>
		
		public string TableType { get; set; }

		/// <summary>
		/// 表的描述
		/// </summary>
		
		public string Description { get; set; }

		/// <summary>
		/// 表的显示名称
		/// </summary>
		
		public string DisplayTitle { get; set; }

		/// <summary>
		/// 表的主键字段名称
		/// </summary>
		
		public string MainKey { get; set; }

		/// <summary>
		/// 节点编号
		/// </summary>
		
		public string DWDM { get; set; }

		/// <summary>
		/// 安全函数
		/// </summary>
		
		public string SecretFun { get; set; }

		/// <summary>
		/// 扩展安全函数
		/// </summary>
		
		public string ExtSecret { get; set; }

		/// <summary>
		/// 本表的字段列表
		/// </summary>
		
		public IList<MD_TableColumn> Columns { get; set; }

		/// <summary>
		/// 本表的主键字段
		/// </summary>
		
		public MD_TableColumn MainKeyColumn { get; set; }

		public override string ToString()
		{
			return DisplayTitle;
		}
	}
}
