using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型子表参数定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_InputModel_ChildParam
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public string Value { get; set; }

        public MD_InputModel_ChildParam() { }
        public MD_InputModel_ChildParam(string _name, string _type, string _value)
        {
            Name = _name;
            DataType = _type;
            Value = _value;
        }
    }
}
