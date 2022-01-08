using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MDCompare_Request : MDQuery_Request
    {
        [DataMember]
        public DataTable ExcelData { get; set; }
        [DataMember]
        public Dictionary<string, string> ColumnDictionary { get; set; }
        [DataMember]
        public string CompareConditionExpression { get; set; }
        [DataMember]
        public Dictionary<string, MDCompare_ConditionItem> CompareItems { get; set; }
    }
}
