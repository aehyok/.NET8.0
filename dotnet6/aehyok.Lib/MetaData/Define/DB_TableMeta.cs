using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 数据库中的表元数据
    /// 
    /// Create by:Lintx
    /// </summary>
    public class DB_TableMeta
    {
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string TableComment { get; set; }
        [DataMember]
        public string TableType { get; set; }
    }
}
