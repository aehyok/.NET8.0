using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MD_DataCheckMsg
    {
        public MD_DataCheckMsg() { }

        public MD_DataCheckMsg(string _id, string _shjlid, string _bh, string _fbdwdm, string _fbdwmc, DateTime _fbsj, string _fbr,
                                                        string _lxdh, string _yjdz, string _xxbt, string _xxnr, string _cddwdm, string _cddwmc, string _fkjg, object _fksj, decimal _sfyc)
        {
            ID = _id;
            SHJLID = _shjlid;
            BH = _bh;
            FBDWDM = _fbdwdm;
            FBDWMC = _fbdwmc;
            FBSJ = _fbsj;
            FBR = _fbr;
            LXDH = _lxdh;
            YJDZ = _yjdz;
            XXBT = _xxbt;
            XXNR = _xxnr;
            CDDWDM = _cddwdm;
            CDDWMC = _cddwmc;
            FKJG = _fkjg;
            FKSJ = _fksj;
            SFYC = _sfyc;
        }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string SHJLID { get; set; }
        [DataMember]
        public string BH { get; set; }
        [DataMember]
        public string FBDWDM { get; set; }
        [DataMember]
        public string FBDWMC { get; set; }
        [DataMember]
        public DateTime FBSJ { get; set; }
        [DataMember]
        public string FBR { get; set; }
        [DataMember]
        public string LXDH { get; set; }
        [DataMember]
        public string YJDZ { get; set; }
        [DataMember]
        public string XXBT { get; set; }
        [DataMember]
        public string XXNR { get; set; }
        [DataMember]
        public string CDDWDM { get; set; }
        [DataMember]
        public string CDDWMC { get; set; }
        [DataMember]
        public string FKJG { get; set; }
        [DataMember]
        public object FKSJ { get; set; }
        [DataMember]
        public decimal SFYC { get; set; }

    }
}
