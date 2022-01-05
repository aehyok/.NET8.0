using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型保存表定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_InputModel_SaveTable
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string TableTitle { get; set; }
        [DataMember]
        public bool IsLock { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string InputModelID { get; set; }
        [DataMember]
        public List<MD_InputModel_SaveTableColumn> Columns { get; set; }
        [DataMember]
        public string SaveMode { get; set; }

      
        public override string ToString()
        {
            return string.Format("{0} [{1}]", TableTitle, TableName);
        }

        public MD_InputModel_SaveTable() { }
        public MD_InputModel_SaveTable(string _id, string _tname, string _title, bool _isLock, string _modelID, int _order)
        {
            ID = _id;
            TableName = _tname;
            TableTitle = _title;
            IsLock = _isLock;
            DisplayOrder = _order;
            InputModelID = _modelID;
        }
        public MD_InputModel_SaveTable(string _id, string _tname, string _title, bool _isLock, string _modelID, int _order, string _saveMode)
        {
            ID = _id;
            TableName = _tname;
            TableTitle = _title;
            IsLock = _isLock;
            DisplayOrder = _order;
            InputModelID = _modelID;
            this.SaveMode = (_saveMode == null || _saveMode.Length == 0) ? "DEFAULT" : _saveMode.ToUpper();
        }
        
    }
}
