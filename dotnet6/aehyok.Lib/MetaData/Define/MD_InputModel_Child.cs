using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 录入模型子表定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_InputModel_Child
    {
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string InputModelName { get; set; }
        [DataMember]
        public string ChildModelName { get; set; }
        [DataMember]
        public MD_InputModel ChildModel { get; set; }
        [DataMember]
        public int DisplayOrder { get; set; }
        [DataMember]
        public List<MD_InputModel_ChildParam> Parameters { get; set; }
        [DataMember]
        public string ShowCondition { get; set; }
        [DataMember]
        public int SelectMode { get; set; }

        //added by lqm 2014.3.14
        [DataMember]
        public string DisplayName { get; set; }

        #region lqm合并添加
        public override string ToString()
        {
            return ChildModelName;
        }
        public MD_InputModel_Child() { }
        public MD_InputModel_Child(string _id, string _modelName, string _childName, int _order)
        {
            ID = _id;
            InputModelName = _modelName;
            ChildModelName = _childName;
            DisplayOrder = _order;
        }
        #endregion

       
        /// <summary>
        /// 集成应用
        /// </summary>
        [DataMember]
        public string IntegratedApp { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        [DataMember]
        public string ResType { get; set; }
       
    }
}
