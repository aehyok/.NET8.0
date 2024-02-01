using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sun.Excel.Export
{
    public class ExcelTemplateRender : IDisposable
    {
        private const string REGEX_ITERATOR = @"^for(each)?\((.+)\s+in\s+(.+)\)";
        private const string REGEX_INSERT_PICTURE = @"^#(?i)InsertPicture\((.+)\)";

        public ExcelTemplateRender(string templatePath)
        {
            this.TemplatePath = templatePath;
            this.ReadTemplate();
        }

        private int OutputRowIndex = 0;
        private int ParseRowIndex = 1;
        private ExcelPackage ExcelPackage;
        private ExcelWorksheet TemplateSheet;
        private Dictionary<string, object> FillModel = new Dictionary<string, object>();
        private List<Statement> StatementList = new List<Statement>();

        public string TemplatePath { get; private set; }

        public int TemplateRows { get; private set; }

        public int TemplateColumns { get; private set; }

        private void ReadTemplate()
        {
            if (string.IsNullOrWhiteSpace(TemplatePath))
            {
                throw new ArgumentNullException(nameof(TemplatePath));
            }

            if (!File.Exists(TemplatePath))
            {
                throw new Exception("模板文件不存在");
            }

            this.ExcelPackage = new ExcelPackage(TemplatePath);
            this.TemplateSheet = ExcelPackage.Workbook.Worksheets[0];

            this.TemplateColumns = this.TemplateSheet.Dimension.Columns;
            this.TemplateRows = this.TemplateSheet.Dimension.Rows;

            this.OutputRowIndex = this.TemplateRows;

            this.ParseStatement();
        }

        public ExcelTemplateRender Fill(object data)
        {
            this.FillModel.Add("model", data);
            return this;
        }

        public ExcelTemplateRender Fill(string name, object data)
        {
            if (this.FillModel.ContainsKey(name))
            {
                throw new Exception($"已包含名为 {name} 的数据");
            }

            this.FillModel.Add(name, data);

            return this;
        }

        public void SaveAs(string path)
        {
            this.Render();

            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            this.ExcelPackage.SaveAs(path);
        }

        public void SaveAs(Stream stream)
        {
            this.Render();
            this.ExcelPackage.SaveAs(stream);
        }

        private void Render()
        {
            do
            {
                this.ParseRow(this.ParseRowIndex);
                this.ParseRowIndex++;
            } while (this.ParseRowIndex <= this.TemplateRows);

            this.TemplateSheet.DeleteColumn(1);
            this.TemplateSheet.DeleteRow(1, this.TemplateRows);
        }

        private void ParseStatement()
        {
            var order = 0;
            var parseList = new List<Statement>();

            for (var rowIndex = 1; rowIndex <= this.TemplateRows; rowIndex++)
            {
                var expressionCell = this.TemplateSheet.Cells[rowIndex, 1];

                if (string.IsNullOrWhiteSpace(expressionCell.Text) || expressionCell.Text.StartsWith("#"))
                {
                    continue;
                }

                Statement statement = parseList.LastOrDefault();

                if (expressionCell.Text.StartsWith("for"))
                {
                    statement = new IteratorStatement();
                }
                else if (expressionCell.Text.StartsWith("if"))
                {
                    statement = new ConditionalStatement();
                }
                else if (expressionCell.Text.StartsWith("else if"))
                {
                    statement = new ConditionalStatement();
                }
                else if (expressionCell.Text.StartsWith("else"))
                {
                    statement = new ConditionalStatement();
                }

                if (statement == null)
                {
                    throw new Exception("代码块语法错误");
                }

                if (!parseList.Contains(statement))
                {
                    statement.Order = order;
                    parseList.Add(statement);
                    order++;
                }

                if (Regex.IsMatch(expressionCell.Text, @"^.*{"))
                {
                    statement.Start = rowIndex;
                }

                if (Regex.IsMatch(expressionCell.Text, @"^.*}"))
                {
                    statement.End = rowIndex;
                    this.StatementList.Add(statement);
                    parseList.Remove(statement);
                }
            }
        }

        private void ParseRow(int rowIndex, bool parseExpression = true)
        {
            var statement = this.StatementList.Where(a => a.Start <= rowIndex && a.End >= rowIndex).OrderByDescending(a => a.Order).FirstOrDefault();

            var expressionCell = this.TemplateSheet.Cells[rowIndex, 1];

            if (string.IsNullOrWhiteSpace(expressionCell.Text) || expressionCell.Text.Trim() == "}" || !parseExpression || expressionCell.Text.StartsWith("#"))
            {
                RenderRow(rowIndex);
            }

            if (!parseExpression)
            {
                return;
            }

            foreach (Match match in Regex.Matches(expressionCell.Text, REGEX_ITERATOR))
            {
                // 循环块代码 参数名
                var parameterName = match.Groups[2].Value;

                // 要迭代的数据集名称
                var iteratorDataName = match.Groups[3].Value;

                if (string.IsNullOrWhiteSpace(parameterName))
                {
                    throw new Exception("foreach 表达式错误");
                }

                // 获取要迭代的数据集
                var iteratorData = ExcelExtensions.GetValue(this.FillModel, iteratorDataName);
                if (!(iteratorData is IEnumerable iteratorEnumerable))
                {
                    throw new Exception($"{iteratorDataName} 不是有效的集合");
                }

                var iteratorStartRowIndex = rowIndex;

                var index = 0;
                foreach (var item in iteratorEnumerable)
                {
                    this.FillModel.Add(parameterName, item);
                    this.FillModel.Add("Index", index + 1);
                    var iteratorRowIndex = statement.Start;

                    do
                    {
                        this.ParseRow(iteratorRowIndex, iteratorRowIndex != statement.Start && iteratorStartRowIndex != statement.End);
                        iteratorRowIndex++;
                    } while (iteratorRowIndex <= (statement.Start == statement.End ? statement.End : statement.End - 1));

                    this.FillModel.Remove(parameterName);
                    this.FillModel.Remove("Index");
                    this.ParseRowIndex = statement.End;
                    index++;
                }
            }

            foreach (Match match in Regex.Matches(expressionCell.Text, REGEX_INSERT_PICTURE))
            {
                // name
                var paramArr = match.Groups[1].Value.Split(",", StringSplitOptions.RemoveEmptyEntries);

                if (paramArr.Length != 7)
                {
                    throw new Exception("InsertPicture 方法参数长度错误");
                }

                var pictureName = paramArr[0];
                var pictureBase64 = GetFunctionParamValue<string>(paramArr[1]);
                if (string.IsNullOrWhiteSpace(pictureBase64))
                {
                    continue;
                }

                var column = GetFunctionParamValue<int>(paramArr[2]);
                var width = GetFunctionParamValue<int>(paramArr[3]);
                var height = GetFunctionParamValue<int>(paramArr[4]);
                var rowOffset = GetFunctionParamValue<int>(paramArr[5]);
                var columnOffset = GetFunctionParamValue<int>(paramArr[6]);

                ReportHelper.InsertPicture(this.TemplateSheet, pictureName, pictureBase64, this.OutputRowIndex - 1, column, width, height, rowOffset, columnOffset);
            }
        }

        private void RenderRow(int rowIndex)
        {
            this.OutputRowIndex++;

            this.TemplateSheet.InsertRow(this.OutputRowIndex, 1, rowIndex);
            this.TemplateSheet.Row(this.OutputRowIndex).Height = this.TemplateSheet.Row(rowIndex).Height;

            for (var columnIndex = 2; columnIndex <= this.TemplateColumns; columnIndex++)
            {
                var mergeAddress = this.TemplateSheet.MergedCells[rowIndex, columnIndex];

                if (!string.IsNullOrEmpty(mergeAddress))
                {
                    var excelAddress = new ExcelAddress(mergeAddress);
                    var mergeCells = this.TemplateSheet.Cells[excelAddress.Start.Row + (this.OutputRowIndex - rowIndex), excelAddress.Start.Column, excelAddress.End.Row + (this.OutputRowIndex - rowIndex), excelAddress.End.Column];

                    if (mergeCells.Merge)
                    {
                        this.TemplateSheet.Cells[this.TemplateSheet.MergedCells[mergeCells.Start.Row, mergeCells.Start.Column]].Merge = false;
                    }

                    mergeCells.Merge = true;
                }

                var cell = this.TemplateSheet.Cells[rowIndex, columnIndex];
                if (!string.IsNullOrEmpty(cell.FormulaR1C1))
                {
                    this.TemplateSheet.Cells[this.OutputRowIndex, columnIndex].FormulaR1C1 = cell.FormulaR1C1;
                }
                else
                {
                    var outputCell = this.TemplateSheet.Cells[this.OutputRowIndex, columnIndex];
                    outputCell.Value = cell.Value;

                    this.RenderCell(outputCell, cell);
                }
            }
        }

        private void RenderCell(ExcelRange outputCell, ExcelRange cell)
        {
            if (cell.IsRichText)
            {
                outputCell.IsRichText = true;
                foreach (var text in outputCell.RichText)
                {
                    text.Text = ExcelExtensions.ReplaceParameter(this.FillModel, text.Text);
                }
            }
            else
            {
                outputCell.Value = ExcelExtensions.ReplaceParameter(this.FillModel, outputCell.Text);
            }
        }

        private T GetFunctionParamValue<T>(string propertyPath)
        {
            if (propertyPath.Contains('.'))
            {
                return ExcelExtensions.GetValue<T>(this.FillModel, propertyPath);
            }

            return (T)Convert.ChangeType(propertyPath, typeof(T));
        }

        public void Dispose()
        {
            if (this.TemplateSheet is not null)
            {
                this.TemplateSheet.Dispose();
            }

            if (this.ExcelPackage is not null)
            {
                this.ExcelPackage.Dispose();
            }

            this.FillModel = null;
            this.StatementList = null;
        }
    }
}
