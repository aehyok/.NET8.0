using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MD_FUNCTION
    {

        public MD_FUNCTION() { }

        public MD_FUNCTION(string _id, string _name, string _title, string _description, string _resultType, string _functionType)
        {
            ID = _id;
            Name = _name;
            DisplayTitle = _title;
            Description = _description;
            ResultType = _resultType;
            FunctionType = _functionType;
        }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public List<string> ParamList { get; set; }

        [DataMember]
        public Dictionary<string, string> ParamTypeDict { get; set; }
        [DataMember]
        public string FunctionType { get; set; }
        [DataMember]
        public string ResultType { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}
