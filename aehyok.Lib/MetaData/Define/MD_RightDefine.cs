using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    ///  权限定义
    ///  
    /// Create by: Lintx
    /// </summary>
    public class MD_RightDefine
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        /// 
        [DataMember]
        public string RightID { get; set; }
        /// <summary>
        /// 父权限ID
        /// </summary>
        [DataMember]
        public string FatherRightID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        [DataMember]
        public string RightName { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        [DataMember]
        public string RightDescript { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        [DataMember]
        public string RightType { get; set; }
        /// <summary>
        /// 权限的META
        /// </summary>
        [DataMember]
        public string RightMeta { get; set; }
        /// <summary>
        /// 显示序号
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 对应的菜单ID
        /// </summary>
        [DataMember]
        public string MenuID { get; set; }

        public MD_RightDefine() { }
        public MD_RightDefine(string _id, string _fid, string _rname, string _des, string _type, string _meta, int _order, string _menuid)
        {
            RightID = _id;
            FatherRightID = (_fid == "-1") ? "" : _fid;
            RightName = _rname;
            RightType = _type;
            RightDescript = _des;
            RightMeta = _meta;
            DisplayOrder = _order;
            MenuID = _menuid;
        }
    }
}
