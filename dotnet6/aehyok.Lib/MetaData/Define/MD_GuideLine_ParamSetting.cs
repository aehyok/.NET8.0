using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    [DataContract]
    public class MD_GuideLine_ParamSetting
    {
        [DataMember]
        public string ParamMeta { get; set; }
        [DataMember]
        public string DWID { get; set; }
        [DataMember]
        public string GuideLineID { get; set; }
        [DataMember]
        public string CSID { get; set; }

        public MD_GuideLine_ParamSetting() { }
        public MD_GuideLine_ParamSetting(string _csid, string _zbid, string _dwid, string _cs)
        {
            CSID = _csid;
            GuideLineID = _zbid;
            DWID = _dwid;
            ParamMeta = _cs;
        }
    }
}
