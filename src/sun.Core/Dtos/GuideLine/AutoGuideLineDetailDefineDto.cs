using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos.GuideLine
{
    /// <summary>
    /// 指标详情元数据定义
    /// 
    /// </summary>
    public class AutoGuideLineDetailDefineDto
    {
        public AutoGuideLineDetailDefineDto()
        {
        }

        public AutoGuideLineDetailDefineDto(string _fname, string _type, string _qid, string _qcs)
        {
            TargetFieldName = _fname;
            QueryDetailType = _type;
            DetailMethodID = _qid;
            DetailParameterMeta = _qcs;
        }

        public AutoGuideLineDetailDefineDto(string _fname, string _type, string _qid, string _qcs, string linkstr)
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
