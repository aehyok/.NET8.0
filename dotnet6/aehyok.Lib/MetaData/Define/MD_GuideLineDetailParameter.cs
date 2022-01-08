using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace aehyok.Lib.MetaData.Define
{
    /// <summary>
    /// 指标详情参数定义
    /// 
    /// Create by: Lintx
    /// </summary>
    public class MD_GuideLineDetailParameter
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string Name { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [DataMember]
        public string DataValue { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public string Type { get; set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        public MD_GuideLineDetailParameter() { }
        public MD_GuideLineDetailParameter(string _name, string _title, string _type, string _value)
        {
            Name = _name;
            Title = _title;
            Type = _type;
            DataValue = _value;
        }
    }
}
