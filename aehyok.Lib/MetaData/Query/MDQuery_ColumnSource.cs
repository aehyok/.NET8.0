using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    /// <summary>
    /// 查询结果字段对应的元数据定义来源
    /// </summary>
    [Serializable]
    [DataContract]
    public class MDQuery_ColumnSource
    {
        /// <summary>
        /// 查询模型名称
        /// </summary>
        [DataMember]
        public string QueryModelName { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        [DataMember]
        public string TableName { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        [DataMember]
        public string ColumnName { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="_qvName">查询模型名称</param>
        /// <param name="_tname">表名称</param>
        /// <param name="_cname">字段名称</param>
        public MDQuery_ColumnSource(string _qvName, string _tname, string _cname)
        {
            QueryModelName = _qvName;
            TableName = _tname;
            ColumnName = _cname;
        }
    }
}
