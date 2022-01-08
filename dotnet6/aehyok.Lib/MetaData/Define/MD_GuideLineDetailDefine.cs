using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 指标详情元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_GuideLineDetailDefine
    {
        public MD_GuideLineDetailDefine()
        {
        }

        public MD_GuideLineDetailDefine(string _fname, string _type, string _qid, string _qcs)
        {
            TargetFieldName = _fname;
            QueryDetailType = _type;
            DetailMethodID = _qid;
            DetailParameterMeta = _qcs;
        }

        public MD_GuideLineDetailDefine(string _fname, string _type, string _qid, string _qcs, string linkstr)
        {
            TargetFieldName = _fname;
            QueryDetailType = _type;
            DetailMethodID = _qid;
            DetailParameterMeta = _qcs;
            DetailLinkMeta = linkstr;
        }

        /// <summary>
        /// 目标字段
        /// </summary>
        [DataMember]
        public string TargetFieldName { get; set; }
        /// <summary>
        /// 查询类型
        /// </summary>
        [DataMember]
        public string QueryDetailType { get; set; }

        /// <summary>
        /// 链接详细的方法或指标ID
        /// </summary>
        [DataMember]
        public string DetailMethodID { get; set; }
        /// <summary>
        /// 链接详细的参数元数据定义
        /// </summary>
        [DataMember]
        public string DetailParameterMeta { get; set; }
        /// <summary>
        /// 链接详细的链接定义
        /// </summary>
        [DataMember]
        public string DetailLinkMeta { get; set; }
    }
}
