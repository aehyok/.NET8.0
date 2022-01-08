using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MD_CheckRule
    {
        public MD_CheckRule() { }
        public MD_CheckRule(string _rid, string _qv, string _name, string _define, string _dw, bool _used)
        {
            ID = _rid;
            QueryModelName = _qv;
            RuleName = _name;
            MethodDefine = _define;
            DWDM = _dw;
            State = _used;
        }
        [DataMember]
        public bool State { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string QueryModelName { get; set; }
        [DataMember]
        public string RuleName { get; set; }
        [DataMember]
        public string MethodDefine { get; set; }
        [DataMember]
        public string DWDM { get; set; }

    }
}
