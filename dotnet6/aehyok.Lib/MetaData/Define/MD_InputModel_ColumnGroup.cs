using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型字段组定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_InputModel_ColumnGroup
    {
        [DataMember]
        public string ModelID { get; set; }
        [DataMember]
        public string GroupID { get; set; }
        [DataMember]
        public string DisplayTitle { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public List<MD_InputModel_Column> Columns { get; set; }
        [DataMember]
        public string GroupType { get; set; }
        [DataMember]
        public string AppRegUrl { get; set; }
        [DataMember]
        public string GroupParam { get; set; }
       
        public override string ToString()
        {
            return DisplayTitle;
        }

        public MD_InputModel_ColumnGroup() { }

        public MD_InputModel_ColumnGroup(string _groupID, string _modelID, string _title, int _order)
        {
            ModelID = _modelID;
            GroupID = _groupID;
            DisplayTitle = _title;
            DisplayOrder = _order;
        }
        
    }
}
