using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型元数据定义
    /// 
    /// Create by:Lintx
    /// </summary>
    public class MD_InputModel
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string NameSpace { get; set; }
        [DataMember]
        public string ModelName { get; set; }
        [DataMember]
        public string ModelType { get; set; }
        [DataMember]
        public string Descript { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public string Param { get; set; }
        [DataMember]
        public string DeleteRule { get; set; }
        [DataMember]
        public string DWDM { get; set; }
        [DataMember]
        public string InitGuideLine { get; set; }
        [DataMember]
        public string GetDataGuideLine { get; set; }     //下面文件无此字段
        [DataMember]
        public string GetNewRecordGuideLine { get; set; }
        [DataMember]
        public string ParamterType { get; set; }
        [DataMember]
        public bool IsMixModel { get; set; }
        [DataMember]
        public string ResourceType { get; set; }
        [DataMember]
        public string IntegretedApplication { get; set; }
        [DataMember]
        public string BeforeWrite { get; set; }
        [DataMember]
        public string AfterWrite { get; set; }

        #region lqm合并添加


        public string ModelFullName
        {
            get
            {
                return string.Format("{0}.{1}", NameSpace, ModelName);
            }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public MD_InputModel() { }
        public MD_InputModel(string _id, string _ns, string _name, string _descript, string _displayName, int _order, string _param, string _delRule, string _dwdm, string _inputegretedApplication, string _resourecType)
        {
            ID = _id;
            NameSpace = _ns;
            ModelName = _name;
            Descript = _descript;
            DisplayName = _displayName;
            DisplayOrder = _order;
            Param = _param;
            DeleteRule = _delRule;
            DWDM = _dwdm;
            IntegretedApplication = _inputegretedApplication;
            ResourceType = _resourecType;

        }

        #endregion

        [DataMember]
        public List<MD_InputModel_Column> Columns { get; set; }

        [DataMember]
        public List<MD_InputModel_ColumnGroup> Groups { get; set; }

        [DataMember]
        public List<MD_InputModel_Child> ChildInputModel { get; set; }

        [DataMember]
        public List<MD_InputModel_SaveTable> WriteTableNames { get; set; }
        [DataMember]
        public string OrderField { get; set; }
        [DataMember]
        public string TableName { get; set; }
    }
}
