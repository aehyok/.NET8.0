using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 表元数据定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_Table
    {
        public MD_Table() { }
        public MD_Table(string _id, string _ns, string _tname, string _ttype, string _des, string _title, string _mkey,
        string _dw, string _sfun, string _extfun)
        {
            TID = _id;
            NamespaceName = _ns;
            TableName = _tname;
            TableType = _ttype;
            Description = _des;
            DisplayTitle = _title;
            MainKey = _mkey;
            DWDM = _dw;
            SecretFun = _sfun;
            ExtSecret = _extfun;
        }

        public MD_Table(string _id, string _ns, string _tname, string _ttype, string _des, string _title, string _mkey,
        string _dw, string _sfun, string _extfun, string _resType)
        {
            TID = _id;
            NamespaceName = _ns;
            TableName = _tname;
            TableType = _ttype;
            Description = _des;
            DisplayTitle = _title;
            MainKey = _mkey;
            DWDM = _dw;
            SecretFun = _sfun;
            ExtSecret = _extfun;
            if (_resType != null)
            {
                ResourceType = _resType.Split(',').ToList<string>();
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
