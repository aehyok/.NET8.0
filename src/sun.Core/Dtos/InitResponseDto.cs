using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
{
    public class InitResponseDto
    {
        /// <summary>
        /// init初始化form表单数据
        /// </summary>
        public ExpandoObject FormData { get; set; } = new ExpandoObject();

        /// <summary>
        /// form表单定义
        /// </summary>
        public string InputDefine { get; set; }

        /// <summary>
        /// form表单子模型定义（暂时不用）
        /// </summary>
        public string ChildrenInputDefine { get; set; }
    }
}
