using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using aehyok.Infrastructure.Options;
using aehyok.Infrastructure.Utils;

namespace aehyok.Excel.Import
{
    public class ExcelReader : IDisposable
    {
        public ExcelReader()
        {
        }

        public ExcelReader(string filePath)
        {
            File = new FileInfo(filePath);
            if (!File.Exists)
            {
                throw new FileNotFoundException(filePath);
            }
        }

        public FileInfo File { get; private set; }

        public List<ExcelColumnProperty> Columns { get; private set; }

        public void LoadMapConfiguration(string filePath)
        {
            using var stream = System.IO.File.OpenRead(filePath);
            var properties = JsonSerializer.Deserialize<List<ExcelColumnProperty>>(stream, JsonOptions.Default);

            this.LoadMapConfiguration(properties);
        }

        public void LoadMapConfiguration(List<ExcelColumnProperty> properties)
        {
            this.Columns = properties;
        }

        public void LoadAsync(string filePath)
        {
            File = new FileInfo(filePath);
        }

        private ExcelPackage Excel { get; set; }

        private ExcelWorksheet Sheet { get; set; }

        private int DataStartRowIndex { get; set; }

        private int DataHeaderRowIndex { get; set; }

        /// <summary>
        /// 读取表中所有行
        /// </summary>
        /// <param name="startRow">数据开始行号</param>
        /// <param name="headerRow">表头行号</param>
        /// <param name="sheetIndex"></param>
        /// <param name="rowValidator">行验证回调</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ExcelDataRow> ReadAllRows(int startRow = 1, int headerRow = 1, int sheetIndex = 0, Action<ExcelDataRow> rowValidator = null)
        {
            if (File is null || !File.Exists)
            {
                throw new Exception("请使用 LoadAsync 方法加载 Excel 文件");
            }

            if (Columns is null || !Columns.Any())
            {
                throw new Exception("请使用 LoadMapConfiguration 方法加载列映射关系");
            }

            this.DataStartRowIndex = startRow;
            this.DataHeaderRowIndex = headerRow;

            Excel = new ExcelPackage(this.File.FullName);

            Sheet = Excel.Workbook.Worksheets[sheetIndex];

            var dimension = Sheet.Dimension;

            var header = Sheet.Cells[headerRow, dimension.Start.Column, headerRow, dimension.End.Column];

            // 匹配表头信息
            foreach (var column in this.Columns)
            {
                var title = header.Where(a => (a.GetCellValue<string>()?.Replace("*", "").Replace(" ", "").Trim() ?? "") == column.Title).FirstOrDefault();
                if (title is not null)
                {
                    column.Column = title.Start.Column;
                }
            }

            var rows = new List<ExcelDataRow>();

            // 读取行数据
            for (var i = startRow; i <= dimension.End.Row; i++)
            {
                var row = new ExcelDataRow()
                {
                    Row = i
                };

                var rowColumns = new List<ExcelDataCell>();

                var startColumn = this.Columns.Where(a => a.Column > 0).OrderBy(a => a.Column).FirstOrDefault()?.Column ?? 1;
                var endColumn = this.Columns.OrderByDescending(a => a.Column).FirstOrDefault()?.Column ?? 1;

                for (var c = startColumn; c <= endColumn; c++)
                {
                    var cell = Sheet.Cells[i, c];

                    var columnProperty = this.Columns.FirstOrDefault(a => a.Column == cell.Start.Column);
                    if (columnProperty is null)
                    {
                        continue;
                    }

                    var cellValue = cell.GetCellValue<string>() ?? "";

                    if (columnProperty.Required && cellValue.IsNullOrEmpty())
                    {
                        row.Errors.Add($"{columnProperty.Title}列数据不能为空");
                    }

                    var columnData = new ExcelDataCell(columnProperty)
                    {
                        Value = cellValue.Trim()
                    };

                    rowColumns.Add(columnData);
                }

                if (rowColumns.Any(c => !c.Value.IsNullOrEmpty()))
                {
                    row.Cells = rowColumns;

                    rowValidator?.Invoke(row);
                    rows.Add(row);
                }
            }

            return rows;
        }

        /// <summary>
        /// 将错误信息写入 Excel 表中
        /// </summary>
        public void WriteErrors(List<ExcelDataRow> rows)
        {
            try
            {
                // 取最后一列的下一列作为错误消息信息列
                var errorColumn = this.Columns.OrderByDescending(a => a.Column).FirstOrDefault()?.Column + 1 ?? 1;

                // 将所有错误行数据写入 Excel 中
                foreach (var errorRow in rows.Where(a => a.Errors.Any()))
                {
                    // 写入错误信息
                    this.Sheet.Cells[errorRow.Row, errorColumn].Value = string.Join("\r\n", errorRow.Errors);
                }

                // 删除成功的行
                var successRows = rows.Where(a => !a.Errors.Any()).OrderByDescending(a => a.Row).ToList();
                foreach (var successRow in successRows)
                {
                    this.Sheet.DeleteRow(successRow.Row);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 保存 Excel 到 Stream
        /// </summary>
        /// <param name="stream"></param>
        public void Save(Stream stream)
        {
            this.Excel.SaveAs(stream);
        }

        public void Dispose()
        {
            if (Excel is not null)
            {
                Excel.Dispose();
            }
        }
    }
}
