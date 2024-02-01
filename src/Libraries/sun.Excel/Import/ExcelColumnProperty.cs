using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Excel.Import
{
    /// <summary>
    /// 导入属性定义
    /// </summary>
    public class ExcelColumnProperty
    {
        /// <summary>
        /// 属性名称，主要用于匹配需要映射的对象属性
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表头标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 字典分组代码
        /// </summary>
        public string DictionaryGroupCode { get; set; }

        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 字段在表格的列序号
        /// </summary>
        public int Column { get; set; }
    }
}
