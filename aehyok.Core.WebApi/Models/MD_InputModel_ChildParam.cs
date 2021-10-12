using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.Core.WebApi.Models
{
    public class MD_InputModel_ChildParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数数据类型
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 参数值
        public string Value { get; set; }

        public MD_InputModel_ChildParam() { }
        public MD_InputModel_ChildParam(string name, string type, string value)
        {
            Name = name;
            DataType = type;
            Value = value;
        }
    }
}
