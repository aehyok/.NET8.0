using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [Serializable]
    [DataContract]
    public class MDQuery_Request
    {

        /// <summary>
        /// 查询模型名称
        /// </summary>
        [DataMember]
        public string QueryModelName { get; set; }
        /// <summary>
        /// 查询条件表达式
        /// </summary>
        [DataMember]
        public string ConditionExpressions { get; set; }
        /// <summary>
        /// 查询条件列表
        /// </summary>
        [DataMember]
        public List<MDQuery_ConditionItem> ConditionItems { get; set; }
        /// <summary>
        /// 查询结果的主表
        /// </summary>
        [DataMember]
        public MDQuery_ResultTable MainResultTable { get; set; }
        /// <summary>
        /// 查询结果的子表
        /// </summary>
        [DataMember]
        public List<MDQuery_ResultTable> ChildResultTables { get; set; }
    }
}
