using aehyok.Lib.MetaData.Define;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    /// <summary>
    /// 需检索的字段
    /// </summary>
    [DataContract]
    public class MDSearch_Column
    {

        public MDSearch_Column() { }

        public MDSearch_Column(string _modelName, string _tableName, string _tableTitle, string _columnName, string _columnTitle, string _tableKey)
        {
            ModelName = _modelName;
            TableName = _tableName;
            ColumnName = _columnName;
            TableKeyColumn = _tableKey;
            TableTitle = _tableTitle;
            ColumnTitle = _columnTitle;
        }

        /// <summary>
        /// 查询模型
        /// </summary>
        [DataMember]
        public MD_QueryModel QueryModel { get; set; }


        /// <summary>
        /// 表的主键字段
        /// </summary>
        [DataMember]
        public string TableKeyColumn { get; set; }
        /// <summary>
        /// 查询模型名称
        /// </summary>
        [DataMember]
        public string ModelName { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string TableTitle { get; set; }


        [DataMember]
        public string ColumnTitle { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [DataMember]
        public string ColumnName { get; set; }
    }
}
