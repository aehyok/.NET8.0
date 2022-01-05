using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MD_ComputeField
    {        
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string ModelName { get; set; }
        [DataMember]
        public string Expression { get; set; }
        [DataMember]
        public string ResultDataType { get; set; }
        [DataMember]
        public string QueryString { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
