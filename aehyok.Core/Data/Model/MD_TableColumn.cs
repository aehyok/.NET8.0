using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Model
{
	public class MD_TableColumn
	{

		public MD_TableColumn(string _tcid, string _tid, string _cname, bool _isnull, string _ctype, int _preci,
				int _sca, int _len, string _refd, string _dmblevel, int _secretlevel, string _displaytitle,
				string _displayformat, int _displaylength, int _displayheight, int _displayorder, bool _candisplay,
				int _colwidth, string _dw, string _tag, string _refword)
		{
			ColumnID = _tcid;
			TID = _tid;
			ColumnName = _cname;
			IsNullable = _isnull;
			ColumnType = _ctype;
			Precision = _preci;
			Scale = _sca;
			Length = _len;
			RefDMB = _refd.ToUpper();
			this.DMBLevelFormat = _dmblevel;
			SecretLevel = _secretlevel;
			DisplayTitle = _displaytitle.Substring(0, (_displaytitle.Length > 50) ? 50 : _displaytitle.Length);
			DisplayFormat = _displayformat;
			DisplayLength = _displaylength;
			DisplayHeight = _displayheight;
			DisplayOrder = _displayorder;
			CanDisplay = _candisplay;
			ColWidth = _colwidth;
			DWDM = _dw;
			this.CTag = _tag;
			RefWordTableName = _refword;
		}

		
		public string TID { get; set; }

		/// <summary>
		/// 本字段的ID
		/// </summary>
		
		public string ColumnID { get; set; }

		/// <summary>
		/// 字段名称
		/// </summary>
		
		public string ColumnName { get; set; }

		/// <summary>
		/// 字段宽度
		/// </summary>
		
		public int ColWidth { get; set; }

		/// <summary>
		/// 字段包含的概念
		/// </summary>
		
		public string CTag { get; set; }

		/// <summary>
		/// 显示格式
		/// </summary>
		
		public string DisplayFormat { get; set; }

		/// <summary>
		/// 显示行高度
		/// </summary>
		
		public int DisplayHeight { get; set; }

		/// <summary>
		/// 显示长度
		/// </summary>
		
		public int DisplayLength { get; set; }

		/// <summary>
		/// 显示顺序
		/// </summary>
		
		public int DisplayOrder { get; set; }

		/// <summary>
		/// 显示名称
		/// </summary>
		
		public string DisplayTitle { get; set; }
		/// <summary>
		/// 代码表分级模式
		/// </summary>
		
		public string DMBLevelFormat { get; set; }

		/// <summary>
		/// 节点编号
		/// </summary>
		
		public string DWDM { get; set; }

		/// <summary>
		/// 本字是否可以为空
		/// </summary>
		
		public bool IsNullable { get; set; }

		/// <summary>
		/// 字段长度
		/// </summary>
		
		public int Length { get; set; }

		
		public int Precision { get; set; }

		/// <summary>
		/// 代码表名称
		/// </summary>
		
		public string RefDMB { get; set; }

		/// <summary>
		/// 对应引用表名称
		/// </summary>
		
		public string RefWordTableName { get; set; }

		
		public int Scale { get; set; }

		/// <summary>
		/// 安全级别
		/// </summary>
		
		public int SecretLevel { get; set; }

		/// <summary>
		/// 字段类型
		/// </summary>
		
		public string ColumnType { get; set; }

		/// <summary>
		/// 对应代码表定义
		/// </summary>
		
		public MD_RefTable RefTable { get; set; }

		/// <summary>
		/// 是否显示
		/// </summary>
		
		public bool CanDisplay { get; set; }
	}
}
