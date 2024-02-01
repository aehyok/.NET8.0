using sun.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Excel.Import
{
    /// <summary>
    /// Excel数据行
    /// </summary>
    public class ExcelDataRow
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// 列数据
        /// </summary>
        public List<ExcelDataCell> Cells { get; set; }

        /// <summary>
        /// 验证错误消息
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// 根据列索引获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public T GetValue<T>(int columnIndex)
        {
            var cellValue = Cells.FirstOrDefault(a => a.Column == columnIndex);
            if (cellValue is null)
            {
                return default;
            }

            if (!ConvertHelper.GetCustomTypeConverter(typeof(T)).CanConvertFrom(typeof(string)))
            {
                return default;
            }

            if (!ConvertHelper.GetCustomTypeConverter(typeof(T)).IsValid(cellValue.Value))
            {
                return default;
            }

            var value = ConvertHelper.GetCustomTypeConverter(typeof(T)).ConvertFromString(cellValue.Value);
            return (T)value;
        }

        public T GetValue<T>(string name)
        {
            var cellValue = Cells.FirstOrDefault(a => a.Name == name);
            if (cellValue is null)
            {
                return default;
            }

            if (!ConvertHelper.GetCustomTypeConverter(typeof(T)).CanConvertFrom(typeof(string)))
            {
                return default;
            }

            if (!ConvertHelper.GetCustomTypeConverter(typeof(T)).IsValid(cellValue.Value))
            {
                return default;
            }

            var value = ConvertHelper.GetCustomTypeConverter(typeof(T)).ConvertFromString(cellValue.Value);
            return (T)value;
        }

        public T To<T>() where T : class, new()
        {
            var instance = new T();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanWrite)
                {
                    continue;
                }

                var cellValue = Cells.FirstOrDefault(a => a.Name.ToLower() == prop.Name.ToLower());
                if (cellValue is null)
                {
                    continue;
                }

                if (!ConvertHelper.GetCustomTypeConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                {
                    continue;
                }

                if (!ConvertHelper.GetCustomTypeConverter(prop.PropertyType).IsValid(cellValue.Value))
                {
                    continue;
                }

                var value = ConvertHelper.GetCustomTypeConverter(prop.PropertyType).ConvertFromString(cellValue.Value);
                prop.SetValue(instance, value, null);
            }

            return instance;
        }
    }
}
