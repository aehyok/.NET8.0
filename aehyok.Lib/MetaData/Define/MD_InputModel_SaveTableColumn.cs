using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型保存表字段定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_InputModel_SaveTableColumn
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string SrcColumn { get; set; }
        [DataMember]
        public string DesColumn { get; set; }
        [DataMember]
        public string Method { get; set; }
        [DataMember]
        public string Descript { get; set; }

        public MD_InputModel_SaveTableColumn() { }
        public MD_InputModel_SaveTableColumn(string _id, string _srcCol, string _desCol, string _method, string _descript)
        {
            ID = _id;
            SrcColumn = _srcCol;
            DesColumn = _desCol;
            Method = _method;
            Descript = _descript;
        }
    }
}
