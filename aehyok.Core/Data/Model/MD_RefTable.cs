using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_RefTable
	{

		public MD_RefTable(string id, string ns, string refName, string format, string des, string dw, int downMode, int paramMode, bool hide)
		{
			RefTableID = id;
			NamespaceName = ns;
			RefTableName = refName;
			LevelFormat = format;
			Description = des;
			DWDM = dw;
			HideCode = hide;
			RefDownloadMode = (downMode > 1) ? MDType_RefDownloadMode.LevelDownload : MDType_RefDownloadMode.FullDownload;
			RefParamMode = (paramMode > 1) ? MDType_RefParamMode.UserParam : MDType_RefParamMode.Normal;
		}
		
		public bool HideCode { get; set; }

		
		public string NamespaceName { get; set; }

		
		public MD_Namespace Namespace { get; set; }

		
		public string RefTableID { get; set; }
		
		public string RefTableName { get; set; }

		/// <summary>
		/// 分级格式
		/// </summary>
		
		public string LevelFormat { get; set; }

		/// <summary>
		/// 描述
		/// </summary>
		
		public string Description { get; set; }

		/// <summary>
		/// 节点编号
		/// </summary>
		
		public string DWDM { get; set; }

		
		public MDType_RefDownloadMode RefDownloadMode { get; set; }

		
		public MDType_RefParamMode RefParamMode { get; set; }

		public override string ToString()
		{
			return (Description == "") ? RefTableName : Description;
		}
	}
}
