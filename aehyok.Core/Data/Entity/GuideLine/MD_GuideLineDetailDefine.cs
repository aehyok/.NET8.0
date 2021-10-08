using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Core.Data.Entity.GuideLine
{
    [DataContract]
    public class MD_GuideLineDetailDefine
    {
        public MD_GuideLineDetailDefine()
        {
        }

        public MD_GuideLineDetailDefine(string fname, string type, string qid, string qcs)
        {
            TargetFieldName = fname;
            QueryDetailType = type;
            DetailMethodID = qid;
            DetailParameterMeta = qcs;
        }

        public MD_GuideLineDetailDefine(string fname, string type, string qid, string qcs, string linkstr)
        {
            TargetFieldName = fname;
            QueryDetailType = type;
            DetailMethodID = qid;
            DetailParameterMeta = qcs;
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
