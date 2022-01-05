using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.RefCode
{
    public class RefCodeData
    {
        public RefCodeData() { }

        public RefCodeData(string _code, string _title, string _pyzt, int _order, bool _isEffective, string _note,
                                string _fcode, bool _canShow, bool _canInput, bool _isLeaves)
        {
            Code = _code;
            DisplayTitle = _title;
            PYZT = _pyzt;
            Order = _order;
            IsEffective = _isEffective;
            Note = _note;
            FatherCode = _fcode;
            CanShow = _canShow;
            CanInput = _canInput;
            IsLeaves = _isLeaves;
        }


        [DataMember]
        public List<RefCodeData> ChildData { get; set; }
        [DataMember]
        public bool HaveChilds { get; set; }
        [DataMember]
        public string Note { get; set; }
        [DataMember]
        public string FatherCode { get; set; }
        [DataMember]
        public bool CanShow { get; set; }
        [DataMember]
        public bool CanInput { get; set; }
        [DataMember]
        public bool IsLeaves { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public string PYZT { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public bool IsEffective { get; set; }

    }
}
