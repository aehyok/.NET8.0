using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    public class MDCompare_ConditionItem
    {
        [DataMember]
        public string ColumnIndex { get; set; }
        [DataMember]
        public MDQuery_TableColumn Column { get; set; }
        [DataMember]
        public string Operator { get; set; }
        [DataMember]
        public string CompareTagetField { get; set; }
    }
}
