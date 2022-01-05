using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 数据库中的字段元数据
    /// 
    /// Create by: Lintx 
    /// </summary>
    public class DB_ColumnMeta
    {
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public bool Nullable { get; set; }
        [DataMember]
        public long DataLength { get; set; }
        [DataMember]
        public int DataPrecision { get; set; }
    }
}
