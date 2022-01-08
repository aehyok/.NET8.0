using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型字段定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_InputModel_Column
    {
        [DataMember]
        public string ColumnID { get; set; }
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public int MaxInputLength { get; set; }  ///将MaxLength更改为MaxInputLength
        [DataMember]
        public string ColumnType { get; set; }
        [DataMember]
        public string InputModelID { get; set; }
        [DataMember]
        public bool CanSave { get; set; }
        [DataMember]
        public bool CanDisplay { get; set; }
        [DataMember]
        public bool IsCompute { get; set; }
        [DataMember]
        public bool ReadOnly { get; set; }
        [DataMember]
        public string DWDM { get; set; }
        [DataMember]
        public string DefaultValue { get; set; }
        [DataMember]
        public string InputRule { get; set; }
        [DataMember]
        public string DataChangedEvent { get; set; }
        [DataMember]
        public string CanEditRule { get; set; }
        [DataMember]
        public int Width { get; set; }
        [DataMember]
        public int LineHeight { get; set; }
        [DataMember]
        public int TextAlign { get; set; }
        [DataMember]
        public string EditFormat { get; set; }
        [DataMember]
        public bool Required { get; set; }
        [DataMember]
        public string ToolTipText { get; set; }
        [DataMember]
        public string DisplayFormat { get; set; }
        [DataMember]
        public bool DefaultShow { get; set; }

        public MD_InputModel_Column() { }
        public MD_InputModel_Column(string _id, string _columnName, string _displayName, string _columnType, int _order, string _inputModelID,
                                                                bool _canSave, bool _canDisplay, bool _isCompute, bool _readOnly, string _dwdm, string _defalutValue,
                                                                string _inputRule, string _editRule, int _width, int _height, int _align, string _editFormat, string _displayFormat,
                                                                bool _required, string _toolTipText, int _maxLength, string _dataChangedEvent)
        {
            Required = _required;
            MaxInputLength = _maxLength;
            ToolTipText = _toolTipText;
            TextAlign = _align;
            EditFormat = _editFormat;
            DisplayFormat = _displayFormat;
            ColumnID = _id;
            ColumnName = _columnName;
            DisplayName = _displayName;
            ColumnType = _columnType;
            DisplayOrder = _order;
            InputModelID = _inputModelID;
            CanSave = _canSave;
            CanDisplay = _canDisplay;
            IsCompute = _isCompute;
            ReadOnly = _readOnly;
            DWDM = _dwdm;
            DefaultValue = _defalutValue;
            InputRule = _inputRule;
            CanEditRule = _editRule;
            Width = _width;
            LineHeight = _height;
            DataChangedEvent = _dataChangedEvent;
        }
    }
}
