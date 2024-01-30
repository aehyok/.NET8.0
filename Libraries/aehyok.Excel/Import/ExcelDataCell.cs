using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Excel.Import
{
    /// <summary>
    /// Exlcel数据单元格
    /// </summary>
    public class ExcelDataCell : ExcelColumnProperty
    {
        public ExcelDataCell(ExcelColumnProperty property)
        {
            this.Column = property.Column;
            this.DictionaryGroupCode = property.DictionaryGroupCode;
            this.Title = property.Title;
            this.Name = property.Name;
        }

        /// <summary>
        /// 单元格的值
        /// </summary>
        /// </summary>
        public string Value { get; set; }
    }
}
