using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Query
{
    [DataContract]
    public class MDQuery_ComputeColumnDefine
    {

        public MDQuery_ComputeColumnDefine() { }
        public MDQuery_ComputeColumnDefine(string _columnName, string _displayName, string _columnMethod, string _description, bool _isPublic,
                string _fullModelName, string _tableName, string _resultType, string _displayMethod, DateTime _createDate)
        {
            ColumnName = _columnName;
            DisplayName = _displayName;
            ColumnMethod = _columnMethod;
            ColumnDescription = _description;
            IsPublic = _isPublic;
            FullModelName = _fullModelName;
            TableName = _tableName;
            ResultDataType = _resultType;
            DisplayMethod = _displayMethod;
            CreateDate = _createDate;
        }
        [DataMember]
        public DateTime CreateDate { get; set; }

        [DataMember]
        public string DisplayMethod { get; set; }
        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public string ResultDataType { get; set; }
        [DataMember]
        public string ColumnDescription { get; set; }

        [DataMember]
        public bool IsPublic { get; set; }

        [DataMember]
        public string FullModelName { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string ColumnMethod { get; set; }

    }
}
