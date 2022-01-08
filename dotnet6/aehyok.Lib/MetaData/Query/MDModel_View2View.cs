using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MDModel_View2View
    {
        public MDModel_View2View(string id, string targetViewName, string relationString, string title, int order, string param)
        {
            ID = id;
            TargetViewName = targetViewName;
            RelationString = relationString;
            DisplayTitle = title;
            DisplayOrder = order;
            MetaDataParam = param;
        }

        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string RelationString { get; set; }
        [DataMember]
        public string TargetViewName { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public string ViewGroupID { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string MetaDataParam { get; set; }
        [DataMember]
        public string QueryModelID { get; set; }
        public override string ToString()
        {
            return this.DisplayTitle;
        }
    }
}
