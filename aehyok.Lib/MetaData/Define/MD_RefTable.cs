using aehyok.Lib.MetaData.EnumDefine;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 代码表元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_RefTable
    {
        public MD_RefTable() { }

        public MD_RefTable(string _id, string _ns, string _refName, string _format, string _des, string _dw, int _downMode, int _paramMode, bool _hide)
        {
            RefTableID = _id;
            NamespaceName = _ns;
            RefTableName = _refName;
            LevelFormat = _format;
            Description = _des;
            DWDM = _dw;
            HideCode = _hide;
            RefDownloadMode = (_downMode > 1) ? MDType_RefDownloadMode.LevelDownload : MDType_RefDownloadMode.FullDownload;
            RefParamMode = (_paramMode > 1) ? MDType_RefParamMode.UserParam : MDType_RefParamMode.Normal;
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
